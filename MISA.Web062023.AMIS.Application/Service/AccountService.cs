using AutoMapper;
using MISA.Web062023.AMIS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web062023.AMIS.Application
{

    /// <summary>
    /// The account service.
    /// </summary>
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IAccountManager _accountManager;
        private readonly IMapper _mapper;

        /// <summary>
        /// The .ctor.
        /// </summary>
        /// <param name="userRepository">The user repository.</param>
        public AccountService(IAccountRepository userRepository, IAccountManager accountManager, IMapper mapper)
        {
            _accountRepository = userRepository;
            _accountManager = accountManager;
            _mapper = mapper;
        }



        /// <summary>
        /// The find user async.
        /// </summary>
        /// <param name="credential">The credential.</param>
        /// <returns>The result.</returns>
        public async Task<Account> FindAccountByCredentialAsync(string credential)
        {
            var user = await _accountRepository.FindAccountByCredentialAsync(credential)
                ?? throw new NotFoundException(string.Format(Domain.Resources.Account.Account.AccountCredentialNotFound, credential));
            return user;
        }
        /// <summary>
        /// HÀm tìm kiếm tài khoản theo email
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        /// Created by: NTLam (20/08/2023)

        public async Task<Account> FindAccountByEmailAsync(string email)
        {
            var user = await _accountRepository.FindAccountByEmailAsync(email)
                ?? throw new NotFoundException(string.Format(Domain.Resources.Account.Account.AccountEmailNotFound, email));
            return user;
        }

        /// <summary>
        /// Hàm tìm kiếm tài khoản theo số điện thoại
        /// </summary>
        /// <param name="phoneNumber"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        /// Created by: NTLam (20/08/2023)
        public async Task<Account> FindAccountByPhoneNumberAsync(string phoneNumber)
        {
            var user = await _accountRepository.FindAccountByPhoneNumberAsync(phoneNumber)
                ?? throw new NotFoundException(string.Format(Domain.Resources.Account.Account.AccountPhoneNumberNotFound, phoneNumber));
            return user;
        }

        /// <summary>
        /// Hàm tìm kiếm tài khoản theo tên đăng nhập
        /// </summary>
        /// <param name="userName"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        /// Created by: NTLam (20/08/2023)
        public async Task<Account> FindAccountByUserNameAsync(string userName)
        {
            var user = await _accountRepository.FindAccountByUserNameAsync(userName)
                ?? throw new NotFoundException(string.Format(Domain.Resources.Account.Account.AccountUsernameNotFound, userName));
            return user;
        }

        /// <summary>
        /// The active account async.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The result.</returns>
        public async Task<int> ActiveAccountAsync(Guid id)
        {
            var account = await _accountRepository.GetAccountByIdAsync(id);
            if (account.Active)
            {
                throw new BadRequestException(Domain.Resources.Account.Account.AccountActive);
            }
            var result = await _accountRepository.ActiveAccountAsync(id);
            return result;
        }

        /// <summary>
        /// The lock account async.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The result.</returns>
        public async Task<int> LockAccountAsync(Guid id)
        {
            var account = await _accountRepository.GetAccountByIdAsync(id);
            if (!account.Active)
            {
                throw new BadRequestException(Domain.Resources.Account.Account.AccountDeactive);
            }
            var result = await _accountRepository.DeActiveAccountAsync(id);
            return result;
        }

        /// <summary>
        /// The change password async.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="oldPassword">The old password.</param>
        /// <param name="newPassword">The new password.</param>
        /// <returns>The result.</returns>
        public async Task<int> ChangePasswordAsync(Guid id, string oldPassword, string newPassword)
        {
            var account = await _accountRepository.GetAccountByIdAsync(id);
            if (AccountUtil.VerifyPassword(oldPassword, account.Password) == false)
            {
                throw new BadRequestException(Domain.Resources.Account.Account.AccountPasswordNotCorrect);
            }
            account.Password = AccountUtil.HashPassword(newPassword);
            var result = await _accountRepository.UpdateAccountAsync(account);
            return result;
        }

        /// <summary>
        /// The change password async.
        /// </summary>
        /// <param name="credential">The credential.</param>
        /// <param name="oldPassword">The old password.</param>
        /// <param name="newPassword">The new password.</param>
        /// <returns>The result.</returns>
        public async Task<int> ChangePasswordAsync(string credential, string oldPassword, string newPassword)
        {
            var account = await _accountRepository.FindAccountByCredentialAsync(credential) ?? throw new NotFoundException(Domain.Resources.Authentication.Authentication.CredentialFail);
            if (AccountUtil.VerifyPassword(oldPassword, account.Password) == false)
            {
                throw new BadRequestException(Domain.Resources.Account.Account.AccountPasswordNotCorrect);
            }
            account.Password = AccountUtil.HashPassword(newPassword);
            var result = await _accountRepository.UpdateAccountAsync(account);
            return result;
        }

        /// <summary>
        /// The delete account async.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The result.</returns>
        public async Task<int> DeleteAccountAsync(Guid id)
        {
            var account = await _accountRepository.GetAccountByIdAsync(id);
            var result = await _accountRepository.DeleteAccountAsync(account.AccountId);
            return result;
        }

        /// <summary>
        /// Hàm thêm mới tài khoản
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        /// Created by: NTLam (20/08/2023)
        public async Task<int> InsertAccountAsync(AccountCreateDto acc)
        {
            var account = MapFromDto(acc);
            await _accountManager.CheckValidNewAccount(account);
            account.Password = AccountUtil.HashPassword(account.Password);
            account.AccountId = Guid.NewGuid();
            var result = await _accountRepository.CreateAccountAsync(account);
            return result;
        }

        /// <summary>
        /// Hàm reset mật khẩu
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="newPassword">The new password.</param>
        /// <returns>The result.</returns>
        /// Created by: NTLam (20/08/2023)
        public async Task<int> ResetPasswordAsync(Guid id, string newPassword)
        {
            var account = await _accountRepository.GetAccountByIdAsync(id);
            account.Password = AccountUtil.HashPassword(newPassword);
            var result = await _accountRepository.UpdateAccountAsync(account);
            return result;
        }

        /// <summary>
        /// The update account async.
        /// </summary>
        /// <param name="account">The account.</param>
        /// <returns>The result.</returns>
        public async Task<int> UpdateAccountAsync(Guid id, AccountUpdateDto accountUpdate)
        {
            var accountEntity = await _accountRepository.GetAccountByIdAsync(id);
            if (!string.IsNullOrEmpty(accountUpdate.Username) && accountUpdate.Username != accountEntity.Username)
            {
                await _accountManager.CheckValidNewUsername(accountUpdate.Username);
                accountEntity.Username = accountUpdate.Username;
            }
            if (!string.IsNullOrEmpty(accountUpdate.Email) && accountUpdate.Email != accountEntity.Email)
            {
                await _accountManager.CheckValidNewEmail(accountUpdate.Email);
                accountEntity.Email = accountUpdate.Email;
            }
            if (!string.IsNullOrEmpty(accountUpdate.PhoneNumber) && accountUpdate.PhoneNumber != accountEntity.PhoneNumber)
            {
                await _accountManager.CheckValidNewPhone(accountUpdate.PhoneNumber);
                accountEntity.PhoneNumber = accountUpdate.PhoneNumber;
            }
            if (accountUpdate.ProvinceId != null && accountUpdate.ProvinceId != accountEntity.ProvinceId)
            {
                accountEntity.ProvinceId = accountUpdate.ProvinceId;
            }
            var result = await _accountRepository.UpdateAccountAsync(accountEntity);
            return result;
        }

        /// <summary>
        /// The get filter.
        /// </summary>
        /// <param name="pageSize">The page size.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="filter">The filter.</param>
        /// <returns>The result.</returns>
        public async Task<FilterData<AccountDto>> GetFilter(int pageSize, int pageNumber, string filter)
        {
            var filterData = await _accountRepository.GetFilterAccountsAsync(pageSize, pageNumber, filter);
            var result = new FilterData<AccountDto>
            {
                TotalRecord = filterData.TotalRecord,
                TotalPage = filterData.TotalPage,
                Data = filterData.Data.Select(MapFromentity).ToList(),
                CurrentPageSize = filterData.CurrentPageSize,
                CurrentPage = filterData.CurrentPage,
                CurrentRecord = filterData.CurrentRecord
            };
            return result;
        }

        /// <summary>
        /// The get all.
        /// </summary>
        /// <returns>The result.</returns>
        public async Task<dynamic> GetAll(string username, string email, string phone)
        {
            dynamic result;
            if (!string.IsNullOrEmpty(username) || !string.IsNullOrEmpty(email) || !string.IsNullOrEmpty(phone))
            {
                result = await _accountRepository.GetOneAccountsAsync(username, email, phone);
            }
            else
            {
                result = await _accountRepository.GetAllAccountsAsync();
            }
            return result;
        }

        /// <summary>
        /// The get account by id async.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The result.</returns>
        public async Task<AccountDto> GetAccountByIdAsync(Guid id)
        {
            var result = await _accountRepository.GetAccountByIdAsync(id);
            return MapFromentity(result);
        }

        /// <summary>
        /// The map fromentity.
        /// </summary>
        /// <param name="account">The account.</param>
        /// <returns>The result.</returns>
        public AccountDto MapFromentity(Account account)
        {
            return _mapper.Map<AccountDto>(account);
        }

        /// <summary>
        /// The map from dto.
        /// </summary>
        /// <param name="account">The account.</param>
        /// <returns>The result.</returns>
        public Account MapFromDto(AccountCreateDto account)
        {
            return _mapper.Map<Account>(account);
        }


    }
}
