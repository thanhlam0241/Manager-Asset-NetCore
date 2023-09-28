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
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IRecordingManager _recordingManager;
        private readonly IResourceBudgetRepository _resourceBudgetRepository;
        private readonly IMapper _mapper;
        public RecordingAssetService(IRecordingAssetsRepository recordingAssetsRepository, IFixedAssetRepository fixedAssetRepository, IRecordingManager recordingManager
            , IDepartmentRepository departmentRepository, IMapper mapper, IResourceBudgetRepository resourceBudgetRepository)
        {
            _recordingAssetsRepository = recordingAssetsRepository;
            _fixedAssetRepository = fixedAssetRepository;
            _recordingManager = recordingManager;
            _departmentRepository = departmentRepository;
            _mapper = mapper;
            _resourceBudgetRepository = resourceBudgetRepository;
        }

        private Recording MapRecordingCreateDto(RecordingCreateDto recordingCreate)
        {
            var recording = _mapper.Map<Recording>(recordingCreate);
            recording.SetId(Guid.NewGuid());
            return recording;
        }

        private RecordedAsset MapRecordedAssetCreate(RecordedAssetCreateDto recordedAssetCreate)
        {
            var recordedAsset = new RecordedAsset()
            {
                RecordedAssetId = Guid.NewGuid(),
                RecordedAssetCode = recordedAssetCreate.RecordedAssetCode,
                RecordedAssetName = recordedAssetCreate.RecordedAssetName,
                Value = recordedAssetCreate.ResourceAssets.Sum(x => x.Cost),
                DepreciationRate = recordedAssetCreate.DepreciationRate,
                RecordingType = recordedAssetCreate.RecordingType
            };
            return recordedAsset;
        }

        public async Task<int> CreateNewRecording(RecordingCreateDto recording, List<RecordedAssetCreateDto> assets)
        {
            Recording recordingEntity = MapRecordingCreateDto(recording);
            recordingEntity.SetId(Guid.NewGuid());

            var listRecordedAsset = new List<RecordedAsset>();

            foreach (var asset in assets)
            {
                RecordedAsset recordedAsset = MapRecordedAssetCreate(asset);
                var department = await _departmentRepository.GetAsync(asset.DepartmentId) ?? throw new Exception("Department not found");
                recordedAsset.Department = department;
                var listResourceAsset = new List<ResourceAsset>();
                foreach (var resource in asset.ResourceAssets)
                {
                    var resourceAsset = new ResourceAsset
                    {
                        Cost = resource.Cost
                    };
                    var resourceBudget = await _resourceBudgetRepository.GetAsync(resource.ResourceBudgetId) ?? throw new Exception("Resource budget not found");
                    resourceAsset.ResourceBudget = resourceBudget;
                    listResourceAsset.Add(resourceAsset);
                }
                recordedAsset.ResourceAssets = listResourceAsset;
                listRecordedAsset.Add(recordedAsset);
            }

            RecordingAsset recordingAsset = new RecordingAsset
            {
                Recording = recordingEntity,
                Assets = listRecordedAsset
            };
            var result = await _recordingAssetsRepository.InsertRecordingAssetsAsync(recordingAsset);
            return result;
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
