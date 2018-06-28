using System;

namespace Lte.Domain.Regular.Attributes
{
    [Flags]
    internal enum ImplicitUseKindFlags
    {
        Access = 1,
        Assign = 2,
        Default = 7,
        InstantiatedNoFixedConstructorSignature = 8,
        InstantiatedWithFixedConstructorSignature = 4
    }
}