using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace MISA.Web062023.AMIS.Domain
{

    /// <summary>
    /// The fixed asset category.
    /// </summary>
    public class FixedAssetCategory : BaseAuditEntity, IEntity
    {

        /// <summary>
        /// Gets or Sets the fixed asset category id.
        /// </summary>
        /// Created by: NTLam(10/8/2023)
        [Required]
        public Guid FixedAssetCategoryId { get; set; }

        /// <summary>
        /// Gets or Sets the fixed asset category code.
        /// </summary>
        /// Created by: NTLam(10/8/2023)
        [Required]
        [NotNull]
        public string? FixedAssetCategoryCode { get; set; }

        /// <summary>
        /// Gets or Sets the fixed asset category name.
        /// </summary>
        /// Created by: NTLam(10/8/2023)
        [DefaultValue(null)]
        [MaxLength(255)]
        public string? FixedAssetCategoryName { get; set; }

        /// <summary>
        /// Gets or Sets the organization id.
        /// </summary>
        /// Created by: NTLam(10/8/2023)
        [DefaultValue(null)]
        public Guid? OrganizationId { get; set; }

        /// <summary>
        /// Gets or Sets the depreciation rate.
        /// </summary>
        /// Created by: NTLam(10/8/2023)
        [DefaultValue(null)]
        public double? DepreciationRate { get; set; }

        /// <summary>
        /// Gets or Sets the life time.
        /// </summary>
        /// Created by: NTLam(10/8/2023)
        [DefaultValue(null)]
        public int? LifeTime { get; set; }

        /// <summary>
        /// Gets or Sets the description.
        /// </summary>
        /// Created by: NTLam(10/8/2023)
        [DefaultValue(null)]
        public string? Description { get; set; }

        /// <summary>
        /// The get id.
        /// </summary>
        /// <returns>The result.</returns>
        /// Created by: NTLam(10/8/2023)
        public Guid GetId()
        {
            return FixedAssetCategoryId;
        }

        /// <summary>
        /// The set id.
        /// </summary>
        /// <param name="id">The id.</param>
        public void SetId(Guid id)
        {
            FixedAssetCategoryId = id;
        }
    }
}
