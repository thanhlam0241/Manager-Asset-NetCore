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
            , IDepartmentRepository departmentRepository, IMapper mapper, IResourceBudgetRepository resourceBudgetRepository, IRecordedAssetRepository recordedAssetRepository)
        {
            _recordingAssetsRepository = recordingAssetsRepository;
            _fixedAssetRepository = fixedAssetRepository;
            _recordingManager = recordingManager;
            _departmentRepository = departmentRepository;
            _mapper = mapper;
            _resourceBudgetRepository = resourceBudgetRepository;
            _recordedAssetRepository = recordedAssetRepository;
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
                RecordingType = recordedAssetCreate.RecordingType
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

            int recordingValue = 0;

            foreach (var asset in assets)
            {
                RecordedAsset recordedAsset = MapRecordedAssetCreate(asset);
                var oldAsset = await _fixedAssetRepository.FindByCodeAsync(asset.RecordedAssetCode)
                    ?? throw new Exception(string.Format(Domain.Resources.FixedAsset.FixedAsset.FixedAssetCodeNotExist, asset.RecordedAssetCode));
                recordedAsset.DepartmentName = oldAsset.DepartmentName;
                recordedAsset.RecordedAssetName = oldAsset.FixedAssetName;
                recordedAsset.DepreciationRate = oldAsset.DepreciationRate;
                int recoredAssetValue = 0;
                var listResourceAsset = new List<ResourceAsset>();
                foreach (var resource in asset.ResourceAssets)
                {
                    recoredAssetValue += resource.Cost;
                    var resourceAsset = new ResourceAsset
                    {
                        Cost = resource.Cost
                    };
                    var resourceBudget = await _resourceBudgetRepository.GetAsync(resource.ResourceBudgetId)
                        ?? throw new Exception(string.Format(Domain.Resources.RecordingAsset.RecordingAsset.ResourceBudgetNotExist, resource.ResourceBudgetId));
                    resourceAsset.ResourceBudget = resourceBudget;
                    listResourceAsset.Add(resourceAsset);
                }
                if (recoredAssetValue < oldAsset.Cost)
                {
                    throw new BadRequestException(string.Format(Domain.Resources.RecordingAsset.RecordingAsset.ResourceBudgetValueNotEnough, asset.RecordedAssetCode));
                }
                else
                {
                    recordedAsset.Value = recoredAssetValue;
                    recordingValue += recoredAssetValue;
                }
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

        public Task<int> InsertRecordingAsset(Guid recordingId, List<Guid> ids)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateRecordingAsync(Guid recordingId, List<Guid> listAssetIdsAfterChange)
        {
            throw new NotImplementedException();
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
            var recording = await GetRecordingAsync(id);
            var listCodeDelete = recording.Assets.Where(x => !recordingUpdateDto.ListDelete.Contains(x.RecordedAssetId)).Select(x => x.RecordedAssetCode).ToList();
            var listCodesAfterDelete = recording.Assets.Select(x => x.RecordedAssetCode).Where(x => !listCodeDelete.Contains(x)).ToList();
            var listCodeCreate = recordingUpdateDto.ListCreate.Select(x => x.RecordedAssetCode);

            var listRecordedAsset = new List<RecordedAsset>();
            int recordingValue = 0;

            foreach (var asset in recordingUpdateDto.ListCreate)
            {
                RecordedAsset recordedAsset = MapRecordedAssetCreate(asset);
                var oldAsset = await _fixedAssetRepository.FindByCodeAsync(asset.RecordedAssetCode)
                    ?? throw new Exception(string.Format(Domain.Resources.FixedAsset.FixedAsset.FixedAssetCodeNotExist, asset.RecordedAssetCode));
                recordedAsset.DepartmentName = oldAsset.DepartmentName;
                recordedAsset.RecordedAssetName = oldAsset.FixedAssetName;
                recordedAsset.DepreciationRate = oldAsset.DepreciationRate;
                int recoredAssetValue = 0;
                var listResourceAsset = new List<ResourceAsset>();
                foreach (var resource in asset.ResourceAssets)
                {
                    recoredAssetValue += resource.Cost;
                    var resourceAsset = new ResourceAsset
                    {
                        Cost = resource.Cost
                    };
                    var resourceBudget = await _resourceBudgetRepository.GetAsync(resource.ResourceBudgetId)
                        ?? throw new Exception(string.Format(Domain.Resources.RecordingAsset.RecordingAsset.ResourceBudgetNotExist, resource.ResourceBudgetId));
                    resourceAsset.ResourceBudget = resourceBudget;
                    listResourceAsset.Add(resourceAsset);
                }
                if (recoredAssetValue < oldAsset.Cost)
                {
                    throw new BadRequestException(string.Format(Domain.Resources.RecordingAsset.RecordingAsset.ResourceBudgetValueNotEnough, asset.RecordedAssetCode));
                }
                else
                {
                    recordedAsset.Value = recoredAssetValue;
                    recordingValue += recoredAssetValue;
                }
                recordedAsset.ResourceAssets = listResourceAsset;
                listRecordedAsset.Add(recordedAsset);
            }

            var listDuplicate = listCodeCreate.Intersect(listCodesAfterDelete).ToList();
            if (listDuplicate.Count > 0)
            {
                throw new BadRequestException(string.Format(Domain.Resources.RecordingAsset.RecordingAsset.DuplicateRecordedAssetCode, string.Join(",", listDuplicate), id));
            }

            var resultDelete = await _recordedAssetRepository.DeleteMultiAsync(recordingUpdateDto.ListDelete);
            var resultUpdate = await _recordedAssetRepository.UpdateMultipleAsync(id, recordingUpdateDto.ListUpdate);
            var resultInsert = await _recordedAssetRepository.InsertMultipleAsync(id, listRecordedAsset);

            if (resultDelete > 0 && resultUpdate > 0 && resultInsert > 0)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
