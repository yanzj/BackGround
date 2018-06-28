using System.Collections.Generic;
using Lte.Parameters.Entities.Channel;

namespace Lte.Evaluations.DataService.Mr
{
    public class AgpsTownView
    {
        public string District { get; set; }

        public string Town { get; set; }

        public IEnumerable<AgpsCoverageView> Views { get; set; } 
    }
}