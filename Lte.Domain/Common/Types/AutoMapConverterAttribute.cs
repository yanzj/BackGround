using System;

namespace Lte.Domain.Common.Types
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AutoMapConverterAttribute : Attribute
    {
        public Type SourceType { get; }

        public Type ConverterType { get; }

        public AutoMapConverterAttribute(Type sourceType, Type converterType)
        {
            SourceType = sourceType;
            ConverterType = converterType;
        }
    }
}