﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lte.Domain.Common.Types
{
    public interface IIsInUse
    {
        bool IsInUse { get; set; }
    }
}
