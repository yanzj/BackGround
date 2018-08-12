using System;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless.Complain;

namespace Lte.Domain.Common.Wireless.Kpi
{
    [EnumTypeDescription(typeof(OrderTopDrop2GPolicy), OrderByDropRateDescending)]
    public enum OrderTopDrop2GPolicy
    {
        OrderByDropsDescending,
        OrderByDropRateDescending,
        OrderByTopDatesDescending
    }

    public class OrderTopDrop2GPolicyDescriptionTransform : DescriptionTransform<OrderTopDrop2GPolicy>
    {

    }

    public class OrderTopDrop2GPolicyTransform : EnumTransform<OrderTopDrop2GPolicy>
    {
        public OrderTopDrop2GPolicyTransform() : base(OrderTopDrop2GPolicy.OrderByDropRateDescending)
        {
        }
    }

    internal static class OrderTopDrop2GPolicyTuples
    {
        public static Tuple<object, string>[] GetTuples()
        {
            return new[]
            {
                new Tuple<object, string>(OrderTopDrop2GPolicy.OrderByDropsDescending, "按照掉话次数降序排列"),
                new Tuple<object, string>(OrderTopDrop2GPolicy.OrderByDropRateDescending, "按照掉话率降序排列"),
                new Tuple<object, string>(OrderTopDrop2GPolicy.OrderByTopDatesDescending, "按照出现次数降序排列")
            };
        }
    }
}