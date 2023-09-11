using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.Web062023.AMIS.Application;

namespace MISA.Web062023.AMIS.API.Controllers
{

    /// <summary>
    /// The base readonly controller.
    /// </summary>
    /// Created By: NTLam (16/08/2023)
    public class BaseReadonlyController<TEntityDto> : ControllerBase where TEntityDto : IBaseDto
    {
        #region Properties
        protected readonly IReadonlyService<TEntityDto> ReadonlyService;

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="readonlyService">The readonly service.</param>
        /// Created By: NTLam (16/08/2023)
        public BaseReadonlyController(IReadonlyService<TEntityDto> readonlyService)
        {
            ReadonlyService = readonlyService;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Lấy danh sách bản ghi
        /// </summary>
        /// <returns>danh sách bản ghi</returns>
        /// Created By: NTLam (16/08/2023)
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var result = await ReadonlyService.GetAllAsync();
            return StatusCode(StatusCodes.Status200OK, result);
        }

        /// <summary>
        /// Lấy một bản ghi theo ID
        /// </summary>
        /// <param name="id">ID bản ghi</param>
        /// <returns>
        /// Bản ghi
        /// </returns>
        /// Created By: NTLam (16/08/2023)
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetAsync(Guid id)
        {
            var result = await ReadonlyService.GetAsync(id);
            return StatusCode(StatusCodes.Status200OK, result);
        }
        #endregion
    }
}