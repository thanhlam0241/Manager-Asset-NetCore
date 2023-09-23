using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MISA.Web062023.AMIS.Application;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MISA.Web062023.AMIS.API.Controllers
{

    /// <summary>
    /// The department controller.
    /// </summary>
    /// Created By: NTLam (16/08/2023)
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = CookieAuthenticationDefaults.AuthenticationScheme)]
    public class DepartmentsController : BaseCrudController<DepartmentDto, DepartmentCreateDto, DepartmentUpdateDto>
    {
        private readonly IDepartmentService _departmentService;

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="_departmentService">The department service.</param>
        /// Created By: NTLam (16/08/2023)
        public DepartmentsController(IDepartmentService departmentService) : base(departmentService)
        {
            _departmentService = departmentService;
        }
    }
}
