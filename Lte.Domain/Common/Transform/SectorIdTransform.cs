﻿using AutoMapper;

namespace Lte.Domain.Common.Transform
{
    public class SectorIdTransform : ValueResolver<int, byte>
    {
        protected override byte ResolveCore(int source)
        {
            return source > 255 ? (byte) 255 : (byte) source;
        }
    }
}