using System;

namespace Lte.Domain.Regular.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    internal sealed class MeansImplicitUseAttribute : Attribute
    {
        [UsedImplicitly]
        public MeansImplicitUseAttribute()
            : this(ImplicitUseKindFlags.Default)
        {
        }

        [UsedImplicitly]
        public MeansImplicitUseAttribute(ImplicitUseTargetFlags targetFlags)
            : this(ImplicitUseKindFlags.Default, targetFlags)
        {
        }

        [UsedImplicitly]
        public MeansImplicitUseAttribute(ImplicitUseKindFlags useKindFlags,
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