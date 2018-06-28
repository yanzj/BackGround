using AutoMapper;

namespace Lte.Domain.Common.Types
{
    public class ByteTransform : ValueResolver<double, double>
    {
        protected override double ResolveCore(double source)
        {
            return source*8;
        }
    }
}