using AutoMapper;
using MISA.Web062023.AMIS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web062023.AMIS.Application
{
    public class ResourceBudgetProfile : Profile
    {
        public ResourceBudgetProfile()
        {
            CreateMap<ResourceBudget, ResourceBudgetUpdateDto>();
            CreateMap<ResourceBudget, ResourceBudgetDto>();
            CreateMap<ResourceBudgetCreateDto, ResourceBudget>();
        }
    }
}
