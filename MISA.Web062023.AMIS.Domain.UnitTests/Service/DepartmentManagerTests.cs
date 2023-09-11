using NSubstitute;
using NSubstitute.ReturnsExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web062023.AMIS.Domain.UnitTests.Service
{
    /// <summary>
    /// Lớp test cho các hàm trong clss DepartmentRepository
    /// </summary>
    /// Created by: NTLAM (23/08/2023)
    [TestFixture]
    public class DepartmentManagerTests
    {
        private IDepartmentRepository _departmentRepository;

        /// <summary>
        /// Khởi tạo giá trị
        /// </summary>
        /// Created by: NTLAM (23/08/2023)
        [SetUp]
        public void SetUp()
        {
            _departmentRepository = Substitute.For<IDepartmentRepository>();
        }

        /// <summary>
        /// Hàm test trùng mã đầu vào là mã chưa tồn tại
        /// </summary>
        /// <returns>Mã không trùng</returns>
        /// Created by: NTLAM (23/08/2023)
        [Test]
        public async Task CheckDuplicateCodeAsync_DepartmentNotExists_Success()
        {
            // Arrange
            string code = "EM-NOTEXISTS";

            _departmentRepository.FindByCodeAsync(code).ReturnsNull();

            var departmentManager = new DepartmentManager(_departmentRepository);

            // Act
            await departmentManager.CheckDuplicateCodeAsync(code);

            // Assert
            await _departmentRepository.Received(1).FindByCodeAsync(code);

        }

        /// <summary>
        /// Hàm test trùng mã đầu vào là mã đã có trong hệ thống
        /// </summary>
        /// <returns>Ném ra 1 Exception</returns>
        /// Created by: NTLAM (23/08/2023)
        [Test]
        public async Task CheckDuplicateCodeAsync_DepartmentExists_Exception()
        {
            // Arrage
            string code = "EM-EXISTS";
            var department = new Department();

            _departmentRepository.FindByCodeAsync(code).Returns(department);

            var DepartmentManager = new DepartmentManager(_departmentRepository);

            // Act
            var handler = async () => await DepartmentManager.CheckDuplicateCodeAsync(code);

            // Assert
            Assert.ThrowsAsync<ConflictException>(async () => await handler());
            await _departmentRepository.Received(1).FindByCodeAsync(code);
        }
    }
}
