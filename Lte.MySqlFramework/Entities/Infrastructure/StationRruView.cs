using System;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities.Station;
using Lte.Domain.Common.Geo;
using Lte.Domain.Common.Transform;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Common.Wireless.Cell;
using Lte.Domain.Common.Wireless.ENodeb;
using Lte.Domain.Common.Wireless.Station;
using Lte.Domain.Regular.Attributes;

namespace Lte.MySqlFramework.Entities.Infrastructure
{
    [AutoMapFrom(typeof(StationRru))]
    [TypeDoc("集团网优平台RRU视图")]
    public class StationRruView : IENodebId, IENodebName, IGeoPoint<double?>
    {
        [MemberDoc("RRU标识")]
        public string RruSerialNum { get; set; }

        [MemberDoc("框号")]
        public int RackId { get; set; }

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

        [MemberDoc("省/自治区/直辖市")]
        public string Province { get; set; }

        [MemberDoc("市/地区/州/盟")]
        public string City { get; set; }

        [MemberDoc("区/市/县/旗")]
        public string StationDistrict { get; set; }

        [MemberDoc("乡/镇/街道")]
        public string StationTown { get; set; }

        [MemberDoc("RRU名称")]
        public string RruName { get; set; }

        [MemberDoc("厂家")]
        [AutoMapPropertyResolve("ENodebFactory", typeof(StationRru), typeof(ENodebFactoryDescriptionTransform))]
        public string ENodebFactoryDescription { get; set; }

        [MemberDoc("型号")]
        public string RruModel { get; set; }

        [MemberDoc("额定功率(w)")]
        public double? Power { get; set; }

        [MemberDoc("双工模式")]
        [AutoMapPropertyResolve("Duplexing", typeof(StationRru), typeof(DuplexingDescriptionTransform))]
        public string DuplexingDescription { get; set; }

        [MemberDoc("电源状态")]
        [AutoMapPropertyResolve("ElectricSource", typeof(StationRru), typeof(ElectricSourceDescriptionTransform))]
        public string ElectricSourceDescription { get; set; }

        [MemberDoc("是否防雷")]
        [AutoMapPropertyResolve("IsAntiThunder", typeof(StationRru), typeof(YesNoTransform))]
        public string AntiThunder { get; set; }

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
        [AutoMapPropertyResolve("IsManualInput", typeof(StationRru), typeof(YesNoTransform))]
        public string ManualInput { get; set; }

        [MemberDoc("工程编码")]
        public string ProjectSerial { get; set; }

        [MemberDoc("RRU等级")]
        [AutoMapPropertyResolve("ENodebClass", typeof(StationRru), typeof(ENodebClassDescriptionTransform))]
        public string ENodebClassDescription { get; set; }

        [MemberDoc("室分编码")]
        public string IndoorDistributionSerial { get; set; }

        [MemberDoc("是否室分信源RRU")]
        [AutoMapPropertyResolve("IsIndoorSource", typeof(StationRru), typeof(YesNoTransform))]
        public string IndoorSource { get; set; }

        [MemberDoc("RRU经度")]
        public double? Longtitute { get; set; }

        [MemberDoc("RRU纬度")]
        public double? Lattitute { get; set; }

        [MemberDoc("RRU地址")]
        public string Address { get; set; }

        [MemberDoc("RRU安装位置")]
        public string Position { get; set; }

        [MemberDoc("共享属性")]
        [AutoMapPropertyResolve("OperatorUsage", typeof(StationRru), typeof(OperatorUsageDescriptionTransform))]
        public string OperatorUsageDescription { get; set; }

        [MemberDoc("共享方式")]
        [AutoMapPropertyResolve("ShareFunction", typeof(StationRru), typeof(ShareFunctionDescriptionTransform))]
        public string ShareFunctionDescription { get; set; }

        [MemberDoc("是否共享RRU")]
        [AutoMapPropertyResolve("IsSharedRru", typeof(StationRru), typeof(YesNoTransform))]
        public string SharedRru { get; set; }

        [MemberDoc("是否虚拟RRU")]
        [AutoMapPropertyResolve("IsVirtualRru", typeof(StationRru), typeof(YesNoTransform))]
        public string VirtualRru { get; set; }

        [MemberDoc("入网日期")]
        public DateTime? OpenDate { get; set; }

        [MemberDoc("启用日期")]
        public DateTime? UsingDate { get; set; }

        public FrequencyBandType FrequencyBandType { get; set; }

    }
}
