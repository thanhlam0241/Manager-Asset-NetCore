using AutoMapper;
using MISA.Web062023.AMIS.Domain;

namespace MISA.Web062023.AMIS.Application
{

    /// <summary>
    /// The base readonly service.
    /// </summary>
    /// Created by: NTLam (17/08/2023)
    public abstract class BaseReadonlyService<TEntity, TEntityDto> : IReadonlyService<TEntityDto> where TEntity : IEntity where TEntityDto : IBaseDto
    {
        #region Properties
        protected readonly IReadonlyRepository<TEntity> ReadonlyRepository;
        protected readonly IMapper Mapper;

        /// <summary>
        /// The .ctor.
        /// </summary>
        /// <param name="readonlyRepository">The readonly repository.</param>
        /// Created by: NTLam (17/08/2023)
        public BaseReadonlyService(IReadonlyRepository<TEntity> readonlyRepository, IMapper mapper)
        {
            ReadonlyRepository = readonlyRepository;
            Mapper = mapper;
        }

        /// <summary>
        /// The get all async.
        /// </summary>
        /// <returns>The result.</returns>
        /// Created by: NTLam (17/08/2023)
        public async Task<List<TEntityDto>> GetAllAsync()
        {
            var entities = await ReadonlyRepository.GetAllAsync();
            var result = entities.Select(EntityToEntityDto)
                                    .ToList();
            return result;
        }

        /// <summary>
        /// The get async.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The result.</returns>
        /// Created by: NTLam (17/08/2023)
        public async Task<TEntityDto> GetAsync(Guid id)
        {
            var entity = await ReadonlyRepository.GetAsync(id);
            var result = EntityToEntityDto(entity);
            return result;
        }

        /// <summary>
        /// The get filter async.
        /// </summary>
        /// <param name="limit">The limit.</param>
        /// <param name="offset">The offset.</param>
        /// <returns>The result.</returns>
        /// Created by: NTLam (17/08/2023)
        public async Task<List<TEntityDto>> GetFilterAsync(int limit, int offset)
        {
            var entities = await ReadonlyRepository.GetFilterAsync(limit, offset);
            var result = entities.Select(EntityToEntityDto)
                                    .ToList();

            return result;
        }

        /// <summary>
        /// Chuyển từ Entity sang EntityDto
        /// </summary>
        /// <param name="entity">Entity</param>
        /// <returns>EntityDto</returns>
        /// Created by: NTLam (17/08/2023)
        public TEntityDto EntityToEntityDto(TEntity entity)
        {
            var entityDto = Mapper.Map<TEntityDto>(entity);
            return entityDto;
        }
        #endregion
    }
}
