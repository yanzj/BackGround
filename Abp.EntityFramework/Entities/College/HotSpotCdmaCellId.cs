using Abp.Domain.Entities;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Common.Wireless.Distribution;

namespace Abp.EntityFramework.Entities.College
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