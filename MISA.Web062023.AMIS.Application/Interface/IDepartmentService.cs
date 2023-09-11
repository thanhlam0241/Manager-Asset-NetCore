namespace MISA.Web062023.AMIS.Application
{
    /// <summary>
    /// Chức năng: Interface chứa các phương thức kết nối và thực thi câu lệnh SQL dùng cho DepartmentController
    /// </summary>
    /// Author: Nguyễn Thanh Lâm
    /// Modified date: 10/8/2023
    public interface IDepartmentService : ICrudService<DepartmentDto, DepartmentCreateDto, DepartmentUpdateDto>
    {
        /// <summary>
        /// The get departments.
        /// </summary>
        /// <returns>The result.</returns>
        /// Author: Nguyễn Thanh Lâm
        /// Modified date: 10/8/2023
        public Task<List<DepartmentDto>> GetDepartmentsAsync();

        /// <summary>
        /// The get department by id.
        /// </summary>
        /// <param name="departmentId">The department id.</param>
        /// <returns>The result.</returns>
        /// Author: Nguyễn Thanh Lâm
        /// Modified date: 10/8/2023
        public Task<DepartmentDto> GetDepartmentByIdAsync(Guid departmentId);

        /// <summary>
        /// The create department.
        /// </summary>
        /// <param name="department">The department.</param>
        /// <returns>The result.</returns>
        /// Author: Nguyễn Thanh Lâm
        /// Modified date: 10/8/2023
        public Task<int> CreateDepartmentAsync(DepartmentCreateDto department);

        /// <summary>
        /// The update department.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="department">The department.</param>
        /// <returns>The result.</returns>
        /// Author: Nguyễn Thanh Lâm
        /// Modified date: 10/8/2023
        public Task<int> UpdateDepartmentAsync(Guid id, DepartmentUpdateDto department);

        /// <summary>
        /// The delete department.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The result.</returns>
        /// Author: Nguyễn Thanh Lâm
        /// Modified date: 10/8/2023
        public Task<int> DeleteDepartmentAsync(Guid id);
    }
}
