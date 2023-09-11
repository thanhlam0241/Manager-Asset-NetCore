using System.Collections;

namespace MISA.Web062023.AMIS.Domain
{

    /// <summary>
    /// The interface fixed asset category repository.
    /// </summary>
    /// Created by: Nguyễn Thanh Lâm (10/8/2023)
    public interface IFixedAssetCategoryRepository : ICrudRepository<FixedAssetCategory>
    {

        /// <summary>
        /// The get categories.
        /// </summary>
        /// <returns>The result.</returns>
        /// Created by: Nguyễn Thanh Lâm (10/8/2023)
        public Task<IEnumerable<FixedAssetCategory>> GetCategoriesAsync();

        /// <summary>
        /// The delete category.
        /// </summary>
        /// <param name="categoryId">The category id.</param>
        /// <returns>The result.</returns>
        /// Created by: Nguyễn Thanh Lâm (10/8/2023)
        public Task<int> DeleteCategoryAsync(Guid categoryId);

        /// <summary>
        /// The find category by code async.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns>The result.</returns>
        /// Created by: Nguyễn Thanh Lâm (10/8/2023)
        public Task<FixedAssetCategory?> FindCategoryByCodeAsync(string code);

        /// <summary>
        /// The create category.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <returns>The result.</returns>
        /// Created by: Nguyễn Thanh Lâm (10/8/2023)
        public Task<int> CreateCategoryAsync(FixedAssetCategory category);

        /// <summary>
        /// The update category.
        /// </summary>
        /// <param name="categoryId">The category id.</param>
        /// <param name="category">The category.</param>
        /// <returns>The result.</returns>
        /// Created by: Nguyễn Thanh Lâm (10/8/2023)
        public Task<int> UpdateCategoryAsync(FixedAssetCategory category);

        /// <summary>
        /// The get category by id.
        /// </summary>
        /// <param name="categoryId">The category id.</param>
        /// <returns>The result.</returns>
        /// Created by: Nguyễn Thanh Lâm (10/8/2023)
        public Task<FixedAssetCategory> GetCategoryByIdAsync(Guid categoryId);
    }
}
