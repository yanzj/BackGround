using System;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Common.Wireless.Cell;
using Lte.Domain.Regular.Attributes;

namespace Lte.Domain.Excel
{
    public class ConstructionExcel: ILteCellQuery, IENodebName, ILteCellParameters<short?, int?>
    {
        [ExcelColumn("小区标识")]
        public string CellSerialNum { get; set; }

        [ExcelColumn("所属eNBID")]
        public int ENodebId { get; set; }

        [ExcelColumn("所属eNBID名称")]
        public string ENodebName { get; set; }

        [ExcelColumn("区/市/县/旗")]
        public string StationDistrict { get; set; }

        [ExcelColumn("乡/镇/街道")]
        public string StationTown { get; set; }

        [ExcelColumn("划小单元")]
        public string SmallUnit { get; set; }

        [ExcelColumn("营服中心/营业部")]
        public string MarketCenter { get; set; }

        [ExcelColumn("片区")]
        public string StationRegion { get; set; }

        [ExcelColumn("簇")]
        public string StationCluster { get; set; }

        [ExcelColumn("网格")]
        public string Grid { get; set; }

        [ExcelColumn("cellId")]
        public byte SectorId { get; set; }

        [ExcelColumn("厂家内部cellId")]
        public byte LocalCellId { get; set; }

        [ExcelColumn("小区名称")]
        public string CellName { get; set; }

        [ExcelColumn("ECI")]
        public string Eci { get; set; }

        [ExcelColumn("PCI")]
        public short Pci { get; set; }

        [ExcelColumn("厂家")]
        public string ENodebFactoryDescription { get; set; }

        [ExcelColumn("双工模式")]
        public string DuplexingDescription { get; set; }

        [ExcelColumn("频段标识")]
        public byte BandClass { get; set; }

        [ExcelColumn("TAC")]
        public int? Tac { get; set; }

        [ExcelColumn("ZC根序列索引")]
        public short? Prach { get; set; }

        [ExcelColumn("上行中心频率(MHz)")]
        public double UplinkFrequencyMhz { get; set; }

        [ExcelColumn("下行中心频率(MHz)")]
        public double FrequencyMhz { get; set; }

        [ExcelColumn("上行中心频点号")]
        public int UplinkFrequency { get; set; }

        [ExcelColumn("下行中心频点号")]
        public int Frequency { get; set; }

        [ExcelColumn("上行带宽(MHz)")]
        public double UplinkBandwidth { get; set; }

        [ExcelColumn("下行带宽(MHz)")]
        public double Bandwidth { get; set; }

        [ExcelColumn("TD时隙比例")]
        public string TddConfig { get; set; }

        [ExcelColumn("TD特殊时隙配比")]
        public string TddSpecialConfig { get; set; }

        [ExcelColumn("小区等级")]
        public string ENodebClassDescription { get; set; }

        [ExcelColumn("小区覆盖类别")]
        public string CellCoverageDescription { get; set; }

        [ExcelColumn("天线拉远类别")]
        public string RemoteTypeDescription { get; set; }

        [ExcelColumn("小区多天线类别")]
        public string AntennaTypeDescription { get; set; }

        [ExcelColumn("是否与其它小区共天线")]
        public string CoAntennaWithOtherCells { get; set; }

        [ExcelColumn("天线编码")]
        public string AntennaSerial { get; set; }

        [ExcelColumn("室分编码")]
        public string IndoorDistributionSerial { get; set; }

        [ExcelColumn("备注")]
        public string Comments { get; set; }

        [ExcelColumn("数据人工更新时间")]
        public DateTime? DataUpdateTime { get; set; }

        [ExcelColumn("数据更新人")]
        public string DataUpdater { get; set; }

        [ExcelColumn("自定义1")]
        public string Customize1 { get; set; }

        [ExcelColumn("自定义2")]
        public string Customize2 { get; set; }

        [ExcelColumn("自定义3")]
        public string Customize3 { get; set; }
    }
}
