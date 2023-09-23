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
        public RecordingsController(IRecordingService recordingService) : base(recordingService)
        {
            
        }
    }
}
