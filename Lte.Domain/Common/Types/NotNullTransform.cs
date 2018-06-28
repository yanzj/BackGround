using AutoMapper;

namespace Lte.Domain.Common.Types
{
    public class NotNullTransform : ValueResolver<object, bool>
    {
        protected override bool ResolveCore(object source)
        {
            return source != null;
        }
    }
}