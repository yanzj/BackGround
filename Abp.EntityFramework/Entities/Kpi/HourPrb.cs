using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Common.Wireless.Kpi;
using Lte.Domain.Regular.Attributes;

namespace Abp.EntityFramework.Entities.Kpi
{
    [TypeDoc("集团网管定义忙时PRB利用率统计")]
    [AutoMapFrom(typeof(HourPrbCsv))]
    public class HourPrb: Entity, IStatTime, ILteCellQuery
    {
        [MemberDoc("时间")]
        public DateTime StatTime { get; set; }

        [MemberDoc("ENBID")]
        public int ENodebId { get; set; }

        [MemberDoc("小区ID")]
        public byte SectorId { get; set; }

        [MemberDoc("小区标识")]
        public string CellSerialNumber { get; set; }

        [MemberDoc("4.5 上行可用的PRB个数(个)")]
        public int UplinkPrbCapacity { get; set; }

        [MemberDoc("4.6 上行控制信息实际平均占用PRB资源个数(个)")]
        public double PucchPrbs { get; set; }

        [MemberDoc("4.7 下行PDSCH信道可用的PRB个数(个)")]
        public int PdschPrbCapacity { get; set; }

        [MemberDoc("4.7 下行业务信息实际平均占用PRB资源个数(个)")]
        public double PdschTotalPrbs { get; set; }

        [MemberDoc("4.7 下行业务信息实际平均占用PRB资源个数(QCI1)(个)")]
        public double PdschQci1Prbs { get; set; }

        [MemberDoc("4.7 下行业务信息实际平均占用PRB资源个数(QCI2)(个)")]
        public double PdschQci2Prbs { get; set; }

        [MemberDoc("4.7 下行业务信息实际平均占用PRB资源个数(QCI3)(个)")]
        public double PdschQci3Prbs { get; set; }

        [MemberDoc("4.7 下行业务信息实际平均占用PRB资源个数(QCI4)(个)")]
        public double PdschQci4Prbs { get; set; }

        [MemberDoc("4.7 下行业务信息实际平均占用PRB资源个数(QCI5)(个)")]
        public double PdschQci5Prbs { get; set; }

        [MemberDoc("4.7 下行业务信息实际平均占用PRB资源个数(QCI6)(个)")]
        public double PdschQci6Prbs { get; set; }

        [MemberDoc("4.7 下行业务信息实际平均占用PRB资源个数(QCI7)(个)")]
        public double PdschQci7Prbs { get; set; }

        [MemberDoc("4.7 下行业务信息实际平均占用PRB资源个数(QCI8)(个)")]
        public double PdschQci8Prbs { get; set; }

        [MemberDoc("4.7 下行业务信息实际平均占用PRB资源个数(QCI9)(个)")]
        public double PdschQci9Prbs { get; set; }

        [MemberDoc("4.8 下行控制信息实际平均占用PRB资源个数(个)")]
        public double DownlinkControlPrbs { get; set; }

        [MemberDoc("4.9 上行实际平均占用PRB资源个数(个)")]
        public double UplinkTotalPrbs { get; set; }

        [MemberDoc("4.10 下行实际平均占用PRB资源个数(个)")]
        public double DownlinkTotalPrbs { get; set; }

        [MemberDoc("4.11 PDCCH的CCE可分配个数(个)")]
        public int PdcchCceCapacity { get; set; }

        [MemberDoc("4.11 PDCCH的CCE占用个数(个)")]
        public double PdcchTotalCces { get; set; }

        [MemberDoc("4.12 PRACH信道检测到的非竞争Preamble数量(个)")]
        public int PrachNonCompetitivePreambles { get; set; }

        [MemberDoc("4.12 PRACH信道检测到的竞争Preamble数量(个)")]
        public int PrachCompetitivePreambles { get; set; }

        [MemberDoc("4.12 小区可使用的非竞争Preamble个数(个)")]
        public int PrachNonCompetitiveCapacity { get; set; }

        [MemberDoc("4.12 小区可使用的竞争Preamble个数(个)")]
        public int PrachCompetitiveCapacity { get; set; }

        [MemberDoc("4.5 上行业务信息实际平均占用PRB资源个数(个)")]
        public double PuschTotalPrbs { get; set; }

        [MemberDoc("4.5 上行业务信息实际平均占用PRB资源个数(QCI1)(个)")]
        public double PuschQci1Prbs { get; set; }

        [MemberDoc("4.5 上行业务信息实际平均占用PRB资源个数(QCI2)(个)")]
        public double PuschQci2Prbs { get; set; }

        [MemberDoc("4.5 上行业务信息实际平均占用PRB资源个数(QCI3)(个)")]
        public double PuschQci3Prbs { get; set; }

        [MemberDoc("4.5 上行业务信息实际平均占用PRB资源个数(QCI4)(个)")]
        public double PuschQci4Prbs { get; set; }

        [MemberDoc("4.5 上行业务信息实际平均占用PRB资源个数(QCI5)(个)")]
        public double PuschQci5Prbs { get; set; }

        [MemberDoc("4.5 上行业务信息实际平均占用PRB资源个数(QCI6)(个)")]
        public double PuschQci6Prbs { get; set; }

        [MemberDoc("4.5 上行业务信息实际平均占用PRB资源个数(QCI7)(个)")]
        public double PuschQci7Prbs { get; set; }

        [MemberDoc("4.5 上行业务信息实际平均占用PRB资源个数(QCI8)(个)")]
        public double PuschQci8Prbs { get; set; }

        [MemberDoc("4.5 上行业务信息实际平均占用PRB资源个数(QCI9)(个)")]
        public double PuschQci9Prbs { get; set; }

        [MemberDoc("4.14 寻呼信道容量(用户数/秒)")]
        public int PagingCapacity { get; set; }

        [MemberDoc("4.14 寻呼信道占用个数")]
        public double PagingCount { get; set; }
    }
}
