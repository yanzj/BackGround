using System;
using Abp.Domain.Entities;
using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Excel;
using Lte.Domain.Regular.Attributes;

namespace Abp.EntityFramework.Entities
{
    [AutoMapFrom(typeof(ENodebBaseExcel))]
    [TypeDoc("集团网优平台基站定义")]
    public class ENodebBase : Entity
    {
        [MemberDoc("eNBID")]
        public int ENodebId { get; set; }

        [MemberDoc("所属站址编码")]
        public string StationNum { get; set; }

        [MemberDoc("所属站址名称")]
        public string StationName { get; set; }

        [MemberDoc("所属铁塔站址编码")]
        public string TowerStationNum { get; set; }

        [MemberDoc("所属铁塔站址名称")]
        public string TowerStationName { get; set; }

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

        [MemberDoc("eNBID名称")]
        public string ENodebName { get; set; }

        [MemberDoc("eNBID采集名称")]
        public string ENodebFormalName { get; set; }

        [MemberDoc("厂家")]
        [AutoMapPropertyResolve("ENodebFactoryDescription", typeof(ENodebBaseExcel), typeof(ENodebFactoryTransform))]
        public ENodebFactory ENodebFactory { get; set; }

        [MemberDoc("设备型号")]
        public string ApplianceModel { get; set; }

        [MemberDoc("IPV4地址")]
        public string Ipv4Address { get; set; }

        [MemberDoc("子网掩码")]
        public string SubNetMask { get; set; }

        [MemberDoc("网关地址")]
        public string GatewayIp { get; set; }

        [MemberDoc("S1配置带宽(Mbps)")]
        public double? S1Bandwidth { get; set; }

        [MemberDoc("所属MME-1标识")]
        public string Mme1Info { get; set; }

        [MemberDoc("所属MME-2标识")]
        public string Mme2Info { get; set; }

        [MemberDoc("eNBID软件版本")]
        public string ENodebSoftwareVersion { get; set; }

        [MemberDoc("双工模式")]
        [AutoMapPropertyResolve("DuplexingDescription", typeof(ENodebBaseExcel), typeof(DuplexingTransform))]
        public Duplexing Duplexing { get; set; }

        [MemberDoc("小区数量")]
        public int? TotalCells { get; set; }

        [MemberDoc("omc中基站运行状态")]
        [AutoMapPropertyResolve("OmcStateDescription", typeof(ENodebBaseExcel), typeof(OmcStateTransform))]
        public OmcState OmcState { get; set; }

        [MemberDoc("基站电子序列号")]
        public string ENodebSerial { get; set; }

        [MemberDoc("基站类型")]
        [AutoMapPropertyResolve("ENodebTypeDescription", typeof(ENodebBaseExcel), typeof(ENodebTypeTransform))]
        public ENodebType ENodebType { get; set; }

        [MemberDoc("基站等级")]
        [AutoMapPropertyResolve("ENodebClassDescription", typeof(ENodebBaseExcel), typeof(ENodebClassTransform))]
        public ENodebClass ENodebClass { get; set; }

        [MemberDoc("基站经度")]
        public double? Longtitute { get; set; }

        [MemberDoc("基站纬度")]
        public double? Lattitute { get; set; }

        [MemberDoc("工程编码")]
        public string ProjectSerial { get; set; }

        [MemberDoc("是否共享基站")]
        [AutoMapPropertyResolve("ENodebShared", typeof(ENodebBaseExcel), typeof(YesToBoolTransform))]
        public bool IsENodebShared { get; set; }

        [MemberDoc("OMCIP地址")]
        public string OmcIp { get; set; }

        [MemberDoc("入网日期")]
        public DateTime? FinishTime { get; set; }

        [MemberDoc("启用日期")]
        public DateTime? OpenTime { get; set; }

        [MemberDoc("频段标识")]
        public string BandClass { get; set; }

        [MemberDoc("业务类型")]
        public string ServiceType { get; set; }

        [MemberDoc("启用日期维护方式")]
        public string OpenTimeUpdateFunction { get; set; }

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