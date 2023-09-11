using AutoMapper;
using Dapper;
using MISA.Web062023.AMIS.Domain;
using System.Data;
using static Dapper.SqlMapper;

namespace MISA.Web062023.AMIS.Application
{

    /// <summary>
    /// The department repository.
    /// </summary>
    /// Created by: NTLam (10/8/2023)
    public class DepartmentService : BaseCrudService<Department, DepartmentDto, DepartmentCreateDto, DepartmentUpdateDto>,
         IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IDepartmentManager _departmentManager;

        /// <summary>
        /// The Constructor.
        /// </summary>
        /// <param name="crudRepository">The crud repository.</param>
        /// <param name="baseManager">The base manager.</param>
        /// <param name="readonlyRepository">The readonly repository.</param>
        /// <param name="departmentRepository">The department repository.</param>
        /// <param name="mapper">The mapper.</param>
        /// Created by: NTLam (10/8/2023)
        public DepartmentService(IDepartmentRepository departmentRepository, IDepartmentManager departmentManager, IMapper mapper) : base(departmentRepository, departmentManager, mapper)
        {
            _departmentRepository = departmentRepository;
            _departmentManager = departmentManager;
        }

        /// <summary>
        /// The create department.
        /// </summary>
        /// <param name="department">The department.</param>
        /// <returns>The result.</returns>
        /// Created by: NTLam (10/8/2023)
        public async Task<int> CreateDepartmentAsync(DepartmentCreateDto departmentCreateDto)
        {
            await _departmentManager.CheckDuplicateCodeAsync(departmentCreateDto.DepartmentCode);
            int result = await _departmentRepository.CreateDepartmentAsync(EntityCreateDtoToEntity(departmentCreateDto));
            return result;
        }

        /// <summary>
        /// The delete department.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The result.</returns>
        /// Created by: NTLam (10/8/2023)
        public async Task<int> DeleteDepartmentAsync(Guid departmentId)
        {
            await _departmentManager.CheckIsExistIdAsync(departmentId);
            int result = await _departmentRepository.DeleteDepartmentAsync(departmentId);
            return result;
        }

        /// <summary>
        /// The get departments.
        /// </summary>
        /// <returns>The result.</returns>
        /// Created by: NTLam (10/8/2023)
        public async Task<List<DepartmentDto>> GetDepartmentsAsync()
        {
            var departments = await _departmentRepository.GetDepartmentsAsync();
            return departments.Select(EntityToEntityDto).ToList();
        }

        /// <summary>
        /// The get department by id.
        /// </summary>
        /// <param name="departmentId">The department id.</param>
        /// <returns>The result.</returns>
        /// Created by: NTLam (10/8/2023)
        public async Task<DepartmentDto> GetDepartmentByIdAsync(Guid departmentId)
        {
            var department = await _departmentRepository.GetDepartmentByIdAsync(departmentId);
            return EntityToEntityDto(department);
        }

        /// <summary>
        /// The update department.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="department">The department.</param>
        /// <returns>The result.</returns>
        /// Created by: NTLam (10/8/2023)
        public async Task<int> UpdateDepartmentAsync(Guid departmentId, DepartmentUpdateDto departmentUpdateDto)
        {
            await _departmentManager.CheckIsExistIdAsync(departmentId);
            int result = await _departmentRepository.UpdateDepartmentAsync(EntityUpdateDtoToEntity(departmentId, departmentUpdateDto));
            return result;
        }
    }
}
