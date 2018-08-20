using System;
using Lte.Domain.Common.Geo;
using Lte.Domain.Regular.Attributes;

namespace Lte.Domain.Excel
{
    public class StationDictionaryExcel : IGeoPoint<double>
    {
        [ExcelColumn("站址编码")]
        public string StationNum { get; set; }

        [ExcelColumn("铁塔站址编码")]
        public string TowerStationNum { get; set; }

        [ExcelColumn("铁塔站址名称")]
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

        [ExcelColumn("站址名称")]
        public string ElementName { get; set; }

        [ExcelColumn("站址等级")]
        public string ENodebClassDescription { get; set; }

        [ExcelColumn("站址经度")]
        public double Longtitute { get; set; }

        [ExcelColumn("站址纬度")]
        public double Lattitute { get; set; }

        [ExcelColumn("站址地址")]
        public string Address { get; set; }

        [ExcelColumn("是否BBU堆叠站址")]
        public string BbuHeapStation { get; set; }

        [ExcelColumn("是否C和L共站")]
        public string CAndLCoExist { get; set; }

        [ExcelColumn("是否布放RRU设备")]
        public string RruBufang { get; set; }

        [ExcelColumn("引电方式")]
        public string ElectricFunctionDescription { get; set; }

        [ExcelColumn("引电类型")]
        public string ElectricTypeDescription { get; set; }

        [ExcelColumn("引电合同编码")]
        public string ElectricContradictNum { get; set; }

        [ExcelColumn("开关电源厂家")]
        public string SwitchSourceFactory { get; set; }

        [ExcelColumn("开关电源型号")]
        public string SwitchSourceModel { get; set; }

        [ExcelColumn("开关电源模块数")]
        public int? SwitchSourceModules { get; set; }

        [ExcelColumn("开关电源容量(A)")]
        public double? SwitchSourceCapacities { get; set; }

        [ExcelColumn("UPS厂家")]
        public string UpsFactory { get; set; }

        [ExcelColumn("UPS型号")]
        public string UpsModel { get; set; }

        [ExcelColumn("UPS容量（VA）")]
        public double? UpsCapacity { get; set; }

        [ExcelColumn("蓄电池厂家")]
        public string BatteryFactory { get; set; }

        [ExcelColumn("蓄电池类型")]
        public string BatteryTypeDescription { get; set; }

        [ExcelColumn("蓄电池型号")]
        public string BatteryModel { get; set; }

        [ExcelColumn("蓄电池容量(Ah)")]
        public double? BatteryCapacity { get; set; }

        [ExcelColumn("放电时长(小时)")]
        public double? BatteryHours { get; set; }

        [ExcelColumn("蓄电池组数")]
        public int? BatteryGroups { get; set; }

        [ExcelColumn("新风设备厂家")]
        public string XinfengFactory { get; set; }

        [ExcelColumn("新风设备型号")]
        public string XinfengModel { get; set; }

        [ExcelColumn("空调1厂家")]
        public string Ac1Factory { get; set; }

        [ExcelColumn("空调1型号")]
        public string Ac1Model { get; set; }

        [ExcelColumn("空调2厂家")]
        public string Ac2Factory { get; set; }

        [ExcelColumn("空调2型号")]
        public string Ac2Model { get; set; }

        [ExcelColumn("动环监控设备厂家")]
        public string DonghuanFactory { get; set; }

        [ExcelColumn("动环监控设备型号")]
        public string DonghuanModel { get; set; }

        [ExcelColumn("站址机房共用情况")]
        public string OperatorUsageDescription { get; set; }

        [ExcelColumn("站址机房产权方")]
        public string StationRoomBelongDescription { get; set; }

        [ExcelColumn("站址塔桅产权方")]
        public string StationTowerBelongDescription { get; set; }

        [ExcelColumn("站址发电责任方")]
        public string StationSupplyBelongDescription { get; set; }

        [ExcelColumn("业主单位名称")]
        public string OwnerCompany { get; set; }

        [ExcelColumn("业主联系人")]
        public string OwnerContact { get; set; }

        [ExcelColumn("业主联系方式")]
        public string OwnerPhone { get; set; }

        [ExcelColumn("业主租赁合同编码")]
        public string OwnerContradictNum { get; set; }

        [ExcelColumn("塔桅类型")]
        public string TowerTypeDescription { get; set; }

        [ExcelColumn("平台总数(个)")]
        public int TotalPlatforms { get; set; }

        [ExcelColumn("塔桅高度(米)")]
        public double TowerHeight { get; set; }

        [ExcelColumn("维护责任人工号")]
        public string MaintanenceStaffId { get; set; }

        [ExcelColumn("维护责任人名称")]
        public string MaintanenceStaffName { get; set; }

        [ExcelColumn("维护责任人电话号码")]
        public string MaintanenceStaffPhone { get; set; }

        [ExcelColumn("维护责任人电子邮箱")]
        public string MaintanenceStaffEmail { get; set; }

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