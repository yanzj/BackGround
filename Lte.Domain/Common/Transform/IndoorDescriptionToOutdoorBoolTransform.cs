using AutoMapper;

namespace Lte.Domain.Common.Transform
{
    public class IndoorDescriptionToOutdoorBoolTransform : ValueResolver<string, bool>
    {
        protected override bool ResolveCore(string source)
        {
            return source.Trim() == "否";
        }
    }
}