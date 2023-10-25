using Microsoft.AspNetCore.Mvc;
using MISA.Web062023.AMIS.Application;

namespace MISA.Web062023.AMIS.API.Controllers
{
    /// <summary>
    /// The account controller.
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RecordedAssetController : Controller
    {
        private readonly IRecordedAssetService _recordedAssetService;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="recordedAssetService"></param>
        /// Created by: NTLam (20/08/2023)
        public RecordedAssetController(IRecordedAssetService recordedAssetService)
        {
            _recordedAssetService = recordedAssetService;
        }

        /// <summary>
        /// The create asset async.
        /// </summary>
        /// <param name="assetCreateDto">The asset create dto.</param>
        /// <returns>The result.</returns>
        /// Created by: NTLam (20/08/2023)
        [HttpPost]
        public Task<IActionResult> CreateAssetAsync([FromBody] RecordedAssetCreateDto assetCreateDto)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The update asset async.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="assetUpdateDto">The asset update dto.</param>
        /// <returns>The result.</returns>
        /// Created by: NTLam (20/08/2023)
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAssetAsync([FromRoute] Guid id, [FromBody] RecordedAssetUpdateDto assetUpdateDto)
        {
            var result = await _recordedAssetService.UpdateAssetAsync(id, assetUpdateDto);
            return StatusCode(StatusCodes.Status202Accepted, "Sửa tài sản thành công");
        }

        /// <summary>
        /// The get asset by id async.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The result.</returns>
        /// Created by: NTLam (20/08/2023)
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAssetByIdAsync([FromRoute] Guid id)
        {
            var result = await _recordedAssetService.GetAssetByIdAsync(id);
            return Ok(result);
        }

        /// <summary>
        /// The delete asset async.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The result.</returns>
        /// Created by: NTLam (20/08/2023)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAssetAsync([FromRoute] Guid id)
        {
            var result = await _recordedAssetService.DeleteAssetAsync(id);
            return StatusCode(StatusCodes.Status202Accepted, "Xóa thành công 1 tài sản chứng từ");
        }
    }
}
