using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework.Dependency;

namespace Lte.Evaluations.ViewModels.Mr
{
    public class SinrHistory : IStatDate
    {
        public string DateString { get; set; }

        public DateTime StatDate { get; set; }

        public int TownSinrUlStats { get; set; }
        
        public int CollegeSinrUlStats { get; set; }
        
        public int MarketSinrUlStats { get; set; }
        
        public int TransportationSinrUlStats { get; set; }

        public int TownSinrUlStats800 { get; set; }

        public int TownSinrUlStats1800 { get; set; }

        public int TownSinrUlStats2100 { get; set; }

        public int TopSinrUlStats { get; set; }
        
    }
}
