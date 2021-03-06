﻿using AutoMapper;

namespace Lte.Domain.Common.Transform
{
    public class ThousandTransform : ValueResolver<int, double>
    {
        protected override double ResolveCore(int source)
        {
            return (double) source/1000;
        }
    }
}