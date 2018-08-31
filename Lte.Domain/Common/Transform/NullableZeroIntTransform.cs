using AutoMapper;

namespace Lte.Domain.Common.Transform
{
    public class NullableZeroIntTransform : ValueResolver<int?, int>
    {
        protected override int ResolveCore(int? source)
        {
            return source ?? 0;
        }
    }
}