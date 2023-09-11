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
    /// The fixed asset update dto.
    /// </summary>
    /// Author: Nguyễn Thanh Lâm
    /// Modified date: 10/8/2023
    public class FixedAssetUpdateDto : IBaseDto
    {
        /// <summary>
        /// Gets or Sets the fixed asset code.
        /// </summary>
        /// Author: Nguyễn Thanh Lâm
        /// Modified date: 10/8/2023]
        [MaxLength(20)]
        public string? FixedAssetCode { get; set; }
        /// <summary>
        /// Gets or Sets the fixed asset name.
        /// </summary>
        /// Author: Nguyễn Thanh Lâm
        /// Modified date: 10/8/2023
        [DefaultValue(null)]
        public string? FixedAssetName { get; set; }

        /// <summary>
        /// Gets or Sets the organization id.
        /// </summary>
        /// Author: Nguyễn Thanh Lâm
        /// Modified date: 10/8/2023
        [DefaultValue(null)]
        public Guid? OrganizationId { get; set; }

        /// <summary>
        /// Gets or Sets the organization code.
        /// </summary>
        /// Author: Nguyễn Thanh Lâm
        /// Modified date: 10/8/2023
        [DefaultValue(null)]
        public string? OrganizationCode { get; set; }

        /// <summary>
        /// Gets or Sets the organization name.
        /// </summary>
        /// Author: Nguyễn Thanh Lâm
        /// Modified date: 10/8/2023
        [DefaultValue(null)]
        public string? OrganizationName { get; set; }

        /// <summary>
        /// Gets or Sets the department id.
        /// </summary>
        /// Author: Nguyễn Thanh Lâm
        /// Modified date: 10/8/2023
        [DefaultValue(null)]
        public Guid? DepartmentId { get; set; }

        /// <summary>
        /// Gets or Sets the department code.
        /// </summary>
        /// Author: Nguyễn Thanh Lâm
        /// Modified date: 10/8/2023
        [DefaultValue(null)]
        public string? DepartmentCode { get; set; }

        /// <summary>
        /// Gets or Sets the department name.
        /// </summary>
        /// Author: Nguyễn Thanh Lâm
        /// Modified date: 10/8/2023
        [DefaultValue(null)]
        public string? DepartmentName { get; set; }

        /// <summary>
        /// Gets or Sets the fixed asset category id.
        /// </summary>
        /// Author: Nguyễn Thanh Lâm
        /// Modified date: 10/8/2023
        [DefaultValue(null)]

        public Guid? FixedAssetCategoryId { get; set; }

        /// <summary>
        /// Gets or Sets the fixed asset category code.
        /// </summary>
        /// Author: Nguyễn Thanh Lâm
        /// Modified date: 10/8/2023
        [DefaultValue(null)]
        public string? FixedAssetCategoryCode { get; set; }

        /// <summary>
        /// Gets or Sets the fixed asset category name.
        /// </summary>
        /// Author: Nguyễn Thanh Lâm
        /// Modified date: 10/8/2023
        [DefaultValue(null)]
        public string? FixedAssetCategoryName { get; set; }

        /// <summary>
        /// Gets or Sets the purchase date.
        /// </summary>
        /// Author: Nguyễn Thanh Lâm
        /// Modified date: 10/8/2023
        [DefaultValue(null)]
        public DateTime? PurchaseDate { get; set; }

        /// <summary>
        /// Gets or Sets the cost.
        /// </summary>
        /// Author: Nguyễn Thanh Lâm
        /// Modified date: 10/8/2023
        [DefaultValue(null)]
        public decimal? Cost { get; set; }

        /// <summary>
        /// Gets or Sets the quantity.
        /// </summary>
        /// Author: Nguyễn Thanh Lâm
        /// Modified date: 10/8/2023
        [DefaultValue(null)]
        public int? Quantity { get; set; }

        /// <summary>
        /// Gets or Sets the depreciation rate.
        /// </summary>
        /// Author: Nguyễn Thanh Lâm
        /// Modified date: 10/8/2023
        [DefaultValue(1.1)]
        public float? DepreciationRate { get; set; }

        /// <summary>
        /// Gets or Sets the tracked year.
        /// </summary>
        /// Author: Nguyễn Thanh Lâm
        /// Modified date: 10/8/2023
        [DefaultValue(2023)]
        public int? TrackedYear { get; set; }

        /// <summary>
        /// Gets or Sets the life time.
        /// </summary>
        /// Author: Nguyễn Thanh Lâm
        /// Modified date: 10/8/2023
        [DefaultValue(1)]
        public int? LifeTime { get; set; }

        /// <summary>
        /// Gets or Sets the production year.
        /// </summary>
        /// Author: Nguyễn Thanh Lâm
        /// Modified date: 10/8/2023
        [DefaultValue(2023)]
        public int? ProductionYear { get; set; }

        /// <summary>
        /// Gets or Sets the active.
        /// </summary>
        /// Author: Nguyễn Thanh Lâm
        /// Modified date: 10/8/2023
        [DefaultValue(0)]
        public byte? Active { get; set; }

        /// <summary>
        /// The get code.
        /// </summary>
        /// <returns>The result.</returns>
        public string GetCode()
        {
            return FixedAssetCode;
        }

        /// <summary>
        /// The set code.
        /// </summary>
        /// <param name="code">The code.</param>
        public void SetCode(string code)
        {
            FixedAssetCode = code;
        }

        /// <summary>
        /// Gets or Sets the created by.
        /// </summary>
        /// Author: Nguyễn Thanh Lâm
        /// Modified date: 10/8/2023
    }
}
