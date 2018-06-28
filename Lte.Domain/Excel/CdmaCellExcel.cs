using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Lte.Domain.Regular.Attributes;

namespace Lte.Domain.Excel
{
    [TypeDoc("定义记录CDMA小区的信息的Excel导出数据项，需要定义与CdmaCell之间的映射关系")]
    public class CdmaCellExcel
    {
        [ExcelColumn("扇区标识")]
        [MemberDoc("扇区标识")]
        [Required]
        public byte SectorId { get; set; }

        [ExcelColumn("频点")]
        [MemberDoc("CDMA频点，如283、201、37等")]
        public int Frequency { get; set; }

        [ExcelColumn("覆盖类型(室内/室外/地铁)")]
        [MemberDoc("覆盖类型(室内/室外/地铁)")]
        public string IsIndoor { get; set; } = "否";

        [ExcelColumn("经度")]
        [MemberDoc("经度")]
        [Required]
        public double Longtitute { get; set; }

        [ExcelColumn("纬度")]
        [MemberDoc("纬度")]
        [Required]
        public double Lattitute { get; set; }

        [ExcelColumn("挂高")]
        [MemberDoc("天线挂高（米）")]
        public double Height { get; set; }

        [ExcelColumn("下倾角（机械）")]
        [MemberDoc("下倾角（机械）")]
        public double MTilt { get; set; }

        [ExcelColumn("下倾角（电调）")]
        [MemberDoc("下倾角（电调）")]
        public double ETilt { get; set; }

        [ExcelColumn("方位角")]
        [MemberDoc("方位角")]
        public double Azimuth { get; set; }

        [ExcelColumn("天线增益（dBi）", TransformEnum.DefaultZeroDouble)]
        [MemberDoc("天线增益（dBi）")]
        public double AntennaGain { get; set; }

        [ExcelColumn("基站编号")]
        [MemberDoc("基站编号")]
        public int BtsId { get; set; }

        [ExcelColumn("小区标识")]
        [MemberDoc("小区标识")]
        public int CellId { get; set; }

        [IgnoreMap]
        [ExcelColumn("载扇类型(1X/DO)")]
        [MemberDoc("载扇类型(1X/DO)")]
        public string CellType { get; set; }

        [IgnoreMap]
        [ExcelColumn("LAC")]
        [MemberDoc("LAC，位置区编码")]
        public string Lac { get; set; }

        [IgnoreMap]
        [ExcelColumn("PN码")]
        [MemberDoc("PN码")]
        public short Pn { get; set; }

        [ExcelColumn("是否是RRU(来自：RRU拉远表)")]
        public string IsRru { get; set; }

        [ExcelColumn("TRM单板号")]
        public byte TrmId { get; set; }

        [ExcelColumn("RRU覆盖名称(来自：RRU拉远表)")]
        public string RruName { get; set; }
    }
}