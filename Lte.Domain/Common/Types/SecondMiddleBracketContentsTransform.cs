using AutoMapper;

namespace Lte.Domain.Common.Types
{
    public class SecondMiddleBracketContentsTransform : ValueResolver<string, string>
    {
        protected override string ResolveCore(string source)
        {
            return source.GetSplittedFields(new[] { '[', ']' })[1];
        }
    }
}