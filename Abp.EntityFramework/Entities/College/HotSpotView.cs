using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities.Infrastructure;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless.Distribution;

namespace Abp.EntityFramework.Entities.College
{
    [AutoMapFrom(typeof(InfrastructureInfo))]
    public class HotSpotView
    {
        [AutoMapPropertyResolve("HotspotType", typeof(InfrastructureInfo), typeof(HotspotTypeDescriptionTransform))]
        public string TypeDescription { get; set; }

        public string HotspotName { get; set; }

        public string Address { get; set; }

        public string SourceName { get; set; }

        public double Longtitute { get; set; }

        public double Lattitute { get; set; }

        public int Id { get; set; }
    }
}