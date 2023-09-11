using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web062023.AMIS.Application
{

    /// <summary>
    /// The i base create dto.
    /// </summary>
    public interface IBaseDto
    {
        /// <summary>
        /// The get code.
        /// </summary>
        /// <returns>The result.</returns>
        public string GetCode();

        /// <summary>
        /// The set code.
        /// </summary>
        /// <param name="code">The code.</param>
        public void SetCode(string code);
    }
}
