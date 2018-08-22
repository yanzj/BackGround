using Abp.EntityFramework.Entities.Region;

namespace Lte.MySqlFramework.Entities.Region
{
    public class RoadRectangle
    {
        public int TownId { get; set; }

        public double West { get; set; }

        public double East { get; set; }

        public double South { get; set; }

        public double North { get; set; }

        public TownBoundary Coors { get; set; }
    }
}