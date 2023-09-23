using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web062023.AMIS.Domain
{
    public class Recording : BaseAuditEntity, IEntity
    {
        public Guid RecordingId { get; set; }
        public string RecordingCode { get; set; }
        public DateTimeOffset RecordingDate { get; set; }
        public DateTimeOffset ActionDate { get; set; }
        public string Description { get; set; }
        public RecordingType RecordingType { get; set; }

        public Guid GetId()
        {
            return RecordingId;
        }

        public void SetId(Guid id)
        {
            RecordingId = id;
        }
    }
}
