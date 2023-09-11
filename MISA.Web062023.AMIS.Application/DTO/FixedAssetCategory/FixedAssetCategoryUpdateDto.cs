using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MISA.Web062023.AMIS.Application
{

    /// <summary>
    /// The fixed asset category update dto.
    /// </summary>
    /// Author: Nguyễn Thanh Lâm
    /// Modified date: 10/8/2023
    public class FixedAssetCategoryUpdateDto : IBaseDto
    {
        /// <summary>
        /// Gets or Sets the fixed asset category code.
        /// </summary>
        /// Author: Nguyễn Thanh Lâm
        /// Modified date: 10/8/2023
        [MaxLength(20)]
        public string? FixedAssetCategoryCode { get; set; }
        /// <summary>
        /// Gets or Sets the fixed asset category name.
        /// </summary>
        /// Author: Nguyễn Thanh Lâm
        /// Modified date: 10/8/2023
        [DefaultValue(null)]
        [MaxLength(255)]
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

        /// <summary>
        /// The get code.
        /// </summary>
        /// <returns>The result.</returns>
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
