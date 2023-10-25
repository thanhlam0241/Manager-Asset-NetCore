using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.Web062023.AMIS.Domain
{
    /// <summary>
    /// Lớp tài sản đã chứng từ
    /// </summary>
    /// Created by: NTLam (20/08/2023)
    public class RecordedAsset : BaseAuditEntity, IEntity
    {

        /// <summary>
        /// Id tài sản đã chứng từ
        /// </summary>
        /// Created by: NTLam (20/08/2023)
        public Guid? RecordedAssetId { get; set; }
        /// <summary>
        /// Mã tài sản đã chứng từ
        /// </summary>
        /// Created by: NTLam (20/08/2023)
        public string? RecordedAssetCode { get; set; }

        /// <summary>
        /// Tên tài sản đã chứng từ
        /// </summary>
        /// Created by: NTLam (20/08/2023)
        public string? RecordedAssetName { get; set; }
        /// <summary>
        /// Tên phòng ban
        /// </summary>
        /// Created by: NTLam (20/08/2023)
        public string? DepartmentName { get; set; }
        /// <summary>
        /// Giá trị tài sản đã chứng từ
        /// </summary>
        /// Created by: NTLam (20/08/2023)
        public int? Value { get; set; } = 0;
        /// <summary>
        /// Tỷ lệ khấu hao
        /// </summary>
        /// Created by: NTLam (20/08/2023)
        public float? DepreciationRate { get; set; } = 0;
        /// <summary>
        /// Loại chứng từ
        /// </summary>
        public RecordingType? RecordingType { get; set; }
        /// <summary>
        /// Nguồn hình thành tài sản
        /// </summary>
        /// Created by: NTLam (20/08/2023)

        public List<ResourceAsset> ResourceAssets { get; set; } = new List<ResourceAsset>();
        /// <summary>
        /// Chứng từ
        /// </summary>
        /// Created by: NTLam (20/08/2023)
        public Recording? Recording { get; set; }
        /// <summary>
        /// Hàm lấy id
        /// </summary>
        /// <returns></returns>
        /// Created by: NTLam (20/08/2023)
        public Guid GetId()
        {
            return RecordedAssetId.Value;
        }
        /// <summary>
        /// Hàm set id
        /// </summary>
        /// <param name="id"></param>
        /// Created by: NTLam (20/08/2023)
        public void SetId(Guid id)
        {
            RecordedAssetId = id;
        }
    }
}
