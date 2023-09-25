using Dapper;
using MISA.Web062023.AMIS.Application;
using MISA.Web062023.AMIS.Domain;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web062023.AMIS.Infrastructure
{

    /// <summary>
    /// The account repository.
    /// </summary>
    public class AccountRepository : IAccountRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// The .ctor.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public AccountRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// The active account async.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>The result.</returns>
        public Task<int> ActiveAccountAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The create account async.
        /// </summary>
        /// <param name="account">The account.</param>
        /// <returns>The result.</returns>
        public async Task<int> CreateAccountAsync(Account account)
        {
            var sql = "Insert INTO account (account_id, username, phone_number, email, password, province_id,active ) VALUES (@AccountId, @Username, @PhoneNumber, @Email, @Password, @ProvinceId, @Active) ";
            var parameters = new DynamicParameters();
            parameters.Add("@AccountId", account.AccountId);
            parameters.Add("@Username", account.Username);
            parameters.Add("@PhoneNumber", account.PhoneNumber);
            parameters.Add("@Email", account.Email);
            parameters.Add("@Password", account.Password);
            parameters.Add("@ProvinceId", account.ProvinceId);
            parameters.Add("@Active", account.Active);
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            var result = await _unitOfWork.Connection.ExecuteAsync(sql, account);
            return result;
        }

        /// <summary>
        /// The update user async.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>The result.</returns>
        public async Task<int> UpdateAccountAsync(Account user)
        {
            var sql = "UPDATE account SET username = @Username, phone_number = @PhoneNumber, email = @Email, password = @Password WHERE account_id = @Id";
            var parameters = new DynamicParameters();
            parameters.Add("@Id", user.AccountId);
            parameters.Add("@Username", user.Username);
            parameters.Add("@PhoneNumber", user.PhoneNumber);
            parameters.Add("@Email", user.Email);
            parameters.Add("@Password", user.Password);
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            var result = await _unitOfWork.Connection.ExecuteAsync(sql, parameters, _unitOfWork.Transaction);
            return result;
        }

        /// <summary>
        /// The delete user async.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>The result.</returns>
        public async Task<int> DeleteAccountAsync(Guid userId)
        {
            var sql = "DELETE FROM account WHERE account_id = @Id";
            var parameters = new DynamicParameters();
            parameters.Add("@Id", userId);
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            var result = await _unitOfWork.Connection.ExecuteAsync(sql, parameters);
            return result;
        }

        /// <summary>
        /// The find user async.
        /// </summary>
        /// <param name="userCredential">The user credential.</param>
        /// <returns>The result.</returns>
        public async Task<Account?> FindAccountByCredentialAsync(string userCredential)
        {
            var sql = "SELECT * FROM account WHERE username = @Credential OR email = @Credential OR phone_number = @Credential";
            var parameters = new DynamicParameters();
            parameters.Add("@Credential", userCredential);
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            var result = await _unitOfWork.Connection.QueryFirstOrDefaultAsync<Account>(sql, parameters, _unitOfWork.Transaction);
            return result;
        }

        /// <summary>
        /// The find active account async.
        /// </summary>
        /// <param name="userCredential">The user credential.</param>
        /// <returns>The result.</returns>
        public async Task<Account?> FindAccountByStatusAsync(string userCredential, bool isActive)
        {
            var sql = "SELECT * FROM account WHERE username = @Credential OR email = @Credential OR phone_number = @Credential AND active = @IsActive";
            var parameters = new DynamicParameters();
            parameters.Add("@Credential", userCredential);
            parameters.Add("@IsActive", isActive);
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            var result = await _unitOfWork.Connection.QueryFirstOrDefaultAsync<Account>(sql, parameters, _unitOfWork.Transaction);
            return result;
        }

        /// <summary>
        /// The find account by user name async.
        /// </summary>
        /// <param name="userName">The user name.</param>
        /// <returns>The result.</returns>
        public async Task<Account?> FindAccountByUserNameAsync(string userName)
        {
            var sql = "SELECT * FROM account WHERE username = @Username";
            var parameters = new DynamicParameters();
            parameters.Add("@Username", userName);
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            var result = await _unitOfWork.Connection.QueryFirstOrDefaultAsync<Account>(sql, parameters, _unitOfWork.Transaction);
            return result;
        }

        /// <summary>
        /// The find account by email async.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>The result.</returns>
        public async Task<Account?> FindAccountByEmailAsync(string email)
        {
            var sql = "SELECT * FROM account WHERE email = @Email";
            var parameters = new DynamicParameters();
            parameters.Add("@Email", email);
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            var result = await _unitOfWork.Connection.QueryFirstOrDefaultAsync<Account>(sql, parameters, _unitOfWork.Transaction);
            return result;
        }

        /// <summary>
        /// The find account by phone number async.
        /// </summary>
        /// <param name="phoneNumber">The phone number.</param>
        /// <returns>The result.</returns>
        public async Task<Account?> FindAccountByPhoneNumberAsync(string phoneNumber)
        {
            var sql = "SELECT * FROM account WHERE phone_number = @Phone";
            var parameters = new DynamicParameters();
            parameters.Add("@Phone", phoneNumber);
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            var result = await _unitOfWork.Connection.QueryFirstOrDefaultAsync<Account>(sql, parameters, _unitOfWork.Transaction);
            return result;
        }
        /// <summary>
        /// The active account.
        /// </summary>
        /// <param name="account">The account.</param>
        /// <returns>The result.</returns>
        public async Task<int> ActiveAccount(Guid accountId)
        {
            var sql = "UPDATE account SET active = 1 WHERE account_id = @Id";
            var parameters = new DynamicParameters();
            parameters.Add("@Id", accountId);
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            var result = await _unitOfWork.Connection.ExecuteAsync(sql, parameters);
            return result;
        }

        /// <summary>
        /// The de active account async.
        /// </summary>
        /// <param name="accountId">The account id.</param>
        /// <returns>The result.</returns>
        public async Task<int> DeActiveAccountAsync(Guid accountId)
        {
            var sql = "UPDATE account SET active = 0 WHERE account_id = @Id";
            var parameters = new DynamicParameters();
            parameters.Add("@Id", accountId);
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            var result = await _unitOfWork.Connection.ExecuteAsync(sql, parameters);
            return result;
        }

        /// <summary>
        /// The get account by id async.
        /// </summary>
        /// <param name="accountId">The account id.</param>
        /// <returns>The result.</returns>
        public async Task<Account> GetAccountByIdAsync(Guid accountId)
        {
            var sql = "SELECT * FROM account WHERE account_id = @Id";
            var parameters = new DynamicParameters();
            parameters.Add("@Id", accountId);
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            var result = await _unitOfWork.Connection.QueryFirstOrDefaultAsync<Account>(sql, parameters, _unitOfWork.Transaction);
            if (result == null)
            {
                throw new NotFoundException(string.Format(Domain.Resources.Account.Account.AccountIdNotFound, accountId));
            }
            return result;
        }

        /// <summary>
        /// The get filter accounts async.
        /// </summary>
        /// <param name="pageSize">The page size.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <param name="filter">The filter.</param>
        /// <returns>The result.</returns>
        public async Task<FilterData<Account>> GetFilterAccountsAsync(int pageSize, int pageNumber, string filter)
        {
            var parameters = new DynamicParameters();
            var fieldAssets = new string[]
            {
               "username", "phone_number", "email"
            };
            string filterQuery = (filter != "") ? $"WHERE ({string.Join(" LIKE @FilterPattern OR ", fieldAssets)} LIKE @FilterPattern)  " : "";
            string query = $"SELECT * FROM account {filterQuery}  ORDER BY modified_date DESC LIMIT @PageSize OFFSET @Offset;";
            var queryTotalRecord = $"SELECT Count(account_id) from account {filterQuery}";

            parameters.Add("PageSize", pageSize, DbType.Int16);
            parameters.Add("Offset", (pageNumber - 1) * pageSize, DbType.Int16);
            parameters.Add("FilterPattern", $"%{filter}%", DbType.String);

            var connection = _unitOfWork.Connection;
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            var accounts = await connection.QueryAsync<Account>(query, parameters, _unitOfWork.Transaction);
            long totalRecords = await connection.ExecuteScalarAsync<long>(queryTotalRecord, parameters, _unitOfWork.Transaction);

            FilterData<Account> filterAccount = new();
            if (accounts.ToList().Count > 0 && totalRecords > 0)
            {
                filterAccount.Data = accounts;
                filterAccount.CurrentPage = pageNumber;
                filterAccount.CurrentPageSize = pageSize;
                filterAccount.TotalRecord = totalRecords;
                filterAccount.CurrentRecord = accounts.ToList().Count;
                filterAccount.TotalPage = totalRecords / pageSize + (totalRecords % pageSize == 0 ? 0 : 1);
            }
            return filterAccount;
        }

        /// <summary>
        /// The get all accounts async.
        /// </summary>
        /// <returns>The result.</returns>
        public async Task<List<Account>> GetAllAccountsAsync()
        {
            var sql = $"SELECT * FROM account";
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            var result = await _unitOfWork.Connection.QueryAsync<Account>(sql, null, _unitOfWork.Transaction);
            return result.ToList();
        }

        /// <summary>
        /// The get all accounts async.
        /// </summary>
        /// <returns>The result.</returns>
        public async Task<Account> GetOneAccountsAsync(string username, string email, string phone)
        {
            List<string> conditions = new List<string>();
            DynamicParameters parameters = new();
            if (!string.IsNullOrEmpty(username))
            {
                conditions.Add("username = @Username");
                parameters.Add("@Username", username);
            }
            if (!string.IsNullOrEmpty(email))
            {
                conditions.Add("email = @Email");
                parameters.Add("@Email", email);
            }
            if (!string.IsNullOrEmpty(phone))
            {
                conditions.Add("phone_number = @Phone");
                parameters.Add("@Phone", phone);
            }
            string condition;
            if (conditions.Count > 0)
            {
                condition = $"WHERE {string.Join(" AND ", conditions)}";
            }
            else
            {
                condition = "";
            }
            var sql = $"SELECT * FROM account {condition}";
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            var result = await _unitOfWork.Connection.QueryFirstOrDefaultAsync<Account>(sql, parameters, _unitOfWork.Transaction);
            return result;
        }
    }
}
