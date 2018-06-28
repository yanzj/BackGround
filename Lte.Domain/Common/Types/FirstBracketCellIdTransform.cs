using AutoMapper;
using Lte.Domain.Regular;

namespace Lte.Domain.Common.Types
{
    public class FirstBracketCellIdTransform : ValueResolver<string, int>
    {
        protected override int ResolveCore(string source)
        {
            return string.IsNullOrEmpty(source) ? 0 : source.GetSubStringInFirstPairOfChars('[', ']').ConvertToInt(1);
        }
    }
}