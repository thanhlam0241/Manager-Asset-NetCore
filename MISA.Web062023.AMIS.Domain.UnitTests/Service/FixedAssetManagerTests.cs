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
    /// Lớp test cho các hàm trong clss FixedAssetRepository
    /// </summary>
    /// Created by: NTLAM (23/08/2023)
    [TestFixture]
    public class FixedAssetManagerTests
    {
        private IFixedAssetRepository _fixedAssetRepository;

        /// <summary>
        /// Khởi tạo giá trị
        /// </summary>
        /// Created by: NTLAM (23/08/2023)
        [SetUp]
        public void SetUp()
        {
            _fixedAssetRepository = Substitute.For<IFixedAssetRepository>();
        }

        /// <summary>
        /// Hàm test trùng mã đầu vào là mã chưa tồn tại
        /// </summary>
        /// <returns>Mã không trùng</returns>
        /// Created by: NTLAM (23/08/2023)
        [Test]
        public async Task CheckDuplicateCodeAsync_FixedAssetNotExists_Success()
        {
            // Arrange
            string code = "ID-NOTEXISTS";

            _fixedAssetRepository.FindByCodeAsync(code).ReturnsNull();

            var fixedAssetManager = new FixedAssetManager(_fixedAssetRepository);

            // Act
            await fixedAssetManager.CheckDuplicateCodeAsync(code);

            // Assert
            await _fixedAssetRepository.Received(1).FindByCodeAsync(code);

        }

        /// <summary>
        /// Hàm test trùng mã đầu vào là mã đã có trong hệ thống
        /// </summary>
        /// <returns>Ném ra 1 Exception</returns>
        /// Created by: NTLAM (23/08/2023)
        [Test]
        public async Task CheckDuplicateCodeAsync_FixedAssetExists_Exception()
        {
            // Arrage
            string code = "ID-EXISTS";
            var fixedAsset = new RecordedAsset();

            _fixedAssetRepository.FindByCodeAsync(code).Returns(fixedAsset);

            var FixedAssetManager = new FixedAssetManager(_fixedAssetRepository);

            // Act
            var handler = async () => await FixedAssetManager.CheckDuplicateCodeAsync(code);

            // Assert
            Assert.ThrowsAsync<ConflictException>(async () => await handler());
            await _fixedAssetRepository.Received(1).FindByCodeAsync(code);
        }
    }
}
