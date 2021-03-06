﻿using System;

namespace LtePlatform.Models
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ApiDocAttribute : Attribute
    {
        public ApiDocAttribute(string doc)
        {
            Documentation = doc;
        }
        public string Documentation { get; }
    }
}
