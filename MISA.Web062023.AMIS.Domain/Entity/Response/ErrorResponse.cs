namespace MISA.Web062023.AMIS.Domain
{

    /// <summary>
    /// The error response.
    /// </summary>
    /// Author: Nguyễn Thanh Lâm
    /// Modified date: 10/8/2023
    public class ErrorResponse
    {

        /// <summary>
        /// Gets or Sets the dev msg.
        /// </summary>
        /// Author: Nguyễn Thanh Lâm
        /// Modified date: 10/8/2023
        public string? DevMsg { get; set; }

        /// <summary>
        /// Gets or Sets the user msg.
        /// </summary>
        /// Author: Nguyễn Thanh Lâm
        /// Modified date: 10/8/2023
        public string? UserMsg { get; set; }

        /// <summary>
        /// Gets or Sets the data.
        /// </summary>
        /// Author: Nguyễn Thanh Lâm
        /// Modified date: 10/8/2023
        public dynamic? Data { get; set; }

        /// <summary>
        /// Gets or Sets the trace id.
        /// </summary>
        /// Author: Nguyễn Thanh Lâm
        /// Modified date: 10/8/2023
        public string? TraceId { get; set; }

        /// <summary>
        /// Gets or Sets the more info.
        /// </summary>
        /// Author: Nguyễn Thanh Lâm
        /// Modified date: 10/8/2023
        public string? MoreInfo { get; set; }
    }
}
