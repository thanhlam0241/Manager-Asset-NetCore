using Dapper;
using MISA.Web062023.AMIS.Application;
using MISA.Web062023.AMIS.Domain;
using System.Data;

namespace MISA.Web062023.AMIS.Infrastructure
{

    /// <summary>
    /// The department repository.
    /// </summary>
    /// Author: Nguyễn Thanh Lâm
    /// Modified date: 10/8/2023
    public class DepartmentRepository : BaseCrudRepository<Department>, IDepartmentRepository
    {

        /// <summary>
        /// Constructor của DepartmentRepository
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        /// Created by: NTLam (10/8/2023)
        public DepartmentRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        /// <summary>
        /// The create department.
        /// </summary>
        /// <param name="department">The department.</param>
        /// <returns>The result.</returns>
        /// Created by: NTLam (10/8/2023)
        public async Task<int> CreateDepartmentAsync(Department department)
        {
            string query = @"Insert into department (department_id,department_code,department_name,
                             description,is_parent,parent_id,organization_id) 
                            VALUES (@DepartmentId,@DepartmentCode,@DepartmentName,@Description,@IsParent,@ParentId,@OrganizationId)";
            var connection = _unitOfWork.Connection;
            var parameters = new DynamicParameters();
            var properties = department.GetType().GetProperties();
            foreach (var property in properties)
            {
                var propertyName = property.Name;
                var propertyValue = property.GetValue(department);
                parameters.Add(propertyName, propertyValue);
            }

            var result = await connection.ExecuteAsync(query, parameters, transaction: _unitOfWork.Transaction);
            return result;

        }

        /// <summary>
        /// The delete department.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <returns>The result.</returns>
        /// Created by: NTLam (10/8/2023)
        public async Task<int> DeleteDepartmentAsync(Guid id)
        {
            string query = $"Delete FROM department WHERE department_id = @Id";
            var connection = _unitOfWork.Connection;
            var result = await connection.ExecuteAsync(query, new { Id = id }, transaction: _unitOfWork.Transaction);
            return result;
        }

        /// <summary>
        /// The find department by code async.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns>The result.</returns>
        /// Created by: NTLam (10/8/2023)
        public async Task<Department?> FindDepartmentByCodeAsync(string code)
        {
            string query = "SELECT * FROM department WHERE @department_code = @Code";
            var connection = _unitOfWork.Connection;
            var department = await connection.QueryFirstOrDefaultAsync<Department?>(query, new { Code = code });
            return department;
        }

        /// <summary>
        /// The get departments.
        /// </summary>
        /// <returns>The result.</returns>
        /// Created by: NTLam (10/8/2023)
        public async Task<IEnumerable<Department>> GetDepartmentsAsync()
        {
            string query = @"Select department_id,department_code,department_name,description,is_parent,parent_id,
                              organization_id,created_by,modified_by from department";
            var connection = _unitOfWork.Connection;
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            var departments = await connection.QueryAsync<Department>(query);
            return departments;
        }

        /// <summary>
        /// The get department by id.
        /// </summary>
        /// <param name="departmentId">The department id.</param>
        /// <returns>The result.</returns>
        /// Created by: NTLam (10/8/2023)
        public async Task<Department> GetDepartmentByIdAsync(Guid departmentId)
        {
            string query = $"""
                              Select department_id,department_code,department_name,description,is_parent,parent_id,
                              organization_id,created_by,modified_by from department WHERE department_id = @Id
                            """;
            var connection = _unitOfWork.Connection;
            DefaultTypeMap.MatchNamesWithUnderscores = true;
            var department = await connection.QueryFirstOrDefaultAsync<Department>(query, new { Id = departmentId });
            return department;
        }

        /// <summary>
        /// The update department.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="department">The department.</param>
        /// <returns>The result.</returns>
        /// Created by: NTLam (10/8/2023)
        public async Task<int> UpdateDepartmentAsync(Department department)
        {
            List<string> fieldsUpdate = new List<string>();
            var parameters = new DynamicParameters();
            var properties = department.GetType().GetProperties();
            foreach (var property in properties)
            {
                var propertyName = property.Name;
                var propertyValue = property.GetValue(department);
                if (propertyValue == null) continue;
                if (propertyName != "DepartmentId")
                {
                    fieldsUpdate.Add($"{StringExtension.PascalCaseToUnderscoreCase(propertyName)} = @{propertyName}");
                }
                parameters.Add(propertyName, propertyValue);
            }
            string query = $"""
                           Update department SET {string.Join(",", fieldsUpdate)} WHERE department_id = @DepartmentId
                           """;

            using var connection = _unitOfWork.Connection;

            var result = await connection.ExecuteAsync(query, parameters, transaction: _unitOfWork.Transaction);
            return result;
        }
    }
}
