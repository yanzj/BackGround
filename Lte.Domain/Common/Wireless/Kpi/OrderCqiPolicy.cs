using System;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless.Complain;

namespace Lte.Domain.Common.Wireless.Kpi
{
    [EnumTypeDescription(typeof(OrderCqiPolicy), OrderByCqiRate)]
    public enum OrderCqiPolicy : byte
    {
        OrderByCqiRate,
        OrderByPoorCqiDescending
    }

    public class OrderCqiPolicyDescriptionTransform : DescriptionTransform<OrderCqiPolicy>
    {

    }

    internal static class OrderCqiPolicyTuples
    {
        public static Tuple<object, string>[] GetTuples()
        {
            return new[]
            {
                new Tuple<object, string>(OrderCqiPolicy.OrderByCqiRate, "按照CQI优良比升序"),
                new Tuple<object, string>(OrderCqiPolicy.OrderByPoorCqiDescending, "按照弱CQI次数降序")
            };
        }
    }
}