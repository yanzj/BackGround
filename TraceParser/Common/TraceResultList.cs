using System.Collections.Generic;

namespace TraceParser.Common
{
    public class TraceResultList
    {
        public List<TraceConfig> Messages { get; } = new List<TraceConfig>();

        public TraceResultList()
        {
            FailCounts = 0;
        }

        public int FailCounts { get; set; }
    }
}
