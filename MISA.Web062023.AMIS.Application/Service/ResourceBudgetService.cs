using AutoMapper;
using MISA.Web062023.AMIS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web062023.AMIS.Application
{
    /// <summary>
    /// Service xử lý nghiệp vụ với bảng ResourceBudget
    /// </summary>
    /// Created by: NTLam (20/08/2023)
    public class ResourceBudgetService : BaseCrudService<ResourceBudget, ResourceBudgetDto, ResourceBudgetCreateDto, ResourceBudgetUpdateDto>,
         IResourceBudgetService
    {
        /// <summary>
        /// CONSTRUCTOR
        /// </summary>
        /// <param name="crudRepository"></param>
        /// <param name="baseManager"></param>
        /// <param name="mapper"></param>
        /// Created by: NTLam (20/08/2023)
        public ResourceBudgetService(IResourceBudgetRepository crudRepository, IResourceBudgetManager baseManager, IMapper mapper) : base(crudRepository, baseManager, mapper)
        {
        }
    }
}
