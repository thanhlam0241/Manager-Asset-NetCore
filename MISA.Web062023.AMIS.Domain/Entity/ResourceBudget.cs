using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web062023.AMIS.Domain
{
    public class ResourceBudget : BaseAuditEntity, IEntity
    {
        [Required]
        public Guid ResourceBudgetId { get; set; }

        [Required]
        public string ResourceBudgetCode { get; set; }


        [MaxLength(100)]
        public string ResourceBudgetName { get; set; }

        public Guid GetId()
        {
            return ResourceBudgetId;
        }

        public void SetId(Guid id)
        {
            ResourceBudgetId = id;
        }
    }
}
