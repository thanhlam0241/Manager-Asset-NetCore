using MISA.Web062023.AMIS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web062023.AMIS.Application
{
    public class RecordingDto : IBaseDto
    {
        public Guid RecordingId { get; set; }
        public string RecordingCode { get; set; }
        public DateTimeOffset RecordingDate { get; set; }
        public DateTimeOffset ActionDate { get; set; }
        public int Value { get; set; } = 0;
        public string Description { get; set; }
        public RecordingType RecordingType { get; set; }

        public List<RecordedAssetDto> Assets { get; set; }

        public string GetCode()
        {
            return RecordingCode;
        }

        public void SetCode(string code)
        {
            RecordingCode = code;
        }
    }
}
