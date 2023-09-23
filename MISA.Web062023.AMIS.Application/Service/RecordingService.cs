using AutoMapper;
using MISA.Web062023.AMIS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web062023.AMIS.Application
{
    public class RecordingService : BaseCrudService<Recording, RecordingDto, RecordingCreateDto, RecordingUpdateDto>,
         IRecordingService
    {
        private readonly IRecordingRepository _recordingRepository;

        public RecordingService(IRecordingRepository recordingRepository, IRecordingManager recordingManager, IMapper mapper) : base(recordingRepository, recordingManager, mapper)
        {
            _recordingRepository = recordingRepository;
        }
    }
}
