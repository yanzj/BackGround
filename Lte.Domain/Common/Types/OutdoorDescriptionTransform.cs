using AutoMapper;

namespace Lte.Domain.Common.Types
{
    public class OutdoorDescriptionTransform : ValueResolver<bool, string>
    {
        protected override string ResolveCore(bool source)
        {
            return source ? "室外" : "室内";
        }
    }
}