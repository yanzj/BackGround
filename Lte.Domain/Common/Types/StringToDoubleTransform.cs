using AutoMapper;
using Lte.Domain.Regular;

namespace Lte.Domain.Common.Types
{
    public class StringToDoubleTransform : ValueResolver<string, double>
    {
        protected override double ResolveCore(string source)
        {
            return source.ConvertToDouble(0);
        }
    }
}