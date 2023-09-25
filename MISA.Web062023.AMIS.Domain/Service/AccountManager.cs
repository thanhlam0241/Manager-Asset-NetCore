using MISA.Web062023.AMIS.Domain.Resources.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web062023.AMIS.Domain
{

    /// <summary>
    /// The account manager.
    /// </summary>
    public class AccountManager : IAccountManager
    {
        private readonly IAccountRepository _accountRepository;

        /// <summary>
        /// The .ctor.
        /// </summary>
        /// <param name="accountRepository">The account repository.</param>
        public AccountManager(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        /// <summary>
        /// Hàm kiểm tra tài khoản mới có hợp lệ không( Các giá trị không trùng, giá trị hợp lệ, phải có ít nhất 1 trong 3 giá trị username, email, phone)
        /// </summary>
        /// <param name="account">The account.</param>
        /// <returns>The result.</returns>
        /// Created by: NTLam (20/08/2023)
        public async Task CheckValidNewAccount(Account account)
        {
            if (account.Username != null)
            {
                if (!ValidationUtil.IsValidUsername(account.Username))
                {
                    throw new BadRequestException(Resources.Account.Account.InvalidUsername);
                }
                else
                {
                    var result = await _accountRepository.FindAccountByUserNameAsync(account.Username);
                    if (result != null)
                    {
                        throw new BadRequestException(Resources.Account.Account.DuplicateUsername);
                    }
                }
            }
            if (account.Email != null)
            {
                if (!ValidationUtil.IsValidEmail(account.Email))
                {
                    throw new BadRequestException(Resources.Account.Account.InvalidEmail);
                }
                else
                {
                    var result = await _accountRepository.FindAccountByEmailAsync(account.Email);
                    if (result != null)
                    {
                        throw new BadRequestException(Resources.Account.Account.DuplicateEmail);
                    }
                }
            }
            if (account.PhoneNumber != null)
            {
                if (!ValidationUtil.IsValidPhoneNumber(account.PhoneNumber))
                {
                    throw new BadRequestException(Resources.Account.Account.InvalidPhoneNumber);
                }
                else
                {
                    var result = await _accountRepository.FindAccountByPhoneNumberAsync(account.PhoneNumber);
                    if (result != null)
                    {
                        throw new BadRequestException(Resources.Account.Account.DuplicatePhoneNumber);
                    }
                }
            }
            if (account.Username == null && account.Email == null && account.PhoneNumber == null)
            {
                throw new BadRequestException(Resources.Account.Account.UsernameOrEmailOrPhoneRequired);
            }
        }

        /// <summary>
        /// Hàm kiểm tra email có hợp lệ hay không (không trùng, giá trị hợp lệ)
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>The result.</returns>
        /// Created by: NTLam (20/08/2023)
        public async Task CheckValidNewEmail(string email)
        {
            if (!ValidationUtil.IsValidEmail(email))
            {
                throw new BadRequestException(Resources.Account.Account.InvalidEmail);
            }
            else
            {
                var result = await _accountRepository.FindAccountByEmailAsync(email);
                if (result != null)
                {
                    throw new BadRequestException(Resources.Account.Account.DuplicateEmail);
                }
            }
        }

        /// <summary>
        /// Hàm kiểm tra số điện thoại có hợp lệ hay không (không trùng, giá trị hợp lệ)
        /// </summary>
        /// <param name="phone">The phone.</param>
        /// <returns>The result.</returns>
        /// Created by: NTLam (20/08/2023)
        public async Task CheckValidNewPhone(string phone)
        {
            if (!ValidationUtil.IsValidPhoneNumber(phone))
            {
                throw new BadRequestException(Resources.Account.Account.InvalidPhoneNumber);
            }
            else
            {
                var result = await _accountRepository.FindAccountByPhoneNumberAsync(phone);
                if (result != null)
                {
                    throw new BadRequestException(Resources.Account.Account.DuplicatePhoneNumber);
                }
            }
        }

        /// <summary>
        /// Hàm kiểm tra tên đăng nhập có hợp lệ hay không (không trùng, giá trị hợp lệ)
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>The result.</returns>
        /// Created by: NTLam (20/08/2023)
        public async Task CheckValidNewUsername(string username)
        {
            if (!ValidationUtil.IsValidUsername(username))
            {
                throw new BadRequestException(Resources.Account.Account.InvalidUsername);
            }
            else
            {
                var result = await _accountRepository.FindAccountByUserNameAsync(username);
                if (result != null)
                {
                    throw new BadRequestException(Resources.Account.Account.DuplicateUsername);
                }
            }
        }
    }
}
