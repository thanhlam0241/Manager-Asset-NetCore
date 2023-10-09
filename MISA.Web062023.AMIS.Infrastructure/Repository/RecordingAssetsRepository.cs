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
    /// Created by: NTLam (19/8/2023)
    public class RecordingAssetsRepository : IRecordingAssetsRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRecordingRepository _recordingRepository;
        private readonly IResourceAssetRepository _resourceAssetRepository;
        private readonly IRecordedAssetRepository _recordedAssetRepository;

        /// <summary>
        /// The constructor
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// <param name="recordingRepository">The recording repository.</param>
        /// Created by: NTLam (19/8/2023)
        public RecordingAssetsRepository(IUnitOfWork unitOfWork, IRecordingRepository recordingRepository, IResourceAssetRepository resourceAssetRepository, IRecordedAssetRepository recordedAssetRepository)
        {
            _unitOfWork = unitOfWork;
            _recordingRepository = recordingRepository;
            _resourceAssetRepository = resourceAssetRepository;
            _recordedAssetRepository = recordedAssetRepository;
        }

        /// <summary>
        /// The delete recording assets async.
        /// </summary>
        /// <param name="recordingId">The recording id.</param>
        /// <param name="assetIds">The asset ids.</param>
        /// <returns>The result.</returns>
        /// Created by: NTLam (19/8/2023)
        public Task<int> DeleteRecordingAssetsAsync(Guid recordingId, List<Guid> assetIds)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The get recording assets.
        /// </summary>
        /// <param name="recordingId">The recording id.</param>
        /// <returns>The result.</returns>
        /// Created by: NTLam (19/8/2023)
        public async Task<List<RecordedAsset>> GetRecordingAssetsAsync(Guid recordingId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("RecordingId", recordingId);
            var sqlQueryAssets = """
                SELECT 
                recorded_asset.recorded_asset_id as recorded_asset_id, recorded_asset_code, recorded_asset_name,department_name, recorded_asset.value, depreciation_rate, 
                resource_asset.resource_asset_id, resource_asset.cost,
                resource_budget.resource_budget_id, resource_budget_code, resource_budget_name,
                recording.recording_id, recording_code, recording_date, action_date, recording.value, description, recording_type
                FROM recorded_asset
                JOIN resource_asset ON recorded_asset.recorded_asset_id = resource_asset.recorded_asset_id
                JOIN resource_budget ON resource_asset.resource_budget_id = resource_budget.resource_budget_id
                JOIN recording ON recorded_asset.recording_id = recording.recording_id
                WHERE recorded_asset.recording_id = @RecordingId
                """;

            DefaultTypeMap.MatchNamesWithUnderscores = true;
            var listAssets1 = await _unitOfWork.Connection.QueryAsync(sqlQueryAssets, parameters);
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            var listAssets = await _unitOfWork.Connection.QueryAsync<RecordedAsset, ResourceAsset, ResourceBudget, Recording, RecordedAsset>(sqlQueryAssets, (asset, resourceAsset, resourceBudget, recording) =>
            {
                resourceAsset.ResourceBudget = resourceBudget;
                asset.ResourceAssets.Add(resourceAsset);
                asset.Recording = recording;
                return asset;
            }
                , parameters, splitOn: "resource_asset_id, resource_budget_id, recording_id");

            var result = listAssets.GroupBy(a => a.RecordedAssetId).Select(
                    a =>
                    {
                        var asset = a.First();
                        asset.ResourceAssets = a.Select(ra => ra.ResourceAssets.First()).ToList();
                        return asset;
                    }
                );
            return result.ToList();

        }

        /// <summary>
        /// The insert recording assets.
        /// </summary>
        /// <param name="recordingAssets">The recording assets.</param>
        /// <returns>The result.</returns>
        /// Created by: NTLam (19/8/2023)
        public async Task<int> InsertRecordingAssetsAsync(RecordingAsset recordingAssets)
        {
            var recording = recordingAssets.Recording;
            var assets = recordingAssets.Assets;

            List<dynamic> paramInsertRecordedAssets = new();
            foreach (var asset in assets)
            {
                paramInsertRecordedAssets.Add(new
                {
                    asset.RecordedAssetId,
                    asset.RecordedAssetCode,
                    asset.RecordedAssetName,
                    asset.DepartmentName,
                    asset.Value,
                    recording.RecordingId,
                    asset.DepreciationRate
                });
            }

            var sqlInsertRecording = """
                INSERT INTO recording (recording_id,recording_code,recording_date,action_date,value,description,recording_type) 
                    VALUES (@RecordingId, @RecordingCode, @RecordingDate, @ActionDate,@Value, @Description,1)
                """;

            var parametersInsertRecording = new DynamicParameters();
            parametersInsertRecording.Add("RecordingId", recording.RecordingId);
            parametersInsertRecording.Add("RecordingCode", recording.RecordingCode);
            parametersInsertRecording.Add("RecordingDate", recording.RecordingDate);
            parametersInsertRecording.Add("ActionDate", recording.ActionDate);
            parametersInsertRecording.Add("Description", recording.Description);
            parametersInsertRecording.Add("Value", recording.Value);


            var sqlInsertRecordedAsset = $"""
                INSERT INTO recorded_asset (recorded_asset_id, recorded_asset_code, recorded_asset_name, recording_id, department_name, value, depreciation_rate) 
                    VALUES (@RecordedAssetId, @RecordedAssetCode, @RecordedAssetName, @RecordingId ,@DepartmentName, @Value, @DepreciationRate)
                """;

            var sqlInsertResourceAsset = """
                                         INSERT INTO resource_asset (resource_budget_id, recorded_asset_id, cost) 
                                         VALUES (@ResourceBudget, @AssetId, @Cost)
                                         """;

            var resultInsertRecording = await _unitOfWork.Connection.ExecuteAsync(sqlInsertRecording, parametersInsertRecording, _unitOfWork.Transaction);
            if (resultInsertRecording == 1)
            {
                try
                {
                    var resultInsertRecordedAsset = await _unitOfWork.Connection.ExecuteAsync(sqlInsertRecordedAsset, paramInsertRecordedAssets, _unitOfWork.Transaction);
                    if (resultInsertRecordedAsset == assets.Count)
                    {
                        List<dynamic> resourceAssets = new List<dynamic>();
                        foreach (RecordedAsset asset in assets)
                        {
                            foreach (ResourceAsset ra in asset.ResourceAssets)
                            {
                                if (ra.ResourceBudget == null)
                                {
                                    throw new Exception("Mỗi tài sản phải có nguồn hình thành.");
                                }
                                resourceAssets.Add(new
                                {
                                    ResourceBudget = ra.ResourceBudget.ResourceBudgetId,
                                    AssetId = asset.RecordedAssetId,
                                    ra.Cost
                                });
                            }
                        }
                        var resultInsertResourceAsset = await _unitOfWork.Connection.ExecuteAsync(sqlInsertResourceAsset, resourceAssets, _unitOfWork.Transaction);
                        if (resultInsertResourceAsset == resourceAssets.Count)
                        {
                            return 1;
                        }
                        else
                        {
                            return 0;
                        }
                    }
                    else
                    {
                        return 0;
                    }
                }
                catch (Exception e)
                {
                    var res = await _recordingRepository.DeleteAsync(recording.RecordingId);
                    if (res > 0) return 0;
                }
            }
            return 0;

        }

        /// <summary>
        /// The update recording assets.
        /// </summary>
        /// <param name="recordingAssets">The recording assets.</param>
        /// <returns>The result.</returns>
        /// Created by: NTLam (19/8/2023)
        public Task<int> UpdateRecordingAssets(RecordingAsset recordingAssets)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The update recording assets async.
        /// </summary>
        /// <param name="recordingId">The recording id.</param>
        /// <param name="assets">The assets.</param>
        /// <returns>The result.</returns>
        /// Created by: NTLam (19/8/2023)
        public Task<bool> UpdateRecordingAssetsAsync(Guid recordingId, List<RecordedAsset> assets)
        {
            throw new NotImplementedException();
        }
    }
}
