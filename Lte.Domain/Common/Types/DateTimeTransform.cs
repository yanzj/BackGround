using System;
using AutoMapper;

namespace Lte.Domain.Common.Types
{
    public class DateTimeTransform : ValueResolver<DateTime, DateTime>
    {
        protected override DateTime ResolveCore(DateTime source)
        {
            return source < new DateTime(2000, 1, 1) ? new DateTime(2200, 1, 1) : source;
        }
    }
}