using System;

namespace Lte.Domain.Regular.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class MemberDocAttribute : Attribute
    {
        public string Documentation { get; }

        public MemberDocAttribute(string doc)
        {
            Documentation = doc;
        }
    }
}