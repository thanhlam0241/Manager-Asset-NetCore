namespace MISA.Web062023.AMIS.Domain
{

    /// <summary>
    /// The filter fixed asset.
    /// </summary>
    /// Author: Nguyễn Thanh Lâm
    /// Modified date: 10/8/2023
    public class FilterFixedAsset
    {

        /// <summary>
        /// Gets or Sets the total page.
        /// </summary>
        /// Author: Nguyễn Thanh Lâm
        /// Modified date: 10/8/2023
        public long? TotalPage { get; set; }

        /// <summary>
        /// Gets or Sets the total record.
        /// </summary>
        /// Author: Nguyễn Thanh Lâm
        /// Modified date: 10/8/2023
        public long? TotalRecord { get; set; }

        /// <summary>
        /// Gets or Sets the current page.
        /// </summary>
        /// Author: Nguyễn Thanh Lâm
        /// Modified date: 10/8/2023
        public long? CurrentPage { get; set; }

        /// <summary>
        /// Gets or Sets the current record.
        /// </summary>
        /// Author: Nguyễn Thanh Lâm
        /// Modified date: 10/8/2023
        public long? CurrentRecord { get; set; }

        /// <summary>
        /// Gets or Sets the current page size.
        /// </summary>
        /// Author: Nguyễn Thanh Lâm
        /// Modified date: 10/8/2023
        public long? CurrentPageSize { get; set; }

        /// <summary>
        /// Gets or Sets the data.
        /// </summary>
        /// Author: Nguyễn Thanh Lâm
        /// Modified date: 10/8/2023
        public IEnumerable<FixedAsset> Data { get; set; } = Enumerable.Empty<FixedAsset>();
    }
}
