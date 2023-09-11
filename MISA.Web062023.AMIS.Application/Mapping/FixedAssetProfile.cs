using AutoMapper;
using MISA.Web062023.AMIS.Application.DTO.FixedAsset;
using MISA.Web062023.AMIS.Application.Extension;
using MISA.Web062023.AMIS.Domain;

namespace MISA.Web062023.AMIS.Application
{

    /// <summary>
    /// The fixed asset profile.
    /// </summary>
    /// Created by: NTLam (17/08/2023)
    public class FixedAssetProfile : Profile
    {

        /// <summary>
        /// The constructor.
        /// </summary>
        /// Created by: NTLam (17/08/2023)
        public FixedAssetProfile()
        {
            CreateMap<FixedAsset, FixedAssetDto>();

            CreateMap<FixedAssetCreateDto, FixedAsset>();

            CreateMap<FixedAssetUpdateDto, FixedAsset>();

            CreateMap<FixedAsset, FixedAssetViewDto>()
                .ForMember(des => des.AccumulatedDepreciation, act => act.MapFrom(src => (float)ConvertValueExtension.ConvertNullableValue(src.Cost) / 100 * ConvertValueExtension.ConvertNullableValue(src.DepreciationRate)))
                .ForMember(des => des.RemainingValue, act => act.MapFrom(src => (float)ConvertValueExtension.ConvertNullableValue(src.Cost) - (float)ConvertValueExtension.ConvertNullableValue(src.Cost) * ConvertValueExtension.ConvertNullableValue(src.DepreciationRate) / 100))
                ;
        }
    }
}
