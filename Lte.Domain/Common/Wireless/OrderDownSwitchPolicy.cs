using System;
using Lte.Domain.Common.Types;

namespace Lte.Domain.Common.Wireless
{
    [EnumTypeDescription(typeof(OrderDownSwitchPolicy), OrderByDownSwitchCountsDescendings)]
    public enum OrderDownSwitchPolicy : byte
    {
        OrderByDownSwitchRateDescending,
        OrderByDownSwitchCountsDescendings
    }

    public class OrderDownSwitchPolicyDescriptionTransform : DescriptionTransform<OrderDownSwitchPolicy>
    {

    }

    public class OrderDownSwitchPolicyTransform : EnumTransform<OrderDownSwitchPolicy>
    {
        public OrderDownSwitchPolicyTransform() : base(OrderDownSwitchPolicy.OrderByDownSwitchCountsDescendings)
        {
        }
    }

    internal static class OrderDownSwitchPolicyTuples
    {
        public static Tuple<object, string>[] GetTuples()
        {
            return new[]
            {
                new Tuple<object, string>(OrderDownSwitchPolicy.OrderByDownSwitchCountsDescendings, "按照下切次数降序"),
                new Tuple<object, string>(OrderDownSwitchPolicy.OrderByDownSwitchRateDescending, "按照下切比例降序"),
            };
        }
    }
}