using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web062023.AMIS.Domain
{
    public class RecordedAsset : BaseAuditEntity, IEntity
    {
        [Key]
        public Guid RecordedAssetId { get; set; }
        public string? RecordedAssetCode { get; set; }
        public string? RecordedAssetName { get; set; }

        public Department? Department { get; set; }

        public int? Value { get; set; } = 0;

        public float? DepreciationRate { get; set; } = 0;

        public RecordingType? RecordingType { get; set; }

        public List<ResourceAsset> ResourceAssets { get; set; } = new List<ResourceAsset>();

        public Guid GetId()
        {
            return RecordedAssetId;
        }

        public void SetId(Guid id)
        {
            RecordedAssetId = id;
        }
    }
}
