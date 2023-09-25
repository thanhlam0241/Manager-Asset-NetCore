using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web062023.AMIS.Application
{

    /// <summary>
    /// The account dto.
    /// </summary>
    public class AccountDto
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
        /// Gets or Sets the province id.
        /// </summary>
        public int? ProvinceId { get; set; }

        /// <summary>
        /// Gets or Sets the active.
        /// </summary>
        /// 
        [DefaultValue(false)]
        public bool Active { get; set; }
    }
}
