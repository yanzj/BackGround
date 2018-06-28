using AutoMapper;

namespace Lte.Domain.Common.Types
{
    public class SecondLittleBracketContentsTransform : ValueResolver<string, string>
    {
        protected override string ResolveCore(string source)
        {
            return source.GetSplittedFields(new[] {'(', ')'})[1];
        }
    }
}