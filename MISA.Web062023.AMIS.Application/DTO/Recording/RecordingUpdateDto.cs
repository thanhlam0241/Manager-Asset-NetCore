using MISA.Web062023.AMIS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web062023.AMIS.Application
{
    public class RecordingUpdateDto : IBaseDto
    {
        public string RecordingCode { get; set; }
        public DateTimeOffset? RecordingDate { get; set; }
        public DateTimeOffset? ActionDate { get; set; }
        public string? Description { get; set; }
        public int? Value { get; set; }
        public RecordingType? RecordingType { get; set; }

        public List<RecordedAsset> Assets { get; set; } = new List<RecordedAsset>();

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
