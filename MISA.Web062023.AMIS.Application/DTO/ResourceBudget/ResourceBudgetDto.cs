using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web062023.AMIS.Application
{
    public class ResourceBudgetDto : IBaseDto
    {
        [Required]
        public Guid ResourceBudgetId { get; set; }

        [Required]
        public string ResourceBudgetCode { get; set; }


        [MaxLength(255)]
        public string ResourceBudgetName { get; set; }

        public string GetCode()
        {
            return ResourceBudgetCode;
        }

        public void SetCode(string code)
        {
            ResourceBudgetCode = code;
        }
    }
}
