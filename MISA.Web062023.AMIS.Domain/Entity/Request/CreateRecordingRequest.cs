using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web062023.AMIS.Domain
{
    public class CreateRecordingRequest
    {
        public List<Guid> assetIds { get; set; }

        public Recording Recording { get; set; }
    }
}
