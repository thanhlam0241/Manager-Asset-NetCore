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
        public RecordingAssetController(IRecordingAssetService recordingAssetService)
        {
            _recordingAssetService = recordingAssetService;
        }

        /// <summary>
        /// The get asset of recording async.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The result.</returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRecordingAsync([FromRoute] Guid id)
        {
            var result = await _recordingAssetService.GetRecordingAsync(id);
            return StatusCode(StatusCodes.Status200OK, result);
        }

        /// <summary>
        /// The create recording.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>The result.</returns>
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
