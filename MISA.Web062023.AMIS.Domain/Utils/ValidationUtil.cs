using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MISA.Web062023.AMIS.Domain
{

    /// <summary>
    /// The validation util.
    /// </summary>
    public class ValidationUtil
    {

        /// <summary>
        /// The is valid email.
        /// </summary>
        /// <param name="email">The email.</param>
        /// <returns>The result.</returns>
        public static bool IsValidEmail(string email)
        {
            if (string.IsNullOrEmpty(email))
                return false;

            // Sử dụng biểu thức chính quy để kiểm tra định dạng email
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}$";
            return Regex.IsMatch(email, pattern);
        }

        /// <summary>
        /// The is valid password.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <returns>The result.</returns>
        public static bool IsValidPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
                return false;

            // Kiểm tra độ dài mật khẩu và các yêu cầu khác (ví dụ: chứa ít nhất 8 ký tự)
            return password.Length >= 6 && password.Length <= 24;
        }

        /// <summary>
        /// The is valid phone number.
        /// </summary>
        /// <param name="phoneNumber">The phone number.</param>
        /// <returns>The result.</returns>
        public static bool IsValidPhoneNumber(string phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber))
                return false;

            // Kiểm tra định dạng số điện thoại (ví dụ: 10 chữ số)
            string pattern = @"^\d{10}$";
            return Regex.IsMatch(phoneNumber, pattern);
        }

        /// <summary>
        /// The is valid username.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <returns>The result.</returns>
        public static bool IsValidUsername(string username)
        {
            if (string.IsNullOrEmpty(username))
                return false;

            // Kiểm tra độ dài tên đăng nhập và các yêu cầu khác (ví dụ: ít nhất 4 ký tự)
            return username.Length >= 4 && username.Length <= 16;
        }
    }
}
