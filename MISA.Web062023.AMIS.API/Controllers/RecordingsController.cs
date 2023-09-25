using Microsoft.AspNetCore.Mvc;
using MISA.Web062023.AMIS.Application;

namespace MISA.Web062023.AMIS.API.Controllers
{
    /// <summary>
    /// The fixed asset controller.
    /// </summary>
    /// Created by: NTLam (10/8/2023)
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RecordingsController : BaseCrudController<RecordingDto, RecordingCreateDto, RecordingUpdateDto>
    {
        private readonly IRecordingService _recordingService;
        /// <summary>
        /// The .ctor.
        /// </summary>
        /// <param name="recordingService">The recording service.</param>
        public RecordingsController(IRecordingService recordingService) : base(recordingService)
        {
            _recordingService = recordingService;
        }

        /// <summary>
        /// The get filter data.
        /// </summary>
        /// <param name="pageSize">The page size.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="filterString">The filter string.</param>
        /// <returns>The result.</returns>
        [HttpGet("filter")]
        public async Task<IActionResult> GetFilterData([FromQuery] int pageSize = 20, [FromQuery] int pageNumber = 1, [FromQuery] string filterString = "")
        {
            var result = await _recordingService.GetFilterData(pageSize, pageNumber, filterString);
            return Ok(result);
        }

        /// <summary>
        /// The get new code.
        /// </summary>
        /// <returns>The result.</returns>
        [HttpGet("new-code")]
        public async Task<IActionResult> GetNewCode()
        {
            var newCode = await _recordingService.GetNewCode();
            return Ok(newCode);
        }
    }
}
