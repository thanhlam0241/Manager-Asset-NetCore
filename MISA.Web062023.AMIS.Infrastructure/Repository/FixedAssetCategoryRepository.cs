using Dapper;
using MISA.Web062023.AMIS.Application;
using MISA.Web062023.AMIS.Domain;
using System.Data;

namespace MISA.Web062023.AMIS.Infrastructure
{

    /// <summary>
    /// The fixed asset category repository.
    /// </summary>
    /// Created by: Nguyễn Thanh Lâm (11/8/2023)
    public class FixedAssetCategoryRepository : BaseCrudRepository<FixedAssetCategory>, IFixedAssetCategoryRepository
    {
        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="dbMISAContext">The database context.</param>
        /// Created by: NTLam (11/8/2023)
        public FixedAssetCategoryRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        /// <summary>
        /// The create category.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <returns>The result.</returns>
        /// Created by: Nguyễn Thanh Lâm (11/8/2023)
        public async Task<int> CreateCategoryAsync(FixedAssetCategory category)
        {
            string query = @"INSERT INTO fixed_asset_category
	                        (fixed_asset_category_id,fixed_asset_category_code, fixed_asset_category_name,
                            organization_id, depreciation_rate, life_time, description)
	                        VALUES (@FixedAssetCategoryId, @FixedAssetCategoryCode, @FixedAssetCategoryName, @OrganizationId, 
                            @DepreciationRate, @LifeTime, @Description)";
            var connection = _unitOfWork.Connection;

            var parameters = new DynamicParameters();
            var properties = category.GetType().GetProperties();
            foreach (var property in properties)
            {
                var propertyName = property.Name;
                var propertyValue = property.GetValue(category);
                parameters.Add(propertyName, propertyValue);
            }

            var result = await connection.ExecuteAsync(query, parameters, transaction: _unitOfWork.Transaction);
            return result;
        }

        /// <summary>
        /// The find category by code async.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns>The result.</returns>
        /// Created by: Nguyễn Thanh Lâm (11/8/2023)
        public async Task<FixedAssetCategory?> FindCategoryByCodeAsync(string code)
        {
            string query = $"SELECT * FROM fixed_asset_category WHERE fixed_asset_category = @Code";
            var connection = _unitOfWork.Connection;
            var parameters = new DynamicParameters();
            parameters.Add("Code", code);
            var fixedAssetCategory = await connection.QueryFirstOrDefaultAsync<FixedAssetCategory?>(query, parameters);
            return fixedAssetCategory;
        }

        /// <summary>
        /// The delete category.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The result.</returns>
        /// Created by: Nguyễn Thanh Lâm (11/8/2023)
        public async Task<int> DeleteCategoryAsync(Guid id)
        {
            string query = $"Delete FROM fixed_asset_category WHERE fixed_asset_category_id = @Id";
            var connection = _unitOfWork.Connection;
            var result = await connection.ExecuteAsync(query, new { Id = id }, transaction: _unitOfWork.Transaction);
            return result;
        }

        /// <summary>
        /// The get categories.
        /// Author: Nguyễn Thanh Lâm
        /// Modified date: 10/8/2023
        /// </summary>
        /// <returns>The result.</returns>
        /// Created by: Nguyễn Thanh Lâm (11/8/2023)
        public async Task<IEnumerable<FixedAssetCategory>> GetCategoriesAsync()
        {
            string query = @"SELECT fixed_asset_category_id,fixed_asset_category_code, fixed_asset_category_name, organization_id, 
                                    depreciation_rate, life_time, description, created_by, modified_by
	                         FROM fixed_asset_category";
            using var connection = _unitOfWork.Connection;
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            var categories = await connection.QueryAsync<FixedAssetCategory>(query);
            return categories;
        }

        /// <summary>
        /// The get category by id.
        /// </summary>
        /// <param name="categoryId">The category id.</param>
        /// <returns>The result.</returns>
        /// Created by: Nguyễn Thanh Lâm (11/8/2023)
        public async Task<FixedAssetCategory> GetCategoryByIdAsync(Guid categoryId)
        {
            string query = $"""
                           SELECT fixed_asset_category_id, fixed_asset_category_code, fixed_asset_category_name, organization_id, 
                           depreciation_rate, life_time, description, created_by, modified_by
                           FROM fixed_asset_category WHERE fixed_asset_category_id = @Id
                           """;
            var connection = _unitOfWork.Connection;
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            var category = await connection.QueryFirstOrDefaultAsync<FixedAssetCategory>(query, new { Id = categoryId });
            return category;
        }

        /// <summary>
        /// The update category.
        /// </summary>
        /// <param name="categoryId">The category id.</param>
        /// <param name="category">The category.</param>
        /// <returns>The result.</returns>
        /// Created by: Nguyễn Thanh Lâm (11/8/2023)
        public async Task<int> UpdateCategoryAsync(FixedAssetCategory category)
        {

            List<string> fieldsUpdate = new List<string>();
            var parameters = new DynamicParameters();
            var properties = category.GetType().GetProperties();
            foreach (var property in properties)
            {
                var propertyName = property.Name;
                var propertyValue = property.GetValue(category);
                if (propertyValue == null) continue;
                if (propertyName != "FixedAssetCategoryId")
                {
                    fieldsUpdate.Add($"{StringExtension.PascalCaseToUnderscoreCase(propertyName)} = @{propertyName}");
                }
                parameters.Add(propertyName, propertyValue);
            }
            string query = $"""
                           UPDATE fixed_asset_category
                           SET {string.Join(", ", fieldsUpdate)}
                           WHERE fixed_asset_category_id = @FixedAssetCategoryId
                           """;

            using var connection = _unitOfWork.Connection;

            int result = await connection.ExecuteAsync(query, parameters, transaction: _unitOfWork.Transaction);
            return result;
        }
    }
}
