using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Dependency;
using Abp.EntityFramework.Entities.Mr;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Common.Wireless.Cell;
using Lte.Domain.Regular.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lte.Parameters.Entities.Kpi
{
    [AutoMapFrom(typeof(MrsTadvStat))]
    [AutoMapTo(typeof(TownMrsTadv))]
    public class TownMrsTadvDto : ICityDistrictTown, IStatDate, ITownId
    {
        public string District { get; set; }

        public string Town { get; set; }

        public string City { get; set; }

        [ArraySumProtection]
        public FrequencyBandType FrequencyBandType { get; set; } = FrequencyBandType.All;

        public string Frequency => FrequencyBandType.ToString();

        public DateTime StatDate { get; set; }

        [ArraySumProtection]
        public int TownId { get; set; }

        public long Tadv_00 { get; set; }

        public long Tadv_01 { get; set; }

        public long Tadv_02 { get; set; }

        public long Tadv_03 { get; set; }

        public long Tadv_04 { get; set; }

        public long Tadv_05 { get; set; }

        public long Tadv_06 { get; set; }

        public long Tadv_07 { get; set; }

        public long Tadv_08 { get; set; }

        public long Tadv_09 { get; set; }

        public long Tadv_10 { get; set; }

        public long Tadv_11 { get; set; }

        public long Tadv_12 { get; set; }

        public long Tadv_13 { get; set; }

        public long Tadv_14 { get; set; }

        public long Tadv_15 { get; set; }

        public long Tadv_16 { get; set; }

        public long Tadv_17 { get; set; }

        public long Tadv_18 { get; set; }

        public long Tadv_19 { get; set; }

        public long Tadv_20 { get; set; }

        public long Tadv_21 { get; set; }

        public long Tadv_22 { get; set; }

        public long Tadv_23 { get; set; }

        public long Tadv_24 { get; set; }

        public long Tadv_25 { get; set; }

        public long Tadv_26 { get; set; }

        public long Tadv_27 { get; set; }

        public long Tadv_28 { get; set; }

        public long Tadv_29 { get; set; }

        public long Tadv_30 { get; set; }

        public long Tadv_31 { get; set; }

        public long Tadv_32 { get; set; }

        public long Tadv_33 { get; set; }

        public long Tadv_34 { get; set; }

        public long Tadv_35 { get; set; }

        public long Tadv_36 { get; set; }

        public long Tadv_37 { get; set; }

        public long Tadv_38 { get; set; }

        public long Tadv_39 { get; set; }

        public long Tadv_40 { get; set; }

        public long Tadv_41 { get; set; }

        public long Tadv_42 { get; set; }

        public long Tadv_43 { get; set; }

        public long Tadv_44 { get; set; }

        public long TadvBelow1 => Tadv_00;

        public long Tadv1To2 => Tadv_01;

        public long Tadv2To3 => Tadv_02;

        public long Tadv3To4 => Tadv_03;

        public long Tadv4To6 => Tadv_04 + Tadv_05;

        public long Tadv6To8 => Tadv_06 + Tadv_07;

        public long Tadv8To10 => Tadv_08 + Tadv_09;

        public long Tadv10To12 => Tadv_10 + Tadv_11;

        public long Tadv12To14 => Tadv_12;

        public long Tadv14To16 => Tadv_13;

        public long Tadv16To18 => Tadv_14;

        public long Tadv18To20 => Tadv_15;

        public long Tadv20To24 => Tadv_16 + Tadv_17;

        public long Tadv24To28 => Tadv_18 + Tadv_19;

        public long Tadv28To32 => Tadv_20 + Tadv_21;

        public long Tadv32To36 => Tadv_22 + Tadv_23;

        public long Tadv36To42 => Tadv_24 + Tadv_25 + Tadv_26;

        public long Tadv42To48 => Tadv_27 + Tadv_28 + Tadv_29;

        public long Tadv48To54 => Tadv_30 + Tadv_31 + Tadv_32;

        public long Tadv54To60 => Tadv_33 + Tadv_34 + Tadv_35;

        public long Tadv60To80 => Tadv_36 + Tadv_37 + Tadv_38;

        public long Tadv80To112 => Tadv_39 + Tadv_40;

        public long Tadv112To192 => Tadv_41 + Tadv_42;

        public long TadvAbove192 => Tadv_43 + Tadv_44;

    }
}
