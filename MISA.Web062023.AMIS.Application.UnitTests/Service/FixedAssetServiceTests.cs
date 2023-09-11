using AutoMapper;
using MISA.Web062023.AMIS.Domain;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web062023.AMIS.Application
{
    /// <summary>
    /// Lớp test cho các hàm trong class FixedAssetService
    /// </summary>
    /// Created by: NTLam (23/08/2023)
    [TestFixture]
    public class FixedAssetServiceTests
    {
        private IFixedAssetRepository _fixedAssetRepository;
        private IFixedAssetCategoryRepository _fixedAssetCategoryRepository;
        private IDepartmentRepository _departmentRepository;
        private IMapper _mapper;
        private IFixedAssetManager _fixedAssetManager;
        private List<FixedAsset> _fixedAssets;
        private List<FixedAssetDto> _fixedAssetsDto;


        /// <summary>
        /// Khởi tạo giá trị
        /// </summary>
        /// Created by: NTLam (23/08/2023)
        [SetUp]
        public void SetUp()
        {
            _fixedAssetRepository = Substitute.For<IFixedAssetRepository>();
            _fixedAssetCategoryRepository = Substitute.For<IFixedAssetCategoryRepository>();
            _departmentRepository = Substitute.For<IDepartmentRepository>();
            _mapper = Substitute.For<IMapper>();
            _fixedAssetManager = Substitute.For<IFixedAssetManager>();

            _fixedAssets = new List<FixedAsset>();
            _fixedAssetsDto = new List<FixedAssetDto>();
            for (int i = 0; i < 10; ++i)
            {
                // Tạo một thực thể FixedAsset và FixedAssetDto
                var FixedAsset = new FixedAsset();
                var FixedAssetDto = new FixedAssetDto();

                // Thêm FixedAsset vào danh sách đầu ra
                _fixedAssets.Add(FixedAsset);

                // Thiết lập giá trị return cho mapper
                _mapper.Map<FixedAssetDto>(FixedAsset).Returns(FixedAssetDto);

                // Thêm FixedAssetDto vào danh sách sau khi được map
                _fixedAssetsDto.Add(FixedAssetDto);
            }
        }

        /// <summary>
        /// Hàm test lấy tất cả bản ghi trong cơ sở dữ liệu
        /// </summary>
        /// <returns>Thành công trả về 10 bản ghi</returns>
        /// Created by: NTLam (23/08/2023)
        [Test]
        public async Task GetAllAsync_NoInput_ReturnList10FixedAssetDto()
        {
            // Arrange
            var expectedResult = _fixedAssetsDto;

            _fixedAssetRepository.GetAllAsync().Returns(_fixedAssets);
            var fixedAssetService = new FixedAssetService(_fixedAssetRepository, _departmentRepository, _fixedAssetCategoryRepository, _fixedAssetManager, _mapper);

            // Act
            var actualResult = await fixedAssetService.GetAllAsync();

            // Assert
            Assert.That(actualResult, Is.EqualTo(expectedResult));

            await _fixedAssetRepository.Received(1).GetAllAsync();
        }

        /// <summary>
        /// Hàm test lấy bản ghi theo Id 
        /// </summary>
        /// <returns>Thành công lấy được bản ghi và map sang Dto</returns>
        /// Created by: NTLam (23/08/2023)
        [Test]
        public async Task GetAsync_FixedAssetExists_Success()
        {
            // Arrange
            var id = Guid.NewGuid();
            var fixedAsset = new FixedAsset() { FixedAssetId = id };
            var fixedAssetDto = new FixedAssetDto() { FixedAssetId = id };

            _mapper.Map<FixedAssetDto>(fixedAsset).Returns(fixedAssetDto);
            _fixedAssetRepository.GetAsync(id).Returns(fixedAsset);
            var fixedAssetService = new FixedAssetService(_fixedAssetRepository, _departmentRepository, _fixedAssetCategoryRepository, _fixedAssetManager, _mapper);

            // Act
            var actualResult = await fixedAssetService.GetAsync(id);

            // Assert
            Assert.That(actualResult, Is.EqualTo(fixedAssetDto));

            await _fixedAssetRepository.Received(1).GetAsync(id);
        }

        /// <summary>
        /// Hàm test thêm mới bản ghi
        /// </summary>
        /// <returns>Thành công thêm mới bản ghi và trả về bản ghi được Map sang Dto</returns>
        /// Created by: NTLam (23/08/2023)
        [Test]
        public async Task InsertAsync_ValidateEntity_Success()
        {
            // Arrage
            var fixedAssetCreateDto = new FixedAssetCreateDto();
            var fixedAsset = new FixedAsset();
            var fixedAssetDto = new FixedAssetDto();

            _mapper.Map<FixedAsset>(fixedAssetCreateDto).Returns(fixedAsset);
            _mapper.Map<FixedAssetDto>(fixedAsset).Returns(fixedAssetDto);

            var FixedAssetService = new FixedAssetService(_fixedAssetRepository, _departmentRepository, _fixedAssetCategoryRepository, _fixedAssetManager, _mapper);

            // Act
            var actualResult = await FixedAssetService.InsertAsync(fixedAssetCreateDto);

            // Assert
            Assert.That(actualResult, Is.EqualTo(fixedAssetDto));

            await _fixedAssetRepository.Received(1).InsertAsync(fixedAsset);
        }

        /// <summary>
        /// Hàm test cập nhật bản ghi
        /// </summary>
        /// <returns>Thành công cập nhật bản ghi và trả về bản ghi được Map sang Dto</returns>
        /// Created by: NTLam (23/08/2023)
        [Test]
        public async Task UpdateAsync_EntityHasBeenValidated_Success()
        {
            // Arrange 
            var id = Guid.NewGuid();
            var FixedAssetUpdateDto = new FixedAssetUpdateDto();
            var FixedAsset = new FixedAsset();
            var FixedAssetDto = new FixedAssetDto();

            _mapper.Map<FixedAsset>(FixedAssetUpdateDto).Returns(FixedAsset);
            _mapper.Map<FixedAssetDto>(FixedAsset).Returns(FixedAssetDto);
            _fixedAssetRepository.GetAsync(id).Returns(FixedAsset);

            var FixedAssetService = new FixedAssetService(_fixedAssetRepository, _departmentRepository, _fixedAssetCategoryRepository, _fixedAssetManager, _mapper);

            // Act
            var actualResult = await FixedAssetService.UpdateAsync(id, FixedAssetUpdateDto);

            // Assert
            Assert.That(actualResult, Is.EqualTo(FixedAssetDto));

            await _fixedAssetRepository.Received(1).GetAsync(id);
            await _fixedAssetRepository.Received(1).UpdateAsync(FixedAsset);
        }

        /// <summary>
        /// Hàm test xóa bản ghi đã tồn tại trong hệ thống
        /// </summary>
        /// <returns>Xóa bản ghi thành công</returns>
        /// Created by: NTLam (23/08/2023)
        [Test]
        public async Task DeleteAsync_RecordExists_Success()
        {
            // Arrange
            var id = Guid.NewGuid();
            int expectedResult = 1;
            var fixedAsset = new FixedAsset()
            {
                FixedAssetId = id
            };

            _fixedAssetRepository.GetAsync(id).Returns(fixedAsset);
            _fixedAssetRepository.DeleteAsync(fixedAsset.FixedAssetId).Returns(1);

            var FixedAssetService = new FixedAssetService(_fixedAssetRepository, _departmentRepository, _fixedAssetCategoryRepository, _fixedAssetManager, _mapper);

            // Act
            var actualResult = await FixedAssetService.DeleteAsync(id);

            // Assert
            Assert.That(actualResult, Is.EqualTo(expectedResult));

            //await _fixedAssetRepository.Received(1).GetAsync(id);
            //await _fixedAssetRepository.Received(1).DeleteAsync(fixedAsset.FixedAssetId);
        }

        /// <summary>
        /// Hàm test xóa nhiều bản ghi trong trường hợp tất cả bản ghi đều tồn tại trong hệ thống
        /// </summary>
        /// <returns>Xóa thành công tất cả bản ghi</returns>
        /// Created by: NTLam (23/08/2023)
        [Test]
        public async Task DeleteMultiAsync_AllRecordExists_Success()
        {
            // Arrange
            int expectedResult = 5;
            var ids = new List<Guid>();
            var fixedAssets = new List<FixedAsset>();

            for (int i = 0; i < 5; i++)
            {
                var id = Guid.NewGuid();
                var FixedAsset = new FixedAsset() { FixedAssetId = id };

                ids.Add(id);
                fixedAssets.Add(FixedAsset);
            }

            _fixedAssetRepository.GetByListIdAsync(ids).Returns(fixedAssets);
            _fixedAssetRepository.DeleteMultiAsync(ids).Returns(5);

            var FixedAssetService = new FixedAssetService(_fixedAssetRepository, _departmentRepository, _fixedAssetCategoryRepository, _fixedAssetManager, _mapper);

            // Act
            var actualResult = await FixedAssetService.DeleteMultiAsync(ids);

            // Assert
            Assert.That(actualResult, Is.EqualTo(expectedResult));

            await _fixedAssetRepository.Received(1).GetByListIdAsync(ids);
            await _fixedAssetRepository.Received(1).DeleteMultiAsync(ids);
        }

        /// <summary>
        /// Hàm test xóa nhiều bản ghi trong trường hợp một số bản ghi không tồn tại trong hệ thống
        /// </summary>
        /// <returns>Bắn ra Exception thông báo về các bản ghi không tồn tại</returns>
        /// Created by: NTLam (23/08/2023)
        [Test]
        public async Task DeleteMultiAsync_NotExistsRecords_Exception()
        {
            // Arrange
            var ids = new List<Guid>();
            var idsError = new List<Guid>();
            var fixedAssets = new List<FixedAsset>();

            for (int i = 0; i < 8; i++)
            {
                var id = Guid.NewGuid();
                var fixedAsset = new FixedAsset() { FixedAssetId = id };

                ids.Add(id);
                fixedAssets.Add(fixedAsset);
            }

            for (int i = 0; i < 2; i++)
            {
                var id = Guid.NewGuid();
                var fixedAsset = new FixedAsset() { FixedAssetId = Guid.NewGuid() };

                ids.Add(id);
                idsError.Add(id);
                fixedAssets.Add(fixedAsset);
            }
            string expectedResult = $"Một số id bản ghi không tồn tại: {string.Join(", ", idsError)}";

            _fixedAssetRepository.GetByListIdAsync(ids).Returns(fixedAssets);

            var FixedAssetService = new FixedAssetService(_fixedAssetRepository, _departmentRepository, _fixedAssetCategoryRepository, _fixedAssetManager, _mapper);

            // Act
            var handler = async () => await FixedAssetService.DeleteMultiAsync(ids);

            // Assert
            var exception = Assert.ThrowsAsync<NotFoundException>(async () => await handler());
            Assert.That(exception.Message, Is.EqualTo(expectedResult));

            await _fixedAssetRepository.Received(1).GetByListIdAsync(ids);
            await _fixedAssetRepository.Received(0).DeleteMultiAsync(fixedAssets.AsEnumerable().Select(d => d.FixedAssetId).ToList());
        }


        /// <summary>
        /// Hàm test Map EntityCreateDto sang Entity thành công
        /// </summary>
        /// <returns>Thành công</returns>
        /// Created by: NTLam (23/08/2023)
        [Test]
        public async Task MapEntityCreateDtoToEntity_FixedAssetCreateDto_Success()
        {
            // Arrange
            var fixedAsset = new FixedAsset();
            var fixedAssetCreateDto = new FixedAssetCreateDto()
            {
                FixedAssetCode = "DP-12345"
            };

            _mapper.Map<FixedAsset>(fixedAssetCreateDto).Returns(fixedAsset);
            var fixedAssetService = new FixedAssetService(_fixedAssetRepository, _departmentRepository, _fixedAssetCategoryRepository, _fixedAssetManager, _mapper);

            // Act
            var expectedResult = fixedAssetService.EntityCreateDtoToEntity(fixedAssetCreateDto);

            // Assert
            Assert.That(fixedAsset, Is.EqualTo(expectedResult));

            await _fixedAssetManager.CheckDuplicateCodeAsync(fixedAssetCreateDto.FixedAssetCode);
        }

        /// <summary>
        /// Hàm test Map Entity sang EntityDto thành công
        /// </summary>
        /// <returns>Thành công</returns>
        /// Created by: NTLam (23/08/2023)
        [Test]
        public async Task MapEntityToEntityDto_FixedAssetCreateDto_Success()
        {
            // Arrange
            var fixedAsset = new FixedAsset();
            var fixedAssetDto = new FixedAssetDto();

            _mapper.Map<FixedAssetDto>(fixedAsset).Returns(fixedAssetDto);
            var fixedAssetService = new FixedAssetService(_fixedAssetRepository, _departmentRepository, _fixedAssetCategoryRepository, _fixedAssetManager, _mapper);

            // Act
            var expectedResult = fixedAssetService.EntityToEntityDto(fixedAsset);

            // Assert
            Assert.That(fixedAssetDto, Is.EqualTo(expectedResult));
        }
    }
}
