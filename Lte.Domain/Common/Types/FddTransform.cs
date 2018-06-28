using System;
using AutoMapper;

namespace Lte.Domain.Common.Types
{
    public class FddTransform : ValueResolver<string, bool>
    {
        protected override bool ResolveCore(string source)
        {
            return source.IndexOf("FDD", StringComparison.Ordinal) >= 0;
        }
    }
}