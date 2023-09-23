using Dapper;
using MISA.Web062023.AMIS.Application;
using MISA.Web062023.AMIS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web062023.AMIS.Infrastructure
{
    public class UserRepository : IUserRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserRepository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// The create user async.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>The result.</returns>
        public async Task<int> CreateUserAsync(User user)
        {
            var sql = "Insert INTO user (user_id, username, phone_number, email, password, province_id ) VALUES (@Id, @Username, @PhoneNumber, @Email, @Password, @ProvinceId) ";
            var parameters = new DynamicParameters();
            parameters.Add("@Id", user.UserId);
            parameters.Add("@Username", user.UserName);
            parameters.Add("@PhoneNumber", user.PhoneNumber);
            parameters.Add("@Email", user.Email);
            parameters.Add("@Password", user.UserPassword);
            parameters.Add("@ProvinceId", user.ProvinceId);
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            var result = await _unitOfWork.Connection.ExecuteAsync(sql, user);
            return result;
        }

        /// <summary>
        /// The delete user async.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>The result.</returns>
        public async Task<int> DeleteUserAsync(Guid userId)
        {
            var sql = "DELETE FROM user WHERE user_id = @Id";
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
        public async Task<User> FindUserAsync(string userCredential)
        {
            var sql = "SELECT * FROM user WHERE username = @Credential OR email = @Credential OR phone_number = @Credential";
            var parameters = new DynamicParameters();
            parameters.Add("@Credential", userCredential);
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            var result = await _unitOfWork.Connection.QueryFirstOrDefaultAsync<User>(sql, parameters);
            return result;
        }

        /// <summary>
        /// The update user async.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>The result.</returns>
        public async Task<int> UpdateUserAsync(User user)
        {
            var sql = "UPDATE user SET username = @Username, phone_number = @PhoneNumber, email = @Email, password = @Password WHERE user_id = @Id";
            var parameters = new DynamicParameters();
            parameters.Add("@Id", user.UserId);
            parameters.Add("@Username", user.UserName);
            parameters.Add("@PhoneNumber", user.PhoneNumber);
            parameters.Add("@Email", user.Email);
            parameters.Add("@Password", user.UserPassword);
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            var result = await _unitOfWork.Connection.ExecuteAsync(sql, parameters);
            return result;
        }
    }
}
