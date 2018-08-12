using Abp.Domain.Entities;
using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Common.Geo;
using Lte.Domain.Excel;

namespace Abp.EntityFramework.Entities.Station
{
    [AutoMapFrom(typeof(IndoorDistributionExcel))]
    public class DistributionSystem : Entity, IGeoPoint<double>
    {
        public string StationNum { get; set; }
        
        public string Name { get; set; }
        
        public string Address { get; set; }

        public double Longtitute { get; set; }

        public double Lattitute { get; set; }

        public string City { get; set; }
        
        public string District { get; set; }
        
        public string Server { get; set; }

        public string ServiceArea { get; set; }

        public string ScaleDescription { get; set; }

        public string Owner { get; set; }

        public byte SourceAppliances { get; set; }

        public byte OutdoorPicos { get; set; }

        public byte OutdoorRepeaters { get; set; }

        public byte OutdoorRrus { get; set; }

        public byte IndoorPicos { get; set; }

        public byte IndoorRepeaters { get; set; }

        public byte IndoorRrus { get; set; }

        public byte Amplifiers { get; set; }

        public int ENodebId { get; set; }

        public byte LteSectorId { get; set; }

        public int BtsId { get; set; }

        public byte CdmaSectorId { get; set; }

        public bool IsLteRru => ENodebId > 0;

        public bool IsCdmaRru => BtsId > 0;
    }
}
