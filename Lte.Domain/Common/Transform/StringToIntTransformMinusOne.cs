using AutoMapper;
using Lte.Domain.Regular;

namespace Lte.Domain.Common.Transform
{
    public class StringToIntTransformMinusOne : ValueResolver<string, int>
    {
        protected override int ResolveCore(string source)
        {
            return source.ConvertToInt(-1);
        }
    }
}