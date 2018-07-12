using System.Collections.Generic;

namespace Lte.Domain.Common.Geo
{
    public class CollegeCellNamesContainer
    {
        public string CollegeName { get; set; }

        public IEnumerable<string> CellNames { get; set; }
    }
}