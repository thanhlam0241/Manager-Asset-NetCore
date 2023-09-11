using System.Collections;

namespace MISA.Web062023.AMIS.Application
{

    /// <summary>
    /// The interface fixed asset category repository.
    /// </summary>
    /// Author: Nguyễn Thanh Lâm
    /// Modified date: 10/8/2023
    public interface IFixedAssetCategoryService : ICrudService<FixedAssetCategoryDto, FixedAssetCategoryCreateDto, FixedAssetCategoryUpdateDto>
    {

        /// <summary>
        /// The get categories.
        /// </summary>
        /// <returns>The result.</returns>
        /// Author: Nguyễn Thanh Lâm
        /// Modified date: 10/8/2023
        public Task<List<FixedAssetCategoryDto>> GetCategoriesAsync();

        /// <summary>
        /// The delete category.
        /// </summary>
        /// <param name="categoryId">The category id.</param>
        /// <returns>The result.</returns>
        /// Author: Nguyễn Thanh Lâm
        /// Modified date: 10/8/2023
        public Task<int> DeleteCategoryAsync(Guid categoryId);

        /// <summary>
        /// The create category.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <returns>The result.</returns>
        /// Author: Nguyễn Thanh Lâm
        /// Modified date: 10/8/2023
        public Task<int> CreateCategoryAsync(FixedAssetCategoryCreateDto category);

        /// <summary>
        /// The update category.
        /// </summary>
        /// <param name="categoryId">The category id.</param>
        /// <param name="category">The category.</param>
        /// <returns>The result.</returns>
        /// Author: Nguyễn Thanh Lâm
        /// Modified date: 10/8/2023
        public Task<int> UpdateCategoryAsync(Guid categoryId, FixedAssetCategoryUpdateDto category);

        /// <summary>
        /// The get category by id.
        /// </summary>
        /// <param name="categoryId">The category id.</param>
        /// <returns>The result.</returns>
        /// Author: Nguyễn Thanh Lâm
        /// Modified date: 10/8/2023
        public Task<FixedAssetCategoryDto> GetCategoryByIdAsync(Guid categoryId);
    }
}
