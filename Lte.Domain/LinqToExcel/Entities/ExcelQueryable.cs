using System;
using System.Linq;
using System.Linq.Expressions;
using Lte.Domain.LinqToExcel.Service;
using Lte.Domain.Regular.Attributes;
using Remotion.Data.Linq;

namespace Lte.Domain.LinqToExcel.Entities
{
    public class ExcelQueryable<T> : QueryableBase<T>
    {
        private static IQueryExecutor CreateExecutor(ExcelQueryArgs args)
        {
            return new ExcelQueryExecutor(args);
        }

        public ExcelQueryable(ExcelQueryArgs args)
            : base(CreateExecutor(args))
        {
            foreach (var property in typeof(T).GetProperties())
            {
                ExcelColumnAttribute att 
                    = (ExcelColumnAttribute)Attribute.GetCustomAttribute(property, typeof(ExcelColumnAttribute));
                att.Register(args, property);
            }
        }

        public ExcelQueryable(IQueryProvider provider, Expression expression)
            : base(provider, expression)
        { }
    }
}