using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Dependency;
using Abp.EntityFramework.Entities.Mr;
using Lte.Domain.Common.Wireless;
using Lte.Parameters.Entities.Kpi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lte.Evaluations.ViewModels.Mr
{
    [AutoMapFrom(typeof(MrsTadvStat))]
    [AutoMapTo(typeof(TopMrsTadv))]
    public class CellMrsTadvDto : IStatDate, ILteCellQuery, IENodebName
    {
        public DateTime StatDate { get; set; }

        public int ENodebId { get; set; }

        public byte SectorId { get; set; }

        public string ENodebName { get; set; }

        public string CellName => ENodebName + "-" + SectorId;

        public int Tadv_00 { get; set; }

        public int Tadv_01 { get; set; }

        public int Tadv_02 { get; set; }

        public int Tadv_03 { get; set; }

        public int Tadv_04 { get; set; }

        public int Tadv_05 { get; set; }

        public int Tadv_06 { get; set; }

        public int Tadv_07 { get; set; }

        public int Tadv_08 { get; set; }

        public int Tadv_09 { get; set; }

        public int Tadv_10 { get; set; }

        public int Tadv_11 { get; set; }

        public int Tadv_12 { get; set; }

        public int Tadv_13 { get; set; }

        public int Tadv_14 { get; set; }

        public int Tadv_15 { get; set; }

        public int Tadv_16 { get; set; }

        public int Tadv_17 { get; set; }

        public int Tadv_18 { get; set; }

        public int Tadv_19 { get; set; }

        public int Tadv_20 { get; set; }

        public int Tadv_21 { get; set; }

        public int Tadv_22 { get; set; }

        public int Tadv_23 { get; set; }

        public int Tadv_24 { get; set; }

        public int Tadv_25 { get; set; }

        public int Tadv_26 { get; set; }

        public int Tadv_27 { get; set; }

        public int Tadv_28 { get; set; }

        public int Tadv_29 { get; set; }

        public int Tadv_30 { get; set; }

        public int Tadv_31 { get; set; }

        public int Tadv_32 { get; set; }

        public int Tadv_33 { get; set; }

        public int Tadv_34 { get; set; }

        public int Tadv_35 { get; set; }

        public int Tadv_36 { get; set; }

        public int Tadv_37 { get; set; }

        public int Tadv_38 { get; set; }

        public int Tadv_39 { get; set; }

        public int Tadv_40 { get; set; }

        public int Tadv_41 { get; set; }

        public int Tadv_42 { get; set; }

        public int Tadv_43 { get; set; }

        public int Tadv_44 { get; set; }

        public int TadvBelow1 => Tadv_00;

        public int Tadv1To2 => Tadv_01;

        public int Tadv2To3 => Tadv_02;

        public int Tadv3To4 => Tadv_03;

        public int Tadv4To6 => Tadv_04 + Tadv_05;

        public int Tadv6To8 => Tadv_06 + Tadv_07;

        public int Tadv8To10 => Tadv_08 + Tadv_09;

        public int Tadv10To12 => Tadv_10 + Tadv_11;

        public int Tadv12To14 => Tadv_12;

        public int Tadv14To16 => Tadv_13;

        public int Tadv16To18 => Tadv_14;

        public int Tadv18To20 => Tadv_15;

        public int Tadv20To24 => Tadv_16 + Tadv_17;

        public int Tadv24To28 => Tadv_18 + Tadv_19;

        public int Tadv28To32 => Tadv_20 + Tadv_21;

        public int Tadv32To36 => Tadv_22 + Tadv_23;

        public int Tadv36To42 => Tadv_24 + Tadv_25 + Tadv_26;

        public int Tadv42To48 => Tadv_27 + Tadv_28 + Tadv_29;

        public int Tadv48To54 => Tadv_30 + Tadv_31 + Tadv_32;

        public int Tadv54To60 => Tadv_33 + Tadv_34 + Tadv_35;

        public int Tadv60To80 => Tadv_36 + Tadv_37 + Tadv_38;

        public int Tadv80To112 => Tadv_39 + Tadv_40;

        public int Tadv112To192 => Tadv_41 + Tadv_42;

        public int TadvAbove192 => Tadv_43 + Tadv_44;

    }
}
