using MISA.Web062023.AMIS.Application.DTO.FixedAsset;
using MISA.Web062023.AMIS.Domain;

namespace MISA.Web062023.AMIS.Application
{

    /// <summary>
    /// The interface fixed asset repository.
    /// </summary>
    /// Created by: Nguyễn Thanh Lâm (10/8/2023)
    public interface IFixedAssetService : ICrudService<FixedAssetDto, FixedAssetCreateDto, FixedAssetUpdateDto>
    {

        /// <summary>
        /// The create fixed asset.
        /// </summary>
        /// <param name="fixedAsset">The fixed asset.</param>
        /// <returns>The result.</returns>
        /// Created by: Nguyễn Thanh Lâm (10/8/2023)
        public Task<int> CreateFixedAssetAsync(FixedAssetCreateDto fixedAsset);

        /// <summary>
        /// The generate new code.
        /// </summary>
        /// <returns>The result.</returns>
        /// Created by: Nguyễn Thanh Lâm (10/8/2023)
        public Task<string> GenerateRandomCode();

        /// <summary>
        /// The generate code.
        /// </summary>
        /// <returns>The result.</returns>
        /// Created by: Nguyễn Thanh Lâm (10/8/2023)
        public Task<string> GenerateCode();

        /// <summary>
        /// The update fixed asset.
        /// </summary>
        /// <param name="assetId">The asset id.</param>
        /// <param name="fixedAsset">The fixed asset.</param>
        /// <returns>The result.</returns>
        /// Created by: Nguyễn Thanh Lâm (10/8/2023)
        public Task<int> UpdateFixedAssetAsync(Guid assetId, FixedAssetUpdateDto fixedAsset);

        /// <summary>
        /// The delete fixed asset.
        /// </summary>
        /// <param name="assetId">The asset id.</param>
        /// <returns>The result.</returns>
        /// Created by: Nguyễn Thanh Lâm (10/8/2023)
        public Task<int> DeleteFixedAssetAsync(Guid assetId);

        /// <summary>
        /// The get fixed asset by id.
        /// </summary>
        /// <param name="assetId">The asset id.</param>
        /// <returns>The result.</returns>
        /// Created by: Nguyễn Thanh Lâm (10/8/2023)
        public Task<FixedAssetDto> GetFixedAssetByIdAsync(Guid assetId);

        /// <summary>
        /// The get filter assets.
        /// </summary>
        /// <param name="pageSize">The page size.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <returns>The result.</returns>
        /// Created by: Nguyễn Thanh Lâm (10/8/2023)
        public Task<FilterFixedAsset> GetFilterAssetsAsync(int pageSize, int pageNumber, string filterString, List<Guid>? departmentId, List<Guid>? fixedAssetCategoryId);

        /// <summary>
        /// The get filter except code async.
        /// </summary>
        /// <param name="pageSize">The page size.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="filterString">The filter string.</param>
        /// <param name="codes">The codes.</param>
        /// <returns>The result.</returns>
        /// Created by: Nguyễn Thanh Lâm (10/8/2023)
        public Task<FilterFixedAsset> GetFilterExceptCodeAsync(int pageSize, int pageNumber, string filterString, List<string>? codes);

        /// <summary>
        /// The get file export.
        /// </summary>
        /// <param name="lists">The lists.</param>
        /// <returns>The result.</returns>
        /// Created by: Nguyễn Thanh Lâm (10/8/2023)
        public MemoryStream GetFileExport(List<FixedAssetViewDto> lists);

        /// <summary>
        /// The get all fixed assets.
        /// </summary>
        /// <returns>The result.</returns>
        /// Created by: Nguyễn Thanh Lâm (10/8/2023)
        public Task<List<FixedAsset>> GetAllFixedAssetsAsync();

        /// <summary>
        /// The get all fixed assets.
        /// </summary>
        /// <returns>The result.</returns>
        /// Created by: Nguyễn Thanh Lâm (10/8/2023)
        public Task<List<FixedAssetViewDto>> GetAllFilterFixedAssetsAsync(string filterString, List<Guid>? departmentIds, List<Guid>? fixedAssetCategoryIds);

        /// <summary>
        /// The delete list assets async.
        /// </summary>
        /// <param name="assetIds">The asset ids.</param>
        /// <returns>The result.</returns>
        /// Created by: Nguyễn Thanh Lâm (10/8/2023)
        public Task<int> DeleteListAssetsAsync(List<Guid> assetIds);

        /// <summary>
        /// The get all assets view.
        /// </summary>
        /// <returns>The result.</returns>
        /// Created by: Nguyễn Thanh Lâm (10/8/2023)
        public Task<List<FixedAssetViewDto>> GetAllAssetsView();

        /// <summary>
        /// The create multi fixed asset async.
        /// </summary>
        /// <param name="lists">The lists.</param>
        /// <returns>The result.</returns>
        /// Created by: Nguyễn Thanh Lâm (10/8/2023)
        public Task<int> InsertMultiFixedAssetAsync(List<FixedAssetCreateDto> lists);
    }
}
