using AutoMapper;

namespace Lte.Domain.Common.Types
{
    public class NullableIntTransform : ValueResolver<int?, int>
    {
        protected override int ResolveCore(int? source)
        {
            return source ?? -1;
        }
    }
}