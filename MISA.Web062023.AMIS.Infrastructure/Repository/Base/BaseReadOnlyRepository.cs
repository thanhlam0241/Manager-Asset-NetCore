using Dapper;
using MISA.Web062023.AMIS.Application;
using MISA.Web062023.AMIS.Domain;

namespace MISA.Web062023.AMIS.Infrastructure
{
    /// <summary>
    /// Thao tác với cơ sở dữ liệu thực chức năng chỉ đọc
    /// </summary>
    /// <typeparam name="TEntity">Thực thể trong cơ sở dữ liệu</typeparam>
    /// Created by: NTLam (17/08/2023)
    public abstract class BaseReadonlyRepository<TEntity> : IReadonlyRepository<TEntity> where TEntity : IEntity
    {
        #region Properties
        protected readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Gets or Sets the table name.
        /// </summary>
        /// Created by: NTLam (17/08/2023)
        protected virtual string TableName { get; set; } = StringExtension.PascalCaseToUnderscoreCase(typeof(TEntity).Name);

        #endregion
        /// <summary>
        /// The .ctor.
        /// </summary>
        /// <param name="_unitOfWork">The _unitOfWork.</param>
        /// Created by: NTLam (17/08/2023)
        public BaseReadonlyRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region Methods
        /// <summary>
        /// The get all async.
        /// </summary>
        /// <returns>The result.</returns>
        /// Created by: NTLam (17/08/2023)
        public async Task<List<TEntity>> GetAllAsync()
        {
            var sql = $"SELECT * FROM {TableName};";
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            var result = await _unitOfWork.Connection.QueryAsync<TEntity>(sql, _unitOfWork.Transaction);
            return result.ToList();
        }

        /// <summary>
        /// The get async.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The result.</returns>
        /// Created by: NTLam (17/08/2023)
        public async Task<TEntity> GetAsync(Guid id)
        {
            var entity = await FindByIdAsync(id);

            if (entity == null)
            {
                throw new NotFoundException(string.Format(Domain.Resources.Exception.Exception.NotExistId, id));
            }

            return entity;
        }

        /// <summary>
        /// The get by list id async.
        /// </summary>
        /// <param name="ids">The ids.</param>
        /// <returns>The result.</returns>
        /// Created by: NTLam (17/08/2023)
        public async Task<IEnumerable<TEntity>> GetByListIdAsync(List<Guid> ids)
        {
            var sql = $"SELECT * FROM {TableName} WHERE {TableName}_id IN @Id;";
            var param = new DynamicParameters();
            param.Add("@Id", ids);

            DefaultTypeMap.MatchNamesWithUnderscores = true;
            var result = await _unitOfWork.Connection.QueryAsync<TEntity>(sql, param, _unitOfWork.Transaction);
            return result;
        }

        /// <summary>
        /// The get paging async.
        /// </summary>
        /// <param name="limit">The limit.</param>
        /// <param name="offset">The offset.</param>
        /// <returns>The result.</returns>
        /// Created by: NTLam (17/08/2023)
        public async Task<List<TEntity>> GetFilterAsync(int limit, int offset)
        {
            var sql = $"SELECT * FROM {TableName} LIMIT @limit OFFSET @offset";

            var param = new DynamicParameters();
            param.Add("@limit", limit);
            param.Add("@offset", offset);
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            var result = await _unitOfWork.Connection.QueryAsync<TEntity>(sql, param, _unitOfWork.Transaction);
            return result.ToList();
        }

        /// <summary>
        /// The find TEntity async by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The result.</returns>
        /// Created by: NTLam (17/08/2023)
        public async Task<TEntity?> FindByIdAsync(Guid id)
        {
            var sql = $"SELECT * FROM {TableName} WHERE {TableName}_id = @Id;";

            var param = new DynamicParameters();
            param.Add("@Id", id);
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            var result = await _unitOfWork.Connection.QueryFirstOrDefaultAsync<TEntity>(sql, param, _unitOfWork.Transaction);

            return result;
        }
        /// <summary>
        /// The find async by code.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The result.</returns>
        /// Created by: NTLam (17/08/2023)
        public async Task<TEntity?> FindByCodeAsync(string code)
        {
            var sql = $"SELECT * FROM {TableName} WHERE {TableName}_code = @Code;";

            var param = new DynamicParameters();
            param.Add("@Code", code);
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            var result = await _unitOfWork.Connection.QueryFirstOrDefaultAsync<TEntity>(sql, param, _unitOfWork.Transaction);

            return result;
        }

        /// <summary>
        /// The get by list code async.
        /// </summary>
        /// <param name="codes">The codes.</param>
        /// <returns>The result.</returns>
        /// Created by: NTLam (17/08/2023)
        public async Task<IEnumerable<string>> GetByListCodeAsync(List<string> codes)
        {
            var sql = $"SELECT Distinct {TableName}_code FROM {TableName} WHERE {TableName}_code IN @Code;";
            var param = new DynamicParameters();
            param.Add("@Code", codes);
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            var results = await _unitOfWork.Connection.QueryAsync<string>(sql, param, _unitOfWork.Transaction);
            return results;
        }

        //public Task<IEnumerable<TEntity>> GetByListCodeAsync(List<string> codes)
        //{
        //    throw new NotImplementedException();
        //}
        #endregion
    }
}