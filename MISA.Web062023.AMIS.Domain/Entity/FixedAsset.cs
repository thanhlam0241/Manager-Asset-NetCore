using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace MISA.Web062023.AMIS.Domain
{

    /// <summary>
    /// The fixed asset class.
    /// </summary>
    /// Author: Nguyễn Thanh Lâm
    /// Modified date: 10/8/2023
    /// 
    [Table("fixed_asset")]
    public class FixedAsset : BaseAuditEntity, IEntity
    {

        /// <summary>
        /// Gets or Sets the fixed asset id.
        /// </summary>
        /// Author: Nguyễn Thanh Lâm
        /// Modified date: 10/8/2023
        [Required]
        public Guid FixedAssetId { get; set; }

        /// <summary>
        /// Gets or Sets the fixed asset code.
        /// </summary>
        /// Author: Nguyễn Thanh Lâm
        /// Modified date: 10/8/2023
        [Required]
        [NotNull]
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
        /// Ghi đè phương thức lấy id
        /// </summary>
        /// <returns></returns>
        /// Created by: NTLam (10/8/2023)
        public Guid GetId()
        {
            return FixedAssetId;
        }

        /// <summary>
        /// Ghì đè phương thức set id
        /// </summary>
        /// <param name="id"></param>
        /// Created by: NTLam (10/8/2023)
        public void SetId(Guid id)
        {
            FixedAssetId = id;
        }
    }
}
