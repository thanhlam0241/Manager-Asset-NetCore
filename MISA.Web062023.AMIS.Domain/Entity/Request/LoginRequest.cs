using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web062023.AMIS.Domain
{
    public class LoginRequest
    {
        [Required]
        public string Credential { get; set; }

        [DataType(DataType.Password)]
        [Required]
        public string Password { get; set; }
        public int? ProvinceId { get; set; }
    }
}
