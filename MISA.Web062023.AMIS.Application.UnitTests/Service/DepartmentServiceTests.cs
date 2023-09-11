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
    /// Lớp test cho các hàm trong class DepartmentService
    /// </summary>
    /// Created by: NTLam (23/08/2023)
    [TestFixture]
    public class DepartmentServiceTests
    {
        private IDepartmentRepository _departmentRepository;
        private IMapper _mapper;
        private IDepartmentManager _departmentManager;
        private List<Department> _departments;
        private List<DepartmentDto> _departmentsDto;


        /// <summary>
        /// Khởi tạo giá trị
        /// </summary>
        /// Created by: NTLam (23/08/2023)
        [SetUp]
        public void SetUp()
        {
            _departmentRepository = Substitute.For<IDepartmentRepository>();
            _mapper = Substitute.For<IMapper>();
            _departmentManager = Substitute.For<IDepartmentManager>();

            _departments = new List<Department>();
            _departmentsDto = new List<DepartmentDto>();
            for (int i = 0; i < 10; ++i)
            {
                // Tạo một thực thể Department và DepartmentDto
                var Department = new Department();
                var DepartmentDto = new DepartmentDto();

                // Thêm Department vào danh sách đầu ra
                _departments.Add(Department);

                // Thiết lập giá trị return cho mapper
                _mapper.Map<DepartmentDto>(Department).Returns(DepartmentDto);

                // Thêm DepartmentDto vào danh sách sau khi được map
                _departmentsDto.Add(DepartmentDto);
            }
        }

        /// <summary>
        /// Hàm test lấy tất cả bản ghi trong cơ sở dữ liệu
        /// </summary>
        /// <returns>Thành công trả về 10 bản ghi</returns>
        /// Created by: NTLam (23/08/2023)
        [Test]
        public async Task GetAllAsync_NoInput_ReturnList10DepartmentDto()
        {
            // Arrange
            var expectedResult = _departmentsDto;

            _departmentRepository.GetAllAsync().Returns(_departments);
            var DepartmentService = new DepartmentService(_departmentRepository, _departmentManager, _mapper);

            // Act
            var actualResult = await DepartmentService.GetAllAsync();

            // Assert
            Assert.That(actualResult, Is.EqualTo(expectedResult));

            await _departmentRepository.Received(1).GetAllAsync();
        }

        /// <summary>
        /// Hàm test lấy bản ghi theo Id 
        /// </summary>
        /// <returns>Thành công lấy được bản ghi và map sang Dto</returns>
        /// Created by: NTLam (23/08/2023)
        [Test]
        public async Task GetAsync_DepartmentExists_Success()
        {
            // Arrange
            var id = Guid.NewGuid();
            var Department = new Department() { DepartmentId = id };
            var DepartmentDto = new DepartmentDto() { DepartmentId = id };

            _mapper.Map<DepartmentDto>(Department).Returns(DepartmentDto);
            _departmentRepository.GetAsync(id).Returns(Department);
            var DepartmentService = new DepartmentService(_departmentRepository, _departmentManager, _mapper);

            // Act
            var actualResult = await DepartmentService.GetAsync(id);

            // Assert
            Assert.That(actualResult, Is.EqualTo(DepartmentDto));

            await _departmentRepository.Received(1).GetAsync(id);
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
            var DepartmentCreateDto = new DepartmentCreateDto();
            var Department = new Department();
            var DepartmentDto = new DepartmentDto();

            _mapper.Map<Department>(DepartmentCreateDto).Returns(Department);
            _mapper.Map<DepartmentDto>(Department).Returns(DepartmentDto);

            var DepartmentService = new DepartmentService(_departmentRepository, _departmentManager, _mapper);

            // Act
            var actualResult = await DepartmentService.InsertAsync(DepartmentCreateDto);

            // Assert
            Assert.That(actualResult, Is.EqualTo(DepartmentDto));

            await _departmentRepository.Received(1).InsertAsync(Department);
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
            var departmentUpdateDto = new DepartmentUpdateDto();
            var department = new Department();
            var departmentDto = new DepartmentDto();

            _mapper.Map<Department>(departmentUpdateDto).Returns(department);
            _mapper.Map<DepartmentDto>(department).Returns(departmentDto);
            _departmentRepository.GetAsync(id).Returns(department);

            var DepartmentService = new DepartmentService(_departmentRepository, _departmentManager, _mapper);

            // Act
            var actualResult = await DepartmentService.UpdateAsync(id, departmentUpdateDto);

            // Assert
            Assert.That(actualResult, Is.EqualTo(departmentDto));

            await _departmentRepository.Received(1).GetAsync(id);
            await _departmentRepository.Received(1).UpdateAsync(department);
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
            var department = new Department()
            {
                DepartmentId = id,
            };

            _departmentRepository.GetAsync(id).Returns(department);
            _departmentRepository.DeleteAsync(department.DepartmentId).Returns(1);

            var DepartmentService = new DepartmentService(_departmentRepository, _departmentManager, _mapper);

            // Act
            var actualResult = await DepartmentService.DeleteAsync(id);

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
            var departments = new List<Department>();

            for (int i = 0; i < 5; i++)
            {
                var id = Guid.NewGuid();
                var Department = new Department() { DepartmentId = id };

                ids.Add(id);
                departments.Add(Department);
            }

            _departmentRepository.GetByListIdAsync(ids).Returns(departments);
            _departmentRepository.DeleteMultiAsync(ids).Returns(5);

            var departmentService = new DepartmentService(_departmentRepository, _departmentManager, _mapper);

            // Act
            var actualResult = await departmentService.DeleteMultiAsync(ids);

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
            var departments = new List<Department>();

            for (int i = 0; i < 4; i++)
            {
                var id = Guid.NewGuid();
                var department = new Department() { DepartmentId = id };

                ids.Add(id);
                departments.Add(department);
            }

            for (int i = 0; i < 4; i++)
            {
                var id = Guid.NewGuid();
                var department = new Department() { DepartmentId = Guid.NewGuid() };

                ids.Add(id);
                idsError.Add(id);
                departments.Add(department);
            }
            string expectedResult = $"Một số id bản ghi không tồn tại: {string.Join(", ", idsError)}";

            _departmentRepository.GetByListIdAsync(ids).Returns(departments);

            var DepartmentService = new DepartmentService(_departmentRepository, _departmentManager, _mapper);

            // Act
            var handler = async () => await DepartmentService.DeleteMultiAsync(ids);

            // Assert
            var exception = Assert.ThrowsAsync<NotFoundException>(async () => await handler());
            Assert.That(exception.Message, Is.EqualTo(expectedResult));

            await _departmentRepository.Received(1).GetByListIdAsync(ids);
            await _departmentRepository.Received(0).DeleteMultiAsync(departments.AsEnumerable().Select(d => d.DepartmentId).ToList());
        }


        /// <summary>
        /// Hàm test Map EntityCreateDto sang Entity thành công
        /// </summary>
        /// <returns>Thành công</returns>
        /// Created by: NTLam (23/08/2023)
        [Test]
        public async Task MapEntityCreateDtoToEntity_DepartmentCreateDto_Success()
        {
            // Arrange
            var department = new Department();
            var departmentCreateDto = new DepartmentCreateDto()
            {
                DepartmentCode = "DP-12345"
            };

            _mapper.Map<Department>(departmentCreateDto).Returns(department);
            var departmentService = new DepartmentService(_departmentRepository, _departmentManager, _mapper);

            // Act
            var expectedResult = departmentService.EntityCreateDtoToEntity(departmentCreateDto);

            // Assert
            Assert.That(department, Is.EqualTo(expectedResult));

            await _departmentManager.CheckDuplicateCodeAsync(departmentCreateDto.DepartmentCode);
        }

        /// <summary>
        /// Hàm test Map Entity sang EntityDto thành công
        /// </summary>
        /// <returns>Thành công</returns>
        /// Created by: NTLam (23/08/2023)
        [Test]
        public async Task MapEntityToEntityDto_DepartmentCreateDto_Success()
        {
            // Arrange
            var department = new Department();
            var departmentDto = new DepartmentDto();

            _mapper.Map<DepartmentDto>(department).Returns(departmentDto);
            var departmentService = new DepartmentService(_departmentRepository, _departmentManager, _mapper);

            // Act
            var expectedResult = departmentService.EntityToEntityDto(department);

            // Assert
            Assert.That(departmentDto, Is.EqualTo(expectedResult));
        }

        /// <summary>
        /// Hàm test Map EntityUpdateDto sang Entity khi mã phòng ban không thay đổi
        /// </summary>
        /// <returns>Thành công</returns>
        /// Created by: NTLam (23/08/2023)
        //[Test]
        //public async Task MapEntityUpdateDtoToEntity_DepartmentCodeNotChange_Success()
        //{
        //    // Arrange
        //    var id = Guid.NewGuid();
        //    var departmentUpdateDto = new DepartmentUpdateDto()
        //    {
        //        DepartmentCode = "DP-12345"
        //    };
        //    var department = new Department();

        //    _departmentRepository.GetAsync(id).Returns(new Department() { DepartmentCode = "DP-12345" });

        //    _mapper.Map<Department>(departmentUpdateDto).Returns(department);
        //    var departmentService = new DepartmentService(_departmentRepository, _departmentManager, _mapper);

        //    // Act
        //    var expectedResult = departmentService.EntityUpdateDtoToEntity(id, departmentUpdateDto);

        //    // Assert
        //    Assert.That(department, Is.EqualTo(expectedResult));

        //    await _departmentRepository.Received(1).GetAsync(id);

        //    await _departmentManager.Received(0).CheckDuplicateCodeAsync(departmentUpdateDto.DepartmentCode);
        //}

    }
}
