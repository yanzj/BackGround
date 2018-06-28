using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Lte.Domain.Regular;

namespace Lte.Domain.Common.Types
{
    public class StringToByteTransform : ValueResolver<string, byte>
    {
        protected override byte ResolveCore(string source)
        {
            return source.ConvertToByte(0);
        }
    }
}
