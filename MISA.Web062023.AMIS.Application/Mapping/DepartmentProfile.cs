using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MISA.Web062023.AMIS.Domain;
using AutoMapper;

namespace MISA.Web062023.AMIS.Application
{

    /// <summary>
    /// The department profile.
    /// </summary>
    /// Created by: NTLam (17/08/2023)
    public class DepartmentProfile : Profile
    {

        /// <summary>
        /// The constructor
        /// </summary>
        /// Created by: NTLam (17/08/2023)
        public DepartmentProfile()
        {
            CreateMap<Department, DepartmentDto>();

            CreateMap<DepartmentCreateDto, Department>();

            CreateMap<DepartmentUpdateDto, Department>();
        }
    }
}
