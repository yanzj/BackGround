using System;

namespace Lte.Domain.Regular.Attributes
{
    [AttributeUsage(AttributeTargets.Delegate | AttributeTargets.Parameter
                    | AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Method)]
    public sealed class CanBeNullAttribute : Attribute
    {
    }
}