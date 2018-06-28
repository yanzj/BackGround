using AutoMapper;

namespace Lte.Domain.Common.Types
{
    public class FirstLittleBracketContentsTransform : ValueResolver<string, string>
    {
        protected override string ResolveCore(string source)
        {
            return source.GetSplittedFields(new[] {'(', ')'})[0];
        }
    }
}