using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web062023.AMIS.Application
{
    /// <summary>
    /// Interface khai báo các phương thức đọc, thêm, sửa, xóa
    /// </summary>
    /// <typeparam name="TEntityDto">Thực thể hiển thị cho người dùng</typeparam>
    /// <typeparam name="TEntityCreateDto">Thực thể tạo mới</typeparam>
    /// <typeparam name="TEntityUpdateDto">Thực thể chỉnh sửa</typeparam>
    /// Created by: Nguyễn Thanh Lâm (17/08/2023)
    public interface ICrudService<TEntityDto, TEntityCreateDto, TEntityUpdateDto> : IReadonlyService<TEntityDto>
        where TEntityDto : IBaseDto where TEntityCreateDto : IBaseDto where TEntityUpdateDto : IBaseDto
    {
        /// <summary>
        /// Thêm mới bản ghi
        /// </summary>
        /// <param name="entityCreateDto">Bản ghi</param>
        /// <returns>Bản ghi</returns>
        /// Created by: Nguyễn Thanh Lâm (17/08/2023)
        Task<int> InsertAsync(TEntityCreateDto entityCreateDto);

        /// <summary>
        /// Thêm mới bản ghi
        /// </summary>
        /// <param name="entityCreateDto">Bản ghi</param>
        /// <returns>Bản ghi</returns>
        /// Created by: Nguyễn Thanh Lâm (17/08/2023)
        Task<int> InsertMultiAsync(List<TEntityCreateDto> entityCreateDto);

        /// <summary>
        /// Chỉnh sửa bản ghi
        /// </summary>
        /// <param name="emulation">Bản ghi</param>
        /// <returns>Bản ghi</returns>
        /// Created by: Nguyễn Thanh Lâm (17/08/2023)
        Task<int> UpdateAsync(Guid id, TEntityUpdateDto entityUpdateDto);

        /// <summary>
        /// Xóa bản ghi
        /// </summary>
        /// <param name="id">Id bản ghi</param>
        /// <returns>Số lượng bản ghi đã xóa</returns>
        /// Created by: Nguyễn Thanh Lâm (17/08/2023)
        Task<int> DeleteAsync(Guid id);

        /// <summary>
        /// Xóa nhiều bản ghi
        /// </summary>
        /// <param name="ids">Danh sách id bản ghi</param>
        /// <returns>Số lượng dbản ghi đã xóa</returns>
        /// Created by: Nguyễn Thanh Lâm (17/08/2023)
        Task<int> DeleteMultiAsync(List<Guid> ids);
    }
}
