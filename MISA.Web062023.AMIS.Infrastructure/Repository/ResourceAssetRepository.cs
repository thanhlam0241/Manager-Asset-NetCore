using Dapper;
using MISA.Web062023.AMIS.Application;
using MISA.Web062023.AMIS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web062023.AMIS.Infrastructure
{
    public class ResourceAssetRepository : IResourceAssetRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        public ResourceAssetRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<int> DeleteMultipleAsync(List<int> ids)
        {
            var sql = $"DELETE FROM ResourceAsset WHERE ResourceAssetId IN @Ids";
            var parameters = new { Ids = ids };
            var result = await _unitOfWork.Connection.ExecuteAsync(sql, parameters, _unitOfWork.Transaction);
            return result;
        }

        public async Task<int> DeleteOneAsync(int id)
        {
            var sql = $"DELETE FROM ResourceAsset WHERE ResourceAssetId = @Id";
            var parameters = new { Id = id };
            var result = await _unitOfWork.Connection.ExecuteAsync(sql, parameters, _unitOfWork.Transaction);
            return result;
        }

        public async Task<List<ResourceAsset>> GetByAssetIdAsync(Guid id)
        {
            var sql = """
                SELECT resource_asset_id, cost, resourse_budget.resourse_budget_id, resource_budget_code, resource_budget_name
                FROM resource_asset JOIN resourse_budget ON resource_asset.resourse_budget_id = resourse_budget.resourse_budget_id
                WHERE recorded_asset_id = @Id
                """;
            var parameters = new { Id = id };
            var result = await _unitOfWork.Connection.QueryAsync<ResourceAsset, ResourceBudget, ResourceAsset>(sql, (resourceAsset, resourceBudget) =>
            {
                resourceAsset.ResourceBudget = resourceBudget;
                return resourceAsset;
            }, parameters, _unitOfWork.Transaction, splitOn: "resourse_budget_id");
            if (result.Count() == 0)
            {
                throw new NotFoundException($"Không tìm thấy nguồn của tài sản có id {id}");
            }
            return result.ToList();
        }

        public async Task<ResourceAsset> GetByIdAsync(int id)
        {
            var sql = $"SELECT * FROM resource_asset JOIN resourse_budget ON resource_asset.resourse_budget_id = resourse_budget.resourse_budget_id WHERE resource_asset_id = @Id";
            var parameters = new { Id = id };
            var result = await _unitOfWork.Connection.QueryAsync<ResourceAsset, ResourceBudget, ResourceAsset>(sql, (resourceAsset, resourceBudget) =>
            {
                resourceAsset.ResourceBudget = resourceBudget;
                return resourceAsset;
            }, parameters, _unitOfWork.Transaction, splitOn: "resourse_budget_id");
            if (!result.Any() || result == null)
            {
                throw new NotFoundException($"Không tìm thấy nguồn tài sản có id {id}");
            }
            return result.FirstOrDefault();
        }

        public async Task<int> InsertMultipleAsync(Guid assetId, List<ResourceAsset> resourceAssets)
        {
            var sql = """
                INSERT INTO resource_asset (recorded_asset_id, resourse_budget_id,recorded_asset_id, cost)
                """;
            var lists = new List<dynamic>();
            foreach (var resourceAsset in resourceAssets)
            {
                if (resourceAsset.ResourceBudget == null)
                {
                    throw new BadRequestException("Nguồn tài sản không được để trống");
                }
                lists.Add(new
                {
                    resource_asset_id = resourceAsset.ResourceAssetId,
                    resourse_budget_id = resourceAsset.ResourceBudget.ResourceBudgetId,
                    recorded_asset_id = assetId,
                    cost = resourceAsset.Cost
                });
            }
            var result = await _unitOfWork.Connection.ExecuteAsync(sql, lists, _unitOfWork.Transaction);
            return result;
        }

        public async Task<int> InsertOneAsync(Guid assetId, ResourceAsset resourceAsset)
        {
            var sql = """
                INSERT INTO resource_asset (resource_asset_id, resourse_budget_id,recorded_asset_id, cost)
                VALUES (@ResourceAssetId, @ResourceBudgetId, @AssetId, @Cost)
                """;
            if (resourceAsset.ResourceBudget == null)
            {
                throw new BadRequestException("Nguồn tài sản không được để trống");
            }
            var parameters = new
            {
                resourceAsset.ResourceAssetId,
                resourceAsset.ResourceBudget.ResourceBudgetId,
                AssetId = assetId,
                resourceAsset.Cost
            };

            var result = await _unitOfWork.Connection.ExecuteAsync(sql, parameters, _unitOfWork.Transaction);
            return result;
        }

        public Task<int> UpdateMultipleAsync(List<ResourceAsset> resourceAssets)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateOneAsync(ResourceAsset resourceAsset)
        {
            throw new NotImplementedException();
        }
    }
}
