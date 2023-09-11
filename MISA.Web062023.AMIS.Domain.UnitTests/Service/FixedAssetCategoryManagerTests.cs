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
    /// Lớp test cho các hàm trong clss FixedAssetCategoryRepository
    /// </summary>
    /// Created by: NTLAM (23/08/2023)
    [TestFixture]
    public class FixedAssetCategoryManagerTests
    {
        private IFixedAssetCategoryRepository _fixedAssetCategoryRepository;

        /// <summary>
        /// Khởi tạo giá trị
        /// </summary>
        /// Created by: NTLAM (23/08/2023)
        [SetUp]
        public void SetUp()
        {
            _fixedAssetCategoryRepository = Substitute.For<IFixedAssetCategoryRepository>();
        }

        /// <summary>
        /// Hàm test trùng mã đầu vào là mã chưa tồn tại
        /// </summary>
        /// <returns>Mã không trùng</returns>
        /// Created by: NTLAM (23/08/2023)
        [Test]
        public async Task CheckDuplicateCodeAsync_FixedAssetCategoryNotExists_Success()
        {
            // Arrange
            string code = "FAC-NOTEXISTS";

            _fixedAssetCategoryRepository.FindByCodeAsync(code).ReturnsNull();

            var fixedAssetCategoryManager = new FixedAssetCategoryManager(_fixedAssetCategoryRepository);

            // Act
            await fixedAssetCategoryManager.CheckDuplicateCodeAsync(code);

            // Assert
            await _fixedAssetCategoryRepository.Received(1).FindByCodeAsync(code);

        }

        /// <summary>
        /// Hàm test trùng mã đầu vào là mã đã có trong hệ thống
        /// </summary>
        /// <returns>Ném ra 1 Exception</returns>
        /// Created by: NTLAM (23/08/2023)
        [Test]
        public async Task CheckDuplicateCodeAsync_FixedAssetCategoryExists_Exception()
        {
            // Arrage
            string code = "FAC-EXISTS";
            var fixedAssetCategory = new FixedAssetCategory();

            _fixedAssetCategoryRepository.FindByCodeAsync(code).Returns(fixedAssetCategory);

            var FixedAssetCategoryManager = new FixedAssetCategoryManager(_fixedAssetCategoryRepository);

            // Act
            var handler = async () => await FixedAssetCategoryManager.CheckDuplicateCodeAsync(code);

            // Assert
            Assert.ThrowsAsync<ConflictException>(async () => await handler());
            await _fixedAssetCategoryRepository.Received(1).FindByCodeAsync(code);
        }
    }
}
