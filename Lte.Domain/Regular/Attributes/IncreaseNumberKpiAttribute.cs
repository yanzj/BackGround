using System;

namespace Lte.Domain.Regular.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class IncreaseNumberKpiAttribute : Attribute
    {
        public string KpiPrefix { get; set; } = "Kpi";

        public string KpiAffix { get; set; } = "";

        public int IndexRange { get; set; } = 1;
    }
}