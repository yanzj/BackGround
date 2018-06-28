using System.Collections.Generic;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities;
using Lte.Domain.Common.Geo;
using Lte.Domain.Common.Wireless;

namespace Lte.MySqlFramework.Entities
{
    [AutoMapFrom(typeof(MicroAddress))]
    public class MicroAmplifierView : IGeoPoint<double>, IDistrictTown
    {
        public string AddressNumber { get; set; }

        public string District { get; set; }

        public string Town { get; set; }

        public string Address { get; set; }

        public double Longtitute { get; set; }

        public double Lattitute { get; set; }

        public string BaseStation { get; set; }

        public int TownId { get; set; }

        public IEnumerable<MicroItem> MicroItems { get; set; } 
    }
}