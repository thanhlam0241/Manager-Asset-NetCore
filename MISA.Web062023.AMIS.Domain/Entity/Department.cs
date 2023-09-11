using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace MISA.Web062023.AMIS.Domain
{

    /// <summary>
    /// The department class.
    /// </summary>
    /// Author: Nguyễn Thanh Lâm
    /// Modified date: 10/8/2023
    public class Department : BaseAuditEntity, IEntity
    {

        /// <summary>
        /// Gets or Sets the department id.
        /// </summary>
        /// Author: Nguyễn Thanh Lâm
        /// Modified date: 10/8/2023
        [Required]
        public Guid DepartmentId { get; set; }

        /// <summary>
        /// Gets or Sets the department code.
        /// </summary>
        /// Author: Nguyễn Thanh Lâm
        /// Modified date: 10/8/2023
        [Required]
        [NotNull]
        public string? DepartmentCode { get; set; }

        /// <summary>
        /// Gets or Sets the department name.
        /// </summary>
        /// Author: Nguyễn Thanh Lâm
        /// Modified date: 10/8/2023
        [DefaultValue(null)]
        public string? DepartmentName { get; set; }

        /// <summary>
        /// Gets or Sets the description.
        /// </summary>
        /// Author: Nguyễn Thanh Lâm
        /// Modified date: 10/8/2023
        [DefaultValue(null)]
        public string? Description { get; set; }

        /// <summary>
        /// Gets or Sets the is parent.
        /// </summary>
        /// Author: Nguyễn Thanh Lâm
        /// Modified date: 10/8/2023
        [DefaultValue(1)]
        public byte? IsParent { get; set; }

        /// <summary>
        /// Gets or Sets the parent id.
        /// </summary>
        /// Author: Nguyễn Thanh Lâm
        /// Modified date: 10/8/2023
        [DefaultValue(null)]
        public Guid? ParentId { get; set; }

        /// <summary>
        /// Gets or Sets the organization id.
        /// </summary>
        /// Author: Nguyễn Thanh Lâm
        /// Modified date: 10/8/2023
        [DefaultValue(null)]
        public Guid? OrganizationId { get; set; }

        /// <summary>
        /// The get id.
        /// </summary>
        /// <returns>The result.</returns>
        public Guid GetId()
        {
            return DepartmentId;
        }

        /// <summary>
        /// The set id.
        /// </summary>
        /// <param name="id">The id.</param>
        public void SetId(Guid id)
        {
            DepartmentId = id;
        }
    }
}
