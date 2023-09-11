using Dapper;
using MISA.Web062023.AMIS.Application;
using MISA.Web062023.AMIS.Domain;
using System.Data;
using System.Data.SqlClient;
using Z.Dapper.Plus;
using static Dapper.SqlMapper;

namespace MISA.Web062023.AMIS.Infrastructure
{
    /// <summary>
    /// Thao tác với cơ sở dữ liệu thực hiện các chức năng đọc, thêm, sửa, xóa
    /// </summary>
    /// <typeparam name="TEntity">Thực thể trong cơ sở dữ liệu</typeparam>
    /// Created by: NTLam (17/08/2023)
    public class BaseCrudRepository<TEntity> : BaseReadonlyRepository<TEntity>, ICrudRepository<TEntity> where TEntity : IEntity
    {

        /// <summary>
        /// The .ctor.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// Created by: NTLam (17/08/2023)
        public BaseCrudRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {

        }

        /// <summary>
        /// The insert async.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>The result.</returns>
        /// Created by: NTLam (17/08/2023)
        public async Task<int> InsertAsync(TEntity entity)
        {
            // Lấy danh sách property của entity
            var props = entity.GetType().GetProperties();
            var param = new DynamicParameters();

            var columns = new List<string>();
            var paramsName = new List<string>();

            // Thêm giá trị của prop vào param và lấy danh sách cột và tên của param theo property
            foreach (var prop in props)
            {
                var propName = prop.Name;
                param.Add($"{propName}", prop.GetValue(entity));
                columns.Add(StringExtension.PascalCaseToUnderscoreCase(propName));
                paramsName.Add($"@{propName}");
            }

            // Chuyển danh sách cột và tên của param thành chuỗi ngăn cách bởi dấu phẩy
            var columnsString = string.Join(", ", columns);
            var paramsNameString = string.Join(", ", paramsName);

            var sql = $"INSERT INTO {TableName} ({columnsString}) VALUES ({paramsNameString})";

            var result = await _unitOfWork.Connection.ExecuteAsync(sql, param, _unitOfWork.Transaction);
            return result;
        }

        /// <summary>
        /// The insert multi async.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns>The result.</returns>
        /// Created by: NTLam (17/08/2023)
        public async Task<int> InsertMultiAsync(List<TEntity> entities)
        {
            var entity = entities[0];
            // Lấy danh sách property của entity
            var props = entity.GetType().GetProperties();
            //var param = new DynamicParameters();

            //var columns = new List<string>();
            //var paramsName = new List<string>();

            //DataTable table = new DataTable();
            //table.TableName = TableName;

            //foreach (var prop in props)
            //{
            //    var propName = prop.Name;
            //    var type = prop.PropertyType;
            //    var newType = Nullable.GetUnderlyingType(type) ?? type;
            //    table.Columns.Add(StringExtension.PascalCaseToUnderscoreCase(propName), newType);
            //}
            //foreach (var entityItem in entities)
            //{
            //    var row = table.NewRow();
            //    foreach (var prop in props)
            //    {
            //        var propName = prop.Name;
            //        var value = prop.GetValue(entityItem);
            //        row[StringExtension.PascalCaseToUnderscoreCase(propName)] = value ?? DBNull.Value;
            //    }
            //    table.Rows.Add(row);
            //}
            //using var bulkCopy = new SqlBulkCopy(_unitOfWork.getConnectionString());
            //bulkCopy.DestinationTableName = TableName;
            //try
            //{
            //    await bulkCopy.WriteToServerAsync(table);
            //}
            //catch (Exception ex)
            //{
            //    Console.WriteLine(ex.Message);
            //}
            //finally
            //{
            //    bulkCopy.Close();
            //}

            // Inssert dùng Dapper Plus
            // await _unitOfWork.Connection.BulkActionAsync(x => x.BulkInsert(entities));

            // Insert dùng Dapper

            var fields = new List<string>();
            var stringParams = new List<string>();

            foreach (var prop in props)
            {
                var propName = prop.Name;
                fields.Add($"{StringExtension.PascalCaseToUnderscoreCase(propName)}");
                stringParams.Add($"@{propName}");
            }

            var query = $"INSERT INTO {TableName} ({string.Join(", ", fields)}) VALUES ({string.Join(", ", stringParams)})";

            var result = await _unitOfWork.Connection.ExecuteAsync(query, param: entities, _unitOfWork.Transaction);
            return result;
        }

        /// <summary>
        /// The update async.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>The result.</returns>
        public async Task<int> UpdateAsync(TEntity entity)
        {
            // Lấy danh sách property của entity
            var props = entity.GetType().GetProperties();
            var param = new DynamicParameters();

            // Thêm giá trị của prop vào param và lấy danh sách của các cột
            var pairKeyValueList = new List<string>();
            foreach (var prop in props)
            {
                var propName = prop.Name;
                var value = prop.GetValue(entity);
                if (value == null) continue;
                param.Add($"{propName}", value);
                pairKeyValueList.Add($"{StringExtension.PascalCaseToUnderscoreCase(propName)} = @{propName}");
            }

            param.Add("ModifiedDate", DateTime.Now);
            pairKeyValueList.Add($"modified_date = @ModifiedDate");

            // Chuyển danh sách cột và tên của param thành chuỗi ngăn cách bởi dấu phẩy
            var pairKeyValueListString = string.Join(", ", pairKeyValueList);

            var sql = $"UPDATE {TableName} SET {pairKeyValueListString} WHERE {TableName}_id = @{StringExtension.UnderscoreCaseToPascalCase(TableName)}Id";

            var result = await _unitOfWork.Connection.ExecuteAsync(sql, param, _unitOfWork.Transaction);
            return result;
        }

        /// <summary>
        /// The delete async.
        /// </summary>
        /// <param name="entity">The entity.</param>
        /// <returns>The result.</returns>
        /// Created by: NTLam (17/08/2023)
        public async Task<int> DeleteAsync(Guid id)
        {
            var sql = $"DELETE FROM {TableName} WHERE {TableName}_id = @Id;";

            var param = new DynamicParameters();
            param.Add("@Id", id);

            var result = await _unitOfWork.Connection.ExecuteAsync(sql, param, _unitOfWork.Transaction);
            return result;
        }

        /// <summary>
        /// The delete multi async.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns>The result.</returns>
        /// Created by: NTLam (17/08/2023)
        public async Task<int> DeleteMultiAsync(List<Guid> ids)
        {
            var param = new DynamicParameters();
            var sql = $"DELETE FROM {TableName} WHERE {TableName}_id IN @Ids;";
            param.Add("@Ids", ids);
            var result = await _unitOfWork.Connection.ExecuteAsync(sql, param, _unitOfWork.Transaction);

            return result;
        }


    }
}
