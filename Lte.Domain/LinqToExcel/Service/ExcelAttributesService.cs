using Lte.Domain.LinqToExcel.Entities;
using Lte.Domain.Regular.Attributes;
using System;
using System.Reflection;

namespace Lte.Domain.LinqToExcel.Service
{
    public static class ExcelAttributesService
    {
        public static void Register(this ExcelColumnAttribute att, ExcelQueryArgs args,
            PropertyInfo property)
        {
            if (att != null && !args.ColumnMappings.ContainsKey(property.Name))
            {
                args.ColumnMappings.Add(property.Name, att.ColumnName);
                Func<string, object> transOper = att.Transformation;
                if (transOper != null)
                    args.Transformations.Add(property.Name, transOper);
            }
        }
    }
}
