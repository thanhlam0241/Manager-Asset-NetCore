using AutoMapper;
using MISA.Web062023.AMIS.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web062023.AMIS.Application
{
    public class RecordedAssetService : BaseCrudService<RecordedAsset, RecordedAssetDto, RecordedAssetCreateDto, RecordedAssetUpdateDto>, IRecordedAssetService
    {
        private readonly IRecordedAssetRepository _recordedAssetRepository;
        private readonly IRecordedAssetManager _recordedAssetManager;
        private readonly IResourceAssetRepository _resourceAssetRepository;
        private readonly IRecordingRepository _recordingRepository;

        public RecordedAssetService(IRecordedAssetRepository recordedAssetRepository, IRecordedAssetManager recordedAssetManager, IMapper mapper, IResourceAssetRepository resourceAssetRepository, IRecordingRepository recordingRepository) : base(recordedAssetRepository, recordedAssetManager, mapper)
        {
            _recordedAssetRepository = recordedAssetRepository;
            _recordedAssetManager = recordedAssetManager;
            _resourceAssetRepository = resourceAssetRepository;
            _recordingRepository = recordingRepository;
        }

        public Task<RecordedAssetCreateDto> CreateAssetAsync(RecordedAssetCreateDto assetCreateDto)
        {
            throw new NotImplementedException();
        }

        public async Task<int> DeleteAssetAsync(Guid assetId)
        {
            var asset = await _recordedAssetRepository.GetAssetAsync(assetId);
            var recording = asset.Recording;
            recording.Value = recording.Value - (int)asset.Value;
            var resultUpdateRecording = await _recordingRepository.UpdateAsync(recording);
            var result = await _recordedAssetRepository.DeleteAsync(assetId);
            if(result > 0 && resultUpdateRecording > 0)
            {
                return 1;
            }
            return 0;
        }

        public async Task<RecordedAsset> GetAssetByIdAsync(Guid id)
        {
            var result = await _recordedAssetRepository.GetAssetAsync(id) ?? throw new NotFoundException("Asset not found");
            return result;
        }

        public async Task<int> UpdateAssetAsync(Guid id, RecordedAssetUpdateDto assetUpdateDto)
        {
            var recordedAsset = await _recordedAssetRepository.GetAssetAsync(id);
            var recording = recordedAsset.Recording;
            recording.Value = recording.Value - (int)recordedAsset.Value + assetUpdateDto.ResourceAssets.Sum(x => (int)x.Cost);
            if (assetUpdateDto.RecordedAssetCode != null && recordedAsset.RecordedAssetCode != assetUpdateDto.RecordedAssetCode)
            {
                throw new ConflictException("Mã tài sản không đúng");
            }
            if (assetUpdateDto.DepartmentName != null && recordedAsset.DepartmentName != assetUpdateDto.DepartmentName)
            {
                throw new ConflictException("Tên phòng ban không đúng");
            }
            var listIdsNew = assetUpdateDto.ResourceAssets.Where(x => x.ResourceAssetId != null).Select(x => x.ResourceAssetId).ToList();
            if(assetUpdateDto.ResourceAssets.Count > 0)
            {
                recordedAsset.Value = assetUpdateDto.ResourceAssets.Sum(x => x.Cost);
            }
            var listIdsDelete = recordedAsset.ResourceAssets.Select(x => (int)x.ResourceAssetId).Where(x=>!listIdsNew.Contains(x)).ToList();
            if(listIdsDelete.Count > 0)
            {
                
               var resultDelete = listIdsDelete.Count == 1 ? await _resourceAssetRepository.DeleteOneAsync(listIdsDelete.First()) 
                    : await _resourceAssetRepository.DeleteMultipleAsync(listIdsDelete);
                if(resultDelete > 0)
                {
                    goto start;
                }
                else
                {
                    throw new Exception("Xóa nguồn tài sản thất bại");
                }
            }
            start:
            recordedAsset.DepreciationRate = assetUpdateDto.DepreciationRate;
            recordedAsset.ResourceAssets = assetUpdateDto.ResourceAssets;
            var result = await _recordedAssetRepository.UpdateAsync(recordedAsset);
            var resultUpdateRecording = await _recordingRepository.UpdateAsync(recording);
            if(result > 0 && resultUpdateRecording > 0)
            {
                return 1;
            }
            return 0;
        }
    }
}
