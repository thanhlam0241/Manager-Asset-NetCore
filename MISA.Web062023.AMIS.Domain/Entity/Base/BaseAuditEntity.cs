namespace MISA.Web062023.AMIS.Domain
{

    /// <summary>
    /// The base audit entity.
    /// </summary>
    /// Created By Nguyễn Thanh Lâm (14/08/2023)
    public abstract class BaseAuditEntity
    {
        /// <summary>
        /// Người tạo bản ghi
        /// </summary>
        /// Created By Nguyễn Thanh Lâm (14/08/2023)
        public string? CreatedBy { get; set; }

        /// <summary>
        /// Ngày tạo bản ghi
        /// </summary>
        /// Created By Nguyễn Thanh Lâm (14/08/2023)
        public DateTimeOffset? CreatedDate { get; set; }

        /// <summary>
        /// Người chỉnh sửa bản ghi
        /// </summary>
        /// Created By Nguyễn Thanh Lâm (14/08/2023)
        public string? ModifiedBy { get; set; }

        /// <summary>
        /// Ngày chỉnh sửa bản ghi
        /// </summary>
        /// Created By Nguyễn Thanh Lâm (14/08/2023)
        public DateTimeOffset? ModifiedDate { get; set; }
    }
}
