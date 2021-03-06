using System.Collections.Generic;
using Abp.EntityFramework.Entities.Region;

namespace Lte.MySqlFramework.Entities.Region
{
    public class TownRectangle
    {
        public int TownId { get; set; }

        public double West { get; set; }

        public double East { get; set; }

        public double South { get; set; }

        public double North { get; set; }

        public List<TownBoundary> Coors { get; set; }
    }
}