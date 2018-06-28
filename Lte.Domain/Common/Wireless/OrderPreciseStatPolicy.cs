using System;
using Lte.Domain.Common.Types;

namespace Lte.Domain.Common.Wireless
{
    [EnumTypeDescription(typeof(OrderPreciseStatPolicy), OrderBySecondRate)]
    public enum OrderPreciseStatPolicy : byte
    {
        OrderBySecondRate,
        OrderBySecondNeighborsDescending,
        OrderByFirstRate,
        OrderByFirstNeighborsDescending,
        OrderByTotalMrsDescending,
        OrderByTopDatesDescending
    }

    public class OrderPreciseStatPolicyDescriptionTransform : DescriptionTransform<OrderPreciseStatPolicy>
    {

    }

    public class OrderPreciseStatPolicyTransform : EnumTransform<OrderPreciseStatPolicy>
    {
        public OrderPreciseStatPolicyTransform() : base(OrderPreciseStatPolicy.OrderBySecondRate)
        {
        }
    }

    internal static class OrderPreciseStatPolicyTuples
    {
        public static Tuple<object, string>[] GetTuples()
        {
            return new[]
            {
                new Tuple<object, string>(OrderPreciseStatPolicy.OrderBySecondRate, "按照精确覆盖率升序"),
                new Tuple<object, string>(OrderPreciseStatPolicy.OrderBySecondNeighborsDescending, "按照第二邻区数量降序"),
                new Tuple<object, string>(OrderPreciseStatPolicy.OrderByFirstRate, "按照第一邻区精确覆盖率升序"),
                new Tuple<object, string>(OrderPreciseStatPolicy.OrderByFirstNeighborsDescending, "按照第一邻区数量降序"),
                new Tuple<object, string>(OrderPreciseStatPolicy.OrderByTotalMrsDescending, "按照总测量报告数降序"),
                new Tuple<object, string>(OrderPreciseStatPolicy.OrderByTopDatesDescending, "按照TOP天数排序")
            };
        }
    }
}