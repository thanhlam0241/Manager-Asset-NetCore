using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web062023.AMIS.Domain
{

    /// <summary>
    /// The filter fixed asset request.
    /// </summary>
    /// Created by: NTLam (12/08/2023)
    public class FilterFixedAssetRequest
    {

        /// <summary>
        /// Gets or Sets the department ids.
        /// </summary>
        /// Created by: NTLam (12/08/2023)
        public List<Guid>? DepartmentIds { get; set; }

        /// <summary>
        /// Gets or Sets the fixed asset category ids.
        /// </summary>
        /// Created by: NTLam (12/08/2023)
        public List<Guid>? FixedAssetCategoryIds { get; set; }
    }
}
