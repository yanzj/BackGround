﻿using System;

namespace Abp.EntityFramework.AutoMapper
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AutoMapFromAttribute : AutoMapAttribute
    {
        internal override AutoMapDirection Direction => AutoMapDirection.From;

        public AutoMapFromAttribute(params Type[] targetTypes)
            : base(targetTypes)
        {

        }
    }
}