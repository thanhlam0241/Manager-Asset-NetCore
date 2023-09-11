using AutoMapper;
using MISA.Web062023.AMIS.Domain;
using System.Resources;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace MISA.Web062023.AMIS.Application
{
    /// <summary>
    /// Lớp abstract khai báo các phương thức đọc, thêm, sửa, xóa
    /// </summary>
    /// <typeparam name="TEntity">Thực thể trong cơ sở dữ liệu</typeparam>
    /// <typeparam name="TEntityDto">Thực thể hiển thị cho người dùng</typeparam>
    /// <typeparam name="TEntityCreateDto">Thực thể tạo mới</typeparam>
    /// <typeparam name="TEntityUpdateDto">Thực thể chỉnh sửa</typeparam>
    /// Created by: NTLam (17/08/2023)
    public abstract class BaseCrudService<TEntity, TEntityDto, TEntityCreateDto, TEntityUpdateDto> : BaseReadonlyService<TEntity, TEntityDto>,
        ICrudService<TEntityDto, TEntityCreateDto, TEntityUpdateDto>
        where TEntity : IEntity where TEntityDto : IBaseDto
        where TEntityCreateDto : IBaseDto where TEntityUpdateDto : IBaseDto
    {
        #region Properties
        protected readonly ICrudRepository<TEntity> CrudRepository;
        protected readonly IBaseManager<TEntity> BaseManager;

        /// <summary>
        /// The .ctor.
        /// </summary>
        /// <param name="crudRepository">The crud repository.</param>
        /// Created by: NTLam (17/08/2023)
        public BaseCrudService(ICrudRepository<TEntity> crudRepository, IBaseManager<TEntity> baseManager, IMapper mapper)
            : base(crudRepository, mapper)
        {
            CrudRepository = crudRepository;
            BaseManager = baseManager;
        }

        /// <summary>
        /// The insert async.
        /// </summary>
        /// <param name="entityCreateDto">The entity create dto.</param>
        /// <returns>The result.</returns>
        /// Created by: NTLam (17/08/2023)
        public async Task<int> InsertAsync(TEntityCreateDto entityCreateDto)
        {
            await BaseManager.CheckDuplicateCodeAsync(entityCreateDto.GetCode());

            var entity = EntityCreateDtoToEntity(entityCreateDto);

            var result = await CrudRepository.InsertAsync(entity);
            return result;
        }

        /// <summary>
        /// The insert multi async.
        /// </summary>
        /// <param name="entityCreateDto">The entity create dto.</param>
        /// <returns>The result.</returns>
        /// Created by: NTLam (17/08/2023)
        public async Task<int> InsertMultiAsync(List<TEntityCreateDto> entityCreateDto)
        {
            var listEntities = entityCreateDto.Select(EntityCreateDtoToEntity).ToList();

            await BaseManager.CheckIsCodeExistInListCodesAsync(entityCreateDto.Select(entity => entity.GetCode()).ToList());

            var result = await CrudRepository.InsertMultiAsync(listEntities);

            return result;
        }

        /// <summary>
        /// The update async.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="entityUpdateDto">The entity update dto.</param>
        /// <returns>The result.</returns>
        /// Created by: NTLam (17/08/2023)
        public async Task<int> UpdateAsync(Guid id, TEntityUpdateDto entityUpdateDto)
        {
            // Nếu không tồn tại bản ghi thì bắn ra Exception
            await BaseManager.CheckIsExistIdAsync(id);

            var entityOld = await CrudRepository.GetAsync(id);

            if (EntityToEntityDto(entityOld).GetCode() != entityUpdateDto.GetCode())
            {
                await BaseManager.CheckDuplicateCodeAsync(entityUpdateDto.GetCode());
            }

            // Nếu tồn tại bản ghi thì cập nhât bản ghi đó
            var entity = EntityUpdateDtoToEntity(id, entityUpdateDto);

            var result = await CrudRepository.UpdateAsync(entity);

            return result;
        }

        /// <summary>
        /// The delete async.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The result.</returns>
        /// Created by: NTLam (17/08/2023)
        public async Task<int> DeleteAsync(Guid id)
        {
            // Nếu không tồn tại bản ghi thì bắn ra Exception
            await BaseManager.CheckIsExistIdAsync(id);

            // Nếu tồn tại bản ghi thì xóa bản ghi đó
            var result = await CrudRepository.DeleteAsync(id);

            return result;
        }

        /// <summary>
        /// The delete multi async.
        /// </summary>
        /// <param name="ids">The ids.</param>
        /// <returns>The result.</returns>
        /// Created by: NTLam (17/08/2023)
        public async Task<int> DeleteMultiAsync(List<Guid> ids)
        {
            var entities = await CrudRepository.GetByListIdAsync(ids);

            // Kiểm tra nếu một trong số Id truyền vào không tồn tại trong cơ sở dữ liệu thì bắn ra Exception
            var entityIds = entities.Select(entity => entity.GetId()).ToList();
            var idsNotExists = new List<Guid>();
            ids.ForEach(id =>
            {
                if (!entityIds.Contains(id))
                {
                    idsNotExists.Add(id);
                }
            });

            if (idsNotExists.Count > 0)
            {
                throw new NotFoundException(string.Format(format: Domain.Resources.Exception.Exception.DuplicateCode, string.Join(", ", idsNotExists)));
            }

            var result = await CrudRepository.DeleteMultiAsync(ids);

            return result;
        }

        /// <summary>
        /// Chuyển từ EntityCreateDto sang Entity
        /// </summary>
        /// <param name="entityCreateDto">EntityCreateDto</param>
        /// <returns>Entity</returns>
        /// Created by: NTLam (17/08/2023)
        public virtual TEntity EntityCreateDtoToEntity(TEntityCreateDto entityCreateDto)
        {
            var result = Mapper.Map<TEntity>(entityCreateDto);
            result.SetId(Guid.NewGuid());
            return result;
        }

        /// <summary>
        /// Chuyển từ EntityUpdateDto sang Entity
        /// </summary>
        /// <param name="id">Id EntityUpdateDto</param>
        /// <param name="entityUpdateDto">EntityUpdateDto</param>
        /// <returns>Entity</returns>
        /// Created by: NTLam (17/08/2023)
        public virtual TEntity EntityUpdateDtoToEntity(Guid id, TEntityUpdateDto entityUpdateDto)
        {
            var result = Mapper.Map<TEntity>(entityUpdateDto);
            result.SetId(id);
            return result;
        }

        #endregion
    }
}

