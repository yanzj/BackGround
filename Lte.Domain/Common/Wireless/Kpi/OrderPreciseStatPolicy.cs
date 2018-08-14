using System;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless.Complain;

namespace Lte.Domain.Common.Wireless.Kpi
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
                new Tuple<object, string>(OrderPreciseStatPolicy.OrderBySecondRate, "���վ�ȷ����������"),
                new Tuple<object, string>(OrderPreciseStatPolicy.OrderBySecondNeighborsDescending, "���յڶ�������������"),
                new Tuple<object, string>(OrderPreciseStatPolicy.OrderByFirstRate, "���յ�һ������ȷ����������"),
                new Tuple<object, string>(OrderPreciseStatPolicy.OrderByFirstNeighborsDescending, "���յ�һ������������"),
                new Tuple<object, string>(OrderPreciseStatPolicy.OrderByTotalMrsDescending, "�����ܲ�������������"),
                new Tuple<object, string>(OrderPreciseStatPolicy.OrderByTopDatesDescending, "����TOP��������")
            };
        }
    }
}