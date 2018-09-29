using AutoMapper;
using Lte.Domain.Regular;

namespace Lte.Domain.Common.Transform
{
    public class StringToDoubleTransform : ValueResolver<string, double>
    {
        protected override double ResolveCore(string source)
        {
            return source.ConvertToDouble(0);
        }
    }

    public class StringToRssiTransform : ValueResolver<string, double>
    {
        protected override double ResolveCore(string source)
        {
            return source.ConvertToDouble(-140);
        }
    }
}