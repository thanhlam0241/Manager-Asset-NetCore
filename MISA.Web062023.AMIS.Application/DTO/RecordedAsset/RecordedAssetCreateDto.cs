using MISA.Web062023.AMIS.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web062023.AMIS.Application
{
    public class RecordedAssetCreateDto : IBaseDto
    {
        [Required]
        public string RecordedAssetCode { get; set; }

        public RecordingType? RecordingType { get; set; }

        public List<ResourceAssetCreateDto> ResourceAssets { get; set; } = new List<ResourceAssetCreateDto>();
        public string GetCode()
        {
            return RecordedAssetCode;
        }

        public void SetCode(string code)
        {
            RecordedAssetCode = code;
        }
    }
}
