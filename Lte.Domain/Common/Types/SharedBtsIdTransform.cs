using AutoMapper;
using Lte.Domain.Regular;

namespace Lte.Domain.Common.Types
{
    public class SharedBtsIdTransform : ValueResolver<string, int>
    {
        protected override int ResolveCore(string source)
        {
            if (string.IsNullOrEmpty(source)) return 0;
            return source.Split('_').Length > 2 ? source.Split('_')[1].ConvertToInt(-1) : -1;
        }
    }
}