using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web062023.AMIS.Application
{
    public class CreateRecordingRequest
    {
        public List<RecordedAssetCreateDto> Assets { get; set; }

        public RecordingCreateDto Recording { get; set; }
    }
}
