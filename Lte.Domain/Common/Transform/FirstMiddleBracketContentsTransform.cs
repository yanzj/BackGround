using AutoMapper;
using Lte.Domain.Common.Types;

namespace Lte.Domain.Common.Transform
{
    public class FirstMiddleBracketContentsTransform : ValueResolver<string, string>
    {
        protected override string ResolveCore(string source)
        {
            return source.GetSplittedFields(new[] {'[', ']'})[0];
        }
    }
}