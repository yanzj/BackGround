using System;

namespace Lte.Domain.Regular.Attributes
{
    [Flags]
    internal enum ImplicitUseTargetFlags
    {
        Default = 1,
        Itself = 1,
        Members = 2,
        WithMembers = 3
    }
}