using System;
using Lte.Domain.Common.Transform;

namespace Lte.Domain.Regular.Attributes
{
    [AttributeUsage(AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public sealed class ExcelColumnAttribute : Attribute
    {
        private readonly TransformEnum _transformation;
        private readonly object _defaultValue;

        public ExcelColumnAttribute(string columnName, TransformEnum transformation = TransformEnum.Default,
            object defaultValue = null)
        {
            ColumnName = columnName;
            _transformation = transformation;
            _defaultValue = defaultValue;
        }

        public string ColumnName { get; }

        public Func<string, object> Transformation
        {
            get
            {
                //这里可以根据需要增加我们的转换规则
                switch (_transformation)
                {
                    case TransformEnum.IntegerDefaultToZero:
                        return x => x.ToString().ConvertToInt((int?) _defaultValue ?? 0);
                    case TransformEnum.IntegerRemoveDots:
                        return x => x.ToString().Replace(".", "").ConvertToInt((int?) _defaultValue ?? 0);
                    case TransformEnum.IntegerRemoveQuotions:
                        return x => x.ToString().Replace("'", "").ConvertToInt((int?) _defaultValue ?? 0);
                    case TransformEnum.ByteRemoveQuotions:
                        return x => x.ToString().Replace("'", "").ConvertToByte((byte?) _defaultValue ?? 0);
                    case TransformEnum.IpAddress:
                        return x => new IpAddress(x.ToString());
                    case TransformEnum.DefaultZeroDouble:
                        return x => x.ToString().ConvertToDouble((double?) _defaultValue ?? 0);
                    case TransformEnum.DefaultOpenDate:
                        return
                            x =>
                                x.ToString()
                                    .ConvertToDateTime((DateTime?) _defaultValue ?? DateTime.Today.AddMonths(-1));
                    case TransformEnum.AntiNullAddress:
                        return x => string.IsNullOrEmpty(x.ToString()) ? "请编辑地址" : x.ToString();
                    case TransformEnum.NullabelDateTime:
                        return x => string.IsNullOrEmpty(x) ? (DateTime?) null : x.ConvertToDateTime(DateTime.Now);
                    case TransformEnum.Longtitute:
                        return x => string.IsNullOrEmpty(x) ? null : MatchRange(x.ConvertToDouble(0), 112, 114, 100);
                    case TransformEnum.Lattitute:
                        return x => string.IsNullOrEmpty(x) ? null : MatchRange(x.ConvertToDouble(0), 22, 24, 20);
                    case TransformEnum.DoubleEmptyZero:
                        return x => string.IsNullOrEmpty(x) ? 0 : x.ConvertToDouble(0);
                    default:
                        return null;
                }
            }
        }

        private static object MatchRange(double l, int low, int high, int offset)
        {
            if (low < l && l < high) return l;
            if (low - offset < l && l < high - offset) return l + offset;
            if (low + offset*10 < l && l < high + offset*10) return l - offset*10;
            if (low*10 < l && l < high*10) return l/10;
            if (low*100 < l && l < high*100) return l/100;
            if (low*1000 < l && l < high*1000) return l/1000;
            if (low*10000 < l && l < high*10000) return l/10000;
            if (low*100000 < l && l < high*100000) return l/100000;
            if (low*1000000 < l && l < high*1000000) return l/1000000;
            if (low*10000000 < l && l < high*10000000) return l/10000000;
            return 0;
        }
    }
}
