using System;
using AutoMapper;

namespace Lte.Domain.Common.Transform
{
    public class DateTimeNowLabelTransform : ValueResolver<string, string>
    {
        protected override string ResolveCore(string source)
        {
            return "[" + DateTime.Now + "]" + source;
        }
    }
}