using AutoMapper;

namespace Lte.Domain.Common.Types
{
    public class YesNoTransform : ValueResolver<bool, string>
    {
        protected override string ResolveCore(bool source)
        {
            return source ? "是" : "否";
        }
    }
}