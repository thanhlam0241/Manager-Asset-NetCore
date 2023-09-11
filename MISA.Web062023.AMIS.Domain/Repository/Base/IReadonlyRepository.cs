namespace MISA.Web062023.AMIS.Domain
{
    /// <summary>
    /// Khai báo Interface repository thực hiện chức năng chỉ đọc
    /// </summary>
    /// <typeparam name="TEntity">Thực thể trong cơ sở dữ liệu</typeparam>
    /// Created by: NTLam (17/08/2023)
    public interface IReadonlyRepository<TEntity> where TEntity : IEntity
    {
        /// <summary>
        /// Lấy toàn bộ bản ghi
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        /// Created by: NTLam (14/08/2023)
        Task<List<TEntity>> GetAllAsync();

        /// <summary>
        /// Lấy danh sách bản ghi theo mảng Id
        /// </summary>
        /// <param name="ids">Mảng Id</param>
        /// <returns>Danh sách bản ghi</returns>
        /// Created by: NTLam (14/08/2023)
        Task<IEnumerable<TEntity>> GetByListIdAsync(List<Guid> ids);

        /// <summary>
        /// The get by list code async.
        /// </summary>
        /// <param name="codes">The codes.</param>
        /// <returns>The result.</returns>
        /// Created by: NTLam (14/08/2023)
        Task<IEnumerable<string>> GetByListCodeAsync(List<string> codes);

        /// <summary>
        /// Lấy danh sách lọc các bản ghi, gồm số lượng bản ghi, trang hiện tại, chuỗi filter
        /// </summary>
        /// <param name="limit">Số lượng bản ghi</param>
        /// <param name="offset">Vị trí bắt đầu lấy</param>
        /// <returns>Danh sách bản ghi</returns>
        /// Created by: NTLam (15/08/2023)
        Task<List<TEntity>> GetFilterAsync(int limit, int offset);

        /// <summary>
        /// Lấy bản ghi theo Id
        /// </summary>
        /// <param name="id">Id của bản ghi</param>
        /// <returns>Bản ghi</returns>
        /// Created by: NTLam (14/08/2023)
        Task<TEntity> GetAsync(Guid id);

        /// <summary>
        /// Tìm kiếm bản ghi theo Id
        /// </summary>
        /// <param name="id">Id của bản ghi</param>
        /// <returns>Bản ghi | null</returns>
        /// Created by: NTLam (14/08/2023)
        Task<TEntity?> FindByIdAsync(Guid id);
        /// <summary>
        /// Tìm kiếm bản ghi theo Code
        /// </summary>
        /// <param name="id">Code của bản ghi</param>
        /// <returns>Bản ghi | null</returns>
        /// Created by: NTLam (14/08/2023)
        Task<TEntity?> FindByCodeAsync(string code);

    }
}
