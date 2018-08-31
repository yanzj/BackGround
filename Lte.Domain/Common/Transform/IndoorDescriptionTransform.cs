using AutoMapper;

namespace Lte.Domain.Common.Transform
{
    public class IndoorDescriptionTransform : ValueResolver<bool, string>
    {
        protected override string ResolveCore(bool source)
        {
            return source ? "室内" : "室外";
        }
    }
}