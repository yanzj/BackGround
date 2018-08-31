using AutoMapper;
using Lte.Domain.Regular;

namespace Lte.Domain.Common.Transform
{
    public class StringToByteTransform : ValueResolver<string, byte>
    {
        protected override byte ResolveCore(string source)
        {
            return source.ConvertToByte(0);
        }
    }
}
