using AutoMapper;

namespace Lte.Domain.Common.Transform
{
    public class YesToBoolTransform : ValueResolver<string, bool>
    {
        protected override bool ResolveCore(string source)
        {
            return source == "是";
        }
    }
}