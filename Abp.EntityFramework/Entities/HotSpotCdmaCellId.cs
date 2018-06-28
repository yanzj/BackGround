using Abp.Domain.Entities;
using Lte.Domain.Common.Wireless;

namespace Abp.EntityFramework.Entities
{
    public class HotSpotCdmaCellId : Entity, IHotSpot, ICdmaCellQuery
    {
        public HotspotType HotspotType { get; set; }

        public string HotspotName { get; set; }

        public InfrastructureType InfrastructureType { get; set; } = InfrastructureType.CdmaCell;

        public int BtsId { get; set; }

        public byte SectorId { get; set; }
    }
}