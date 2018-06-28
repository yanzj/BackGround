using System;

namespace Lte.Domain.Regular.Attributes
{
    /// <summary>
    /// 数组求和保护字段，即本字段不参与求和
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class ArraySumProtectionAttribute : Attribute
    {

    }
}