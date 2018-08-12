using System;
using Lte.Domain.Common.Types;

namespace Lte.Domain.Common.Wireless.Complain
{
    [EnumTypeDescription(typeof(CustomerType), Unknown)]
    public enum CustomerType : byte
    {
        Individual,
        Family,
        Company,
        Unknown,
        Juminzhuzhai,
        Shangwubangong,
        Shangyequ,
        Zhuzhaiqu,
        Gongyequ,
        OldZhuzhai,
        NewZhuzhai,
        ShangwuChangsuo,
        LowZhuzhai,
        HighZhuzhai,
        Jiaoyisuo
    }

    public class CustomerTypeDescriptionTransform : DescriptionTransform<CustomerType>
    {

    }

    public class CustomerTypeTransform : EnumTransform<CustomerType>
    {
        public CustomerTypeTransform() : base(CustomerType.Unknown)
        {
        }
    }

    internal static class CustomerTypeTuples
    {
        public static Tuple<object, string>[] GetTuples()
        {
            return new[]
            {
                new Tuple<object, string>(CustomerType.Individual, "个人客户"),
                new Tuple<object, string>(CustomerType.Family, "家庭客户"),
                new Tuple<object, string>(CustomerType.Company, "政企客户"),
                new Tuple<object, string>(CustomerType.Unknown, "未知"),
                new Tuple<object, string>(CustomerType.Juminzhuzhai, "居民住宅"),
                new Tuple<object, string>(CustomerType.Shangwubangong, "商务办公"),
                new Tuple<object, string>(CustomerType.Shangyequ, "商业区"),
                new Tuple<object, string>(CustomerType.Zhuzhaiqu, "住宅区"),
                new Tuple<object, string>(CustomerType.Gongyequ, "工业区"),
                new Tuple<object, string>(CustomerType.OldZhuzhai, "旧住宅小区"),
                new Tuple<object, string>(CustomerType.OldZhuzhai, "旧式住宅小区"),
                new Tuple<object, string>(CustomerType.NewZhuzhai, "新式住宅小区"),
                new Tuple<object, string>(CustomerType.ShangwuChangsuo, "商务场所"),
                new Tuple<object, string>(CustomerType.LowZhuzhai, "低层住宅"),
                new Tuple<object, string>(CustomerType.HighZhuzhai, "高层住宅"),
                new Tuple<object, string>(CustomerType.Jiaoyisuo, "交易所"),
            };
        }
    }
}
