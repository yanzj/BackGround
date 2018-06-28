using System;

namespace LtePlatform.Models
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ApiControlAttribute : Attribute
    {
        public ApiControlAttribute(string doc)
        {
            Documentation = doc;
        }
        public string Documentation { get; }
    }
}