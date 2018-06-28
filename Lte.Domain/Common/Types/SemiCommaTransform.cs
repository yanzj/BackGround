using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace Lte.Domain.Common.Types
{
    public class SemiCommaTransform : ValueResolver<string, List<string>>
    {
        protected override List<string> ResolveCore(string source)
        {
            return string.IsNullOrEmpty(source) ? new List<string>() : source.Split(';').ToList();
        }
    }
}