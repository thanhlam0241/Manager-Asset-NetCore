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
    /// Lớp test cho các hàm trong class FixedAssetCategoryService
    /// </summary>
    /// Created by: NTLam (23/08/2023)
    [TestFixture]
    public class FixedAssetCategoryServiceTests
    {
        private IFixedAssetCategoryRepository _fixedAssetCategoryRepository;
        private IMapper _mapper;
        private IFixedAssetCategoryManager _fixedAssetCategoryManager;
        private List<FixedAssetCategory> _fixedAssetCategorys;
        private List<FixedAssetCategoryDto> _fixedAssetCategorysDto;


        /// <summary>
        /// Khởi tạo giá trị
        /// </summary>
        /// Created by: NTLam (23/08/2023)
        [SetUp]
        public void SetUp()
        {
            _fixedAssetCategoryRepository = Substitute.For<IFixedAssetCategoryRepository>();
            _mapper = Substitute.For<IMapper>();
            _fixedAssetCategoryManager = Substitute.For<IFixedAssetCategoryManager>();

            _fixedAssetCategorys = new List<FixedAssetCategory>();
            _fixedAssetCategorysDto = new List<FixedAssetCategoryDto>();
            for (int i = 0; i < 10; ++i)
            {
                // Tạo một thực thể FixedAssetCategory và FixedAssetCategoryDto
                var FixedAssetCategory = new FixedAssetCategory();
                var FixedAssetCategoryDto = new FixedAssetCategoryDto();

                // Thêm FixedAssetCategory vào danh sách đầu ra
                _fixedAssetCategorys.Add(FixedAssetCategory);

                // Thiết lập giá trị return cho mapper
                _mapper.Map<FixedAssetCategoryDto>(FixedAssetCategory).Returns(FixedAssetCategoryDto);

                // Thêm FixedAssetCategoryDto vào danh sách sau khi được map
                _fixedAssetCategorysDto.Add(FixedAssetCategoryDto);
            }
        }

        /// <summary>
        /// Hàm test lấy tất cả bản ghi trong cơ sở dữ liệu
        /// </summary>
        /// <returns>Thành công trả về 10 bản ghi</returns>
        /// Created by: NTLam (23/08/2023)
        [Test]
        public async Task GetAllAsync_NoInput_ReturnList10FixedAssetCategoryDto()
        {
            // Arrange
            var expectedResult = _fixedAssetCategorysDto;

            _fixedAssetCategoryRepository.GetAllAsync().Returns(_fixedAssetCategorys);
            var fixedAssetCategoryService = new FixedAssetCategoryService(_fixedAssetCategoryRepository, _fixedAssetCategoryManager, _mapper);

            // Act
            var actualResult = await fixedAssetCategoryService.GetAllAsync();

            // Assert
            Assert.That(actualResult, Is.EqualTo(expectedResult));

            await _fixedAssetCategoryRepository.Received(1).GetAllAsync();
        }

        /// <summary>
        /// Hàm test lấy bản ghi theo Id 
        /// </summary>
        /// <returns>Thành công lấy được bản ghi và map sang Dto</returns>
        /// Created by: NTLam (23/08/2023)
        [Test]
        public async Task GetAsync_FixedAssetCategoryExists_Success()
        {
            // Arrange
            var id = Guid.NewGuid();
            var fixedAssetCategory = new FixedAssetCategory() { FixedAssetCategoryId = id };
            var fixedAssetCategoryDto = new FixedAssetCategoryDto() { FixedAssetCategoryId = id };

            _mapper.Map<FixedAssetCategoryDto>(fixedAssetCategory).Returns(fixedAssetCategoryDto);
            _fixedAssetCategoryRepository.GetAsync(id).Returns(fixedAssetCategory);
            var fixedAssetCategoryService = new FixedAssetCategoryService(_fixedAssetCategoryRepository, _fixedAssetCategoryManager, _mapper);

            // Act
            var actualResult = await fixedAssetCategoryService.GetAsync(id);

            // Assert
            Assert.That(actualResult, Is.EqualTo(fixedAssetCategoryDto));

            await _fixedAssetCategoryRepository.Received(1).GetAsync(id);
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
            var fixedAssetCategoryCreateDto = new FixedAssetCategoryCreateDto();
            var FixedAssetCategory = new FixedAssetCategory();
            var FixedAssetCategoryDto = new FixedAssetCategoryDto();

            _mapper.Map<FixedAssetCategory>(fixedAssetCategoryCreateDto).Returns(FixedAssetCategory);
            _mapper.Map<FixedAssetCategoryDto>(FixedAssetCategory).Returns(FixedAssetCategoryDto);

            var FixedAssetCategoryService = new FixedAssetCategoryService(_fixedAssetCategoryRepository, _fixedAssetCategoryManager, _mapper);

            // Act
            var actualResult = await FixedAssetCategoryService.InsertAsync(fixedAssetCategoryCreateDto);

            // Assert
            Assert.That(actualResult, Is.EqualTo(FixedAssetCategoryDto));

            await _fixedAssetCategoryRepository.Received(1).InsertAsync(FixedAssetCategory);
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
            var FixedAssetCategoryUpdateDto = new FixedAssetCategoryUpdateDto();
            var FixedAssetCategory = new FixedAssetCategory();
            var FixedAssetCategoryDto = new FixedAssetCategoryDto();

            _mapper.Map<FixedAssetCategory>(FixedAssetCategoryUpdateDto).Returns(FixedAssetCategory);
            _mapper.Map<FixedAssetCategoryDto>(FixedAssetCategory).Returns(FixedAssetCategoryDto);
            _fixedAssetCategoryRepository.GetAsync(id).Returns(FixedAssetCategory);

            var FixedAssetCategoryService = new FixedAssetCategoryService(_fixedAssetCategoryRepository, _fixedAssetCategoryManager, _mapper);

            // Act
            var actualResult = await FixedAssetCategoryService.UpdateAsync(id, FixedAssetCategoryUpdateDto);

            // Assert
            Assert.That(actualResult, Is.EqualTo(FixedAssetCategoryDto));

            await _fixedAssetCategoryRepository.Received(1).GetAsync(id);
            await _fixedAssetCategoryRepository.Received(1).UpdateAsync(FixedAssetCategory);
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
            var fixedAssetCategory = new FixedAssetCategory()
            {
                FixedAssetCategoryId = id,
            };

            _fixedAssetCategoryRepository.GetAsync(id).Returns(fixedAssetCategory);
            _fixedAssetCategoryRepository.DeleteAsync(fixedAssetCategory.FixedAssetCategoryId).Returns(1);

            var FixedAssetCategoryService = new FixedAssetCategoryService(_fixedAssetCategoryRepository, _fixedAssetCategoryManager, _mapper);

            // Act
            var actualResult = await FixedAssetCategoryService.DeleteAsync(id);

            // Assert
            Assert.That(actualResult, Is.EqualTo(expectedResult));
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
            var fixedAssetCategorys = new List<FixedAssetCategory>();

            for (int i = 0; i < 5; i++)
            {
                var id = Guid.NewGuid();
                var FixedAssetCategory = new FixedAssetCategory() { FixedAssetCategoryId = id };

                ids.Add(id);
                fixedAssetCategorys.Add(FixedAssetCategory);
            }

            _fixedAssetCategoryRepository.GetByListIdAsync(ids).Returns(fixedAssetCategorys);
            _fixedAssetCategoryRepository.DeleteMultiAsync(ids).Returns(5);

            var fixedAssetCategoryService = new FixedAssetCategoryService(_fixedAssetCategoryRepository, _fixedAssetCategoryManager, _mapper);

            // Act
            var actualResult = await fixedAssetCategoryService.DeleteMultiAsync(ids);

            // Assert
            Assert.That(actualResult, Is.EqualTo(expectedResult));
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
            var fixedAssetCategorys = new List<FixedAssetCategory>();

            for (int i = 0; i < 8; i++)
            {
                var id = Guid.NewGuid();
                var fixedAssetCategory = new FixedAssetCategory() { FixedAssetCategoryId = id };

                ids.Add(id);
                fixedAssetCategorys.Add(fixedAssetCategory);
            }

            for (int i = 0; i < 2; i++)
            {
                var id = Guid.NewGuid();
                var fixedAssetCategory = new FixedAssetCategory() { FixedAssetCategoryId = Guid.NewGuid() };

                ids.Add(id);
                idsError.Add(id);
                fixedAssetCategorys.Add(fixedAssetCategory);
            }
            string expectedResult = $"Một số id bản ghi không tồn tại: {string.Join(", ", idsError)}";

            _fixedAssetCategoryRepository.GetByListIdAsync(ids).Returns(fixedAssetCategorys);

            var FixedAssetCategoryService = new FixedAssetCategoryService(_fixedAssetCategoryRepository, _fixedAssetCategoryManager, _mapper);

            // Act
            var handler = async () => await FixedAssetCategoryService.DeleteMultiAsync(ids);

            // Assert
            var exception = Assert.ThrowsAsync<NotFoundException>(async () => await handler());
            Assert.That(exception.Message, Is.EqualTo(expectedResult));

            await _fixedAssetCategoryRepository.Received(1).GetByListIdAsync(ids);
            await _fixedAssetCategoryRepository.Received(0).DeleteMultiAsync(fixedAssetCategorys.AsEnumerable().Select(d => d.FixedAssetCategoryId).ToList());
        }


        /// <summary>
        /// Hàm test Map EntityCreateDto sang Entity thành công
        /// </summary>
        /// <returns>Thành công</returns>
        /// Created by: NTLam (23/08/2023)
        [Test]
        public async Task MapEntityCreateDtoToEntity_FixedAssetCategoryCreateDto_Success()
        {
            // Arrange
            var fixedAssetCategory = new FixedAssetCategory();
            var fixedAssetCategoryCreateDto = new FixedAssetCategoryCreateDto()
            {
                FixedAssetCategoryCode = "DP-12345"
            };

            _mapper.Map<FixedAssetCategory>(fixedAssetCategoryCreateDto).Returns(fixedAssetCategory);
            var fixedAssetCategoryService = new FixedAssetCategoryService(_fixedAssetCategoryRepository, _fixedAssetCategoryManager, _mapper);

            // Act
            var expectedResult = fixedAssetCategoryService.EntityCreateDtoToEntity(fixedAssetCategoryCreateDto);

            // Assert
            Assert.That(fixedAssetCategory, Is.EqualTo(expectedResult));

            await _fixedAssetCategoryManager.CheckDuplicateCodeAsync(fixedAssetCategoryCreateDto.FixedAssetCategoryCode);
        }

        /// <summary>
        /// Hàm test Map Entity sang EntityDto thành công
        /// </summary>
        /// <returns>Thành công</returns>
        /// Created by: NTLam (23/08/2023)
        [Test]
        public async Task MapEntityToEntityDto_FixedAssetCategoryCreateDto_Success()
        {
            // Arrange
            var fixedAssetCategory = new FixedAssetCategory();
            var fixedAssetCategoryDto = new FixedAssetCategoryDto();

            _mapper.Map<FixedAssetCategoryDto>(fixedAssetCategory).Returns(fixedAssetCategoryDto);
            var fixedAssetCategoryService = new FixedAssetCategoryService(_fixedAssetCategoryRepository, _fixedAssetCategoryManager, _mapper);

            // Act
            var expectedResult = fixedAssetCategoryService.EntityToEntityDto(fixedAssetCategory);

            // Assert
            Assert.That(fixedAssetCategoryDto, Is.EqualTo(expectedResult));
        }

        /// <summary>
        /// Hàm test Map EntityUpdateDto sang Entity khi mã loại tài sản không thay đổi
        /// </summary>
        /// <returns>Thành công</returns>
        /// Created by: NTLam (23/08/2023)
        //[Test]
        //public async Task MapEntityUpdateDtoToEntity_FixedAssetCategoryCodeNotChange_Success()
        //{
        //    // Arrange
        //    var id = Guid.NewGuid();
        //    var fixedAssetCategoryUpdateDto = new FixedAssetCategoryUpdateDto()
        //    {
        //        FixedAssetCategoryCode = "DP-12345"
        //    };
        //    var fixedAssetCategory = new FixedAssetCategory();

        //    _fixedAssetCategoryRepository.GetAsync(id).Returns(new FixedAssetCategory() { FixedAssetCategoryCode = "DP-12345" });

        //    _mapper.Map<FixedAssetCategory>(fixedAssetCategoryUpdateDto).Returns(fixedAssetCategory);
        //    var fixedAssetCategoryService = new FixedAssetCategoryService(_fixedAssetCategoryRepository, _fixedAssetCategoryManager, _mapper);

        //    // Act
        //    var expectedResult = fixedAssetCategoryService.EntityUpdateDtoToEntity(id, fixedAssetCategoryUpdateDto);

        //    // Assert
        //    Assert.That(fixedAssetCategory, Is.EqualTo(expectedResult));

        //    await _fixedAssetCategoryRepository.Received(1).GetAsync(id);

        //    await _fixedAssetCategoryManager.Received(0).CheckDuplicateCodeAsync(fixedAssetCategoryUpdateDto.FixedAssetCategoryCode);
        //}

        /// <summary>
        /// Hàm test Map EntityUpdateDto sang Entity khi mã loại tài sản thay đổi
        /// </summary>
        /// <returns>Check mã trùng</returns>
        /// Created by: NTLam (23/08/2023)
        //[Test]
        //public async Task MapEntityUpdateDtoToEntity_FixedAssetCategoryChanged_CheckDuplicateCode()
        //{
        //    // Arrange
        //    var id = Guid.NewGuid();
        //    var fixedAssetCategoryUpdateDto = new FixedAssetCategoryUpdateDto()
        //    {
        //        FixedAssetCategoryCode = "DP-1234"
        //    };

        //    _fixedAssetCategoryRepository.GetAsync(id).Returns(new FixedAssetCategory() { FixedAssetCategoryCode = "DP-1234" });

        //    var fixedAssetCategoryService = new FixedAssetCategoryService(_fixedAssetCategoryRepository, _fixedAssetCategoryManager, _mapper);

        //    // Act
        //    fixedAssetCategoryService.EntityUpdateDtoToEntity(id, fixedAssetCategoryUpdateDto);

        //    // Assert
        //    await _fixedAssetCategoryRepository.Received(1).GetAsync(id);

        //    await _fixedAssetCategoryManager.Received(1).CheckDuplicateCodeAsync(fixedAssetCategoryUpdateDto.FixedAssetCategoryCode);
        //}
    }
}
