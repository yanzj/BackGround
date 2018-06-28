using AutoMapper;

namespace Lte.Domain.Common.Types
{
    public class IntToBoolTransform : ValueResolver<int, bool>
    {
        protected override bool ResolveCore(int source)
        {
            return source == 1;
        }
    }
}