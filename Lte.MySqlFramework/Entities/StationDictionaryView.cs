using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Station;
using Lte.Domain.Common.Geo;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Common.Wireless.Antenna;
using Lte.Domain.Common.Wireless.Station;
using Lte.Domain.Regular.Attributes;

namespace Lte.MySqlFramework.Entities
{
    [AutoMapFrom(typeof(StationDictionary))]
    [TypeDoc("集团定义站址视图")]
    public class StationDictionaryView : IGeoPoint<double>
    {
        [MemberDoc("站址编码")]
        public string StationNum { get; set; }

        [MemberDoc("铁塔站址编码")]
        public string TowerStationNum { get; set; }

        [MemberDoc("铁塔站址名称")]
        public string TowerStationName { get; set; }

        [MemberDoc("区/市/县/旗")]
        public string StationDistrict { get; set; }

        [MemberDoc("乡/镇/街道")]
        public string StationTown { get; set; }

        [MemberDoc("站址名称")]
        public string ElementName { get; set; }

        [MemberDoc("站址经度")]
        public double Longtitute { get; set; }

        [MemberDoc("站址纬度")]
        public double Lattitute { get; set; }

        [MemberDoc("站址地址")]
        public string Address { get; set; }

        [MemberDoc("是否BBU堆叠站址")]
        [AutoMapPropertyResolve("IsBbuHeapStation", typeof(StationDictionary), typeof(YesNoTransform))]
        public string BbuHeapStation { get; set; }

        [MemberDoc("是否C和L共站")]
        [AutoMapPropertyResolve("IsCAndLCoExist", typeof(StationDictionary), typeof(YesNoTransform))]
        public string CAndLCoExist { get; set; }

        [MemberDoc("是否布放RRU设备")]
        [AutoMapPropertyResolve("IsRruBufang", typeof(StationDictionary), typeof(YesNoTransform))]
        public string RruBufang { get; set; }

        [MemberDoc("引电方式")]
        [AutoMapPropertyResolve("ElectricFunction", typeof(StationDictionary), typeof(ElectricFunctionDescriptionTransform))]
        public string ElectricFunctionDescription { get; set; }

        [MemberDoc("引电类型")]
        [AutoMapPropertyResolve("ElectricType", typeof(StationDictionary), typeof(ElectricTypeDescriptionTransform))]
        public string ElectricTypeDescription { get; set; }

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
        [AutoMapPropertyResolve("BatteryType", typeof(StationDictionary), typeof(BatteryTypeDescriptionTransform))]
        public string BatteryTypeDescription { get; set; }

        [MemberDoc("蓄电池型号")]
        public string BatteryModel { get; set; }

        [MemberDoc("蓄电池容量(Ah)")]
        public double? BatteryCapacity { get; set; }

        [MemberDoc("放电时长(小时)")]
        public double? BatteryHours { get; set; }

        [MemberDoc("蓄电池组数")]
        public int? BatteryGroups { get; set; }

        [MemberDoc("站址机房共用情况")]
        [AutoMapPropertyResolve("OperatorUsage", typeof(StationDictionary), typeof(OperatorUsageDescriptionTransform))]
        public string OperatorUsageDescription { get; set; }

        [MemberDoc("站址机房产权方")]
        [AutoMapPropertyResolve("StationRoomBelong", typeof(StationDictionary), typeof(OperatorDescriptionTransform))]
        public string StationRoomBelongDescription { get; set; }

        [MemberDoc("站址塔桅产权方")]
        [AutoMapPropertyResolve("StationTowerBelong", typeof(StationDictionary), typeof(OperatorDescriptionTransform))]
        public string StationTowerBelongDescription { get; set; }

        [MemberDoc("站址发电责任方")]
        [AutoMapPropertyResolve("StationSupplyBelong", typeof(StationDictionary), typeof(OperatorDescriptionTransform))]
        public string StationSupplyBelongDescription { get; set; }

        [MemberDoc("业主单位名称")]
        public string OwnerCompany { get; set; }

        [MemberDoc("业主联系人")]
        public string OwnerContact { get; set; }

        [MemberDoc("业主联系方式")]
        public string OwnerPhone { get; set; }

        [MemberDoc("业主租赁合同编码")]
        public string OwnerContradictNum { get; set; }

        [MemberDoc("塔桅类型")]
        [AutoMapPropertyResolve("TowerType", typeof(StationDictionary), typeof(TowerTypeDescriptionTransform))]
        public string TowerTypeDescription { get; set; }

        [MemberDoc("平台总数(个)")]
        public int TotalPlatforms { get; set; }

        [MemberDoc("塔桅高度(米)")]
        public double TowerHeight { get; set; }

    }
}
