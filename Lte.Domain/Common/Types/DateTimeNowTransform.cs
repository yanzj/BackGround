using System;
using AutoMapper;

namespace Lte.Domain.Common.Types
{
    public class DateTimeNowTransform : ValueResolver<object, DateTime>
    {
        protected override DateTime ResolveCore(object source)
        {
            return DateTime.Now;
        }
    }
}