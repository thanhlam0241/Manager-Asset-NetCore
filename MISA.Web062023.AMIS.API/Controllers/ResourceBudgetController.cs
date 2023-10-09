using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MISA.Web062023.AMIS.Application;

namespace MISA.Web062023.AMIS.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ResourceBudgetController : BaseCrudController<ResourceBudgetDto, ResourceBudgetCreateDto, ResourceBudgetUpdateDto>
    {
        private readonly IResourceBudgetService _resourceBudgetService;
        public ResourceBudgetController(IResourceBudgetService resourceBudgetService) : base(resourceBudgetService)
        {
            _resourceBudgetService = resourceBudgetService;
        }
    }
}
