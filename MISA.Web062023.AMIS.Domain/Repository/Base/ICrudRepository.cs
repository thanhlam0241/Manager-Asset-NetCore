namespace MISA.Web062023.AMIS.Domain
{

    /// <summary>
    /// The repository crud base.
    /// </summary>
    /// Created by: NTLam (14/08/2023)
    public interface ICrudRepository<TEntity> : IReadonlyRepository<TEntity> where TEntity : IEntity
    {
        /// <summary>
        /// Thêm mới bản ghi
        /// </summary>
        /// <param name="entity">Bản ghi</param>
        /// <returns>Bản ghi</returns>
        /// Created by: NTLam (14/08/2023)
        Task<int> InsertAsync(TEntity entity);

        /// <summary>
        /// The insert multi async.
        /// </summary>
        /// <param name="entities">The entities.</param>
        /// <returns>The result.</returns>
        /// Created by: NTLam (14/08/2023)
        Task<int> InsertMultiAsync(List<TEntity> entities);

        /// <summary>
        /// Chỉnh sửa bản ghi
        /// </summary>
        /// <param name="entity">Bản ghi</param>
        /// <returns>Bản ghi</returns>
        /// Created by: NTLam (14/08/2023)
        Task<int> UpdateAsync(TEntity entity);

        /// <summary>
        /// Xóa bản ghi
        /// </summary>
        /// <param name="entity">Bản ghi</param>
        /// <returns>Số lượng bản ghi đã xóa</returns>
        /// Created by: NTLam (14/08/2023)
        Task<int> DeleteAsync(Guid id);

        /// <summary>
        /// Xóa nhiều bản ghi
        /// </summary>
        /// <param name="ids">Danh sách id các bản ghi</param>
        /// <returns>Số lượng bản ghi đã xóa</returns>
        /// Created by: NTLam (14/08/2023)
        Task<int> DeleteMultiAsync(List<Guid> ids);
    }
}
