using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Dependency;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless.Cell;
using Lte.Domain.Regular.Attributes;

namespace Abp.EntityFramework.Entities.RegionKpi
{
    [AutoMap(typeof(TownPreciseStat))]
    public class FrequencyPreciseView : IStatTime, IFrequencyBand
    {
        [ArraySumProtection]
        public DateTime StatTime { get; set; }
        
        [ArraySumProtection]
        public FrequencyBandType FrequencyBandType { get; set; }
        
        public string FrequencyBand => FrequencyBandType.GetBandDescription();

        public long TotalMrs { get; set; }

        public long ThirdNeighbors { get; set; }

        public long SecondNeighbors { get; set; }

        public long FirstNeighbors { get; set; }

        public double PreciseRate => 100 - (double)SecondNeighbors * 100 / TotalMrs;

        public double FirstRate => 100 - (double)FirstNeighbors * 100 / TotalMrs;

        public double ThirdRate => 100 - (double)ThirdNeighbors * 100 / TotalMrs;

        public long NeighborsMore { get; set; }

        public long InterFirstNeighbors { get; set; }

        public long InterSecondNeighbors { get; set; }

        public long InterThirdNeighbors { get; set; }
    }
}
