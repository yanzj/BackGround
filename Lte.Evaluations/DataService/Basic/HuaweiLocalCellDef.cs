using System.Collections.Generic;

namespace Lte.Evaluations.DataService.Basic
{
    public class HuaweiLocalCellDef
    {
        public int ENodebId { get; set; }

        public Dictionary<int, int> LocalCellDict { get; set; }
    }
}