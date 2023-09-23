using AutoMapper;
using MISA.Web062023.AMIS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web062023.AMIS.Application
{
    public class RecordingProfile : Profile
    {
        public RecordingProfile()
        {
            CreateMap<Recording, RecordingDto>();

            CreateMap<RecordingCreateDto, Recording>();

            CreateMap<RecordingUpdateDto, Recording>();
        }
    }
}
