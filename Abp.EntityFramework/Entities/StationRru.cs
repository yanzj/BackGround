using System;
using Abp.Domain.Entities;
using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Common.Geo;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Excel;
using Lte.Domain.Regular;
using Lte.Domain.Regular.Attributes;

namespace Abp.EntityFramework.Entities
{
    [AutoMapFrom(typeof(StationRruExcel))]
    [TypeDoc("集团网优平台RRU信息")]
    public class StationRru : Entity, IENodebId, IENodebName, IGeoPoint<double?>
    {
        [MemberDoc("RRU标识")]
        public string RruSerialNum { get; set; }

        public int RackId => RruSerialNum.GetRruRackId(ENodebFactory);

        [MemberDoc("所属站址编码")]
        public string StationNum { get; set; }

        [MemberDoc("所属站址名称")]
        public string StationName { get; set; }

        [MemberDoc("所属铁塔站址编码")]
        public string TowerStationNum { get; set; }

        [MemberDoc("所属铁塔站址名称")]
        public string TowerStationName { get; set; }

        [MemberDoc("所属eNBID")]
        public int ENodebId { get; set; }

        [MemberDoc("所属eNBID名称")]
        public string ENodebName { get; set; }

        [MemberDoc("区/市/县/旗")]
        public string StationDistrict { get; set; }

        [MemberDoc("乡/镇/街道")]
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

        [MemberDoc("RRU名称")]
        public string RruName { get; set; }

        [MemberDoc("厂家")]
        [AutoMapPropertyResolve("ENodebFactoryDescription", typeof(StationRruExcel), typeof(ENodebFactoryTransform))]
        public ENodebFactory ENodebFactory { get; set; }

        [MemberDoc("型号")]
        public string RruModel { get; set; }

        [MemberDoc("额定功率(w)")]
        public double? Power { get; set; }

        [MemberDoc("双工模式")]
        [AutoMapPropertyResolve("DuplexingDescription", typeof(StationRruExcel), typeof(DuplexingTransform))]
        public Duplexing Duplexing { get; set; }

        [MemberDoc("电源状态")]
        [AutoMapPropertyResolve("ElectricSourceDescription", typeof(StationRruExcel), typeof(ElectricSourceTransform))]
        public ElectricSource ElectricSource { get; set; }

        [MemberDoc("是否防雷")]
        [AutoMapPropertyResolve("AntiThunder", typeof(StationRruExcel), typeof(YesToBoolTransform))]
        public bool IsAntiThunder { get; set; }

        [MemberDoc("RRU的Tx端口数")]
        public int? TransmitPorts { get; set; }

        [MemberDoc("RRU的Rx端口数")]
        public int? ReceivePorts { get; set; }

        [MemberDoc("RRU的厂家定位信息")]
        public string FactoryInfo { get; set; }

        [MemberDoc("RRU的电子序列号")]
        public string ElectricSerialNum { get; set; }

        [MemberDoc("所属小区标识")]
        public string CellSerialNum { get; set; }

        [MemberDoc("是否人工导入小区")]
        [AutoMapPropertyResolve("ManualInput", typeof(StationRruExcel), typeof(YesToBoolTransform))]
        public bool IsManualInput { get; set; }

        [MemberDoc("工程编码")]
        public string ProjectSerial { get; set; }

        [MemberDoc("RRU等级")]
        [AutoMapPropertyResolve("ENodebClassDescription", typeof(StationRruExcel), typeof(ENodebClassTransform))]
        public ENodebClass ENodebClass { get; set; }

        [MemberDoc("室分编码")]
        public string IndoorDistributionSerial { get; set; }

        [MemberDoc("是否室分信源RRU")]
        [AutoMapPropertyResolve("IndoorSource", typeof(StationRruExcel), typeof(YesToBoolTransform))]
        public bool IsIndoorSource { get; set; }

        [MemberDoc("RRU经度")]
        public double? Longtitute { get; set; }

        [MemberDoc("RRU纬度")]
        public double? Lattitute { get; set; }

        [MemberDoc("RRU地址")]
        public string Address { get; set; }

        [MemberDoc("RRU安装位置")]
        public string Position { get; set; }

        [MemberDoc("共享属性")]
        [AutoMapPropertyResolve("OperatorUsageDescription", typeof(StationRruExcel), typeof(OperatorUsageTransform))]
        public OperatorUsage OperatorUsage { get; set; }

        [MemberDoc("共享方式")]
        [AutoMapPropertyResolve("ShareFunctionDescription", typeof(StationRruExcel), typeof(ShareFunctionTransform))]
        public ShareFunction ShareFunction { get; set; }

        [MemberDoc("是否虚拟RRU")]
        [AutoMapPropertyResolve("VirtualRru", typeof(StationRruExcel), typeof(YesToBoolTransform))]
        public bool IsVirtualRru { get; set; }

        [MemberDoc("入网日期")]
        public DateTime? OpenDate { get; set; }

        [MemberDoc("启用日期")]
        public DateTime? UsingDate { get; set; }

        public FrequencyBandType FrequencyBandType { get; set; }

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
