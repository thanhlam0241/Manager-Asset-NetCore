using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web062023.AMIS.Domain
{

    /// <summary>
    /// The base manager.
    /// </summary>
    /// CreatedBy: NTLam (10/8/2023)
    public abstract class BaseManager<TEntity> : IBaseManager<TEntity> where TEntity : IEntity
    {
        private readonly ICrudRepository<TEntity> _baseRepository;

        /// <summary>
        /// The .ctor.
        /// </summary>
        /// <param name="baseRepository">The base repository.</param>
        /// CreatedBy: NTLam (10/8/2023)
        public BaseManager(ICrudRepository<TEntity> baseRepository)
        {
            _baseRepository = baseRepository;
        }
        /// <summary>
        /// The check duplicate code.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns>The result.</returns>
        /// CreatedBy: NTLam (10/8/2023)
        public async Task CheckDuplicateCodeAsync(string code)
        {
            var entity = await _baseRepository.FindByCodeAsync(code);
            if (entity != null)
            {
                throw new ConflictException(string.Format(format: Resources.Exception.Exception.DuplicateCode, code));
            }
        }

        /// <summary>
        /// The check is exist id.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The result.</returns>
        /// CreatedBy: NTLam (10/8/2023)
        public async Task CheckIsExistIdAsync(Guid id)
        {
            _ = await _baseRepository.FindByIdAsync(id) ?? throw new NotFoundException(string.Format(format: Resources.Exception.Exception.NotExistId, id));
        }

        /// <summary>
        /// The check is code exist in list codes async.
        /// </summary>
        /// <param name="codes">The codes.</param>
        /// <returns>The result.</returns>
        /// CreatedBy: NTLam (10/8/2023)
        public async Task CheckIsCodeExistInListCodesAsync(List<string> codes)
        {
            var listCodesExist = await _baseRepository.GetByListCodeAsync(codes);
            if (listCodesExist.ToList().Count > 0)
            {
                throw new ConflictException(string.Format(format: Resources.Exception.Exception.SomeIdsExist, string.Join(" ,", listCodesExist)));
            }
        }
    }
}
