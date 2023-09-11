using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MISA.Web062023.AMIS.Application
{

    /// <summary>
    /// The fixed asset category.
    /// </summary>
    public class FixedAssetCategoryDto : IBaseDto
    {

        /// <summary>
        /// Gets or Sets the fixed asset category id.
        /// </summary>
        /// Author: Nguyễn Thanh Lâm
        /// Modified date: 10/8/2023
        public Guid? FixedAssetCategoryId { get; set; }

        /// <summary>
        /// Gets or Sets the fixed asset category code.
        /// </summary>
        /// Author: Nguyễn Thanh Lâm
        /// Modified date: 10/8/2023
        [Required]
        public string? FixedAssetCategoryCode { get; set; }

        /// <summary>
        /// Gets or Sets the fixed asset category name.
        /// </summary>
        /// Author: Nguyễn Thanh Lâm
        /// Modified date: 10/8/2023
        [DefaultValue(null)]
        public string? FixedAssetCategoryName { get; set; }

        /// <summary>
        /// Gets or Sets the organization id.
        /// </summary>
        /// Author: Nguyễn Thanh Lâm
        /// Modified date: 10/8/2023
        [DefaultValue(null)]
        public Guid? OrganizationId { get; set; }

        /// <summary>
        /// Gets or Sets the depreciation rate.
        /// </summary>
        /// Author: Nguyễn Thanh Lâm
        /// Modified date: 10/8/2023
        [DefaultValue(null)]
        public double? DepreciationRate { get; set; }

        /// <summary>
        /// Gets or Sets the life time.
        /// </summary>
        /// Author: Nguyễn Thanh Lâm
        /// Modified date: 10/8/2023
        [DefaultValue(null)]
        public int? LifeTime { get; set; }

        /// <summary>
        /// Gets or Sets the description.
        /// </summary>
        /// Author: Nguyễn Thanh Lâm
        /// Modified date: 10/8/2023
        [DefaultValue(null)]
        public string? Description { get; set; }
        public string GetCode()
        {
            return FixedAssetCategoryCode;
        }

        /// <summary>
        /// The set code.
        /// </summary>
        /// <param name="code">The code.</param>
        public void SetCode(string code)
        {
            FixedAssetCategoryCode = code;
        }
    }
}
