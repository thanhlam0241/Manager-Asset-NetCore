using Dapper;
using MISA.Web062023.AMIS.Application;
using MISA.Web062023.AMIS.Domain;
using MISA.Web062023.AMIS.Domain.Resources.FixedAssetCategory;
using System;
using System.Data;
using System.Drawing.Printing;
using System.Reflection.Metadata.Ecma335;

namespace MISA.Web062023.AMIS.Infrastructure
{

    /// <summary>
    /// The fixed asset repository.
    /// </summary>
    /// Author: Nguyễn Thanh Lâm
    /// Modified date: 10/8/2023
    public class FixedAssetRepository : BaseCrudRepository<FixedAsset>, IFixedAssetRepository
    {
        /// <summary>
        /// The injection repository.
        /// </summary>
        /// <param name="dbMISAContext">The unitOfWork</param>
        /// Created by: Nguyễn Thanh Lâm (11/8/2023)
        public FixedAssetRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        /// <summary>
        /// The create fixed asset.
        /// </summary>
        /// <param name="fixedAsset">The fixed asset.</param>
        /// <returns>The result.</returns>
        /// Created by: Nguyễn Thanh Lâm (11/8/2023)
        public async Task<int> CreateFixedAssetAsync(FixedAsset fixedAsset)
        {
            var query = """
                INSERT INTO fixed_asset
                (fixed_asset_id, fixed_asset_code, fixed_asset_name, organization_id, 
                organization_code, organization_name, department_id, department_code, 
                department_name, fixed_asset_category_id, fixed_asset_category_code, 
                fixed_asset_category_name, purchase_date, cost, quantity, 
                depreciation_rate, tracked_year, life_time, production_year, 
                active)
                VALUES (@FixedAssetId, @FixedAssetCode, @FixedAssetName, @OrganizationId, 
                @OrganizationCode, @OrganizationName, @DepartmentId, @DepartmentCode, 
                @DepartmentName, @FixedAssetCategoryId, @FixedAssetCategoryCode, @FixedAssetCategoryName
                ,@PurchaseDate , @Cost, @Quantity, @DepreciationRate, @TrackedYear, @LifeTime, 
                @ProductionYear, @Active)
                """;
            var connection = _unitOfWork.Connection;
            var parameters = new DynamicParameters();
            var properties = fixedAsset.GetType().GetProperties();
            foreach (var property in properties)
            {
                var propertyName = property.Name;
                var propertyValue = property.GetValue(fixedAsset);
                parameters.Add(propertyName, propertyValue);
            }
            //parameters.Add("CreatedBy", fixedAsset.CreatedBy, DbType.String);
            //parameters.Add("ModifiedBy", fixedAsset.ModifiedBy, DbType.String);

            int result = await connection.ExecuteAsync(query, parameters, transaction: _unitOfWork.Transaction);
            return result;
        }

        /// <summary>
        /// The delete fixed asset.
        /// </summary>
        /// <param name="assetId">The asset id.</param>
        /// Created by: Nguyễn Thanh Lâm (11/8/2023)
        public async Task<int> DeleteFixedAssetAsync(Guid assetId)
        {
            String query = $"Delete FROM fixed_asset WHERE fixed_asset_id = @Id";
            using var connection = _unitOfWork.Connection;
            int numberRowDeleted = await connection.ExecuteAsync(query, new { Id = assetId }, transaction: _unitOfWork.Transaction);
            return numberRowDeleted;
        }

        /// <summary>
        /// The get filter assets.
        /// </summary>
        /// <param name="pageSize">The page size.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <returns>The result.</returns>
        /// Created by: Nguyễn Thanh Lâm (11/8/2023)
        public async Task<FilterFixedAsset> GetFilterAssetsAsync(int pageSize, int pageNumber, string filterString, List<Guid>? departmantIds, List<Guid>? fixedAssetCategoryIds)
        {
            var parameters = new DynamicParameters();
            var fieldAssets = new string[]
            {
               "fixed_asset_code", "fixed_asset_name",
                "department_name",
                "fixed_asset_category_name", "cost", "quantity"
            };
            string stringFilterCode = "";
            if (departmantIds != null && departmantIds.Count > 0)
            {
                parameters.Add("DepartmentIds", departmantIds);
                stringFilterCode += "department_id IN @DepartmentIds";
            }
            if (fixedAssetCategoryIds != null && fixedAssetCategoryIds.Count > 0)
            {
                parameters.Add("FixedAssetCategoryIds", fixedAssetCategoryIds);
                if (stringFilterCode != "")
                {
                    stringFilterCode += " AND fixed_asset_category_id IN @FixedAssetCategoryIds";
                }
                else
                {
                    stringFilterCode += "fixed_asset_category_id IN @FixedAssetCategoryIds";
                }
            }
            string filterQuery = (filterString != "") ? $"WHERE ({string.Join(" LIKE @FilterPattern OR ", fieldAssets)} LIKE @FilterPattern)  " : "";
            if (filterQuery != "" && stringFilterCode != "")
            {
                stringFilterCode = $" AND {stringFilterCode}";
                filterQuery += stringFilterCode;
            }
            else if (filterQuery == "" && stringFilterCode != "")
            {
                stringFilterCode = $"WHERE {stringFilterCode}";
                filterQuery += stringFilterCode;
            }
            string query = $"SELECT * FROM fixed_asset {filterQuery}  ORDER BY modified_date DESC LIMIT @PageSize OFFSET @Offset;";
            var queryTotalRecord = $"SELECT Count(fixed_asset_code) from fixed_asset {filterQuery}";

            parameters.Add("PageSize", pageSize, DbType.Int16);
            parameters.Add("Offset", (pageNumber - 1) * pageSize, DbType.Int16);
            parameters.Add("FilterPattern", $"%{filterString}%", DbType.String);

            var connection = _unitOfWork.Connection;
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            var filterAssets = await connection.QueryAsync<FixedAsset>(query, parameters, _unitOfWork.Transaction);
            long totalRecords = await connection.ExecuteScalarAsync<long>(queryTotalRecord, parameters, _unitOfWork.Transaction);

            FilterFixedAsset filterFixedAsset = new();
            if (filterAssets.ToList().Count > 0 && totalRecords > 0)
            {
                filterFixedAsset.Data = filterAssets;
                filterFixedAsset.CurrentPage = pageNumber;
                filterFixedAsset.CurrentPageSize = pageSize;
                filterFixedAsset.TotalRecord = totalRecords;
                filterFixedAsset.CurrentRecord = filterAssets.ToList().Count;
                filterFixedAsset.TotalPage = totalRecords / pageSize + (totalRecords % pageSize == 0 ? 0 : 1);
            }
            return filterFixedAsset;
        }


        /// <summary>
        /// The get all filter asset async.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="departmantCode">The departmant code.</param>
        /// <param name="fixedAssetCategoryCode">The fixed asset category code.</param>
        /// <returns>The result.</returns>
        /// Created by: Nguyễn Thanh Lâm (11/8/2023)
        public async Task<IEnumerable<FixedAsset>> GetAllFilterAssetAsync(string filterString, List<Guid>? departmantIds, List<Guid>? fixedAssetCategoryIds)
        {
            var parameters = new DynamicParameters();
            var fieldAssets = new string[]
            {
               "fixed_asset_code", "fixed_asset_name",
                "department_name",
                "fixed_asset_category_name", "cost", "quantity"
            };
            string stringFilterCode = "";
            if (departmantIds != null && departmantIds.Count > 0)
            {
                parameters.Add("DepartmentIds", departmantIds);
                stringFilterCode += "department_id IN @DepartmentIds";
            }
            if (fixedAssetCategoryIds != null && fixedAssetCategoryIds.Count > 0)
            {
                parameters.Add("FixedAssetCategoryIds", fixedAssetCategoryIds);
                if (stringFilterCode != "")
                {
                    stringFilterCode += " AND fixed_asset_category_id IN @FixedAssetCategoryIds";
                }
                else
                {
                    stringFilterCode += "fixed_asset_category_id IN @FixedAssetCategoryIds";
                }
            }
            string filterQuery = (filterString != "") ? $"WHERE ({string.Join(" LIKE @FilterPattern OR ", fieldAssets)} LIKE @FilterPattern)  " : "";
            if (filterQuery != "" && stringFilterCode != "")
            {
                stringFilterCode = $" AND {stringFilterCode}";
                filterQuery += stringFilterCode;
            }
            else if (filterQuery == "" && stringFilterCode != "")
            {
                stringFilterCode = $"WHERE {stringFilterCode}";
                filterQuery += stringFilterCode;
            }
            string query = $"SELECT * FROM fixed_asset {filterQuery}  ORDER BY modified_date DESC;";
            parameters.Add("FilterPattern", $"%{filterString}%", DbType.String);
            var connection = _unitOfWork.Connection;
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            var filterAssets = await connection.QueryAsync<FixedAsset>(query, parameters, _unitOfWork.Transaction);
            return filterAssets;
        }


        /// <summary>
        /// The get all fixed assets.
        /// </summary>
        /// <returns>The result.</returns>
        /// Created by: Nguyễn Thanh Lâm (11/8/2023)
        public async Task<IEnumerable<FixedAsset>> GetAllFixedAssetsAsync()
        {
            string query = $"""
                SELECT fixed_asset_id,fixed_asset_code, fixed_asset_name, organization_id, 
                organization_code, organization_name, department_id, department_code, 
                department_name, fixed_asset_category_id, fixed_asset_category_code, 
                fixed_asset_category_name, purchase_date, cost, quantity, 
                depreciation_rate, tracked_year, life_time, production_year, 
                active, created_by, modified_by FROM fixed_asset
                """;
            using var connection = _unitOfWork.Connection;
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            var fixedAssets = await connection.QueryAsync<FixedAsset>(query);
            return fixedAssets;
        }

        /// <summary>
        /// The get fixed asset by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The result.</returns>
        /// Created by: Nguyễn Thanh Lâm (11/8/2023)
        public async Task<FixedAsset> GetFixedAssetByIdAsync(Guid id)
        {
            string query = $"""
                SELECT fixed_asset_id, fixed_asset_code, fixed_asset_name, organization_id, 
                organization_code, organization_name, department_code, 
                department_name,  fixed_asset_category_code, 
                fixed_asset_category_name, purchase_date, cost, quantity, 
                depreciation_rate, tracked_year, life_time, production_year, 
                active, created_by, modified_by 
                FROM fixed_asset WHERE fixed_asset_id = @Id
                """;
            var connection = _unitOfWork.Connection;
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            var fixedAsset = await connection.QueryFirstOrDefaultAsync<FixedAsset>(query, new { Id = id });
            return fixedAsset;
        }

        /// <summary>
        /// The find fixed asset by code async.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns>The result.</returns>
        /// Created by: Nguyễn Thanh Lâm (11/8/2023)
        public async Task<FixedAsset?> FindFixedAssetByCodeAsync(string code)
        {
            string query = $"""
                SELECT * FROM fixed_asset WHERE fixed_asset_code = @Code
                """;
            var connection = _unitOfWork.Connection;
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            var fixedAsset = await connection.QueryFirstOrDefaultAsync<FixedAsset?>(query, new { Code = code });
            return fixedAsset;
        }

        /// <summary>
        /// The update fixed asset.
        /// </summary>
        /// <param name="assetId">The asset id.</param>
        /// <param name="fixedAsset">The fixed asset.</param>
        /// <returns>The result.</returns>
        /// Created by: Nguyễn Thanh Lâm (11/8/2023)
        public async Task<int> UpdateFixedAssetAsync(FixedAsset fixedAsset)
        {
            List<string> fieldsUpdate = new List<string>();
            var parameters = new DynamicParameters();
            var properties = fixedAsset.GetType().GetProperties();
            foreach (var property in properties)
            {
                var propertyName = property.Name;
                var propertyValue = property.GetValue(fixedAsset);
                if (propertyValue == null) continue;
                if (propertyName != "FixedAssetId")
                {
                    fieldsUpdate.Add($"{StringExtension.PascalCaseToUnderscoreCase(propertyName)} = @{propertyName}");
                }
                parameters.Add(propertyName, propertyValue);
            }

            fieldsUpdate.Add("modified_date = NOW()");

            string query = $"""
                UPDATE fixed_asset 
                SET {string.Join(", ", fieldsUpdate)}
                WHERE fixed_asset_id = @FixedAssetId
                """;

            var connecttion = _unitOfWork.Connection;
            var result = await connecttion.ExecuteAsync(query, parameters, transaction: _unitOfWork.Transaction);
            return result;
        }

        /// <summary>
        /// The delete many async.
        /// </summary>
        /// <param name="ids">The ids.</param>
        /// <returns>The result.</returns>
        /// Created by: Nguyễn Thanh Lâm (11/8/2023)
        public async Task<int> DeleteManyAsync(List<Guid> ids)
        {
            string query = "Delete FROM fixed_asset WHERE fixed_asset_id IN @Ids";
            var connection = _unitOfWork.Connection;
            var result = await connection.ExecuteAsync(query, new { Ids = ids }, transaction: _unitOfWork.Transaction);
            return result;
        }

        /// <summary>
        /// The generate code.
        /// </summary>
        /// <returns>The result.</returns>
        /// Created by: Nguyễn Thanh Lâm (11/8/2023)
        public Task<string> GenerateCode()
        {
            var sql = "SELECT MAX(fixed_asset_code) FROM fixed_asset WHERE fixed_asset_code RLIKE '^TS[0-9]{6}$' ";
            var connection = _unitOfWork.Connection;
            var code = connection.ExecuteScalar<string>(sql);
            if (code == null)
            {
                return Task.FromResult("TS000001");
            }
            string result;
            var number = int.Parse(code[2..]) + 1;
            if (number < 1000000)
            {
                result = $"TS{number.ToString().PadLeft(6, '0')}";
            }
            else
            {
                result = $"TS{number}";
            }
            return Task.FromResult(result);
        }

    }
}
