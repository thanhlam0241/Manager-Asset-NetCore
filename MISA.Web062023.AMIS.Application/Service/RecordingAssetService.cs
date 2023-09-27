using AutoMapper;
using MISA.Web062023.AMIS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web062023.AMIS.Application
{
    public class RecordingAssetService : IRecordingAssetService
    {
        private readonly IRecordingAssetsRepository _recordingAssetsRepository;
        private readonly IFixedAssetRepository _fixedAssetRepository;
        private readonly IRecordingManager _recordingManager;
        private readonly IMapper _mapper;
        public RecordingAssetService(IRecordingAssetsRepository recordingAssetsRepository, IFixedAssetRepository fixedAssetRepository, IRecordingManager recordingManager, IMapper mapper)
        {
            _recordingAssetsRepository = recordingAssetsRepository;
            _fixedAssetRepository = fixedAssetRepository;
            _recordingManager = recordingManager;
            _mapper = mapper;
        }

        private Recording MapRecordingCreateDto(RecordingCreateDto recordingCreate)
        {
            var recording = _mapper.Map<Recording>(recordingCreate);
            recording.SetId(Guid.NewGuid());
            return recording;
        }

        public async Task<int> CreateNewRecording(RecordingCreateDto recording, List<Guid> ids)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteRecordingAsset(Guid recordingId, List<Guid> ids)
        {
            throw new NotImplementedException();
        }

        public async Task<List<RecordedAsset>> GetAssetsByRecordingIdAsync(Guid recordingId)
        {
            var result = await _recordingAssetsRepository.GetRecordingAssetsAsync(recordingId);
            return result.ToList();
        }

        public Task<int> InsertRecordingAsset(Guid recordingId, List<Guid> ids)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateRecordingAsset(Guid recordingId, List<Guid> listAssetIdsAfterChange)
        {
            throw new NotImplementedException();
        }
    }
}
