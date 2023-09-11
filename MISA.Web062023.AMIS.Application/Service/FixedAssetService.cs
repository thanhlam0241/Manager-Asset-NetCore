using AutoMapper;
using Dapper;
using MISA.Web062023.AMIS.Application.DTO.FixedAsset;
using MISA.Web062023.AMIS.Domain;
using OfficeOpenXml;
using System;
using System.Data;

namespace MISA.Web062023.AMIS.Application
{

    /// <summary>
    /// The fixed asset repository.
    /// </summary>
    /// Created by: NTLam (10/8/2023)
    public class FixedAssetService : BaseCrudService<FixedAsset, FixedAssetDto, FixedAssetCreateDto, FixedAssetUpdateDto>, IFixedAssetService
    {
        private readonly IFixedAssetRepository _fixedAssetRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IFixedAssetCategoryRepository _fixedAssetCategoryRepository;
        private readonly IFixedAssetManager _fixedAssetManager;

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="fixedAssetRepository">The fixed asset repository.</param>
        /// <param name="departmentRepository">The department repository.</param>
        /// <param name="fixedAssetCategoryRepository">The fixed asset category repository.</param>
        /// <param name="fixedAssetManager">The fixed asset manager.</param>
        /// Created by: NTLam (10/8/2023)
        public FixedAssetService(IFixedAssetRepository fixedAssetRepository, IDepartmentRepository departmentRepository, IFixedAssetCategoryRepository fixedAssetCategoryRepository, IFixedAssetManager fixedAssetManager, IMapper mapper) : base(fixedAssetRepository, fixedAssetManager, mapper)
        {
            _fixedAssetRepository = fixedAssetRepository;
            _departmentRepository = departmentRepository;
            _fixedAssetCategoryRepository = fixedAssetCategoryRepository;
            _fixedAssetManager = fixedAssetManager;
        }

        /// <summary>
        /// The create fixed asset.
        /// </summary>
        /// <param name="fixedAsset">The fixed asset.</param>
        /// <returns>The result.</returns>
        /// Created by: NTLam (10/8/2023)
        public async Task<int> CreateFixedAssetAsync(FixedAssetCreateDto fixedAssetCreateDto)
        {
            await _fixedAssetManager.CheckDuplicateCodeAsync(fixedAssetCreateDto.FixedAssetCode);
            FixedAsset fixedAsset = EntityCreateDtoToEntity(fixedAssetCreateDto);
            int result = await _fixedAssetRepository.CreateFixedAssetAsync(fixedAsset);
            return result;
        }

        /// <summary>
        /// The delete fixed asset.
        /// </summary>
        /// <param name="assetId">The asset id.</param>
        /// <returns>The result.</returns>
        /// Created by: NTLam (10/8/2023)
        public async Task<int> DeleteFixedAssetAsync(Guid assetId)
        {
            await _fixedAssetManager.CheckIsExistIdAsync(assetId);
            int numberRowDeleted = await _fixedAssetRepository.DeleteFixedAssetAsync(assetId);
            return numberRowDeleted;
        }

        /// <summary>
        /// The get filter assets.
        /// </summary>
        /// <param name="pageSize">The page size.</param>
        /// <param name="pageNumber">The page number.</param>
        /// <returns>The result.</returns>
        /// Created by: NTLam (10/8/2023)
        public async Task<FilterFixedAsset> GetFilterAssetsAsync(int pageSize, int pageNumber, string filterString, List<Guid>? departmentIds, List<Guid>? fixedAssetCategoryIds)
        {
            var filterFixedAsset = await _fixedAssetRepository.GetFilterAssetsAsync(pageSize, pageNumber, filterString, departmentIds, fixedAssetCategoryIds);
            return filterFixedAsset;
        }

        /// <summary>
        /// The get all fixed assets.
        /// </summary>
        /// <returns>The result.</returns>
        /// Created by: NTLam (10/8/2023)
        public async Task<List<FixedAsset>> GetAllFixedAssetsAsync()
        {
            var fixedAssets = await _fixedAssetRepository.GetAllFixedAssetsAsync();
            return fixedAssets.ToList();
        }

        /// <summary>
        /// The get all assets view.
        /// </summary>
        /// <returns>The result.</returns>
        /// Created by: NTLam (10/8/2023)
        public async Task<List<FixedAssetViewDto>> GetAllAssetsView()
        {
            var fixedssets = await _fixedAssetRepository.GetAllFixedAssetsAsync();
            if (fixedssets.ToList().Count == 0)
            {
                throw new NotFoundException(Domain.Resources.FixedAsset.FixedAsset.NoAsset);
            }
            return fixedssets.Select(f => Mapper.Map<FixedAssetViewDto>(f)).ToList();
        }

        /// <summary>
        /// The get fixed asset by id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The result.</returns>
        /// Created by: NTLam (10/8/2023)
        public async Task<FixedAssetDto> GetFixedAssetByIdAsync(Guid id)
        {
            var fixedAsset = await _fixedAssetRepository.GetFixedAssetByIdAsync(id);
            return EntityToEntityDto(fixedAsset);
        }

        /// <summary>
        /// The update fixed asset.
        /// </summary>
        /// <param name="assetId">The asset id.</param>
        /// <param name="fixedAsset">The fixed asset.</param>
        /// <returns>The result.</returns>
        /// Created by: NTLam (10/8/2023)
        public async Task<int> UpdateFixedAssetAsync(Guid assetId, FixedAssetUpdateDto fixedAssetUpdateDto)
        {
            await _fixedAssetManager.CheckIsExistIdAsync(assetId);
            var fixedAsset = EntityUpdateDtoToEntity(assetId, fixedAssetUpdateDto);
            int result = await _fixedAssetRepository.UpdateFixedAssetAsync(fixedAsset);
            return result;
        }

        /// <summary>
        /// The delete list assets async.
        /// </summary>
        /// <param name="assetIds">The asset ids.</param>
        /// <returns>The result.</returns>
        /// Created by: NTLam (20/8/2023)
        public async Task<int> DeleteListAssetsAsync(List<Guid> assetIds)
        {
            int result = await _fixedAssetRepository.DeleteManyAsync(assetIds);
            return result;
        }

        /// <summary>
        /// The generate new code.
        /// </summary>
        /// <returns>The result.</returns>
        /// Created by: NTLam (20/8/2023)
        public async Task<string> GenerateRandomCode()
        {
            string newCode = "TS";
            int count = 0;
            Random random = new Random();
        startGenNewCode:
            for (int i = 0; i < 6; i++)
            {
                newCode += random.Next(0, 9).ToString();
            }
            var checkCode = await _fixedAssetRepository.FindByCodeAsync(newCode);
            if (checkCode != null)
            {
                count++;
                if (count > 3)
                {
                    throw new ConflictException(Domain.Resources.FixedAsset.FixedAsset.CanNotGenCode);
                };
                goto startGenNewCode;
            }
            return newCode;
        }

        /// <summary>
        /// The generate code.
        /// </summary>
        /// <returns>The result.</returns>
        /// Created by: NTLam (20/8/2023)
        public async Task<string> GenerateCode()
        {
            var tryCount = 0;

        beginGencode:
            tryCount++;
            var newCode = await _fixedAssetRepository.GenerateCode();

            var isExist = await _fixedAssetRepository.FindByCodeAsync(newCode);

            if (isExist != null)
            {
                if (tryCount > 3)
                {
                    throw new ConflictException(Domain.Resources.FixedAsset.FixedAsset.CanNotGenCode);
                }
                goto beginGencode;
            }

            return newCode;
        }

        /// <summary>
        /// The create multi fixed asset async.
        /// </summary>
        /// <param name="lists">The lists.</param>
        /// <returns>The result.</returns>
        /// Created by: NTLam (20/8/2023)
        public async Task<int> InsertMultiFixedAssetAsync(List<FixedAssetCreateDto> lists)
        {
            var listFixedAsset = lists.Select(EntityCreateDtoToEntity).ToList();

            await _fixedAssetManager.CheckIsCodeExistInListCodesAsync(listFixedAsset.Select(f => f.FixedAssetCode).ToList());

            var result = await _fixedAssetRepository.InsertMultiAsync(listFixedAsset.ToList());

            return result;
        }

        /// <summary>
        /// The get all filter fixed assets async.
        /// </summary>
        /// <param name="filterString">The filter string.</param>
        /// <param name="departmentId">The department id.</param>
        /// <param name="fixedAssetCategoryId">The fixed asset category id.</param>
        /// <returns>The result.</returns>
        /// Created by: NTLam (20/8/2023)
        public async Task<List<FixedAssetViewDto>> GetAllFilterFixedAssetsAsync(string filterString, List<Guid>? departmentIds, List<Guid>? fixedAssetCategoryIds)
        {
            var fixedAssets = await _fixedAssetRepository.GetAllFilterAssetAsync(filterString, departmentIds, fixedAssetCategoryIds);
            if (fixedAssets.ToList().Count == 0)
            {
                throw new NotFoundException(Domain.Resources.FixedAsset.FixedAsset.NoAsset);
            }
            return fixedAssets.Select(MapEntityToEntityView).ToList();
        }

        /// <summary>
        /// The map entity to entity view.
        /// </summary>
        /// <param name="fixedAsset">The fixed asset.</param>
        /// <returns>The result.</returns>
        /// Created by: NTLam (20/8/2023)
        public FixedAssetViewDto MapEntityToEntityView(FixedAsset fixedAsset)
        {
            return Mapper.Map<FixedAssetViewDto>(fixedAsset);
        }

        /// <summary>
        /// The get file export.
        /// </summary>
        /// <param name="lists">The lists.</param>
        /// <returns>The result.</returns>
        /// Created by: NTLam (20/8/2023)
        public MemoryStream GetFileExport(List<FixedAssetViewDto> lists)
        {
            var stream = new MemoryStream();

            using var package = new ExcelPackage(stream);
            var sheet = package.Workbook.Worksheets.Add("Assets");

            var tableRange = sheet.Cells.LoadFromCollection(lists, true);

            sheet.Cells["A1:I1"].AutoFilter = true;
            sheet.Cells.AutoFitColumns();
            sheet.Cells.Style.Font.Size = 11;
            sheet.Cells.Style.Font.Name = "Times New Roman";
            package.Save();

            stream.Position = 0;
            string excelName = $"Assets-{DateTime.Now.ToString("yyyyMMddHHmmssfff")}.xlsx";
            return stream;
        }
    }
}
