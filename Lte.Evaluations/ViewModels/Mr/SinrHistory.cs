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

        public int TopSinrUlStats { get; set; }
    }
}
