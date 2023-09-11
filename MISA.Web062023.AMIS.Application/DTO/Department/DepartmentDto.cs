using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MISA.Web062023.AMIS.Application
{

    /// <summary>
    /// The department class.
    /// </summary>
    /// Author: Nguyễn Thanh Lâm
    /// Modified date: 10/8/2023
    public class DepartmentDto : IBaseDto
    {

        /// <summary>
        /// Gets or Sets the department id.
        /// </summary>
        /// Author: Nguyễn Thanh Lâm
        /// Modified date: 10/8/2023
        public Guid? DepartmentId { get; set; }

        /// <summary>
        /// Gets or Sets the department code.
        /// </summary>
        /// Author: Nguyễn Thanh Lâm
        /// Modified date: 10/8/2023
        [Required]
        [MaxLength(20)]
        public string DepartmentCode { get; set; }

        /// <summary>
        /// Gets or Sets the department name.
        /// </summary>
        /// Author: Nguyễn Thanh Lâm
        /// Modified date: 10/8/2023
        [DefaultValue(null)]
        [MaxLength(255)]
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
        public string GetCode()
        {
            return DepartmentCode;
        }

        /// <summary>
        /// The set code.
        /// </summary>
        /// <param name="code">The code.</param>
        public void SetCode(string code)
        {
            DepartmentCode = code;
        }
    }
}
