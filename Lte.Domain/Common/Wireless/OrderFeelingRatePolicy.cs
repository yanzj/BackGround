﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Common.Types;

namespace Lte.Domain.Common.Wireless
{
    [EnumTypeDescription(typeof(OrderFeelingRatePolicy), OrderByDownlinkDurationDescendings)]
    public enum OrderFeelingRatePolicy : byte
    {
        OrderByDownlinkFeelingRateRate,
        OrderByDownlinkDurationDescendings,
        OrderByUplinkFeelingRateRate,
        OrderByUplinkDurationDescendings
    }

    public class OrderFeelingRatePolicyDescriptionTransform : DescriptionTransform<OrderFeelingRatePolicy>
    {

    }

    public class OrderFeelingRatePolicyTransform : EnumTransform<OrderFeelingRatePolicy>
    {
        public OrderFeelingRatePolicyTransform() : base(OrderFeelingRatePolicy.OrderByDownlinkDurationDescendings)
        {
        }
    }

    internal static class OrderFeelingRatePolicyTuples
    {
        public static Tuple<object, string>[] GetTuples()
        {
            return new[]
            {
                new Tuple<object, string>(OrderFeelingRatePolicy.OrderByDownlinkDurationDescendings, "按照下行有效包传输时长降序"),
                new Tuple<object, string>(OrderFeelingRatePolicy.OrderByDownlinkFeelingRateRate, "按照下行感知速率升序"),
                new Tuple<object, string>(OrderFeelingRatePolicy.OrderByUplinkDurationDescendings, "按照上行有效包传输时长降序"),
                new Tuple<object, string>(OrderFeelingRatePolicy.OrderByUplinkFeelingRateRate, "按照上行感知速率升序"),
            };
        }
    }
}
