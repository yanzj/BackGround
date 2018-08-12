using Abp.Domain.Entities;
using Lte.Domain.Common.Geo;

namespace Abp.EntityFramework.Entities.Test
{
    public class RasterInfo : Entity, IGeoPoint<double>
    {
        public double Longtitute { get; set; }

        public double Lattitute { get; set; }

        public string Area { get; set; }
    }
}
