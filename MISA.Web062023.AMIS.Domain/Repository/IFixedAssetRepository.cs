namespace MISA.Web062023.AMIS.Domain
{

    /// <summary>
    /// The interface fixed asset repository.
    /// </summary>
    /// Created by: Nguyễn Thanh Lâm 10/8/2023
    public interface IFixedAssetRepository : ICrudRepository<FixedAsset>
    {

        /// <summary>
        /// The create fixed asset.
        /// </summary>
        /// <param name="fixedAsset">The fixed asset.</param>
        /// <returns>The result.</returns>
        /// Created by: Nguyễn Thanh Lâm 10/8/2023
        public Task<int> CreateFixedAssetAsync(FixedAsset fixedAsset);

        /// <summary>
        /// The update fixed asset.
        /// </summary>
        /// <param name="assetId">The asset id.</param>
        /// <param name="fixedAsset">The fixed asset.</param>
        /// <returns>The result.</returns>
        /// Created by: Nguyễn Thanh Lâm 10/8/2023
        public Task<int> UpdateFixedAssetAsync(FixedAsset fixedAsset);

        /// <summary>
        /// The delete fixed asset.
        /// </summary>
        /// <param name="assetId">The asset id.</param>
        /// <returns>The result.</returns>
        /// Created by: Nguyễn Thanh Lâm 10/8/2023
        public Task<int> DeleteFixedAssetAsync(Guid assetId);

        /// <summary>
        /// The get fixed asset by id.
        /// </summary>
        /// <param name="assetId">The asset id.</param>
        /// <returns>The result.</returns>
        /// Created by: Nguyễn Thanh Lâm 10/8/2023
        public Task<FixedAsset> GetFixedAssetByIdAsync(Guid assetId);

        /// <summary>
        /// The get filter assets.
        /// </summary>
        /// <param name="pageSize">The page size.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <returns>The result.</returns>
        /// Created by: Nguyễn Thanh Lâm 10/8/2023
        public Task<FilterFixedAsset> GetFilterAssetsAsync(int pageSize, int pageNumber, string filter, List<Guid>? departmantId, List<Guid>? fixedAssetCategoryId);

        /// <summary>
        /// The get all filter asset async.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="departmantCode">The departmant code.</param>
        /// <param name="fixedAssetCategoryCode">The fixed asset category code.</param>
        /// <returns>The result.</returns>
        /// Created by: Nguyễn Thanh Lâm 10/8/2023
        public Task<IEnumerable<FixedAsset>> GetAllFilterAssetAsync(string filter, List<Guid>? departmantIds, List<Guid>? fixedAssetCategoryIds);

        /// <summary>
        /// The get all fixed assets.
        /// </summary>
        /// <returns>The result.</returns>
        /// Created by: Nguyễn Thanh Lâm 10/8/2023
        public Task<IEnumerable<FixedAsset>> GetAllFixedAssetsAsync();

        /// <summary>
        /// The delete many async.
        /// </summary>
        /// <param name="fixedAssetIds">The fixed asset ids.</param>
        /// <returns>The result.</returns>
        /// Created by: Nguyễn Thanh Lâm 10/8/2023
        public Task<int> DeleteManyAsync(List<Guid> fixedAssetIds);

        /// <summary>
        /// The find fixed asset by code async.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns>The result.</returns>
        /// Created by: Nguyễn Thanh Lâm 10/8/2023
        public Task<FixedAsset?> FindFixedAssetByCodeAsync(string code);

        /// <summary>
        /// The generate code.
        /// </summary>
        /// <returns>The result.</returns>
        /// Created by: Nguyễn Thanh Lâm 10/8/2023
        public Task<string> GenerateCode();
    }
}
