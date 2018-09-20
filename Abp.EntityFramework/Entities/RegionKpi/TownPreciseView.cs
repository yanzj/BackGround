using System;
using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Common.Wireless.Cell;

namespace Abp.EntityFramework.Entities.RegionKpi
{
    [AutoMap(typeof(TownPreciseStat))]
    public class TownPreciseView : ICityDistrictTown, IStatTime
    {
        public DateTime StatTime { get; set; }

        public string City { get; set; } = "-";

        public string District { get; set; } = "-";

        public string Town { get; set; } = "-";

        public int TownId { get; set; }

        public FrequencyBandType FrequencyBandType { get; set; }

        public long TotalMrs { get; set; }

        public long ThirdNeighbors { get; set; }

        public long SecondNeighbors { get; set; }

        public int FirstNeighbors { get; set; }

        public double PreciseRate => 100 - (double)SecondNeighbors * 100 / TotalMrs;

        public double FirstRate => 100 - (double)FirstNeighbors * 100 / TotalMrs;

        public double ThirdRate => 100 - (double)ThirdNeighbors * 100 / TotalMrs;

        public long NeighborsMore { get; set; }

        public long InterFirstNeighbors { get; set; }

        public long InterSecondNeighbors { get; set; }

        public long InterThirdNeighbors { get; set; }
    }
}