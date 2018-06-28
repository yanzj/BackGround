using System;

namespace Lte.Domain.Regular.Attributes
{
    [AttributeUsage(AttributeTargets.All)]
    internal sealed class UsedImplicitlyAttribute : Attribute
    {
        [UsedImplicitly]
        public UsedImplicitlyAttribute()
            : this(ImplicitUseKindFlags.Default)
        {
        }

        [UsedImplicitly]
        public UsedImplicitlyAttribute(ImplicitUseTargetFlags targetFlags)
            : this(ImplicitUseKindFlags.Default, targetFlags)
        {
        }

        [UsedImplicitly]
        public UsedImplicitlyAttribute(ImplicitUseKindFlags useKindFlags,
            ImplicitUseTargetFlags targetFlags = ImplicitUseTargetFlags.Default)
        {
            UseKindFlags = useKindFlags;
            TargetFlags = targetFlags;
        }

        [UsedImplicitly]
        public ImplicitUseTargetFlags TargetFlags { get; private set; }

        [UsedImplicitly]
        public ImplicitUseKindFlags UseKindFlags { get; private set; }
    }
}