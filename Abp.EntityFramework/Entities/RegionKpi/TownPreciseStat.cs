using System;
using Abp.Domain.Entities;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities.Kpi;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Common.Wireless.Cell;
using Lte.Domain.Regular.Attributes;

namespace Abp.EntityFramework.Entities.RegionKpi
{
    [AutoMapFrom(typeof(PreciseCoverage4G))]
    public class TownPreciseStat : Entity, ITownId, IStatTime
    {
        [ArraySumProtection]
        public DateTime StatTime { get; set; }
        
        [ArraySumProtection]
        public int TownId { get; set; }
        
        [ArraySumProtection]
        public FrequencyBandType FrequencyBandType { get; set; } = FrequencyBandType.All;

        public int TotalMrs { get; set; }

        public int ThirdNeighbors { get; set; }

        public int SecondNeighbors { get; set; }

        public int FirstNeighbors { get; set; }

        public int NeighborsMore { get; set; }

        public int InterFirstNeighbors { get; set; }

        public int InterSecondNeighbors { get; set; }

        public int InterThirdNeighbors { get; set; }
    }
}