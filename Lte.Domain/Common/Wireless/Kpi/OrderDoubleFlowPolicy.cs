using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless.Complain;

namespace Lte.Domain.Common.Wireless.Kpi
{
    [EnumTypeDescription(typeof(OrderDoubleFlowPolicy), OrderByDoubleFlowRate)]
    public enum OrderDoubleFlowPolicy : byte
    {
        OrderByDoubleFlowRate,
        OrderByCloseLoopDoubleFlowRate,
        OrderByOpenLoopDoubleFlowRate,
        OrderByRank1PrbsDescendings
    }

    public class OrderDoubleFlowPolicyDescriptionTransform : DescriptionTransform<OrderDoubleFlowPolicy>
    {

    }

    public class OrderDoubleFlowPolicyTransform : EnumTransform<OrderDoubleFlowPolicy>
    {
        public OrderDoubleFlowPolicyTransform() : base(OrderDoubleFlowPolicy.OrderByDoubleFlowRate)
        {
        }
    }

    internal static class OrderDoubleFlowPolicyTuples
    {
        public static Tuple<object, string>[] GetTuples()
        {
            return new[]
            {
                new Tuple<object, string>(OrderDoubleFlowPolicy.OrderByDoubleFlowRate, "按照总体双流比升序"),
                new Tuple<object, string>(OrderDoubleFlowPolicy.OrderByCloseLoopDoubleFlowRate, "按照闭环双流比升序"),
                new Tuple<object, string>(OrderDoubleFlowPolicy.OrderByOpenLoopDoubleFlowRate, "按照开环双流比升序"),
                new Tuple<object, string>(OrderDoubleFlowPolicy.OrderByRank1PrbsDescendings, "按照单流PRB数降序"),
            };
        }
    }
}
