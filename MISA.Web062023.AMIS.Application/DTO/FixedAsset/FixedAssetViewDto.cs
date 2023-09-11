using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml.Attributes;
using System.Reflection.PortableExecutable;
using OfficeOpenXml.Table;

namespace MISA.Web062023.AMIS.Application.DTO.FixedAsset
{
    [EpplusTable(TableStyle = TableStyles.Dark1, PrintHeaders = true, AutofitColumns = true, AutoCalculate = true, ShowTotal = true, ShowFirstColumn = true)]
    /*[
         EpplusFormulaTableColumn(Order = 6, NumberFormat = "€#,##0.00", Header = "Tax amount", FormulaR1C1 = "RC[-2] * RC[-1]", TotalsRowFunction = RowFunctions.Sum, TotalsRowNumberFormat = "€#,##0.00"),
         EpplusFormulaTableColumn(Order = 7, NumberFormat = "€#,##0.00", Header = "Net salary", Formula = "E2-G2", TotalsRowFunction = RowFunctions.Sum, TotalsRowNumberFormat = "€#,##0.00")
    ]*/

    [EPPlusTableColumnSortOrder]
    public class FixedAssetViewDto
    {
        /// <summary>
        /// Gets or Sets the fixed asset id.
        /// </summary>
        /// Author: Nguyễn Thanh Lâm
        /// Modified date: 10/8/2023
        /// 
        [EpplusTableColumn(Header = "Id", TotalsRowLabel = "Tổng số")]
        public Guid? FixedAssetId { get; set; }

        /// <summary>
        /// Gets or Sets the fixed asset code.
        /// </summary>
        /// Author: Nguyễn Thanh Lâm
        /// Modified date: 10/8/2023
        [Required]
        [MaxLength(20)]
        [EpplusTableColumn(Header = "Mã tài sản")]
        public string FixedAssetCode { get; set; }

        /// <summary>
        /// Gets or Sets the fixed asset name.
        /// </summary>
        /// Author: Nguyễn Thanh Lâm
        /// Modified date: 10/8/2023
        [DefaultValue(null)]
        [EpplusTableColumn(Header = "Tên tài sản")]
        public string? FixedAssetName { get; set; }

        /// <summary>
        /// Gets or Sets the department name.
        /// </summary>
        /// Author: Nguyễn Thanh Lâm
        /// Modified date: 10/8/2023
        [DefaultValue(null)]
        [EpplusTableColumn(Header = "Phòng ban")]
        public string? DepartmentName { get; set; }

        /// <summary>
        /// Gets or Sets the fixed asset category name.
        /// </summary>
        /// Author: Nguyễn Thanh Lâm
        /// Modified date: 10/8/2023
        [DefaultValue(null)]
        [EpplusTableColumn(Header = "Loại tài sản")]
        public string? FixedAssetCategoryName { get; set; }

        /// <summary>
        /// Gets or Sets the cost.
        /// </summary>
        [EpplusTableColumn(Header = "Nguyên giá", TotalsRowFunction = RowFunctions.Sum)]
        public decimal? Cost { get; set; }

        /// <summary>
        /// Gets or Sets the quantity.
        /// </summary>
        /// Author: Nguyễn Thanh Lâm
        /// Modified date: 10/8/2023
        [DefaultValue(null)]
        [EpplusTableColumn(Header = "Số lượng", TotalsRowFunction = RowFunctions.Sum)]
        public int? Quantity { get; set; }

        /// <summary>
        /// Gets or Sets the accumulated depreciation.
        /// </summary>
        /// Author: Nguyễn Thanh Lâm
        /// Modified date: 10/8/2023
        [EpplusTableColumn(Header = "Lũy kế", TotalsRowFunction = RowFunctions.Sum)]
        public int AccumulatedDepreciation { get; set; }

        /// <summary>
        /// Gets or Sets the remaining value.
        /// </summary>
        [EpplusTableColumn(Header = "Giá trị còn lại", TotalsRowFunction = RowFunctions.Sum)]
        public int RemainingValue { get; set; }
    }
}
