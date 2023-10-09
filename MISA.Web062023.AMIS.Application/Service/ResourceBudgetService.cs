using AutoMapper;
using MISA.Web062023.AMIS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web062023.AMIS.Application
{
    public class ResourceBudgetService : BaseCrudService<ResourceBudget, ResourceBudgetDto, ResourceBudgetCreateDto, ResourceBudgetUpdateDto>,
         IResourceBudgetService
    {
        public ResourceBudgetService(IResourceBudgetRepository crudRepository, IResourceBudgetManager baseManager, IMapper mapper) : base(crudRepository, baseManager, mapper)
        {
        }
    }
}
