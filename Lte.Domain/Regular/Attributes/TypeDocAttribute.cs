using System;

namespace Lte.Domain.Regular.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum | AttributeTargets.Interface)]
    public class TypeDocAttribute : Attribute
    {
        public string Documentation { get; }

        public TypeDocAttribute(string doc)
        {
            Documentation = doc;
        }
    }
}
