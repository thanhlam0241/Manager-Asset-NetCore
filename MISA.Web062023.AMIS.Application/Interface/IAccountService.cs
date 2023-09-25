using MISA.Web062023.AMIS.Domain;

namespace MISA.Web062023.AMIS.Application
{

    /// <summary>
    /// The i account service.
    /// </summary>
    public interface IAccountService
    {

        /// <summary>
        /// The find user async.
        /// </summary>
        /// <param name="credential">The credential.</param>
        /// <returns>The result.</returns>
        public Task<Account> FindAccountByCredentialAsync(string credential);

        /// <summary>
        /// The find account by user name async.
        /// </summary>
        /// <param name="userName">The user name.</param>
        /// <returns>The result.</returns>
        public Task<Account> FindAccountByUserNameAsync(string userName);

        /// <summary>
        /// The find account by email async.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>The result.</returns>
        public Task<Account> FindAccountByEmailAsync(string email);

        /// <summary>
        /// The find account by phone number async.
        /// </summary>
        /// <param name="phoneNumber">The phone number.</param>
        /// <returns>The result.</returns>
        public Task<Account> FindAccountByPhoneNumberAsync(string phoneNumber);

        /// <summary>
        /// The get filter.
        /// </summary>
        /// <param name="pageSize">The page size.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="filter">The filter.</param>
        /// <returns>The result.</returns>
        public Task<FilterData<AccountDto>> GetFilter(int pageSize, int pageNumber, string filter);

        /// <summary>
        /// The get all.
        /// </summary>
        /// <returns>The result.</returns>
        public Task<dynamic> GetAll(string username, string email, string phone);

        /// <summary>
        /// The get account by id async.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The result.</returns>
        public Task<AccountDto> GetAccountByIdAsync(Guid id);

        /// <summary>
        /// The insert account async.
        /// </summary>
        /// <param name="account">The account.</param>
        /// <returns>The result.</returns>
        public Task<int> InsertAccountAsync(AccountCreateDto account);

        /// <summary>
        /// The update account async.
        /// </summary>
        /// <param name="account">The account.</param>
        /// <returns>The result.</returns>
        public Task<int> UpdateAccountAsync(Guid id, AccountUpdateDto account);

        /// <summary>
        /// The delete account async.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The result.</returns>
        public Task<int> DeleteAccountAsync(Guid id);

        /// <summary>
        /// The change password async.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="oldPassword">The old password.</param>
        /// <param name="newPassword">The new password.</param>
        /// <returns>The result.</returns>
        public Task<int> ChangePasswordAsync(Guid id, string oldPassword, string newPassword);

        /// <summary>
        /// The change password async.
        /// </summary>
        /// <param name="credential">The credential.</param>
        /// <param name="oldPassword">The old password.</param>
        /// <param name="newPassword">The new password.</param>
        /// <returns>The result.</returns>
        public Task<int> ChangePasswordAsync(string credential, string oldPassword, string newPassword);

        /// <summary>
        /// The reset password async.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="newPassword">The new password.</param>
        /// <returns>The result.</returns>
        public Task<int> ResetPasswordAsync(Guid id, string newPassword);

        /// <summary>
        /// The lock account async.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The result.</returns>
        public Task<int> LockAccountAsync(Guid id);

        /// <summary>
        /// The active account async.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The result.</returns>
        public Task<int> ActiveAccountAsync(Guid id);

        /// <summary>
        /// The map fromentity.
        /// </summary>
        /// <param name="account">The account.</param>
        /// <returns>The result.</returns>
        public AccountDto MapFromentity(Account account);

        /// <summary>
        /// The map from dto.
        /// </summary>
        /// <param name="account">The account.</param>
        /// <returns>The result.</returns>
        public Account MapFromDto(AccountCreateDto account);
    }
}
