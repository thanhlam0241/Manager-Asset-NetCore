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

        public RecordedAssetService(IRecordedAssetRepository recordedAssetRepository, IRecordedAssetManager recordedAssetManager, IMapper mapper, IResourceAssetRepository resourceAssetRepository) : base(recordedAssetRepository, recordedAssetManager, mapper)
        {
            _recordedAssetRepository = recordedAssetRepository;
            _recordedAssetManager = recordedAssetManager;
            _resourceAssetRepository = resourceAssetRepository;
        }

        public Task<RecordedAssetCreateDto> CreateAssetAsync(RecordedAssetCreateDto assetCreateDto)
        {
            throw new NotImplementedException();
        }

        public async Task<int> DeleteAssetAsync(Guid assetId)
        {
            await _recordedAssetManager.CheckIsExistIdAsync(assetId);
            var result = await _recordedAssetRepository.DeleteAsync(assetId);
            return result;
        }

        public async Task<RecordedAsset> GetAssetByIdAsync(Guid id)
        {
            var result = await _recordedAssetRepository.GetAssetAsync(id) ?? throw new NotFoundException("Asset not found");
            return result;
        }

        public async Task<int> UpdateAssetAsync(Guid id, RecordedAssetUpdateDto assetUpdateDto)
        {
            var recordedAsset = await _recordedAssetRepository.GetAssetAsync(id);
            if (assetUpdateDto.RecordedAssetCode != null && recordedAsset.RecordedAssetCode != assetUpdateDto.RecordedAssetCode)
            {
                throw new ConflictException("Mã tài sản không đúng");
            }
            if (assetUpdateDto.DepartmentName != null && recordedAsset.DepartmentName != assetUpdateDto.DepartmentName)
            {
                throw new ConflictException("Tên phòng ban không đúng");
            }
            var listIdsNew = assetUpdateDto.ResourceAssets.Where(x => x.ResourceAssetId != null).Select(x => x.ResourceAssetId).ToList();
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

            return result;
        }
    }
}
