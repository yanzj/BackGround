using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework.Dependency;

namespace Lte.Evaluations.ViewModels.Mr
{
    public class RsrpHistory : IStatDate
    {
        public string DateString { get; set; }

        public DateTime StatDate { get; set; }
        
        public int TownMrsStats { get; set; }

        public int CollegeMrsStats { get; set; }
        
        public int MarketMrsStats { get; set; }
        
        public int TransportationMrsStats { get; set; }

        public int TownMrsStats800 { get; set; }

        public int TownMrsStats1800 { get; set; }

        public int TownMrsStats2100 { get; set; }

        public int TopMrsStats { get; set; }

    }
}
