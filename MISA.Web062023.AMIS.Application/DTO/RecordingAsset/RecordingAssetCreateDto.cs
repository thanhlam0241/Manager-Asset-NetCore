using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web062023.AMIS.Application
{
    public class RecordingAssetCreateDto
    {
        public RecordingCreateDto RecordingCreateDto { get; set; } = new RecordingCreateDto();

        public List<ResourceAssetCreateDto> ResourceAssetCreateDtos { get; set; } = new List<ResourceAssetCreateDto>();
    }
}
