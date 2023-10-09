using AutoMapper;
using MISA.Web062023.AMIS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web062023.AMIS.Application
{
    public class RecordedAssetProfile : Profile
    {
        public RecordedAssetProfile()
        {
            CreateMap<RecordedAssetCreateDto, RecordedAsset>();

            CreateMap<RecordedAsset, RecordedAssetDto>();

            CreateMap<RecordedAssetUpdateDto, RecordedAsset>();
        }
    }
}
