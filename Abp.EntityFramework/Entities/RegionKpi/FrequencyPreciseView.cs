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
        
        public string FrequenyBand => FrequencyBandType.GetBandDescription();

        public int TotalMrs { get; set; }

        public int ThirdNeighbors { get; set; }

        public int SecondNeighbors { get; set; }

        public int FirstNeighbors { get; set; }

        public double PreciseRate => 100 - (double)SecondNeighbors * 100 / TotalMrs;

        public double FirstRate => 100 - (double)FirstNeighbors * 100 / TotalMrs;

        public double ThirdRate => 100 - (double)ThirdNeighbors * 100 / TotalMrs;

        public int NeighborsMore { get; set; }

        public int InterFirstNeighbors { get; set; }

        public int InterSecondNeighbors { get; set; }

        public int InterThirdNeighbors { get; set; }
    }
}
