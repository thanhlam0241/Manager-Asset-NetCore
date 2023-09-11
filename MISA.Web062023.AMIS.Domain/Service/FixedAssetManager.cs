using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web062023.AMIS.Domain
{

    /// <summary>
    /// The fixed asset manager.
    /// </summary>
    /// CreatedBy: NTLam (10/8/2023)
    public class FixedAssetManager : BaseManager<FixedAsset>, IFixedAssetManager
    {
        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="baseRepository">The base repository.</param>
        /// CreatedBy: NTLam (10/8/2023)
        public FixedAssetManager(IFixedAssetRepository fixedAssetRepository) : base(fixedAssetRepository)
        {
        }
    }
}
