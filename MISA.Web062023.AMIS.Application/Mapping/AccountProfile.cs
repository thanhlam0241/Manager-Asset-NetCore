using AutoMapper;
using MISA.Web062023.AMIS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web062023.AMIS.Application.Mapping
{

    /// <summary>
    /// The account profile.
    /// </summary>
    public class AccountProfile : Profile
    {

        /// <summary>
        /// The .ctor.
        /// </summary>
        public AccountProfile()
        {

            CreateMap<AccountCreateDto, Account>();

            CreateMap<AccountUpdateDto, Account>();

            CreateMap<Account, AccountDto>();
        }
    }
}
