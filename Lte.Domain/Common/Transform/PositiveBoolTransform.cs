using AutoMapper;

namespace Lte.Domain.Common.Transform
{
    public class PositiveBoolTransform : ValueResolver<int, bool>
    {
        protected override bool ResolveCore(int source)
        {
            return source > 0;
        }
    }
}