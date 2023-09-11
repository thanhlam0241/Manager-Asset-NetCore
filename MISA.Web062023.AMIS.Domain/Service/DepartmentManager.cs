using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web062023.AMIS.Domain
{

    /// <summary>
    /// The department manager.
    /// </summary>
    /// CreatedBy: NTLam (10/8/2023)
    public class DepartmentManager : BaseManager<Department>, IDepartmentManager
    {

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="baseRepository">The base repository.</param>
        /// CreatedBy: NTLam (10/8/2023)
        public DepartmentManager(IDepartmentRepository departmentRepository) : base(departmentRepository)
        {
        }
    }
}
