using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web062023.AMIS.Domain
{

    /// <summary>
    /// The interface base manager.
    /// </summary>
    /// CreatedBy: NTLam (10/8/2023)
    public interface IBaseManager<TEntity> where TEntity : IEntity
    {
        /// <summary>
        /// The check duplicate code.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns>The result.</returns>
        /// CreatedBy: NTLam (10/8/2023)
        public Task CheckDuplicateCodeAsync(string code);
        /// <summary>
        /// The check is exist.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The result.</returns>
        /// CreatedBy: NTLam (10/8/2023)
        public Task CheckIsExistIdAsync(Guid id);

        /// <summary>
        /// The check is code exist in list codes async.
        /// </summary>
        /// <param name="codes">The codes.</param>
        /// <returns>The result.</returns>
        /// CreatedBy: NTLam (10/8/2023)
        public Task CheckIsCodeExistInListCodesAsync(List<string> codes);
    }
}
