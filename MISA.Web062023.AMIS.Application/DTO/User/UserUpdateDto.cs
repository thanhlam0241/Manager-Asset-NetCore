using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web062023.AMIS.Application
{
    public class UserUpdateDto
    {
        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string? PhoneNumber { get; set; }
        public string? UserName { get; set; }

        [DataType(DataType.Password)]
        public string UserPassword { get; set; }

        [MaxLength(2)]
        public int ProvinceId { get; set; }
    }
}
