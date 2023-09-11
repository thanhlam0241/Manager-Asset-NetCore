using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web062023.AMIS.Application
{
    /// <summary>
    /// Interface khai báo phương thức chỉ đọc
    /// </summary>
    /// <typeparam name="TEntityDto">Thực thể hiển thị cho người dùng</typeparam>
    /// Created by: Nguyễn Thanh Lâm (17/08/2023)
    public interface IReadonlyService<TEntityDto> where TEntityDto : IBaseDto
    {
        /// <summary>
        /// Lấy toàn bộ bản ghi
        /// </summary>
        /// <returns>Danh sách bản ghi</returns>
        /// Created by: Nguyễn Thanh Lâm (17/08/2023)
        Task<List<TEntityDto>> GetAllAsync();

        /// <summary>
        /// Lấy bản ghi theo Id
        /// </summary>
        /// <param name="emulaitonId">Id của bản ghi</param>
        /// <returns>Bản ghi</returns>
        /// Created by: Nguyễn Thanh Lâm (17/08/2023)
        Task<TEntityDto> GetAsync(Guid id);

        /// <summary>
        /// Lấy giới hạn số bản ghi bản ghi
        /// </summary>
        /// <param name="limit">Số lượng bản ghi</param>
        /// <param name="offset">Vị trí bắt đầu lấy</param>
        /// <returns>Danh sách bản ghi</returns>
        /// Created by: Nguyễn Thanh Lâm (17/08/2023)
        Task<List<TEntityDto>> GetFilterAsync(int limit, int offset);
    }
}
