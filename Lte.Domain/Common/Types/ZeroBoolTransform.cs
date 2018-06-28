using AutoMapper;

namespace Lte.Domain.Common.Types
{
    public class ZeroBoolTransform : ValueResolver<int, bool>
    {
        protected override bool ResolveCore(int source)
        {
            return source == 0;
        }
    }
}