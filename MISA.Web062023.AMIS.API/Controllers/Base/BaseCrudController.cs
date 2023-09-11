using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Web062023.AMIS.Application;

namespace MISA.Web062023.AMIS.API.Controllers
{

    /// <summary>
    /// The base crud controller.
    /// </summary>
    /// Created By: NTLam (16/08/2023)
    public abstract class BaseCrudController<TEntityDto, TEntityCreateDto, TEntityUpdateDto> : BaseReadonlyController<TEntityDto>
        where TEntityDto : IBaseDto where TEntityCreateDto : IBaseDto where TEntityUpdateDto : IBaseDto
    {
        #region Properties
        protected readonly ICrudService<TEntityDto, TEntityCreateDto, TEntityUpdateDto> CrudService;

        /// <summary>
        /// The .ctor.
        /// </summary>
        /// <param name="crudService">The crud service.</param>
        /// Created By: NTLam (16/08/2023)
        protected BaseCrudController(ICrudService<TEntityDto, TEntityCreateDto, TEntityUpdateDto> crudService) : base(crudService)
        {
            CrudService = crudService;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Tạo mới bản ghi
        /// </summary>
        /// <param name="emulation">Dữ liệu bản ghi</param>
        /// <returns>Bản ghi được tạo mới</returns>
        /// Created By: NTLam (16/08/2023)
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public virtual async Task<IActionResult> InsertAsync(TEntityCreateDto entityCreateDto)
        {
            var result = await CrudService.InsertAsync(entityCreateDto);
            return StatusCode(StatusCodes.Status201Created, string.Format(Domain.Resources.Base.Base.Insert, result));
        }

        // <summary>
        // Tạo mới nhiều bản ghi
        // </summary>
        // <param name = "entities" > The entity create dtos.</param>
        // <returns>The result.</returns>
        // Created By: NTLam (16/08/2023)
        //[HttpPost("multiple")]
        //public virtual async Task<IActionResult> InsertMultipleAsync([FromForm] string entities)
        //{
        //    var string1 = entities;
        //    //var result = await CrudService.InsertMultiAsync(entityCreateDtos);
        //    return StatusCode(StatusCodes.Status201Created);
        //}

        /// <summary>
        /// Cập nhật thông tin bản ghi
        /// </summary>
        /// <param name="id">ID bản ghi</param>
        /// <param name="entityUpdateDto">Thông tin bản ghi</param>
        /// <returns>Bản ghi được cập nhật</returns>
        /// Created By: NTLam (16/08/2023)
        [HttpPut]
        [Route("{id}")]
        public virtual async Task<IActionResult> UpdateAsync(Guid id, TEntityUpdateDto entityUpdateDto)
        {
            var result = await CrudService.UpdateAsync(id, entityUpdateDto);
            return StatusCode(StatusCodes.Status202Accepted, string.Format(Domain.Resources.Base.Base.Update, result));
        }

        /// <summary>
        /// Xóa bản ghi
        /// </summary>
        /// <param name="id">ID bản ghi</param>
        /// <returns>Số bản ghi đã xóa</returns>
        /// Created By: NTLam (16/08/2023)
        [HttpDelete]
        [Route("{id}")]
        public virtual async Task<IActionResult> DeleteAsync(Guid id)
        {
            var result = await CrudService.DeleteAsync(id);

            return StatusCode(StatusCodes.Status202Accepted, string.Format(Domain.Resources.Base.Base.Delete, result));
        }

        /// <summary>
        /// Xóa nhiều bản ghi
        /// </summary>
        /// <param name="ids">Danh sách id bản ghi</param>
        /// <returns>Số bản ghi đã xóa</returns>
        /// Created By: NTLam (16/08/2023)
        [HttpDelete("multiple")]
        public virtual async Task<IActionResult> DeleteMultiAsync(List<Guid> ids)
        {
            var result = await CrudService.DeleteMultiAsync(ids);

            return StatusCode(StatusCodes.Status202Accepted, string.Format(Domain.Resources.Base.Base.Delete, result));
        }
        #endregion
    }
}