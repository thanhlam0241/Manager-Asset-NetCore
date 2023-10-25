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
    /// The recording asset service.
    /// </summary>
    /// Created by: NTLam (19/8/2023)
    public class RecordingAssetService : IRecordingAssetService
    {
        private readonly IRecordingAssetsRepository _recordingAssetsRepository;
        private readonly IFixedAssetRepository _fixedAssetRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IRecordingManager _recordingManager;
        private readonly IRecordedAssetRepository _recordedAssetRepository;
        private readonly IResourceBudgetRepository _resourceBudgetRepository;
        private readonly IMapper _mapper;
        private readonly IRecordingRepository _recordingRepository;

        /// <summary>
        /// The .ctor.
        /// </summary>
        /// <param name="recordingAssetsRepository">The recording assets repository.</param>
        /// <param name="fixedAssetRepository">The fixed asset repository.</param>
        /// <param name="recordingManager">The recording manager.</param>
        /// <param name="departmentRepository">The department repository.</param>
        /// <param name="mapper">The mapper.</param>
        /// <param name="resourceBudgetRepository">The resource budget repository.</param>
        /// Created by: NTLam (19/8/2023)
        public RecordingAssetService(IRecordingAssetsRepository recordingAssetsRepository, IFixedAssetRepository fixedAssetRepository, IRecordingManager recordingManager
            , IDepartmentRepository departmentRepository, IMapper mapper, IResourceBudgetRepository resourceBudgetRepository, IRecordedAssetRepository recordedAssetRepository
            , IRecordingRepository recordingRepository)
        {
            _recordingAssetsRepository = recordingAssetsRepository;
            _fixedAssetRepository = fixedAssetRepository;
            _recordingManager = recordingManager;
            _departmentRepository = departmentRepository;
            _mapper = mapper;
            _resourceBudgetRepository = resourceBudgetRepository;
            _recordedAssetRepository = recordedAssetRepository;
            _recordingRepository = recordingRepository;
        }

        /// <summary>
        /// The map recording create dto.
        /// </summary>
        /// <param name="recordingCreate">The recording create.</param>
        /// <returns>The result.</returns>
        /// Created by: NTLam (19/8/2023)
        private Recording MapRecordingCreateDto(RecordingCreateDto recordingCreate)
        {
            var recording = _mapper.Map<Recording>(recordingCreate);
            recording.SetId(Guid.NewGuid());
            return recording;
        }

        /// <summary>
        /// The map recorded asset create.
        /// </summary>
        /// <param name="recordedAssetCreate">The recorded asset create.</param>
        /// <returns>The result.</returns>
        /// Created by: NTLam (19/8/2023)
        private RecordedAsset MapRecordedAssetCreate(RecordedAssetCreateDto recordedAssetCreate)
        {
            var recordedAsset = new RecordedAsset()
            {
                RecordedAssetId = Guid.NewGuid(),
                RecordedAssetCode = recordedAssetCreate.RecordedAssetCode,
                RecordingType = recordedAssetCreate.RecordingType,
                Value = recordedAssetCreate.Value,
            };
            return recordedAsset;
        }

        /// <summary>
        /// The create new recording.
        /// </summary>
        /// <param name="recording">The recording.</param>
        /// <param name="assets">The assets.</param>
        /// <returns>The result.</returns>
        /// Created by: NTLam (19/8/2023)
        public async Task<int> CreateNewRecording(RecordingCreateDto recording, List<RecordedAssetCreateDto> assets)
        {
            await _recordingManager.CheckDuplicateCodeAsync(recording.RecordingCode);
            Recording recordingEntity = MapRecordingCreateDto(recording);
            recordingEntity.SetId(Guid.NewGuid());

            var listRecordedAsset = new List<RecordedAsset>();

            var listFixedAsset = await _fixedAssetRepository.GetListEntitiesByListCode(assets.Select(x => x.RecordedAssetCode).ToList());
            var listResourceBudget = await _resourceBudgetRepository.GetByListIdAsync(assets.SelectMany(x => x.ResourceAssets.Select(y => y.ResourceBudgetId)).ToList());

            int recordingValue = 0;

            foreach (var asset in assets)
            {
                RecordedAsset recordedAsset = MapRecordedAssetCreate(asset);
                var oldAsset = listFixedAsset.Where(x => x.FixedAssetCode == asset.RecordedAssetCode).FirstOrDefault();
                recordedAsset.DepartmentName = oldAsset.DepartmentName;
                recordedAsset.RecordedAssetName = oldAsset.FixedAssetName;
                recordedAsset.DepreciationRate = oldAsset.DepreciationRate;
                int recoredAssetValue = 0;
                var listResourceAsset = new List<ResourceAsset>();
                if (asset.ResourceAssets != null && asset.ResourceAssets.Count > 0)
                {
                    foreach (var resource in asset.ResourceAssets)
                    {
                        recoredAssetValue += resource.Cost;
                        var resourceAsset = new ResourceAsset
                        {
                            Cost = resource.Cost
                        };
                        var resourceBudget = listResourceBudget.Where(x => x.ResourceBudgetId == resource.ResourceBudgetId).FirstOrDefault();
                        resourceAsset.ResourceBudget = resourceBudget;
                        listResourceAsset.Add(resourceAsset);
                    }
                    recordedAsset.Value = recoredAssetValue;
                }
                else
                {
                    recordedAsset.Value = (int)oldAsset.Cost;
                }
                recordingValue += (int)recordedAsset.Value;

                recordedAsset.ResourceAssets = listResourceAsset;
                listRecordedAsset.Add(recordedAsset);
            }

            recordingEntity.Value = recordingValue;

            RecordingAsset recordingAsset = new RecordingAsset
            {
                Recording = recordingEntity,
                Assets = listRecordedAsset
            };
            var result = await _recordingAssetsRepository.InsertRecordingAssetsAsync(recordingAsset);
            return result;
        }

        public Task<int> DeleteRecordingAsync(Guid recordingId, List<Guid> ids)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The get recording async.
        /// </summary>
        /// <param name="recordingId">The recording id.</param>
        /// <returns>The result.</returns>
        /// Created by: NTLam (19/8/2023)
        public async Task<RecordingDto> GetRecordingAsync(Guid recordingId)
        {
            var listAssets = await _recordingAssetsRepository.GetRecordingAssetsAsync(recordingId);
            var recordingDto = _mapper.Map<RecordingDto>(listAssets.First().Recording);
            recordingDto.Assets = listAssets.Select(x => _mapper.Map<RecordedAssetDto>(x)).ToList();
            return recordingDto;
        }

        /// <summary>
        /// The update recording async.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="recordingUpdateDto">The recording update dto.</param>
        /// <returns>The result.</returns>
        /// Created by: NTLam (19/8/2023)
        public async Task<bool> UpdateRecordingAsync(Guid id, RecordingUpdateDto recordingUpdateDto)
        {
            var recording = await _recordingRepository.GetAsync(id);
            var recordedAssets = await _recordingAssetsRepository.GetRecordingAssetsAsync(id);

            if(recording.RecordingCode != recordingUpdateDto.RecordingCode)
            {
                await _recordingManager.CheckDuplicateCodeAsync(recordingUpdateDto.RecordingCode);
                recording.RecordingCode = recordingUpdateDto.RecordingCode;
            }

            recording.RecordingDate = recordingUpdateDto.RecordingDate;
            recording.ActionDate = recordingUpdateDto.ActionDate;
            recording.Description = recordingUpdateDto.Description;

            var result = await _recordingAssetsRepository.UpdateRecordingAssetsAsync(recording, recordedAssets, recordingUpdateDto.Assets);

            return result;

        }
    }
}
