using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Web062023.AMIS.Domain;
using Newtonsoft.Json;

namespace MISA.Web062023.AMIS.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploaderController : ControllerBase
    {
        [HttpPost]
        public IActionResult UploadChunk(IFormFile file)
        {
            var chunkMetadata = Request.Form["chunkMetadata"];

            try
            {
                if (!string.IsNullOrEmpty(chunkMetadata))
                {
                    var metaDataObject = JsonConvert.DeserializeObject<ChunkMetaData>(chunkMetadata);

                    // Write code that appends the 'file' file chunk to the temporary file.
                    // You can use the $"{metaDataObject.FileGuid}.tmp" name for the temporary file.
                    // Don't rely on or trust the FileName property without validation.



                    if (metaDataObject.Index == metaDataObject.TotalCount - 1)
                    {
                        // Write code that saves the 'file' file.
                        // Don't rely on or trust the FileName property without validation.
                    }
                }
            }
            catch
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
