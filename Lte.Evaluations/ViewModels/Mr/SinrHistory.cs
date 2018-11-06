using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lte.Evaluations.ViewModels.Mr
{
    public class SinrHistory
    {
        public string DateString { get; set; }

        public DateTime StatDate { get; set; }

        public int TownSinrUlStats { get; set; }
        
        public int CollegeSinrUlStats { get; set; }

        public int TownSinrUlStats800 { get; set; }

        public int TownSinrUlStats1800 { get; set; }

        public int TownSinrUlStats2100 { get; set; }

        public int TopSinrUlStats { get; set; }
        
        public int TownTadvStats { get; set; }
        
        public int CollegeTadvStats { get; set; }

        public int TownTadvStats800 { get; set; }

        public int TownTadvStats1800 { get; set; }

        public int TownTadvStats2100 { get; set; }

        public int TopTadvStats { get; set; }
    }
}
