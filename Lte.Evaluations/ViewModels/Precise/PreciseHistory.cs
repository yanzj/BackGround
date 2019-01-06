using System;
using Abp.EntityFramework.Dependency;

namespace Lte.Evaluations.ViewModels.Precise
{
    public class PreciseHistory : IStatDate
    {
        public string DateString { get; set; }

        public DateTime StatDate { get; set; }

        public int PreciseStats { get; set; }
        
        public int TownPreciseStats { get; set; }

        public int CollegePreciseStats { get; set; }
        
        public int MarketPreciseStats { get; set; }

        public int TownPrecise800Stats { get; set; }

        public int TownPrecise1800Stats { get; set; }
        
        public int TownPrecise2100Stats { get; set; }

    }
}