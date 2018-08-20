using System;
using Lte.Domain.Common.Geo;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Regular.Attributes;

namespace Lte.Domain.Excel
{
    public class ENodebBaseExcel : IENodebId, IGeoPoint<double?>
    {
        [ExcelColumn("eNBID")]
        public int ENodebId { get; set; }

        [ExcelColumn("所属站址编码")]
        public string StationNum { get; set; }

        [ExcelColumn("所属站址名称")]
        public string StationName { get; set; }

        [ExcelColumn("所属铁塔站址编码")]
        public string TowerStationNum { get; set; }

        [ExcelColumn("所属铁塔站址名称")]
        public string TowerStationName { get; set; }

        [ExcelColumn("省/自治区/直辖市")]
        public string Province { get; set; }

        [ExcelColumn("市/地区/州/盟")]
        public string City { get; set; }

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

        [ExcelColumn("eNBID名称")]
        public string ENodebName { get; set; }

        [ExcelColumn("eNBID采集名称")]
        public string ENodebFormalName { get; set; }

        [ExcelColumn("厂家")]
        public string ENodebFactoryDescription { get; set; }

        [ExcelColumn("设备型号")]
        public string ApplianceModel { get; set; }  

        [ExcelColumn("IPV4地址")]
        public string Ipv4Address { get; set; }

        [ExcelColumn("子网掩码")]
        public string SubNetMask { get; set; }

        [ExcelColumn("网关地址")]
        public string GatewayIp { get; set; }

        [ExcelColumn("S1配置带宽(Mbps)")]
        public double? S1Bandwidth { get; set; }

        [ExcelColumn("所属MME-1标识")]
        public string Mme1Info { get; set; }

        [ExcelColumn("所属MME-2标识")]
        public string Mme2Info { get; set; }

        [ExcelColumn("eNBID软件版本")]
        public string ENodebSoftwareVersion { get; set; }

        [ExcelColumn("双工模式")]
        public string DuplexingDescription { get; set; }

        [ExcelColumn("小区数量")]
        public int? TotalCells { get; set; }

        [ExcelColumn("omc中基站运行状态")]
        public string OmcStateDescription { get; set; }

        [ExcelColumn("基站电子序列号")]
        public string ENodebSerial { get; set; }

        [ExcelColumn("基站类型")]
        public string ENodebTypeDescription { get; set; }

        [ExcelColumn("基站等级")]
        public string ENodebClassDescription { get; set; }

        [ExcelColumn("基站经度")]
        public double? Longtitute { get; set; }

        [ExcelColumn("基站纬度")]
        public double? Lattitute { get; set; }

        [ExcelColumn("工程编码")]
        public string ProjectSerial { get; set; }

        [ExcelColumn("是否共享基站")]
        public string ENodebShared { get; set; }

        [ExcelColumn("OMCID")]
        public string OmcIp { get; set; }

        [ExcelColumn("入网日期")]
        public DateTime? FinishTime { get; set; }

        [ExcelColumn("启用日期")]
        public DateTime? OpenTime { get; set; }

        [ExcelColumn("频段标识")]
        public string BandClass { get; set; }

        [ExcelColumn("业务类型")]
        public string ServiceType { get; set; }

        [ExcelColumn("启用日期维护方式")]
        public string OpenTimeUpdateFunction { get; set; }

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
