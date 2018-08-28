using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities.Kpi;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Regular.Attributes;

namespace Lte.MySqlFramework.Entities.Kpi
{
    [AutoMapFrom(typeof(HourPrb))]
    [TypeDoc("集团网管定义忙时PRB利用率统计")]
    public class HourPrbView : IStatTime, ILteCellQuery, IENodebName, ICityDistrictTown
    {
        public DateTime StatTime { get; set; }

        public int ENodebId { get; set; }

        public byte SectorId { get; set; }

        public string ENodebName { get; set; }

        public string District { get; set; }

        public string Town { get; set; }

        public string City { get; set; }
        
        [MemberDoc("4.5 上行可用的PRB个数(个)")]
        public int UplinkPrbCapacity { get; set; }

        [MemberDoc("4.6 上行控制信息实际平均占用PRB资源个数(个)")]
        public double PucchPrbs { get; set; }

        public double PucchPrbRate => UplinkPrbCapacity == 0 ? 0 : PucchPrbs / UplinkPrbCapacity;
        
        [MemberDoc("4.7 下行PDSCH信道可用的PRB个数(个)")]
        public int PdschPrbCapacity { get; set; }

        [MemberDoc("4.7 下行业务信息实际平均占用PRB资源个数(个)")]
        public double PdschTotalPrbs { get; set; }

        public double PdschPrbRate => PdschPrbCapacity == 0 ? 0 : PdschTotalPrbs / PdschPrbCapacity;

        [MemberDoc("4.7 下行业务信息实际平均占用PRB资源个数(QCI1)(个)")]
        public double PdschQci1Prbs { get; set; }

        public double PdschQci1PrbRate => PdschPrbCapacity == 0 ? 0 : PdschQci1Prbs / PdschPrbCapacity;

        [MemberDoc("4.7 下行业务信息实际平均占用PRB资源个数(QCI2)(个)")]
        public double PdschQci2Prbs { get; set; }

        public double PdschQci2PrbRate => PdschPrbCapacity == 0 ? 0 : PdschQci2Prbs / PdschPrbCapacity;

        [MemberDoc("4.7 下行业务信息实际平均占用PRB资源个数(QCI3)(个)")]
        public double PdschQci3Prbs { get; set; }

        public double PdschQci3PrbRate => PdschPrbCapacity == 0 ? 0 : PdschQci3Prbs / PdschPrbCapacity;

        [MemberDoc("4.7 下行业务信息实际平均占用PRB资源个数(QCI4)(个)")]
        public double PdschQci4Prbs { get; set; }

        public double PdschQci4PrbRate => PdschPrbCapacity == 0 ? 0 : PdschQci4Prbs / PdschPrbCapacity;

        [MemberDoc("4.7 下行业务信息实际平均占用PRB资源个数(QCI5)(个)")]
        public double PdschQci5Prbs { get; set; }

        public double PdschQci5PrbRate => PdschPrbCapacity == 0 ? 0 : PdschQci5Prbs / PdschPrbCapacity;

        [MemberDoc("4.7 下行业务信息实际平均占用PRB资源个数(QCI6)(个)")]
        public double PdschQci6Prbs { get; set; }

        public double PdschQci6PrbRate => PdschPrbCapacity == 0 ? 0 : PdschQci6Prbs / PdschPrbCapacity;

        [MemberDoc("4.7 下行业务信息实际平均占用PRB资源个数(QCI7)(个)")]
        public double PdschQci7Prbs { get; set; }

        public double PdschQci7PrbRate => PdschPrbCapacity == 0 ? 0 : PdschQci7Prbs / PdschPrbCapacity;

        [MemberDoc("4.7 下行业务信息实际平均占用PRB资源个数(QCI8)(个)")]
        public double PdschQci8Prbs { get; set; }

        public double PdschQci8PrbRate => PdschPrbCapacity == 0 ? 0 : PdschQci8Prbs / PdschPrbCapacity;

        [MemberDoc("4.7 下行业务信息实际平均占用PRB资源个数(QCI9)(个)")]
        public double PdschQci9Prbs { get; set; }

        public double PdschQci9PrbRate => PdschPrbCapacity == 0 ? 0 : PdschQci9Prbs / PdschPrbCapacity;

        [MemberDoc("4.8 下行控制信息实际平均占用PRB资源个数(个)")]
        public double DownlinkControlPrbs { get; set; }

        public double PdschControlRate => PdschPrbCapacity == 0 ? 0 : DownlinkControlPrbs / PdschPrbCapacity;

        [MemberDoc("4.9 上行实际平均占用PRB资源个数(个)")]
        public double UplinkTotalPrbs { get; set; }

        public double UplinkPrbRate => UplinkPrbCapacity == 0 ? 0 : UplinkTotalPrbs / UplinkPrbCapacity;
        
        [MemberDoc("4.10 下行实际平均占用PRB资源个数(个)")]
        public double DownlinkTotalPrbs { get; set; }

        public double DownlinkPrbRate => PdschPrbCapacity == 0 ? 0 : DownlinkTotalPrbs / PdschPrbCapacity;

        [MemberDoc("4.11 PDCCH的CCE可分配个数(个)")]
        public int PdcchCceCapacity { get; set; }

        [MemberDoc("4.11 PDCCH的CCE占用个数(个)")]
        public double PdcchTotalCces { get; set; }

        public double PdcchCceRate => PdcchCceCapacity == 0 ? 0 : PdcchTotalCces / PdcchCceCapacity;

        [MemberDoc("4.12 PRACH信道检测到的非竞争Preamble数量(个)")]
        public int PrachNonCompetitivePreambles { get; set; }

        [MemberDoc("4.12 PRACH信道检测到的竞争Preamble数量(个)")]
        public int PrachCompetitivePreambles { get; set; }

        [MemberDoc("4.12 小区可使用的非竞争Preamble个数(个)")]
        public int PrachNonCompetitiveCapacity { get; set; }

        public double PrachNonCompetitiveRate => PrachNonCompetitiveCapacity == 0
            ? 0
            : PrachNonCompetitivePreambles / PrachNonCompetitiveCapacity;

        [MemberDoc("4.12 小区可使用的竞争Preamble个数(个)")]
        public int PrachCompetitiveCapacity { get; set; }

        public double PrachCompetitiveRate => PrachCompetitiveCapacity == 0
            ? 0
            : PrachCompetitivePreambles / PrachCompetitiveCapacity;

        [MemberDoc("4.5 上行业务信息实际平均占用PRB资源个数(个)")]
        public double PuschTotalPrbs { get; set; }

        public double PuschPrbRate => UplinkPrbCapacity == 0 ? 0 : PuschTotalPrbs / UplinkPrbCapacity;

        [MemberDoc("4.5 上行业务信息实际平均占用PRB资源个数(QCI1)(个)")]
        public double PuschQci1Prbs { get; set; }

        public double PuschQci1Rate => UplinkPrbCapacity == 0 ? 0 : PuschQci1Prbs / UplinkPrbCapacity;

        [MemberDoc("4.5 上行业务信息实际平均占用PRB资源个数(QCI2)(个)")]
        public double PuschQci2Prbs { get; set; }

        public double PuschQci2Rate => UplinkPrbCapacity == 0 ? 0 : PuschQci2Prbs / UplinkPrbCapacity;

        [MemberDoc("4.5 上行业务信息实际平均占用PRB资源个数(QCI3)(个)")]
        public double PuschQci3Prbs { get; set; }

        public double PuschQci3Rate => UplinkPrbCapacity == 0 ? 0 : PuschQci3Prbs / UplinkPrbCapacity;

        [MemberDoc("4.5 上行业务信息实际平均占用PRB资源个数(QCI4)(个)")]
        public double PuschQci4Prbs { get; set; }

        public double PuschQci4Rate => UplinkPrbCapacity == 0 ? 0 : PuschQci4Prbs / UplinkPrbCapacity;

        [MemberDoc("4.5 上行业务信息实际平均占用PRB资源个数(QCI5)(个)")]
        public double PuschQci5Prbs { get; set; }

        public double PuschQci5Rate => UplinkPrbCapacity == 0 ? 0 : PuschQci5Prbs / UplinkPrbCapacity;

        [MemberDoc("4.5 上行业务信息实际平均占用PRB资源个数(QCI6)(个)")]
        public double PuschQci6Prbs { get; set; }

        public double PuschQci6Rate => UplinkPrbCapacity == 0 ? 0 : PuschQci6Prbs / UplinkPrbCapacity;

        [MemberDoc("4.5 上行业务信息实际平均占用PRB资源个数(QCI7)(个)")]
        public double PuschQci7Prbs { get; set; }

        public double PuschQci7Rate => UplinkPrbCapacity == 0 ? 0 : PuschQci7Prbs / UplinkPrbCapacity;

        [MemberDoc("4.5 上行业务信息实际平均占用PRB资源个数(QCI8)(个)")]
        public double PuschQci8Prbs { get; set; }

        public double PuschQci8Rate => UplinkPrbCapacity == 0 ? 0 : PuschQci8Prbs / UplinkPrbCapacity;

        [MemberDoc("4.5 上行业务信息实际平均占用PRB资源个数(QCI9)(个)")]
        public double PuschQci9Prbs { get; set; }

        public double PuschQci9Rate => UplinkPrbCapacity == 0 ? 0 : PuschQci9Prbs / UplinkPrbCapacity;

        [MemberDoc("4.14 寻呼信道容量(用户数/秒)")]
        public int PagingCapacity { get; set; }

        [MemberDoc("4.14 寻呼信道占用个数")]
        public double PagingCount { get; set; }

        public double PagingRate => PagingCapacity == 0 ? 0 : PagingCount / PagingCapacity;

    }
}
