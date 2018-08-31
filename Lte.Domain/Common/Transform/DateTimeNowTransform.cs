using System;
using AutoMapper;

namespace Lte.Domain.Common.Transform
{
    public class DateTimeNowTransform : ValueResolver<object, DateTime>
    {
        protected override DateTime ResolveCore(object source)
        {
            return DateTime.Now;
        }
    }
}