using System;
using Abp.Domain.Entities;
using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Common.Geo;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Excel;
using Lte.Domain.Regular.Attributes;

namespace Abp.EntityFramework.Entities
{
    [AutoMapFrom(typeof(StationDictionaryExcel))]
    [TypeDoc("集团定义站址")]
    public class StationDictionary : Entity, IGeoPoint<double>
    {
        [MemberDoc("站址编码")]
        public string StationNum { get; set; }

        [MemberDoc("铁塔站址编码")]
        public string TowerStationNum { get; set; }

        [MemberDoc("铁塔站址名称")]
        public string TowerStationName { get; set; }

        [MemberDoc("区/市/县/旗")]
        public string StationDistrict { get; set; }

        [MemberDoc("乡//街道")]
        public string StationTown { get; set; }

        [MemberDoc("划小单元")]
        public string SmallUnit { get; set; }

        [MemberDoc("营服中心/营业部")]
        public string MarketCenter { get; set; }

        [MemberDoc("片区")]
        public string StationRegion { get; set; }

        [MemberDoc("簇")]
        public string StationCluster { get; set; }

        [MemberDoc("网格")]
        public string Grid { get; set; }

        [MemberDoc("站址名称")]
        public string ElementName { get; set; }

        [MemberDoc("站址经度")]
        public double Longtitute { get; set; }

        [MemberDoc("站址纬度")]
        public double Lattitute { get; set; }

        [MemberDoc("站址地址")]
        public string Address { get; set; }

        [MemberDoc("是否BBU堆叠站址")]
        [AutoMapPropertyResolve("BbuHeapStation", typeof(StationDictionaryExcel), typeof(YesToBoolTransform))]
        public bool IsBbuHeapStation { get; set; }

        [MemberDoc("是否C和L共站")]
        [AutoMapPropertyResolve("CAndLCoExist", typeof(StationDictionaryExcel), typeof(YesToBoolTransform))]
        public bool IsCAndLCoExist { get; set; }

        [MemberDoc("是否布放RRU设备")]
        [AutoMapPropertyResolve("RruBufang", typeof(StationDictionaryExcel), typeof(YesToBoolTransform))]
        public bool IsRruBufang { get; set; }

        [MemberDoc("引电方式")]
        [AutoMapPropertyResolve("ElectricFunctionDescription", typeof(StationDictionaryExcel), typeof(ElectricFunctionTransform))]
        public ElectricFunction ElectricFunction { get; set; }

        [MemberDoc("引电类型")]
        [AutoMapPropertyResolve("ElectricTypeDescription", typeof(StationDictionaryExcel), typeof(ElectricTypeTransform))]
        public ElectricType ElectricType { get; set; }

        [MemberDoc("引电合同编码")]
        public string ElectricContradictNum { get; set; }

        [MemberDoc("开关电源厂家")]
        public string SwitchSourceFactory { get; set; }

        [MemberDoc("开关电源型号")]
        public string SwitchSourceModel { get; set; }

        [MemberDoc("开关电源模块数")]
        public int? SwitchSourceModules { get; set; }

        [MemberDoc("开关电源容量(A)")]
        public double? SwitchSourceCapacities { get; set; }

        [MemberDoc("UPS厂家")]
        public string UpsFactory { get; set; }

        [MemberDoc("UPS型号")]
        public string UpsModel { get; set; }

        [MemberDoc("UPS容量（VA）")]
        public double? UpsCapacity { get; set; }

        [MemberDoc("蓄电池厂家")]
        public string BatteryFactory { get; set; }

        [MemberDoc("蓄电池类型")]
        [AutoMapPropertyResolve("BatteryTypeDescription", typeof(StationDictionaryExcel), typeof(BatteryTypeTransform))]
        public BatteryType BatteryType { get; set; }

        [MemberDoc("蓄电池型号")]
        public string BatteryModel { get; set; }

        [MemberDoc("蓄电池容量(Ah)")]
        public double? BatteryCapacity { get; set; }

        [MemberDoc("放电时长(小时)")]
        public double? BatteryHours { get; set; }

        [MemberDoc("蓄电池组数")]
        public int? BatteryGroups { get; set; }

        [MemberDoc("新风设备厂家")]
        public string XinfengFactory { get; set; }

        [MemberDoc("新风设备型号")]
        public string XinfengModel { get; set; }

        [MemberDoc("空调1厂家")]
        public string Ac1Factory { get; set; }

        [MemberDoc("空调1型号")]
        public string Ac1Model { get; set; }

        [MemberDoc("空调2厂家")]
        public string Ac2Factory { get; set; }

        [MemberDoc("空调2型号")]
        public string Ac2Model { get; set; }

        [MemberDoc("动环监控设备厂家")]
        public string DonghuanFactory { get; set; }

        [MemberDoc("动环监控设备型号")]
        public string DonghuanModel { get; set; }

        [MemberDoc("站址机房共用情况")]
        [AutoMapPropertyResolve("OperatorUsageDescription", typeof(StationDictionaryExcel), typeof(OperatorUsageTransform))]
        public OperatorUsage OperatorUsage { get; set; }

        [MemberDoc("站址机房产权方")]
        [AutoMapPropertyResolve("StationRoomBelongDescription", typeof(StationDictionaryExcel), typeof(OperatorTransform))]
        public Operator StationRoomBelong { get; set; }

        [MemberDoc("站址塔桅产权方")]
        [AutoMapPropertyResolve("StationTowerBelongDescription", typeof(StationDictionaryExcel), typeof(OperatorTransform))]
        public Operator StationTowerBelong { get; set; }

        [MemberDoc("站址发电责任方")]
        [AutoMapPropertyResolve("StationSupplyBelongDescription", typeof(StationDictionaryExcel), typeof(OperatorTransform))]
        public Operator StationSupplyBelong { get; set; }

        [MemberDoc("业主单位名称")]
        public string OwnerCompany { get; set; }

        [MemberDoc("业主联系人")]
        public string OwnerContact { get; set; }

        [MemberDoc("业主联系方式")]
        public string OwnerPhone { get; set; }

        [MemberDoc("业主租赁合同编码")]
        public string OwnerContradictNum { get; set; }

        [MemberDoc("塔桅类型")]
        [AutoMapPropertyResolve("TowerTypeDescription", typeof(StationDictionaryExcel), typeof(TowerTypeTransform))]
        public TowerType TowerType { get; set; }

        [MemberDoc("平台总数(个)")]
        public int TotalPlatforms { get; set; }

        [MemberDoc("塔桅高度(米)")]
        public double TowerHeight { get; set; }

        [MemberDoc("维护责任人工号")]
        public string MaintanenceStaffId { get; set; }

        [MemberDoc("维护责任人名称")]
        public string MaintanenceStaffName { get; set; }

        [MemberDoc("维护责任人电话号码")]
        public string MaintanenceStaffPhone { get; set; }

        [MemberDoc("维护责任人电子邮箱")]
        public string MaintanenceStaffEmail { get; set; }

        [MemberDoc("备注")]
        public string Comments { get; set; }

        [MemberDoc("数据人工更新时间")]
        public DateTime? DataUpdateTime { get; set; }

        [MemberDoc("数据更新人")]
        public string DataUpdater { get; set; }

        [MemberDoc("自定义1")]
        public string Customize1 { get; set; }

        [MemberDoc("自定义2")]
        public string Customize2 { get; set; }

        [MemberDoc("自定义3")]
        public string Customize3 { get; set; }
    }
}