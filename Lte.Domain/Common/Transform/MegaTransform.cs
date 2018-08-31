using AutoMapper;

namespace Lte.Domain.Common.Transform
{
    public class MegaTransform : ValueResolver<long, double>
    {
        protected override double ResolveCore(long source)
        {
            return (double) source/(1024*1024);
        }
    }
}