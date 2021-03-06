﻿using Abp.EntityFramework.Entities.Mr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lte.MySqlFramework.Support.Container
{
    public class TownSinrViewContainer
    {
        public IEnumerable<TownMrsSinrUl> MrsSinrUls { get; set; }
        
        public IEnumerable<TownMrsSinrUl> CollegeMrsSinrUls { get; set; }
        
        public IEnumerable<TownMrsSinrUl> MarketMrsSinrUls { get; set; }
        
        public IEnumerable<TownMrsSinrUl> TransportationMrsSinrUls { get; set; }

        public IEnumerable<TownMrsSinrUl> MrsSinrUls800 { get; set; }
        
        public IEnumerable<TownMrsSinrUl> MrsSinrUls1800 { get; set; }
        
        public IEnumerable<TownMrsSinrUl> MrsSinrUls2100 { get; set; }
        
    }
}
