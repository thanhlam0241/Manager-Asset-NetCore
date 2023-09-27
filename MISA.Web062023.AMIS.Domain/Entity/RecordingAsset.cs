using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web062023.AMIS.Domain
{

    /// <summary>
    /// The recording asset.
    /// </summary>
    public class RecordingAsset : BaseAuditEntity
    {
        public List<RecordedAsset> Assets { get; set; }
        public Recording Recording { get; set; }

    }
}
