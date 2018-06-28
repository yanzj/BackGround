using Lte.Domain.Regular.Attributes;

namespace Lte.Domain.Excel
{
    public class HotSpotCellExcel
    {
        [ExcelColumn("热点类型")]
        public string HotSpotTypeDescription { get; set; }

        [ExcelColumn("热点名称")]
        public string HotspotName { get; set; }

        [ExcelColumn("基站编号")]
        public int ENodebId { get; set; }

        [ExcelColumn("小区编号")]
        public byte SectorId { get; set; }
    }
}