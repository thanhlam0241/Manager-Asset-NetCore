namespace MISA.Web062023.AMIS.Domain
{
    /// <summary>
    /// Chức năng: Interface chứa các phương thức kết nối và thực thi câu lệnh SQL dùng cho DepartmentController
    /// </summary>
    /// Created by: Nguyễn Thanh Lâm (10/8/2023)
    public interface IDepartmentRepository : ICrudRepository<Department>
    {

        /// <summary>
        /// The get departments.
        /// </summary>
        /// <returns>The result.</returns>
        /// Created by: Nguyễn Thanh Lâm (10/8/2023)
        public Task<IEnumerable<Department>> GetDepartmentsAsync();

        /// <summary>
        /// The get department by id.
        /// </summary>
        /// <param name="departmentId">The department id.</param>
        /// <returns>The result.</returns>
        /// Created by: Nguyễn Thanh Lâm (10/8/2023)
        public Task<Department> GetDepartmentByIdAsync(Guid departmentId);

        /// <summary>
        /// The find department by code async.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns>The result.</returns>
        /// Created by: Nguyễn Thanh Lâm (10/8/2023)
        public Task<Department?> FindDepartmentByCodeAsync(string code);

        /// <summary>
        /// The create department.
        /// </summary>
        /// <param name="department">The department.</param>
        /// <returns>The result.</returns>
        /// Created by: Nguyễn Thanh Lâm (10/8/2023)
        public Task<int> CreateDepartmentAsync(Department department);

        /// <summary>
        /// The update department.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="department">The department.</param>
        /// <returns>The result.</returns>
        /// Created by: Nguyễn Thanh Lâm (10/8/2023)
        public Task<int> UpdateDepartmentAsync(Department department);

        /// <summary>
        /// The delete department.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The result.</returns>
        /// Created by: Nguyễn Thanh Lâm (10/8/2023)
        public Task<int> DeleteDepartmentAsync(Guid id);
    }
}
