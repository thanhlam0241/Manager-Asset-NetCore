using AutoMapper;
using MISA.Web062023.AMIS.Domain;
using System.Data;

namespace MISA.Web062023.AMIS.Application
{

    /// <summary>
    /// The fixed asset category repository.
    /// </summary>
    /// Created By: NTLam (14/8/2023)
    public class FixedAssetCategoryService : BaseCrudService<FixedAssetCategory, FixedAssetCategoryDto, FixedAssetCategoryCreateDto, FixedAssetCategoryUpdateDto>, IFixedAssetCategoryService
    {
        private readonly IFixedAssetCategoryRepository _fixedAssetCategoryRepository;
        private readonly IFixedAssetCategoryManager _fixedAssetCategoryManager;

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="CrudRepository">The crud repository.</param>
        /// <param name="BaseManager">The base manager.</param>
        /// <param name="readonlyRepository">The readonly repository.</param>
        /// <param name="fixedAssetCategory">The fixed asset category.</param>
        /// <param name="mapper">The mapper.</param>
        /// Created By: NTLam (14/8/2023)
        public FixedAssetCategoryService(IFixedAssetCategoryRepository fixedAssetCategory, IFixedAssetCategoryManager fixedAssetCategoryManager,
            IMapper mapper) : base(fixedAssetCategory, fixedAssetCategoryManager, mapper)
        {
            _fixedAssetCategoryRepository = fixedAssetCategory;
            _fixedAssetCategoryManager = fixedAssetCategoryManager;
        }

        /// <summary>
        /// The create category.
        /// </summary>
        /// <param name="category">The category.</param>
        /// <returns>The result.</returns>
        /// Author: Nguyễn Thanh Lâm
        /// Modified date: 10/8/2023
        public async Task<int> CreateCategoryAsync(FixedAssetCategoryCreateDto category)
        {
            await _fixedAssetCategoryManager.CheckDuplicateCodeAsync(category.FixedAssetCategoryCode);
            var result = await _fixedAssetCategoryRepository.CreateCategoryAsync(EntityCreateDtoToEntity(category));
            return result;
        }

        /// <summary>
        /// The delete category.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The result.</returns>
        /// Author: Nguyễn Thanh Lâm
        /// Modified date: 10/8/2023
        public async Task<int> DeleteCategoryAsync(Guid id)
        {
            await _fixedAssetCategoryManager.CheckIsExistIdAsync(id);
            int result = await _fixedAssetCategoryRepository.DeleteCategoryAsync(id);
            return result;
        }

        /// <summary>
        /// The get categories.
        /// Author: Nguyễn Thanh Lâm
        /// Modified date: 10/8/2023
        /// </summary>
        /// <returns>The result.</returns>
        public async Task<List<FixedAssetCategoryDto>> GetCategoriesAsync()
        {
            var categories = await _fixedAssetCategoryRepository.GetCategoriesAsync();
            return categories.Select(EntityToEntityDto).ToList();
        }

        /// <summary>
        /// The get category by id.
        /// </summary>
        /// <param name="categoryId">The category id.</param>
        /// <returns>The result.</returns>
        /// Author: Nguyễn Thanh Lâm
        /// Modified date: 10/8/2023
        public async Task<FixedAssetCategoryDto> GetCategoryByIdAsync(Guid categoryId)
        {
            var category = await _fixedAssetCategoryRepository.GetCategoryByIdAsync(categoryId);
            return EntityToEntityDto(category);
        }

        /// <summary>
        /// The update category.
        /// </summary>
        /// <param name="categoryId">The category id.</param>
        /// <param name="category">The category.</param>
        /// <returns>The result.</returns>
        /// Author: Nguyễn Thanh Lâm
        /// Modified date: 10/8/2023
        public async Task<int> UpdateCategoryAsync(Guid categoryId, FixedAssetCategoryUpdateDto category)
        {
            await _fixedAssetCategoryManager.CheckIsExistIdAsync(categoryId);
            int result = await _fixedAssetCategoryRepository.UpdateCategoryAsync(
                EntityUpdateDtoToEntity(categoryId, category));
            return result;
        }
    }
}
