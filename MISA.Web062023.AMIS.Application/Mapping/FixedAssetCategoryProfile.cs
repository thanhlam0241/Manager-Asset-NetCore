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
    /// The fixed asset category profile.
    /// </summary>
    /// Created by: NTLam (17/08/2023)
    public class FixedAssetCategoryProfile : Profile
    {

        /// <summary>
        /// The constructor.
        /// </summary>
        /// Created by: NTLam (17/08/2023)
        public FixedAssetCategoryProfile()
        {
            CreateMap<FixedAssetCategory, FixedAssetCategoryDto>();

            CreateMap<FixedAssetCategoryCreateDto, FixedAssetCategory>();

            CreateMap<FixedAssetCategoryUpdateDto, FixedAssetCategory>();
        }
    }
}
