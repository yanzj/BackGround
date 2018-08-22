using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Dependency;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Regular.Attributes;

namespace Lte.MySqlFramework.Entities.RegionKpi
{
    [AutoMapFrom(typeof(TownHourUsersView))]
    [TypeDoc("区忙时用户数统计")]
    public class DistrictHourUsersView : ICityDistrict, IStatDate
    {
        public string District { get; set; }

        public string City { get; set; }

        public DateTime StatDate { get; set; }

        [MemberDoc("4.14 最大RRC连接用户数(个)")]
        public int MaxRrcUsers { get; set; }

        [MemberDoc("4.16 平均RRC连接用户数(个)")]
        public double AverageRrcUsers { get; set; }

        [MemberDoc("4.17 上行平均激活用户数(个)")]
        public double UplinkAverageActiveUsers { get; set; }

        [MemberDoc("4.17 上行平均激活用户数(QCI1)(个)")]
        public double UplinkAverageActiveUsersQci1 { get; set; }

        [MemberDoc("4.17 上行平均激活用户数(QCI2)(个)")]
        public double UplinkAverageActiveUsersQci2 { get; set; }

        [MemberDoc("4.17 上行平均激活用户数(QCI3)(个)")]
        public double UplinkAverageActiveUsersQci3 { get; set; }

        [MemberDoc("4.17 上行平均激活用户数(QCI4)(个)")]
        public double UplinkAverageActiveUsersQci4 { get; set; }

        [MemberDoc("4.17 上行平均激活用户数(QCI5)(个)")]
        public double UplinkAverageActiveUsersQci5 { get; set; }

        [MemberDoc("4.17 上行平均激活用户数(QCI6)(个)")]
        public double UplinkAverageActiveUsersQci6 { get; set; }

        [MemberDoc("4.17 上行平均激活用户数(QCI7)(个)")]
        public double UplinkAverageActiveUsersQci7 { get; set; }

        [MemberDoc("4.17 上行平均激活用户数(QCI8)(个)")]
        public double UplinkAverageActiveUsersQci8 { get; set; }

        [MemberDoc("4.17 上行平均激活用户数(QCI9)(个)")]
        public double UplinkAverageActiveUsersQci9 { get; set; }

        [MemberDoc("4.18 下行平均激活用户数(个)")]
        public double DownlinkAverageActiveUsers { get; set; }

        [MemberDoc("4.18 下行平均激活用户数(QCI1)(个)")]
        public double DownlinkAverageActiveUsersQci1 { get; set; }

        [MemberDoc("4.18 下行平均激活用户数(QCI2)(个)")]
        public double DownlinkAverageActiveUsersQci2 { get; set; }

        [MemberDoc("4.18 下行平均激活用户数(QCI3)(个)")]
        public double DownlinkAverageActiveUsersQci3 { get; set; }

        [MemberDoc("4.18 下行平均激活用户数(QCI4)(个)")]
        public double DownlinkAverageActiveUsersQci4 { get; set; }

        [MemberDoc("4.18 下行平均激活用户数(QCI5)(个)")]
        public double DownlinkAverageActiveUsersQci5 { get; set; }

        [MemberDoc("4.18 下行平均激活用户数(QCI6)(个)")]
        public double DownlinkAverageActiveUsersQci6 { get; set; }

        [MemberDoc("4.18 下行平均激活用户数(QCI7)(个)")]
        public double DownlinkAverageActiveUsersQci7 { get; set; }

        [MemberDoc("4.18 下行平均激活用户数(QCI8)(个)")]
        public double DownlinkAverageActiveUsersQci8 { get; set; }

        [MemberDoc("4.18 下行平均激活用户数(QCI9)(个)")]
        public double DownlinkAverageActiveUsersQci9 { get; set; }

        [MemberDoc("4.20 最大激活用户数(个)")]
        public int MaxActiveUsers { get; set; }

        [MemberDoc("4.21 小区平均CA能力用户数(个)")]
        public double? AverageCaUsers { get; set; }

        [MemberDoc("4.22 小区最大CA能力用户数(个)")]
        public int MaxCaUsers { get; set; }

        [MemberDoc("4.23 PCell小区下行CA平均激活UE数(个)")]
        public double PCellDownlinkAverageCaUes { get; set; }

        [MemberDoc("4.24 PCell小区下行CA最大激活UE数(个)")]
        public int? PCellDownlinkMaxCaUes { get; set; }

        [MemberDoc("4.25 Pcell分配给CA用户的下行PDSCH PRB总数(个)")]
        public long PCellPdschCaPrbs { get; set; }

        [MemberDoc("4.26 Scell分配给CA用户的下行PDSCH PRB总数(个)")]
        public long SCellPdschCaPrbs { get; set; }

        [MemberDoc("4.27 小区上行CoMP状态的平均用户数(个)")]
        public double UplinkCompAverageUsers { get; set; }

        [MemberDoc("4.28 小区上行CoMP状态的最大用户数(个)")]
        public int? UplinkCompMaxUsers { get; set; }

        public static DistrictHourUsersView ConstructView(TownHourUsersView townView)
        {
            return townView.MapTo<DistrictHourUsersView>();
        }
    }
}
