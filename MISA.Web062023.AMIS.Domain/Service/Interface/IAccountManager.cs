using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web062023.AMIS.Domain
{

    /// <summary>
    /// The i account manager.
    /// </summary>
    public interface IAccountManager
    {

        /// <summary>
        /// The check valid account.
        /// </summary>
        /// <param name="account">The account.</param>
        /// <returns>The result.</returns>
        public Task CheckValidNewAccount(Account account);

        /// <summary>
        /// The check valid new username.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>The result.</returns>
        public Task CheckValidNewUsername(string username);

        /// <summary>
        /// The check valid new email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>The result.</returns>
        public Task CheckValidNewEmail(string email);

        /// <summary>
        /// The check valid new phone.
        /// </summary>
        /// <param name="phone">The phone.</param>
        /// <returns>The result.</returns>
        public Task CheckValidNewPhone(string phone);
    }
}
