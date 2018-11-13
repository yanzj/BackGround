using Lte.Domain.Common.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lte.Domain.Common.Wireless.Kpi
{
    [EnumTypeDescription(typeof(OrderMrsTadvPolicy), OrderBy112RateDescending)]
    public enum OrderMrsTadvPolicy : byte
    {
        OrderBy112RateDescending,
        OrderBy60RateDescending,
        OrderBy42RateDescending,
        OrderBy112TimesDescending,
        OrderBy60TimesDescending,
        OrderBy42TimesDescending
    }

    internal static class OrderMrsTadvPolicyTuples
    {
        public static Tuple<object, string>[] GetTuples()
        {
            return new[]
            {
                new Tuple<object, string>(OrderMrsTadvPolicy.OrderBy112RateDescending, "按照9km过覆盖率降序"),
                new Tuple<object, string>(OrderMrsTadvPolicy.OrderBy60RateDescending, "按照5km过覆盖率降序"),
                new Tuple<object, string>(OrderMrsTadvPolicy.OrderBy42RateDescending, "按照3km过覆盖率降序"),
                new Tuple<object, string>(OrderMrsTadvPolicy.OrderBy112TimesDescending, "按照9km过覆盖数降序"),
                new Tuple<object, string>(OrderMrsTadvPolicy.OrderBy60TimesDescending, "按照5km过覆盖数降序"),
                new Tuple<object, string>(OrderMrsTadvPolicy.OrderBy42TimesDescending, "按照3km过覆盖数降序"),
            };
        }
    }
}
