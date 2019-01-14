using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework.Dependency;

namespace Lte.Evaluations.ViewModels.Mr
{
    public class TadvHistory : IStatDate
    {
        public string DateString { get; set; }

        public DateTime StatDate { get; set; }
        
        public int TownTadvStats { get; set; }
        
        public int CollegeTadvStats { get; set; }
        
        public int MarketTadvStats { get; set; }
        
        public int TransportationTadvStats { get; set; }

        public int TownTadvStats800 { get; set; }

        public int TownTadvStats1800 { get; set; }

        public int TownTadvStats2100 { get; set; }

        public int TopTadvStats { get; set; }
    }
}
