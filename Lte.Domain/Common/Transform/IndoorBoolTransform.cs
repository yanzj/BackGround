using AutoMapper;

namespace Lte.Domain.Common.Transform
{
    public class IndoorBoolTransform : ValueResolver<string, bool>
    {
        protected override bool ResolveCore(string source)
        {
            return source == "室内";
        }
    }
}