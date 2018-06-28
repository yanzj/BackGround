using AutoMapper;

namespace Lte.Domain.Common.Types
{
    public class NullableZeroIntTransform : ValueResolver<int?, int>
    {
        protected override int ResolveCore(int? source)
        {
            return source ?? 0;
        }
    }
}