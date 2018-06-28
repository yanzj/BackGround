using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Common.Types;

namespace Lte.Domain.Common.Wireless
{
    [EnumTypeDescription(typeof(OrderPrbStatPolicy), OrderByPdschPrbRateDescending)]
    public enum OrderPrbStatPolicy : byte
    {
        OrderByPdschPrbRateDescending,
        OrderByPdschDtchPrbRateDescending,
        OrderByPuschPrbRateDescending,
        OrderByPuschDtchPrbRateDescending,
        OrderByPdschHighUsageDescending,
        OrderByPuschHighUsageDescending
    }

    internal static class OrderPrbStatPolicyTuples
    {
        public static Tuple<object, string>[] GetTuples()
        {
            return new[]
            {
                new Tuple<object, string>(OrderPrbStatPolicy.OrderByPdschDtchPrbRateDescending, "按照PDSCH DRB PRB利用率降序排列"),
                new Tuple<object, string>(OrderPrbStatPolicy.OrderByPdschPrbRateDescending, "按照PDSCH PRB利用率降序排列"),
                new Tuple<object, string>(OrderPrbStatPolicy.OrderByPuschDtchPrbRateDescending, "按照PUSCH DRB PRB利用率降序排列"),
                new Tuple<object, string>(OrderPrbStatPolicy.OrderByPuschPrbRateDescending, "按照PUSCH PRB利用率降序排列"),
                new Tuple<object, string>(OrderPrbStatPolicy.OrderByPdschHighUsageDescending, "按照PDSCH高PRB利用率时长降序排列"),
                new Tuple<object, string>(OrderPrbStatPolicy.OrderByPuschHighUsageDescending, "按照PUSCH高PRB利用率时长降序排列"),
            };
        }
    }
}
