using System;

namespace LtePlatform.Models
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ApiResponseAttribute : Attribute
    {
        public ApiResponseAttribute(string doc)
        {
            Documentation = doc;
        }
        public string Documentation { get; }
    }
}