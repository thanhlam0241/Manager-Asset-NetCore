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
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        /// <summary>
        /// The .ctor.
        /// </summary>
        /// <param name="authService">The auth service.</param>
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// The login.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns>The result.</returns>
        /// 
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var user = await _authService.FindUserAsync(request.Credential);

            if (user.UserPassword != request.Password)
            {
                return Unauthorized(Domain.Resources.Authentication.Authentication.IncorrectPassword);
            }
            var claim = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
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
        /// The logout.
        /// </summary>
        /// <returns>The result.</returns>
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
        [HttpGet("check-login")]
        public IActionResult CheckLogin()
        {
            var user = User.Identity as ClaimsIdentity;
            if (User.Identity == null || user == null || !User.Identity.IsAuthenticated)
            {
                return Unauthorized();
            }
            return Ok(new
            {
                username = user.Name,
                role = 1
            });
        }
    }
}
