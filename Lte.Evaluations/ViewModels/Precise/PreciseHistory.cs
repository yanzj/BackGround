using System;

namespace Lte.Evaluations.ViewModels.Precise
{
    public class PreciseHistory
    {
        public string DateString { get; set; }

        public DateTime StatDate { get; set; }

        public int PreciseStats { get; set; }
        
        public int TownPreciseStats { get; set; }

        public int CollegePreciseStats { get; set; }

        public int TownPrecise800Stats { get; set; }

        public int TownPrecise1800Stats { get; set; }
        
        public int TownPrecise2100Stats { get; set; }

        public int TownMrsStats { get; set; }

        public int CollegeMrsStats { get; set; }
        
        public int TownMrsStats800 { get; set; }

        public int TownMrsStats1800 { get; set; }

        public int TownMrsStats2100 { get; set; }

        public int TopMrsStats { get; set; }

        public int TownSinrUlStats { get; set; }

        public int TopSinrUlStats { get; set; }
    }
}