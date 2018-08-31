using AutoMapper;
using Lte.Domain.Regular;

namespace Lte.Domain.Common.Transform
{
    public class IpAddressTransform : ValueResolver<IpAddress, int>
    {
        protected override int ResolveCore(IpAddress source)
        {
            return source.AddressValue;
        }
    }
}