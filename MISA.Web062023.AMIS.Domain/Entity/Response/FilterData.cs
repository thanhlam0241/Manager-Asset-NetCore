using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web062023.AMIS.Domain
{

    /// <summary>
    /// The filter data.
    /// </summary>
    /// Modified date: 10/8/2023
    public class FilterData<T> where T : class
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
        public IEnumerable<T> Data { get; set; } = Enumerable.Empty<T>();
    }
}
