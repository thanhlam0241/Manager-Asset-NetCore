using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MISA.Web062023.AMIS.Application;
using MISA.Web062023.AMIS.Domain;
using System.Net;
using System.Security.Claims;

namespace MISA.Web062023.AMIS.API.Controllers
{

    /// <summary>
    /// The auth controller.
    /// </summary>
    /// Created by: NTLam (20/08/2023)
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        private readonly IAccountService _accountService;

        /// <summary>
        /// The .ctor.
        /// </summary>
        /// <param name="accountService">The auth service.</param>
        /// Created by: NTLam (20/08/2023)
        public AuthenticationController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        /// <summary>
        /// Đăng nhập
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>The result.</returns>
        /// Created by: NTLam (20/08/2023)
        /// 
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _accountService.FindAccountByCredentialAsync(request.Credential);

            if (!AccountUtil.VerifyPassword(request.Password, user.Password))
            {
                throw new NotFoundException(Domain.Resources.Authentication.Authentication.IncorrectPassword);
            }
            var claim = new List<Claim>
            {
                new Claim(ClaimTypes.Name, value: request.Credential),
                new Claim("Id",user.AccountId.ToString()),
                new Claim(ClaimTypes.Role, "1")
            };
            var claimsIdentity = new ClaimsIdentity(claim, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30),
                IsPersistent = true
            };

            await HttpContext.SignInAsync(
                               CookieAuthenticationDefaults.AuthenticationScheme,
                                              new ClaimsPrincipal(claimsIdentity),
                                                             authProperties);
            return Ok(Domain.Resources.Authentication.Authentication.LoginSuccess);
        }

        /// <summary>
        /// Đăng ký tài khoản mới
        /// </summary>
        /// <param name="accountCreate">The account create.</param>
        /// <returns>The result.</returns>
        /// Created by: NTLam (20/08/2023)
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] AccountCreateDto accountCreate)
        {
            var result = await _accountService.InsertAccountAsync(accountCreate);
            if (result == 0)
            {
                return BadRequest(Domain.Resources.Exception.Exception.Server);
            }
            return StatusCode(StatusCodes.Status201Created, Domain.Resources.Account.Account.RegisterSuccess);
        }

        /// <summary>
        /// The logout.
        /// </summary>
        /// <returns>The result.</returns>
        /// Created by: NTLam (20/08/2023)
        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
                               CookieAuthenticationDefaults.AuthenticationScheme);
            return Ok(Domain.Resources.Authentication.Authentication.LogoutSuccess);
        }

        /// <summary>
        /// The check login.
        /// </summary>
        /// <returns>The result.</returns>
        /// Created by: NTLam (20/08/2023)
        [HttpGet("check-login")]
        public IActionResult CheckLogin()
        {
            var user = User.Identity as ClaimsIdentity;
            if (User.Identity == null || user == null || !User.Identity.IsAuthenticated)
            {
                return StatusCode(StatusCodes.Status406NotAcceptable);
            }
            return Ok(new
            {
                username = user.Name,
                role = 1
            });
        }
    }
}
