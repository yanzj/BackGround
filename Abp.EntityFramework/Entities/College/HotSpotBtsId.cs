using Abp.Domain.Entities;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Common.Wireless.Distribution;

namespace Abp.EntityFramework.Entities.College
{
    public class HotSpotBtsId : Entity, IHotSpot, IBtsIdQuery
    {
        public HotspotType HotspotType { get; set; }

        public string HotspotName { get; set; }

        public InfrastructureType InfrastructureType { get; set; } = InfrastructureType.CdmaBts;

        public int BtsId { get; set; }
    }
}