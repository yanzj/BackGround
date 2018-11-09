using Lte.Domain.Common.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lte.Domain.Common.Wireless.Kpi
{
    [EnumTypeDescription(typeof(OrderMrsSinrUlPolicy), OrderByM3Rate)]
    public enum OrderMrsSinrUlPolicy : byte
    {
        OrderByM3Rate,
        OrderBy0Rate,
        OrderBy3Rate,
        OrderByM3TimesDescending,
        OrderBy0TimesDescending,
        OrderBy3TimesDescending
    }

    internal static class OrderMrsSinrUlPolicyTuples
    {
        public static Tuple<object, string>[] GetTuples()
        {
            return new[]
            {
                new Tuple<object, string>(OrderMrsSinrUlPolicy.OrderByM3Rate, "按照-3dB覆盖率升序"),
                new Tuple<object, string>(OrderMrsSinrUlPolicy.OrderBy0Rate, "按照0dB覆盖率升序"),
                new Tuple<object, string>(OrderMrsSinrUlPolicy.OrderBy3Rate, "按照3dB覆盖率升序"),
                new Tuple<object, string>(OrderMrsSinrUlPolicy.OrderByM3TimesDescending, "按照-3dB以下MR数降序"),
                new Tuple<object, string>(OrderMrsSinrUlPolicy.OrderBy0TimesDescending, "按照0dB以下MR数降序"),
                new Tuple<object, string>(OrderMrsSinrUlPolicy.OrderBy3TimesDescending, "按照3dB以下MR数降序"),
            };
        }
    }
}
