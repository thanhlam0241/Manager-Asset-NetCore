using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web062023.AMIS.Domain
{

    /// <summary>
    /// The not found exception.
    /// </summary>
    /// Created by: Nguyễn Thanh Lâm (14/08/2023)
    public class NotFoundException : Exception
    {

        /// <summary>
        /// Gets or Sets the error code.
        /// </summary>
        /// Created by: Nguyễn Thanh Lâm (14/08/2023)
        public int ErrorCode { get; set; }

        /// <summary>
        /// The .ctor.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// Created by: Nguyễn Thanh Lâm (14/08/2023)
        public NotFoundException(int errorCode)
        {
            ErrorCode = errorCode;
        }

        /// <summary>
        /// The .ctor.
        /// </summary>
        /// <param name="message">The message.</param>
        /// Created by: Nguyễn Thanh Lâm (14/08/2023)
        public NotFoundException(string message) : base(message) { }

        /// <summary>
        /// The .ctor.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="errorCode">The error code.</param>
        /// Created by: Nguyễn Thanh Lâm (14/08/2023)
        public NotFoundException(string message, int errorCode) : base(message)
        {
            ErrorCode = errorCode;
        }
    }
}
