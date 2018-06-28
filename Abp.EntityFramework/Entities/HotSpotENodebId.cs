using Abp.Domain.Entities;
using Lte.Domain.Common.Wireless;

namespace Abp.EntityFramework.Entities
{
    public class HotSpotENodebId : Entity, IHotSpot, IENodebId
    {
        public HotspotType HotspotType { get; set; }

        public string HotspotName { get; set; }

        public InfrastructureType InfrastructureType { get; set; } = InfrastructureType.ENodeb;
        
        public int ENodebId { get; set; }
    }
}