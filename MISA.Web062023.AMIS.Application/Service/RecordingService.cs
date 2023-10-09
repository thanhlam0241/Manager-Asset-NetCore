using AutoMapper;
using MISA.Web062023.AMIS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web062023.AMIS.Application
{

    /// <summary>
    /// The recording service.
    /// </summary>
    public class RecordingService : BaseCrudService<Recording, RecordingDto, RecordingCreateDto, RecordingUpdateDto>,
         IRecordingService
    {
        private readonly IRecordingRepository _recordingRepository;

        /// <summary>
        /// The .ctor.
        /// </summary>
        /// <param name="recordingRepository">The recording repository.</param>
        /// <param name="recordingManager">The recording manager.</param>
        /// <param name="mapper">The mapper.</param>
        public RecordingService(IRecordingRepository recordingRepository, IRecordingManager recordingManager, IMapper mapper) : base(recordingRepository, recordingManager, mapper)
        {
            _recordingRepository = recordingRepository;
        }

        /// <summary>
        /// The get filter data.
        /// </summary>
        /// <param name="filter">The filter.</param>
        /// <param name="pageIndex">The page index.</param>
        /// <param name="pageSize">The page size.</param>
        /// <returns>The result.</returns>
        public async Task<FilterData<Recording>> GetFilterDataAsync(int pageSize, int pageNumber, string filter)
        {
            var result = await _recordingRepository.GetFilterRecording(pageSize, pageNumber, filter);
            return result;
        }

        /// <summary>
        /// The get new code.
        /// </summary>
        /// <returns>The result.</returns>
        public async Task<string> GetNewCode()
        {
            var tryCount = 0;

        beginGencode:
            tryCount++;
            var newCode = await _recordingRepository.GenerateCode();

            var isExist = await _recordingRepository.FindByCodeAsync(newCode);

            if (isExist != null)
            {
                if (tryCount > 3)
                {
                    throw new ConflictException(Domain.Resources.RecordingAsset.RecordingAsset.CanGenCode);
                }
                goto beginGencode;
            }

            return newCode;
        }
    }
}
