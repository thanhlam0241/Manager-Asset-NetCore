using GrapeCity.Documents.Common;
using GrapeCity.Documents.DX;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using MISA.Web062023.AMIS.Application;
using MISA.Web062023.AMIS.Domain;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System.Data;
using System.Drawing;
using System.Net;
using System.Text;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MISA.Web062023.AMIS.API.Controllers
{

    /// <summary>
    /// The fixed asset controller.
    /// </summary>
    /// Created by: NTLam (10/8/2023)
    [Route("api/v1/[controller]")]
    [ApiController]
    public class FixedAssetsController : BaseCrudController<FixedAssetDto, FixedAssetCreateDto, FixedAssetUpdateDto>
    {
        private readonly IFixedAssetService _fixedAssetService;
        private const string ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="fixedAssetRepository">The fixed asset repository.</param>
        /// Created by: NTLam (10/8/2023)
        public FixedAssetsController(IFixedAssetService fixedAssetService) : base(fixedAssetService)
        {
            _fixedAssetService = fixedAssetService;
        }

        /// <summary>
        /// The get filter assets.
        /// </summary>
        /// <param name="pageSize">The page size.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <returns>The result.</returns>
        /// Created by: NTLam (10/8/2023)
        [HttpPost("filter")]
        public async Task<IActionResult> GetFilterAssets([FromBody] FilterFixedAssetRequest requestFilter
            , [FromQuery] int pageSize = 20, [FromQuery] int pageNumber = 1, [FromQuery] string filterString = "")
        {
            if (pageSize <= 0)
            {
                return BadRequest(Domain.Resources.FixedAsset.FixedAsset.PageSizeLessThanZero);
            }
            if (pageNumber <= 0)
            {
                return BadRequest(Domain.Resources.FixedAsset.FixedAsset.PageNumberLessThanZero);
            }
            var filterResponse = await _fixedAssetService.GetFilterAssetsAsync(pageSize, pageNumber, filterString, requestFilter.DepartmentIds, requestFilter.FixedAssetCategoryIds);

            return Ok(filterResponse);
        }

        /// <summary>
        /// The get filter assets.
        /// </summary>
        /// <param name="pageSize">The page size.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <returns>The result.</returns>
        /// Created by: NTLam (10/8/2023)
        [HttpPost("filter-except")]
        public async Task<IActionResult> GetFilterExceptAssets([FromBody] FilterFixedAssetRequest requestFilter
            , [FromQuery] int pageSize = 20, [FromQuery] int pageNumber = 1, [FromQuery] string filterString = "")
        {
            if (pageSize <= 0)
            {
                return BadRequest(Domain.Resources.FixedAsset.FixedAsset.PageSizeLessThanZero);
            }
            if (pageNumber <= 0)
            {
                return BadRequest(Domain.Resources.FixedAsset.FixedAsset.PageNumberLessThanZero);
            }
            var filterResponse = await _fixedAssetService.GetFilterExceptCodeAsync(pageSize, pageNumber, filterString, requestFilter.CodeExcepts);

            return Ok(filterResponse);
        }


        /// <summary>
        /// The get new code.
        /// </summary>
        /// <returns>The result.</returns>
        /// Created by: NTLam (10/8/2023)
        [HttpGet("new-code")]
        public async Task<IActionResult> GetCode()
        {
            var newCode = await _fixedAssetService.GenerateCode();
            return Ok(newCode);
        }

        /// <summary>
        /// The get random code.
        /// </summary>
        /// <returns>The result.</returns>
        /// Created by: NTLam (10/8/2023)
        [HttpGet("random-code")]
        public async Task<IActionResult> GetRandomCode()
        {
            var newCode = await _fixedAssetService.GenerateCode();
            return Ok(newCode);
        }

        /// <summary>
        /// The get all filter async.
        /// </summary>
        /// <param name="filterString">The filter string.</param>
        /// <param name="departmentId">The department id.</param>
        /// <param name="fixedAssetCategoryId">The fixed asset category id.</param>
        /// <returns>The result.</returns>
        /// Created by: NTLam (10/8/2023)
        [HttpPost("export")]
        public async Task<IActionResult> Export([FromBody] FilterFixedAssetRequest filterRequest, [FromQuery] string filterString = "")
        {
            var listAssets = await _fixedAssetService.GetAllFilterFixedAssetsAsync(filterString, filterRequest.DepartmentIds, filterRequest.FixedAssetCategoryIds);
            if (listAssets.Count == 0)
            {
                return NotFound(Domain.Resources.FixedAsset.FixedAsset.NoAsset);
            }

            var stream = new MemoryStream();

            using var package = new ExcelPackage(stream);
            var sheet = package.Workbook.Worksheets.Add("Assets");

            var tableRange = sheet.Cells.LoadFromCollection(listAssets, true);

            sheet.Cells["A1:I1"].AutoFilter = true;
            sheet.Cells.AutoFitColumns();
            sheet.Cells.Style.Font.Size = 11;
            sheet.Cells.Style.Font.Name = "Times New Roman";
            package.Save();

            stream.Position = 0;
            string excelName = $"Assets-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";

            return File(stream, ContentType, excelName);
        }

        /// <summary>
        /// The insert multiple async.
        /// </summary>
        /// <param name="lists">The lists.</param>
        /// <returns>The result.</returns>
        /// Created by: NTLam (10/8/2023)
        [HttpPost("multiple")]
        public virtual async Task<IActionResult> InsertMultipleAsync([FromBody] ListFixedAssetCreateDto lists)
        {
            var listDtos = lists.FixedAssetCreateDtos;
            var result = await CrudService.InsertMultiAsync(listDtos);
            return StatusCode(StatusCodes.Status201Created, string.Format(Domain.Resources.FixedAsset.FixedAsset.InsertMany, result));
        }
    }
}

