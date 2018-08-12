using Abp.Domain.Entities;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities.College;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Common.Wireless.Distribution;

namespace Abp.EntityFramework.Entities.Infrastructure
{
    [AutoMapFrom(typeof(HotSpotView))]
    public class InfrastructureInfo : Entity, IHotSpot
    {
        [AutoMapPropertyResolve("TypeDescription", typeof(HotSpotView), typeof(HotspotTypeTransform))]
        public HotspotType HotspotType { get; set; }

        public string HotspotName { get; set; }

        public InfrastructureType InfrastructureType { get; set; }

        public int InfrastructureId { get; set; }

        public string Address { get; set; }

        public string SourceName { get; set; }

        public double Longtitute { get; set; }

        public double Lattitute { get; set; }
    }
}