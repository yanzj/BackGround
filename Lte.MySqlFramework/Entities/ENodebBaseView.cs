using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities;
using Lte.Domain.Common.Geo;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Regular.Attributes;

namespace Lte.MySqlFramework.Entities
{
    [AutoMapFrom(typeof(ENodebBase))]
    [TypeDoc("集团网优平台基站视图")]
    public class ENodebBaseView : IENodebId, IGeoPoint<double?>
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
        
        [MemberDoc("eNBID名称")]
        public string ENodebName { get; set; }

        [MemberDoc("eNBID采集名称")]
        public string ENodebFormalName { get; set; }

        [MemberDoc("厂家")]
        [AutoMapPropertyResolve("ENodebFactory", typeof(ENodebBase), typeof(ENodebFactoryDescriptionTransform))]
        public string ENodebFactoryDescription { get; set; }

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
        [AutoMapPropertyResolve("Duplexing", typeof(ENodebBase), typeof(DuplexingDescriptionTransform))]
        public string DuplexingDescription { get; set; }

        [MemberDoc("小区数量")]
        public int? TotalCells { get; set; }

        [MemberDoc("omc中基站运行状态")]
        [AutoMapPropertyResolve("OmcState", typeof(ENodebBase), typeof(OmcStateDescriptionTransform))]
        public string OmcStateDescription { get; set; }

        [MemberDoc("基站电子序列号")]
        public string ENodebSerial { get; set; }

        [MemberDoc("基站类型")]
        [AutoMapPropertyResolve("ENodebType", typeof(ENodebBase), typeof(ENodebTypeDescriptionTransform))]
        public string ENodebTypeDescription { get; set; }

        [MemberDoc("基站等级")]
        [AutoMapPropertyResolve("ENodebClass", typeof(ENodebBase), typeof(ENodebClassDescriptionTransform))]
        public string ENodebClassDescription { get; set; }

        [MemberDoc("基站经度")]
        public double? Longtitute { get; set; }

        [MemberDoc("基站纬度")]
        public double? Lattitute { get; set; }

        [MemberDoc("工程编码")]
        public string ProjectSerial { get; set; }

        [MemberDoc("是否共享基站")]
        [AutoMapPropertyResolve("IsENodebShared", typeof(ENodebBase), typeof(YesNoTransform))]
        public string ENodebShared { get; set; }

        [MemberDoc("OMCIP地址")]
        public string OmcIp { get; set; }

        [MemberDoc("入网日期")]
        public DateTime? FinishTime { get; set; }

        [MemberDoc("启用日期")]
        public DateTime? OpenTime { get; set; }

        [MemberDoc("启用日期维护方式")]
        public string OpenTimeUpdateFunction { get; set; }

        [MemberDoc("频段标识")]
        public string BandClass { get; set; }

        [MemberDoc("业务类型")]
        public string ServiceType { get; set; }

    }
}
