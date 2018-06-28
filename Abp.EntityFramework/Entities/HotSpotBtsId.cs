using Abp.Domain.Entities;
using Lte.Domain.Common.Wireless;

namespace Abp.EntityFramework.Entities
{
    public class HotSpotBtsId : Entity, IHotSpot, IBtsIdQuery
    {
        public HotspotType HotspotType { get; set; }

        public string HotspotName { get; set; }

        public InfrastructureType InfrastructureType { get; set; } = InfrastructureType.CdmaBts;

        public int BtsId { get; set; }
    }
}