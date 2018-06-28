using AutoMapper;

namespace Lte.Domain.Common.Types
{
    public class IndoorBoolTransform : ValueResolver<string, bool>
    {
        protected override bool ResolveCore(string source)
        {
            return source == "室内";
        }
    }
}