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
        public RecordedAssetController(IRecordedAssetService recordedAssetService)
        {
            _recordedAssetService = recordedAssetService;
        }

        [HttpPost]
        public Task<IActionResult> CreateAssetAsync([FromBody] RecordedAssetCreateDto assetCreateDto)
        {
            throw new NotImplementedException();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAssetAsync([FromRoute] Guid id, [FromBody] RecordedAssetUpdateDto assetUpdateDto)
        {
            var result = await _recordedAssetService.UpdateAssetAsync(id, assetUpdateDto);
            return StatusCode(StatusCodes.Status202Accepted, "Sửa tài sản thành công");
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAssetByIdAsync([FromRoute] Guid id)
        {
            var result = await _recordedAssetService.GetAssetByIdAsync(id);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAssetAsync([FromRoute] Guid id)
        {
            var result = await _recordedAssetService.DeleteAssetAsync(id);
            return Ok("Xóa thành công 1 tài sản chứng từ");
        }
    }
}
