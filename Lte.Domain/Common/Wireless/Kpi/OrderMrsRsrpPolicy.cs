using System;
using Lte.Domain.Common.Types;

namespace Lte.Domain.Common.Wireless.Kpi
{
    [EnumTypeDescription(typeof(OrderMrsRsrpPolicy), OrderBy110Rate)]
    public enum OrderMrsRsrpPolicy : byte
    {
        OrderBy110Rate,
        OrderBy105Rate,
        OrderBy110TimesDescending,
        OrderBy105TimesDescending
    }

    internal static class OrderMrsRsrpPolicyTuples
    {
        public static Tuple<object, string>[] GetTuples()
        {
            return new[]
            {
                new Tuple<object, string>(OrderMrsRsrpPolicy.OrderBy110Rate, "按照-110dBm覆盖率升序"),
                new Tuple<object, string>(OrderMrsRsrpPolicy.OrderBy105Rate, "按照-105dBm覆盖率升序"),
                new Tuple<object, string>(OrderMrsRsrpPolicy.OrderBy110TimesDescending, "按照-110dBm以下MR数降序"),
                new Tuple<object, string>(OrderMrsRsrpPolicy.OrderBy105TimesDescending, "按照-105dBm以下MR数降序"),
            };
        }
    }
}