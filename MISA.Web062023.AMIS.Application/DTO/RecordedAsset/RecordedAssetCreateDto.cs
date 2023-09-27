using MISA.Web062023.AMIS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web062023.AMIS.Application
{
    public class RecordedAssetCreateDto
    {
        public string? RecordedAssetCode { get; set; }
        public string? RecordedAssetName { get; set; }

        public Guid DepartmentId { get; set; }

        public int? Value { get; set; } = 0;

        public float? DepreciationRate { get; set; } = 0;

        public RecordingType? RecordingType { get; set; }

        public List<ResourceAssetCreateDto> ResourceAssets { get; set; } = new List<ResourceAssetCreateDto>();
    }
}
