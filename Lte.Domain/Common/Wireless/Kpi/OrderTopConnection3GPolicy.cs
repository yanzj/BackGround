using System;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless.Complain;

namespace Lte.Domain.Common.Wireless.Kpi
{
    [EnumTypeDescription(typeof(OrderTopConnection3GPolicy), OrderByConnectionRate)]
    public enum OrderTopConnection3GPolicy
    {
        OrderByConnectionFailsDescending,
        OrderByConnectionRate,
        OrderByTopDatesDescending
    }

    public class OrderTopConnection3GPolicyDescriptionTransform : DescriptionTransform<OrderTopConnection3GPolicy>
    {

    }

    public class OrderTopConnection3GPolicyTransform : EnumTransform<OrderTopConnection3GPolicy>
    {
        public OrderTopConnection3GPolicyTransform() : base(OrderTopConnection3GPolicy.OrderByConnectionRate)
        {
        }
    }

    internal static class OrderTopConnection3GPolicyTuples
    {
        public static Tuple<object, string>[] GetTuples()
        {
            return new[]
            {
                new Tuple<object, string>(OrderTopConnection3GPolicy.OrderByConnectionFailsDescending,
                    "按照连接失败次数降序排列"),
                new Tuple<object, string>(OrderTopConnection3GPolicy.OrderByConnectionRate, "按照连接成功率升序排列"),
                new Tuple<object, string>(OrderTopConnection3GPolicy.OrderByTopDatesDescending, "按照出现次数降序排列")
            };
        }
    }
}