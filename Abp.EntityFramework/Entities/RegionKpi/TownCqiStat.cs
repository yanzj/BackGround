using System;
using Abp.Domain.Entities;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Dependency;
using Abp.EntityFramework.Entities.Kpi;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Common.Wireless.Cell;
using Lte.Domain.Regular.Attributes;

namespace Abp.EntityFramework.Entities.RegionKpi
{
    [AutoMapFrom(typeof(CqiHuawei), typeof(CqiZte), typeof(CqiView))]
    public class TownCqiStat : Entity, ITownId, IStatTime, IFrequency
    {
        [ArraySumProtection]
        public int TownId { get; set; }

        [ArraySumProtection]
        public DateTime StatTime { get; set; }

        [ArraySumProtection]
        public FrequencyBandType FrequencyBandType { get; set; } = FrequencyBandType.All;

        public string Frequency => FrequencyBandType.ToString();

        public long Cqi0Reports { get; set; }

        public long Cqi1Reports { get; set; }

        public long Cqi2Reports { get; set; }

        public long Cqi3Reports { get; set; }

        public long Cqi4Reports { get; set; }

        public long Cqi5Reports { get; set; }

        public long Cqi6Reports { get; set; }

        public long Cqi7Reports { get; set; }

        public long Cqi8Reports { get; set; }

        public long Cqi9Reports { get; set; }

        public long Cqi10Reports { get; set; }

        public long Cqi11Reports { get; set; }

        public long Cqi12Reports { get; set; }

        public long Cqi13Reports { get; set; }

        public long Cqi14Reports { get; set; }

        public long Cqi15Reports { get; set; }
    }
}