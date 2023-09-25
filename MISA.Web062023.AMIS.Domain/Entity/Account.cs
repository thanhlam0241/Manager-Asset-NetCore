using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web062023.AMIS.Domain
{

    /// <summary>
    /// The account.
    /// </summary>
    public class Account : IEntity
    {

        /// <summary>
        /// Gets or Sets the account id.
        /// </summary>
        public Guid AccountId { get; set; }

        /// <summary>
        /// Gets or Sets the user name.
        /// </summary>
        public string? Username { get; set; }

        /// <summary>
        /// Gets or Sets the email.
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Gets or Sets the phone number.
        /// </summary>
        public string? PhoneNumber { get; set; }

        /// <summary>
        /// Gets or Sets the password.
        /// </summary>
        [Required(ErrorMessage = "Password is required")]
        public required string Password { get; set; }

        /// <summary>
        /// Gets or Sets the province id.
        /// </summary>
        public int? ProvinceId { get; set; }

        /// <summary>
        /// Gets or Sets the active.
        /// </summary>
        /// 
        [DefaultValue(false)]
        public bool Active { get; set; }

        /// <summary>
        /// The get id.
        /// </summary>
        /// <returns>The result.</returns>
        public Guid GetId()
        {
            return AccountId;
        }

        /// <summary>
        /// The set id.
        /// </summary>
        /// <param name="id">The id.</param>
        public void SetId(Guid id)
        {
            AccountId = id;
        }
    }
}
