using System.ComponentModel.DataAnnotations;

namespace MISA.Web062023.AMIS.API
{

    /// <summary>
    /// The change password request.
    /// </summary>
    public class ChangePasswordRequest
    {

        /// <summary>
        /// Gets or Sets the old password.
        /// </summary>
        [Required(ErrorMessage = "Mật khẩu cũ không được để trống")]
        public required string OldPassword { get; set; }

        /// <summary>
        /// Gets or Sets the new password.
        /// </summary>
        [Required(ErrorMessage = "Mật khẩu mới không được để trống")]
        [MaxLength(24, ErrorMessage = "Mật khẩu mới không được quá 24 ký tự")]
        public required string NewPassword { get; set; }
    }
}
