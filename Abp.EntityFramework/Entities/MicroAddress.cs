using Abp.Domain.Entities;
using Lte.Domain.Common.Geo;

namespace Abp.EntityFramework.Entities
{
    public class MicroAddress : Entity, IGeoPoint<double>
    {
        public string AddressNumber { get; set; }

        public string District { get; set; }

        public string Address { get; set; }

        public double Longtitute { get; set; }

        public double Lattitute { get; set; }

        public string BaseStation { get; set; }

        public int TownId { get; set; }
    }
}