using Microsoft.AspNetCore.Mvc;
using MISA.Web062023.AMIS.Application;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MISA.Web062023.AMIS.API.Controllers
{

    /// <summary>
    /// The fixed asset category controller.
    /// </summary>
    /// Created By: NTLam (16/08/2023)
    [Route("api/v1/[controller]")]
    [ApiController]
    public class FixedAssetCategoriesController : BaseCrudController<FixedAssetCategoryDto, FixedAssetCategoryCreateDto, FixedAssetCategoryUpdateDto>
    {
        private readonly IFixedAssetCategoryService _fixedAssetCategoryService;

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="repository">The repository.</param>
        /// Created By: NTLam (16/08/2023)
        public FixedAssetCategoriesController(IFixedAssetCategoryService fixedAssetCategoryService) : base(fixedAssetCategoryService)
        {
            _fixedAssetCategoryService = fixedAssetCategoryService;
        }

    }
}
