using Abp.Domain.Entities;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Common.Wireless.Distribution;

namespace Abp.EntityFramework.Entities.College
{
    public class HotSpotENodebId : Entity, IHotSpot, IENodebId
    {
        public HotspotType HotspotType { get; set; }

        public string HotspotName { get; set; }

        public InfrastructureType InfrastructureType { get; set; } = InfrastructureType.ENodeb;
        
        public int ENodebId { get; set; }
    }
}