using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web062023.AMIS.Domain
{

    /// <summary>
    /// The i account repository.
    /// </summary>
    public interface IAccountRepository
    {

        /// <summary>
        /// The create account async.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>The result.</returns>
        public Task<int> CreateAccountAsync(Account user);

        /// <summary>
        /// The update account async.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>The result.</returns>
        public Task<int> UpdateAccountAsync(Account user);


        /// <summary>
        /// The delete account async.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>The result.</returns>
        public Task<int> DeleteAccountAsync(Guid userId);

        /// <summary>
        /// The get account by id async.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>The result.</returns>
        public Task<Account> GetAccountByIdAsync(Guid userId);

        /// <summary>
        /// The get account async.
        /// </summary>
        /// <param name="pageSize">The page size.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="filter">The filter.</param>
        /// <returns>The result.</returns>
        public Task<FilterData<Account>> GetFilterAccountsAsync(int pageSize, int pageNumber, string filter);

        /// <summary>
        /// The get all accounts async.
        /// </summary>
        /// <returns>The result.</returns>
        public Task<List<Account>> GetAllAccountsAsync();

        /// <summary>
        /// The get all accounts async.
        /// </summary>
        /// <returns>The result.</returns>
        public Task<Account> GetOneAccountsAsync(string username, string email, string phone);

        /// <summary>
        /// The find account async.
        /// </summary>
        /// <param name="userCredential">The user credential.</param>
        /// <returns>The result.</returns>
        public Task<Account?> FindAccountByCredentialAsync(string userCredential);

        /// <summary>
        /// The find account by user name async.
        /// </summary>
        /// <param name="userName">The user name.</param>
        /// <returns>The result.</returns>
        public Task<Account?> FindAccountByUserNameAsync(string userName);

        /// <summary>
        /// The find account by email async.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>The result.</returns>
        public Task<Account?> FindAccountByEmailAsync(string email);

        /// <summary>
        /// The find account by phone number async.
        /// </summary>
        /// <param name="phoneNumber">The phone number.</param>
        /// <returns>The result.</returns>
        public Task<Account?> FindAccountByPhoneNumberAsync(string phoneNumber);

        /// <summary>
        /// The find account by status async.
        /// </summary>
        /// <param name="userCredential">The user credential.</param>
        /// <param name="isActive">The is active.</param>
        /// <returns>The result.</returns>
        public Task<Account?> FindAccountByStatusAsync(string userCredential, bool isActive);

        /// <summary>
        /// The active account async.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>The result.</returns>
        public Task<int> ActiveAccountAsync(Guid userId);
        /// <summary>
        /// The deactive account async.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>The result.</returns>
        public Task<int> DeActiveAccountAsync(Guid userId);

    }
}
