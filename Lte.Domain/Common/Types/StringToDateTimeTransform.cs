using System;
using AutoMapper;
using Lte.Domain.Regular;

namespace Lte.Domain.Common.Types
{
    public class StringToDateTimeTransform : ValueResolver<string, DateTime>
    {
        protected override DateTime ResolveCore(string source)
        {
            return source.ConvertToDateTime(new DateTime(2200, 1, 1));
        }
    }
}