using Microsoft.AspNetCore.Mvc;
using MISA.Web062023.AMIS.Application;

namespace MISA.Web062023.AMIS.API.Controllers
{

    /// <summary>
    /// The recording asset controller.
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RecordingAssetController : Controller
    {
        private readonly IRecordingAssetService _recordingAssetService;

        /// <summary>
        /// The .ctor.
        /// </summary>
        /// <param name="recordingAssetService">The recording asset service.</param>
        /// Created by: NTLam (15/09/2023)
        public RecordingAssetController(IRecordingAssetService recordingAssetService)
        {
            _recordingAssetService = recordingAssetService;
        }

        /// <summary>
        /// The get asset of recording async.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The result.</returns>
        /// Created by: NTLam (15/09/2023)
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRecordingAsync([FromRoute] Guid id)
        {
            var result = await _recordingAssetService.GetRecordingAsync(id);
            return StatusCode(StatusCodes.Status200OK, result);
        }

        /// <summary>
        /// The update recording.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="recordingUpdateDto">The recording update dto.</param>
        /// <returns>The result.</returns>
        /// Created by: NTLam (15/09/2023)
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRecording([FromRoute] Guid id, [FromBody] RecordingUpdateDto recordingUpdateDto)
        {
            var result = await _recordingAssetService.UpdateRecordingAsync(id, recordingUpdateDto);
            if (result)
            {
                return StatusCode(StatusCodes.Status202Accepted, "Sửa thành công chứng từ");
            }
            return StatusCode(StatusCodes.Status501NotImplemented, "Sửa thất bại chứng từ");
        }
        /// <summary>
        /// The create recording.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>The result.</returns>
        /// Created by: NTLam (15/09/2023)
        [HttpPost]
        public async Task<IActionResult> CreateRecording([FromBody] CreateRecordingRequest request)
        {
            var lists = request.Assets;
            var recording = request.Recording;
            if (lists.Count == 0)
            {
                return BadRequest(Domain.Resources.RecordingAsset.RecordingAsset.NoAssetSelect);
            }
            var result = await _recordingAssetService.CreateNewRecording(recording, lists);
            if (result == 0)
            {
                return BadRequest(Domain.Resources.RecordingAsset.RecordingAsset.CreateNewRecordingFail);
            }
            return StatusCode(StatusCodes.Status201Created, Domain.Resources.RecordingAsset.RecordingAsset.CreateNewRecording);
        }
    }
}
