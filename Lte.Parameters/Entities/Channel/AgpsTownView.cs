using System.Collections.Generic;

namespace Lte.Parameters.Entities.Channel
{
    public class AgpsTownView
    {
        public string District { get; set; }

        public string Town { get; set; }

        public IEnumerable<AgpsCoverageView> Views { get; set; } 
    }
}