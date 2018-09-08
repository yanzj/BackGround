using Lte.Domain.Common.Wireless.Distribution;

namespace Lte.Domain.Common.Wireless
{
    public interface IHotSpot
    {
        HotspotType HotspotType { get; set; }

        string HotspotName { get; set; }

        InfrastructureType InfrastructureType { get; set; }
    }
}