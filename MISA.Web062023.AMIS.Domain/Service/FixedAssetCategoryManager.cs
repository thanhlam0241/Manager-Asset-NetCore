using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web062023.AMIS.Domain
{

    /// <summary>
    /// The fixed asset category manager.
    /// </summary>
    /// CreatedBy: NTLam (10/8/2023)
    public class FixedAssetCategoryManager : BaseManager<FixedAssetCategory>, IFixedAssetCategoryManager
    {

        /// <summary>
        /// The .ctor.
        /// </summary>
        /// <param name="baseRepository">The base repository.</param>
        /// CreatedBy: NTLam (10/8/2023)
        public FixedAssetCategoryManager(IFixedAssetCategoryRepository baseRepository) : base(baseRepository)
        {
        }
    }
}
