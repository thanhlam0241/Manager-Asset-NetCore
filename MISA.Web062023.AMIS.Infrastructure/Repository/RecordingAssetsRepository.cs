using Dapper;
using MISA.Web062023.AMIS.Application;
using MISA.Web062023.AMIS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MISA.Web062023.AMIS.Infrastructure
{

    /// <summary>
    /// The recording assets repository.
    /// </summary>
    public class RecordingAssetsRepository : IRecordingAssetsRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRecordingRepository _recordingRepository;
        private readonly IResourceAssetRepository _resourceAssetRepository;
        private readonly IRecordedAssetRepository _recordedAssetRepository;

        /// <summary>
        /// The .ctor.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="recordingRepository">The recording repository.</param>
        public RecordingAssetsRepository(IUnitOfWork unitOfWork, IRecordingRepository recordingRepository, IResourceAssetRepository resourceAssetRepository, IRecordedAssetRepository recordedAssetRepository)
        {
            _unitOfWork = unitOfWork;
            _recordingRepository = recordingRepository;
            _resourceAssetRepository = resourceAssetRepository;
            _recordedAssetRepository = recordedAssetRepository;
        }

        public Task<int> DeleteRecordingAssetsAsync(Guid recordingId, List<Guid> assetIds)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The get recording assets.
        /// </summary>
        /// <param name="recordingId">The recording id.</param>
        /// <returns>The result.</returns>
        public async Task<IEnumerable<RecordedAsset>> GetRecordingAssetsAsync(Guid recordingId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("RecordingId", recordingId);
            var sqlQueryAssets = """
                WITH RecordingAssets AS (
                    SELECT
                        asset
                    FROM
                        recording_asset
                    WHERE
                        recording = @RecordingId
                )
                SELECT 
                recorded_asset.recorded_asset_id as recorded_asset_id, recorded_asset_code, recorded_asset_name, recorded_asset.value, depreciation_rate, recording_type,
                department.department_id, department_code, department_name, description,
                resource_asset.resource_asset_id, resource_asset.cost,
                resource_budget.resource_budget_id, resource_budget_code, resource_budget_name
                FROM recorded_asset 
                JOIN RecordingAssets ON RecordingAssets.asset = recorded_asset.recorded_asset_id
                JOIN department ON recorded_asset.department_id = department.department_id
                JOIN resource_asset ON recorded_asset.recorded_asset_id = resource_asset.recorded_asset_id
                JOIN resource_budget ON resource_asset.resource_budget_id = resource_budget.resource_budget_id
                """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;
            var listAssets1 = await _unitOfWork.Connection.QueryAsync(sqlQueryAssets, parameters);
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            var listAssets = await _unitOfWork.Connection.QueryAsync<RecordedAsset, Department, ResourceAsset, ResourceBudget, RecordedAsset>(sqlQueryAssets, (asset, department, resourceAsset, resourceBudget) =>
            {
                asset.Department = department;
                resourceAsset.ResourceBudget = resourceBudget;
                asset.ResourceAssets.Add(resourceAsset);
                return asset;
            }
                , parameters, splitOn: "department_id, resource_asset_id, resource_budget_id");

            var result = listAssets.GroupBy(a => a.RecordedAssetId).Select(
                a =>
                {
                    var groupResourceAssets = a.First();
                    groupResourceAssets.ResourceAssets = a.Select(ra => ra.ResourceAssets.Single()).ToList();
                    return groupResourceAssets;
                });

            if (!result.Any())
            {
                throw new NotFoundException(string.Format(Domain.Resources.RecordingAsset.RecordingAsset.NoAssetWithRecording, recordingId));
            }

            return result;

        }

        /// <summary>
        /// The insert recording assets.
        /// </summary>
        /// <param name="recordingAssets">The recording assets.</param>
        /// <returns>The result.</returns>
        public async Task<int> InsertRecordingAssetsAsync(RecordingAsset recordingAssets)
        {
            var recording = recordingAssets.Recording;
            var assets = recordingAssets.Assets;

            List<dynamic> lists = new List<dynamic>();

            foreach (RecordedAsset asset in assets)
            {
                lists.Add(new
                {
                    Asset = asset.RecordedAssetId,
                    Recording = recording.RecordingId
                });
            }

            List<dynamic> paramInsertRecordedAssets = new();
            foreach (var asset in assets)
            {
                if (asset.Department == null)
                {
                    throw new Exception("Tài sản phải có bộ phận sử dụng.");
                }
                paramInsertRecordedAssets.Add(new
                {
                    asset.RecordedAssetId,
                    asset.RecordedAssetCode,
                    asset.RecordedAssetName,
                    asset.Department.DepartmentId,
                    asset.Value,
                    asset.DepreciationRate
                });
            }

            var sqlInsertRecording = """
                INSERT INTO recording (recording_id,recording_code,recording_date,action_date,description,recording_type) 
                    VALUES (@RecordingId, @RecordingCode, @RecordingDate, @ActionDate, @Description,1)
                """;

            var parametersInsertRecording = new DynamicParameters();
            parametersInsertRecording.Add("RecordingId", recording.RecordingId);
            parametersInsertRecording.Add("RecordingCode", recording.RecordingCode);
            parametersInsertRecording.Add("RecordingDate", recording.RecordingDate);
            parametersInsertRecording.Add("ActionDate", recording.ActionDate);
            parametersInsertRecording.Add("Description", recording.Description);


            var sqlInsertRecordedAsset = """
                INSERT INTO recorded_asset (recorded_asset_id, recorded_asset_code, recorded_asset_name, department_id, value, depreciation_rate, recording_type) 
                    VALUES (@RecordedAssetId, @RecordedAssetCode, @RecordedAssetName, @DepartmentId, @Value, @DepreciationRate, 1)
                """;

            var sqlInsertResourceAsset = """
                                         INSERT INTO resource_asset (resource_budget_id, asset_id, cost) 
                                         VALUES (@ResourceBudget, @AssetId, @Cost)
                                         """;

            var sqlInsertRecordingAsset = "INSERT INTO recording_asset (asset, recording) VALUES (@Asset, @Recording)";

            using var connectionInsertRecording = _unitOfWork.DbConnection();
            connectionInsertRecording.Open();
            using var transactionInsertRecording = connectionInsertRecording.BeginTransaction();
            var resultInsertRecording = await connectionInsertRecording.ExecuteAsync(sqlInsertRecording, parametersInsertRecording);
            if (resultInsertRecording == 1)
            {
                try
                {
                    using var connectionInsertRecordedAsset = _unitOfWork.DbConnection();
                    connectionInsertRecordedAsset.Open();
                    using var transactionInsertRecordedAsset = connectionInsertRecordedAsset.BeginTransaction();
                    var resultInsertRecordedAsset = await connectionInsertRecordedAsset.ExecuteAsync(sqlInsertRecordedAsset, paramInsertRecordedAssets);
                    if (resultInsertRecordedAsset == assets.Count)
                    {
                        List<dynamic> resourceAssets = new List<dynamic>();
                        foreach (RecordedAsset asset in assets)
                        {
                            foreach (ResourceAsset ra in asset.ResourceAssets)
                            {
                                if (ra.ResourceBudget == null)
                                {
                                    throw new Exception("Lỗi");
                                }
                                resourceAssets.Add(new
                                {
                                    ResourceBudget = ra.ResourceBudget.ResourceBudgetId,
                                    AssetId = asset.RecordedAssetId,
                                    ra.Cost
                                });
                            }
                        }
                        using var connectionInsertResourceAsset = _unitOfWork.DbConnection();
                        connectionInsertResourceAsset.Open();
                        using var transactionInsertResourceAsset = connectionInsertResourceAsset.BeginTransaction();
                        try
                        {
                            var resultInsertResourceAsset = await connectionInsertResourceAsset.ExecuteAsync(sqlInsertResourceAsset, resourceAssets);
                            if (resultInsertResourceAsset == resourceAssets.Count)
                            {
                                var connectionInsertRecordingAsset = _unitOfWork.DbConnection();
                                connectionInsertRecordingAsset.Open();
                                try
                                {
                                    var resultInsertRecordingAsset = await connectionInsertRecordingAsset.ExecuteAsync(sqlInsertRecordingAsset, lists);
                                    if (resultInsertRecordingAsset == lists.Count)
                                    {
                                        transactionInsertResourceAsset.Commit();
                                        transactionInsertRecordedAsset.Commit();
                                        transactionInsertRecording.Commit();
                                        connectionInsertResourceAsset.Close();
                                        connectionInsertRecordedAsset.Close();
                                        connectionInsertRecording.Close();
                                        connectionInsertRecordingAsset.Close();
                                        return 1;
                                    }
                                    else
                                    {
                                        transactionInsertResourceAsset.Rollback();
                                        transactionInsertRecordedAsset.Rollback();
                                        transactionInsertRecording.Rollback();
                                        connectionInsertResourceAsset.Close();
                                        connectionInsertRecordedAsset.Close();
                                        connectionInsertRecording.Close();
                                        connectionInsertRecordingAsset.Close();
                                        return 0;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    transactionInsertResourceAsset.Rollback();
                                    transactionInsertRecordedAsset.Rollback();
                                    transactionInsertRecording.Rollback();
                                    connectionInsertResourceAsset.Close();
                                    connectionInsertRecordedAsset.Close();
                                    connectionInsertRecording.Close();
                                    connectionInsertRecordingAsset.Close();
                                    throw new Exception(ex.Message);
                                }
                            }
                            else
                            {
                                transactionInsertResourceAsset.Rollback();
                                transactionInsertRecordedAsset.Rollback();
                                transactionInsertRecording.Rollback();
                                connectionInsertResourceAsset.Close();
                                connectionInsertRecordedAsset.Close();
                                connectionInsertRecording.Close();
                                return 0;
                            }
                        }
                        catch (Exception ex)
                        {
                            transactionInsertResourceAsset.Rollback();
                            transactionInsertRecording.Rollback();
                            connectionInsertRecordedAsset.Close();
                            connectionInsertRecording.Close();
                            connectionInsertRecordedAsset.Close();
                            throw new Exception(ex.Message);
                        }
                    }
                    else
                    {
                        transactionInsertRecording.Rollback();
                        connectionInsertRecording.Close();
                        connectionInsertRecordedAsset.Close();
                        return 0;
                    }
                }
                catch (Exception ex)
                {
                    transactionInsertRecording.Rollback();
                    connectionInsertRecording.Close();
                    throw new Exception(ex.Message);
                }
            }
            return 0;

        }

        public async Task<int> InsertMatchTable(RecordingAsset recordingAssets)
        {
            var sqlInsertRecordingAsset = "INSERT INTO recording_asset (asset, recording) VALUES (@Asset, @Recording)";
            List<dynamic> lists = new List<dynamic>();
            var assets = recordingAssets.Assets;
            var recording = recordingAssets.Recording;

            foreach (RecordedAsset asset in assets)
            {
                lists.Add(new
                {
                    Asset = asset.RecordedAssetId,
                    Recording = recording.RecordingId
                });
            }

            var resultInsertRecordingAsset = await _unitOfWork.Connection.ExecuteAsync(sqlInsertRecordingAsset, lists, _unitOfWork.Transaction);
            return resultInsertRecordingAsset;
        }

        public async Task<int> InsertRecordingAssetsWithoutTransactionAsync(RecordingAsset recordingAssets)
        {
            var recording = recordingAssets.Recording;
            var assets = recordingAssets.Assets;

            List<dynamic> lists = new List<dynamic>();

            foreach (RecordedAsset asset in assets)
            {
                lists.Add(new
                {
                    Asset = asset.RecordedAssetId,
                    Recording = recording.RecordingId
                });
            }

            var resultInsertRecording = await _recordingRepository.InsertAsync(recording);
            if (resultInsertRecording > 0)
            {
                var resultInsertRecordedAsset = await _recordedAssetRepository.InsertMultipleAsync(assets);
                if (resultInsertRecordedAsset > 0)
                {
                    foreach (RecordedAsset asset in assets)
                    {
                        var res = await _resourceAssetRepository.InsertMultipleAsync(asset.RecordedAssetId, asset.ResourceAssets);
                        if (res == 0)
                        {
                            return 0;
                        }
                    }
                    var result = await InsertMatchTable(recordingAssets);
                    return result > 0 ? 1 : 0;
                }
            }
            return 0;

        }

        /// <summary>
        /// The update recording assets.
        /// </summary>
        /// <param name="recordingAssets">The recording assets.</param>
        /// <returns>The result.</returns>
        public Task<int> UpdateRecordingAssets(RecordingAsset recordingAssets)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The update recording assets.
        /// </summary>
        /// <param name="recordingId">The recording id.</param>
        /// <param name="assetIds">The asset ids.</param>
        /// <returns>The result.</returns>
        public Task<int> UpdateRecordingAssetsAsync(Guid recordingId, List<Guid> assetIds)
        {
            throw new NotImplementedException();
        }
    }
}
