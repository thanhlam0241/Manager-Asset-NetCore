using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web062023.AMIS.Domain
{
    public class ResourceAsset : BaseAuditEntity
    {
        public int? ResourceAssetId { get; set; }

        public int? Cost { get; set; }
        public ResourceBudget? ResourceBudget { get; set; }

    }
}
