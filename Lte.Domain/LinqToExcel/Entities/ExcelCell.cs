using System;

namespace Lte.Domain.LinqToExcel.Entities
{
    public class ExcelCell
    {
        public object Value { get; private set; }

        public ExcelCell(object value)
        {
            Value = value;
        }

        public T Cast<T>()
        {
            return (Value == null || Value is DBNull) ?
                default(T) :
                (T)Convert.ChangeType(Value, typeof(T));
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public static implicit operator string (ExcelCell cell)
        {
            return cell.ToString();
        }
    }
}
