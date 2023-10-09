using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web062023.AMIS.Application
{
    public class ResourceAssetUpdateDto
    {
        public int? ResourceAssetId { get; set; }

        public int Cost { get; set; }

        public Guid ResourceBudgetId { get; set; }
    }
}
