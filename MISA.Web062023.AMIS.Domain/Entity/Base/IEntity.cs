using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web062023.AMIS.Domain
{
    /// <summary>
    /// Interface thực thể
    /// </summary>
    /// Created By NTLam (17/08/2023)
    public interface IEntity
    {
        /// <summary>
        /// Lấy Id thực thể
        /// </summary>
        /// <returns>Id thực thể</returns>
        /// Created By NTLam (17/08/2023)
        Guid GetId();

        /// <summary>
        /// Gán giá trị Id cho thực thể
        /// </summary>
        /// <param name="id">Id thực thể</param>
        /// Created By NTLam (17/08/2023)
        void SetId(Guid id);
    }
}
