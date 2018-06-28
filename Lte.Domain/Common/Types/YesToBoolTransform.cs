using AutoMapper;

namespace Lte.Domain.Common.Types
{
    public class YesToBoolTransform : ValueResolver<string, bool>
    {
        protected override bool ResolveCore(string source)
        {
            return source == "是";
        }
    }
}