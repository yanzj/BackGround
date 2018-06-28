using System;
using Lte.Domain.Common.Geo;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Regular.Attributes;

namespace Lte.Domain.Excel
{
    public class StationRruExcel : IENodebId, IENodebName, IGeoPoint<double?>
    {
        [ExcelColumn("RRU标识")]
        public string RruSerialNum { get; set; }

        [ExcelColumn("所属站址编码")]
        public string StationNum { get; set; }

        [ExcelColumn("所属站址名称")]
        public string StationName { get; set; }

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

        [ExcelColumn("厂家")]
        public string ENodebFactoryDescription { get; set; }

        [ExcelColumn("型号")]
        public string RruModel { get; set; }

        [ExcelColumn("额定功率(w)")]
        public double? Power { get; set; }

        [ExcelColumn("双工模式")]
        public string DuplexingDescription { get; set; }

        [ExcelColumn("电源状态")]
        public string ElectricSourceDescription { get; set; }

        [ExcelColumn("是否防雷")]
        public string AntiThunder { get; set; }

        [ExcelColumn("RRU的Tx端口数")]
        public int? TransmitPorts { get; set; }

        [ExcelColumn("RRU的Rx端口数")]
        public int? ReceivePorts { get; set; }

        [ExcelColumn("RRU的厂家定位信息")]
        public string FactoryInfo { get; set; }

        [ExcelColumn("RRU的电子序列号")]
        public string ElectricSerialNum { get; set; }

        [ExcelColumn("所属小区标识")]
        public string CellSerialNum { get; set; }

        [ExcelColumn("是否人工导入小区")]
        public string ManualInput { get; set; }

        [ExcelColumn("工程编码")]
        public string ProjectSerial { get; set; }

        [ExcelColumn("RRU等级")]
        public string ENodebClassDescription { get; set; }

        [ExcelColumn("室分编码")]
        public string IndoorDistributionSerial { get; set; }

        [ExcelColumn("是否室分信源RRU")]
        public string IndoorSource { get; set; }

        [ExcelColumn("RRU经度")]
        public double? Longtitute { get; set; }

        [ExcelColumn("RRU纬度")]
        public double? Lattitute { get; set; }

        [ExcelColumn("RRU地址")]
        public string Address { get; set; }

        [ExcelColumn("RRU安装位置")]
        public string Position { get; set; }

        [ExcelColumn("共享属性")]
        public string OperatorUsageDescription { get; set; }

        [ExcelColumn("共享方式")]
        public string ShareFunctionDescription { get; set; }

        [ExcelColumn("是否虚拟RRU")]
        public string VirtualRru { get; set; }

        [ExcelColumn("入网日期")]
        public DateTime? OpenDate { get; set; }

        [ExcelColumn("启用日期")]
        public DateTime? UsingDate { get; set; }

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
