using AutoMapper;
using Lte.Domain.Regular;

namespace Lte.Domain.Common.Transform
{
    public class IpByte4Transform : ValueResolver<IpAddress, byte>
    {
        protected override byte ResolveCore(IpAddress source)
        {
            return source.IpByte4;
        }
    }
}