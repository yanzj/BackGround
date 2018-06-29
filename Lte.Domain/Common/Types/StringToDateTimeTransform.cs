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

    public class StringToDateTimeMillisecondTransform : ValueResolver<string, DateTime>
    {
        protected override DateTime ResolveCore(string source)
        {
            var fields = source.GetSplittedFields(':');
            if (fields.Length == 4)
            {
                source = source.Replace(':' + fields[3], '.' + fields[3]);
            }
            return source.ConvertToDateTime(new DateTime(2200, 1, 1));
        }
    }
}