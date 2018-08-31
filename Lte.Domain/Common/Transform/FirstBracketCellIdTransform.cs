using AutoMapper;
using Lte.Domain.Common.Types;
using Lte.Domain.Regular;

namespace Lte.Domain.Common.Transform
{
    public class FirstBracketCellIdTransform : ValueResolver<string, int>
    {
        protected override int ResolveCore(string source)
        {
            return string.IsNullOrEmpty(source) ? 0 : source.GetSubStringInFirstPairOfChars('[', ']').ConvertToInt(1);
        }
    }
}