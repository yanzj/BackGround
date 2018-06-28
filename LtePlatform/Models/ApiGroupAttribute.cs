using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LtePlatform.Models
{
    [AttributeUsage(AttributeTargets.Class)]
    public class ApiGroupAttribute : Attribute
    {
        public ApiGroupAttribute(string doc)
        {

            Documentation = doc;
        }
        public string Documentation { get; }
    }
}