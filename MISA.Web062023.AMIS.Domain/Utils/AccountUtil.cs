using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web062023.AMIS.Domain
{

    /// <summary>
    /// The account util.
    /// </summary>
    public class AccountUtil
    {

        /// <summary>
        /// The hash password.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <returns>The result.</returns>
        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        /// <summary>
        /// The verify password.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <param name="hashPassword">The hash password.</param>
        /// <returns>The result.</returns>
        public static bool VerifyPassword(string password, string hashPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashPassword);
        }

    }
}
