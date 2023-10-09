using Dapper;
using MISA.Web062023.AMIS.Application;
using MISA.Web062023.AMIS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web062023.AMIS.Infrastructure
{
    public class RecordedAssetRepository : BaseCrudRepository<RecordedAsset>, IRecordedAssetRepository
    {
        /// <summary>
        /// Constructor của DepartmentRepository
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// Created by: NTLam (10/8/2023)
        public RecordedAssetRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        /// <summary>
        /// The delete recorded asset.
        /// </summary>
        /// <param name="assetId">The asset id.</param>
        /// <returns>The result.</returns>
        /// Created by: NTLam (19/8/2023)
        public Task<int> DeleteRecordedAsset(Guid assetId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The check is exist code.
        /// </summary>
        /// <param name="codes">The codes.</param>
        /// <param name="recordingId">The recording id.</param>
        /// <returns>The result.</returns>
        /// Created by: NTLam (19/8/2023)
        public Task<bool> CheckIsExistCode(List<string> codes, Guid recordingId)
        {
            var sql = """
                SELECT Count(recorded_asset_code) FROM recorded_asset WHERE recorded_asset_code IN @Codes AND recording_id = @RecordingId
                """;
            var param = new { Codes = codes, RecordingId = recordingId };
            var result = _unitOfWork.Connection.QueryFirstOrDefault<int>(sql, param, transaction: _unitOfWork.Transaction);
            return Task.FromResult(result > 0);
        }

        /// <summary>
        /// The get asset async.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The result.</returns>
        /// Created by: NTLam (19/8/2023)
        public async Task<RecordedAsset> GetAssetAsync(Guid id)
        {
            var sql = """
                SELECT recorded_asset_id, recorded_asset_code, recorded_asset_name, department_name, value, depreciation_rate
                FROM recorded_asset
                WHERE recorded_asset_id = @RecordedAssetId
                """;
            var sqlSelectResource = """
                SELECT 
                resource_asset_id, cost, 
                resource_budget.resource_budget_id, resource_budget_code, resource_budget_name
                FROM resource_asset
                JOIN resource_budget ON resource_asset.resource_budget_id = resource_budget.resource_budget_id
                WHERE resource_asset.recorded_asset_id = @RecordedAssetId
                """;
            var param = new { RecordedAssetId = id };
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            var result = await _unitOfWork.Connection.QueryFirstOrDefaultAsync<RecordedAsset>(sql, param)
                ?? throw new NotFoundException("Không tìm thấy tài sản chứng từ");
            var resultResourceAssets = await _unitOfWork.Connection.QueryAsync<ResourceAsset, ResourceBudget, ResourceAsset>(sqlSelectResource, (resourceAsset, resourceBudget) =>
            {
                resourceAsset.ResourceBudget = resourceBudget;
                return resourceAsset;
            }, param, splitOn: "resource_budget_id");

            result.ResourceAssets = resultResourceAssets.ToList();

            return result;
        }

        /// <summary>
        /// The insert multiple async.
        /// </summary>
        /// <param name="recordedAssets">The recorded assets.</param>
        /// <returns>The result.</returns>
        /// Created by: NTLam (19/8/2023)
        public async Task<int> InsertMultipleAsync(Guid id, List<RecordedAsset> recordedAssets)
        {
            var sqlInsertRecordedAsset = """
                INSERT INTO recorded_asset (recorded_asset_id, recorded_asset_code, recorded_asset_name, recording_id, department_name, cost, depreciation_rate, recording_type) 
                    VALUES (@RecordedAssetId, @RecordedAssetCode, @RecordedAssetName, @RecordingId, @DepartmentName, @Cost, @DepreciationRate, 1)
                """;
            var sqlInsertResourceAsset = """
                                         INSERT INTO resource_asset (resource_budget_id, recorded_asset_id, cost) 
                                         VALUES (@ResourceBudget, @AssetId, @Cost)
                                         """;
            List<dynamic> resourceAssets = new List<dynamic>();
            foreach (RecordedAsset asset in recordedAssets)
            {
                foreach (ResourceAsset ra in asset.ResourceAssets)
                {
                    if (ra.ResourceBudget == null)
                    {
                        throw new Exception("Mỗi nguồn tài sản phải có nguồn hình thành.");
                    }
                    resourceAssets.Add(new
                    {
                        ResourceBudget = ra.ResourceBudget.ResourceBudgetId,
                        AssetId = asset.RecordedAssetId,
                        ra.Cost
                    });
                }
            }
            List<dynamic> paramInsertRecordedAssets = new();
            foreach (var asset in recordedAssets)
            {
                paramInsertRecordedAssets.Add(new
                {
                    asset.RecordedAssetId,
                    asset.RecordedAssetCode,
                    asset.RecordedAssetName,
                    RecordingId = id,
                    asset.DepartmentName,
                    asset.Value,
                    asset.DepreciationRate
                });
            }
            var connection = _unitOfWork.Connection;
            connection.Open();
            var transaction = connection.BeginTransaction();
            try
            {
                var result = await _unitOfWork.Connection.ExecuteAsync(sqlInsertRecordedAsset, paramInsertRecordedAssets);
                if (result > 0)
                {
                    var resultInsertResourceAsset = await _unitOfWork.Connection.ExecuteAsync(sqlInsertResourceAsset, resourceAssets);
                    if (resultInsertResourceAsset > 0)
                    {
                        transaction.Commit();
                        connection.Close();
                        return 1;
                    }
                }
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                connection.Close();
                throw new Exception(ex.Message);
            }
            return 0;
        }

        /// <summary>
        /// The update recorded asset.
        /// </summary>
        /// <param name="assetId">The asset id.</param>
        /// <param name="resources">The resources.</param>
        /// <returns>The result.</returns>
        /// Created by: NTLam (19/8/2023)
        public async Task<int> UpdateRecordedAsset(Guid assetId, List<ResourceAsset> resources)
        {
            var sql = """
                INSERT INTO resource_asset (resource_budget_id,recorded_asset_id,cost)
                                    VALUES (@ResourceBudgetId,@RecordedAssetId,@Cost)
                """;
            var sqlUpdate = """
                UPDATE resource_asset SET cost = @Cost  WHERE resource_asset_id = @ResourceAssetId
                """;
            List<dynamic> paramInserts = new();
            foreach (var resource in resources)
            {
                if (resource.ResourceAssetId == null)
                    paramInserts.Add(new
                    {
                        resource.ResourceBudget.ResourceBudgetId,
                        RecordedAssetId = assetId,
                        resource.Cost
                    });
            }
            List<dynamic> paramUpdates = new();
            foreach (var resource in resources)
            {
                if (resource.ResourceAssetId != null)
                    paramUpdates.Add(new
                    {
                        resource.ResourceAssetId,
                        resource.Cost
                    });
            }
            var resultUpdate = await _unitOfWork.Connection.ExecuteAsync(sqlUpdate, paramUpdates, transaction: _unitOfWork.Transaction);
            if (resultUpdate > 0)
            {
                var result = await _unitOfWork.Connection.ExecuteAsync(sql, paramInserts, transaction: _unitOfWork.Transaction);
                if (result > 0 && paramInserts.Count > 0)
                {
                    return 1;
                }
            }
            return 1;
        }

        /// <summary>
        /// The delete multiple async.
        /// </summary>
        /// <param name="ids">The ids.</param>
        /// <returns>The result.</returns>
        /// Created by: NTLam (19/8/2023)
        public async Task<int> DeleteMultipleAsync(List<Guid> ids)
        {
            var sql = """
                DELETE FROM recorded_asset WHERE recorded_asset_id IN @Ids
                """;
            var param = new { Ids = ids };
            var result = await _unitOfWork.Connection.ExecuteAsync(sql, param, transaction: _unitOfWork.Transaction);
            return result;
        }

        /// <summary>
        /// The update recorded asset.
        /// </summary>
        /// <param name="recordedAsset">The recorded asset.</param>
        /// <returns>The result.</returns>
        /// Created by: NTLam (19/8/2023)
        public async Task<int> UpdateAsync(RecordedAsset recordedAsset)
        {
            string sql = "";
            var newValue = recordedAsset.ResourceAssets.Sum(i => i.Cost);
            if (newValue != recordedAsset.Value)
            {
                sql = """
                    UPDATE recorded_asset SET value = @Value WHERE recorded_asset_id = @RecordedAssetId
                    """;
            }
            var connection = _unitOfWork.Connection;
            connection.Open();
            var transaction = connection.BeginTransaction();
            var sqlUpdate = """
                INSERT INTO resource_asset (resource_asset_id,resource_budget_id,recorded_asset_id, cost)
                                    VALUES (@ResourceAssetId, @ResourceBudgetId, @RecordedAssetId ,@Cost)
                        ON DUPLICATE KEY UPDATE cost = @Cost, resource_budget_id = @ResourceBudgetId
                """;
            var paramsUpdate = new List<dynamic>();
            foreach (var resource in recordedAsset.ResourceAssets)
            {
                if (resource.ResourceBudget == null)
                {
                    throw new BadRequestException("Mã nguồn vốn không được để trống");
                }
                paramsUpdate.Add(new
                {
                    resource.ResourceAssetId,
                    resource.ResourceBudget.ResourceBudgetId,
                    recordedAsset.RecordedAssetId,
                    resource.Cost
                });
            }
            try
            {
                var resultInsert = await connection.ExecuteAsync(sqlUpdate, paramsUpdate, transaction: transaction);
                if (resultInsert > 0)
                {
                    if (!string.IsNullOrEmpty(sql))
                    {
                        var result = await connection.ExecuteAsync(sql, new { recordedAsset.RecordedAssetId, Value = newValue }, transaction: transaction);
                        if (result > 0)
                        {
                            transaction.Commit();
                            connection.Close();
                            return 1;
                        }
                    }
                    else
                    {
                        transaction.Commit();
                        connection.Close();
                        return 1;
                    }
                }
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                connection.Close();
                throw new Exception(ex.Message);
            }
            return 0;
        }

        /// <summary>
        /// The update multiple async.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="recordedAssets">The recorded assets.</param>
        /// <returns>The result.</returns>
        public Task<int> UpdateMultipleAsync(Guid id, List<RecordedAsset> recordedAssets)
        {
            throw new NotImplementedException();
        }
    }
}
