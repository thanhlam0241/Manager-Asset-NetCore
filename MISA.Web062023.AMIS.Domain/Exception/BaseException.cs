using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MISA.Web062023.AMIS.Domain
{

    /// <summary>
    /// The base exception.
    /// </summary>
    /// Created by: Nguyễn Thanh Lâm (14/08/2023)
    public class BaseException
    {
        #region Properties
        /// <summary>
        /// Error code
        /// </summary>
        /// Created by: Nguyễn Thanh Lâm (14/08/2023)
        public int ErrorCode { get; set; }

        /// <summary>
        /// Cảnh báo cho Deverloper
        /// </summary>
        /// Created by: Nguyễn Thanh Lâm (14/08/2023)
        public dynamic? DevMessage { get; set; }

        /// <summary>
        /// Cảnh báo cho người dùng
        /// </summary>
        /// Created by: Nguyễn Thanh Lâm (14/08/2023)
        public dynamic? UserMessage { get; set; }

        /// <summary>
        /// Trace Id error
        /// </summary>
        /// Created by: Nguyễn Thanh Lâm (14/08/2023)
        public string? TraceId { get; set; }

        /// <summary>
        /// Thông tin thêm
        /// </summary>
        /// Created by: Nguyễn Thanh Lâm (14/08/2023)
        public string? MoreInfo { get; set; }

        /// <summary>
        /// Các đối tượng lỗi
        /// </summary>
        /// Created by: Nguyễn Thanh Lâm (14/08/2023)
        public object? Errors { get; set; }
        #endregion

        #region Methods
        /// <summary>
        /// Override hàm chuyển đổi đối tượng thành chuỗi tương ứng
        /// </summary>
        /// <returns>Chuỗi của đối tượng</returns>
        /// Created by: Nguyễn Thanh Lâm (14/08/2023)
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
        #endregion
    }
}
