using System;

namespace LtePlatform.Models
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class ApiParameterDocAttribute : Attribute
    {
        public ApiParameterDocAttribute(string param, string doc)
        {
            Parameter = param;
            Documentation = doc;
        }
        public string Parameter { get; }
        public string Documentation { get; }
    }
}