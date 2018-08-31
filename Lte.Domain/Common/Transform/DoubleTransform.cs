using AutoMapper;

namespace Lte.Domain.Common.Transform
{
    public class DoubleTransform : ValueResolver<double, double>
    {
        protected override double ResolveCore(double source)
        {
            return source*2;
        }
    }
}