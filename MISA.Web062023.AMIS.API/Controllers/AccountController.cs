using Microsoft.AspNetCore.Mvc;
using MISA.Web062023.AMIS.Application;
using MISA.Web062023.AMIS.Domain;

namespace MISA.Web062023.AMIS.API.Controllers
{

    /// <summary>
    /// The account controller.
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        /// <summary>
        /// The .ctor.
        /// </summary>
        /// <param name="accountService">The account service.</param>
        /// Created by: NTLam (20/08/2023)
        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        /// <summary>
        /// The get account by id async.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The result.</returns>
        /// Created by: NTLam (20/08/2023)
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAccountByIdAsync([FromRoute] Guid id)
        {
            var result = await _accountService.GetAccountByIdAsync(id);
            return StatusCode(StatusCodes.Status200OK, result);
        }

        /// <summary>
        /// The get all accounts async.
        /// </summary>
        /// <returns>The result.</returns>
        /// Created by: NTLam (20/08/2023)
        [HttpGet]
        public async Task<IActionResult> GetAllAccountsAsync([FromQuery] string username = "", [FromQuery] string email = "", [FromQuery] string phoneNumber = "")
        {
            var result = await _accountService.GetAll(username, email, phoneNumber);
            return StatusCode(StatusCodes.Status200OK, result);
        }

        /// <summary>
        /// The get filter.
        /// </summary>
        /// <param name="pageSize">The page size.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="filterString">The filter string.</param>
        /// <returns>The result.</returns>
        /// Created by: NTLam (20/08/2023)
        [HttpGet("filter")]
        public async Task<IActionResult> GetFilter([FromQuery] int pageSize = 20, [FromQuery] int pageNumber = 1, [FromQuery] string filterString = "")
        {
            var result = await _accountService.GetFilter(pageSize, pageNumber, filterString);
            return StatusCode(StatusCodes.Status200OK, result);
        }

        /// <summary>
        /// The create account.
        /// </summary>
        /// <param name="account">The account.</param>
        /// <returns>The result.</returns>
        /// Created by: NTLam (20/08/2023)
        [HttpPost]
        public async Task<IActionResult> CreateAccountAsync([FromBody] AccountCreateDto account)
        {
            account.Active = true;
            var result = await _accountService.InsertAccountAsync(account);
            if (result == 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Domain.Resources.Exception.Exception.Server);
            }
            return StatusCode(StatusCodes.Status201Created, Domain.Resources.Account.Account.CreateAccountSuccess);
        }

        /// <summary>
        /// The update account.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="account">The account.</param>
        /// <returns>The result.</returns>
        /// Created by: NTLam (20/08/2023)
        [HttpPatch("{id}")]
        public async Task<IActionResult> UpdateAccountAsync([FromRoute] Guid id, [FromBody] AccountUpdateDto account)
        {
            var result = await _accountService.UpdateAccountAsync(id, account);
            if (result == 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Domain.Resources.Exception.Exception.Server);
            }
            return StatusCode(StatusCodes.Status202Accepted, Domain.Resources.Account.Account.UpdateAccountSuccess);
        }

        /// <summary>
        /// The change status account.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="active">The active.</param>
        /// <returns>The result.</returns>
        /// Created by: NTLam (20/08/2023)
        [HttpPatch("{id}/status")]
        public async Task<IActionResult> ChangeStatusAccountAsync([FromRoute] Guid id, [FromQuery] bool active)
        {
            dynamic result;
            if (active)
            {
                result = await _accountService.ActiveAccountAsync(id);
            }
            else
            {
                result = await _accountService.LockAccountAsync(id);
            }
            if (result == 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Domain.Resources.Exception.Exception.Server);
            }
            return StatusCode(StatusCodes.Status202Accepted,
                active ? Domain.Resources.Account.Account.UnlockAccountSuccess : Domain.Resources.Account.Account.LockAccountSuccess
                );
        }

        /// <summary>
        /// The change status account.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="active">The active.</param>
        /// <returns>The result.</returns>
        /// Created by: NTLam (20/08/2023)
        [HttpPatch("{id}/reset")]
        public async Task<IActionResult> ResetPasswordAsync([FromRoute] Guid id, [FromBody] string password)
        {
            var result = await _accountService.ResetPasswordAsync(id, password);
            if (result == 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Domain.Resources.Exception.Exception.Server);
            }
            return StatusCode(StatusCodes.Status202Accepted, Domain.Resources.Account.Account.ResetPasswordSuccess);
        }

        /// <summary>
        /// The change password.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="request">The request.</param>
        /// <returns>The result.</returns>
        /// Created by: NTLam (20/08/2023)
        [HttpPatch("{id}/password")]
        public async Task<IActionResult> ChangePasswordAsync([FromRoute] Guid id, [FromBody] ChangePasswordRequest request)
        {
            {
                var result = await _accountService.ChangePasswordAsync(id, request.OldPassword, request.NewPassword);
                if (result == 0)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, Domain.Resources.Exception.Exception.Server);
                }
                return StatusCode(StatusCodes.Status202Accepted, Domain.Resources.Account.Account.ChangePasswordSuccess);
            }
        }

        /// <summary>
        /// The delete account.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The result.</returns>
        /// Created by: NTLam (20/08/2023)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccountAsync([FromRoute] Guid id)
        {
            var result = await _accountService.DeleteAccountAsync(id);
            if (result == 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, Domain.Resources.Exception.Exception.Server);
            }
            return StatusCode(StatusCodes.Status202Accepted, Domain.Resources.Account.Account.DeleteAccountSuccess);
        }

    }
}
