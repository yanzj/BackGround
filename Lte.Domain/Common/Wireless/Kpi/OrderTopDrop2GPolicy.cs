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
                new Tuple<object, string>(OrderTopDrop2GPolicy.OrderByDropsDescending, "���յ���������������"),
                new Tuple<object, string>(OrderTopDrop2GPolicy.OrderByDropRateDescending, "���յ����ʽ�������"),
                new Tuple<object, string>(OrderTopDrop2GPolicy.OrderByTopDatesDescending, "���ճ��ִ�����������")
            };
        }
    }
}