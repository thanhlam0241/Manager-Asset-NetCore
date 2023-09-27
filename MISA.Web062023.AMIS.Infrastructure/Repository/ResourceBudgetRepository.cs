using MISA.Web062023.AMIS.Application;
using MISA.Web062023.AMIS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web062023.AMIS.Infrastructure
{
    public class ResourceBudgetRepository : BaseCrudRepository<ResourceBudget>, IResourceBudgetRepository
    {
        public ResourceBudgetRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}
