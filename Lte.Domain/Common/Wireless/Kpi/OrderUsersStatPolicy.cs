using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Common.Types;

namespace Lte.Domain.Common.Wireless.Kpi
{
    [EnumTypeDescription(typeof(OrderUsersStatPolicy), OrderByMaxRrcUsersDescending)]
    public enum OrderUsersStatPolicy: byte
    {
        OrderByMaxRrcUsersDescending,
        OrderByMaxActiveUsersDescending,
        OrderByDownlinkActiveUsersDescending,
        OrderByUplinkActiveUsersDescending,
        OrderByMaxCaUsersDescending
    }
    
    internal static class OrderUsersStatPolicyTuples
    {
        public static Tuple<object, string>[] GetTuples()
        {
            return new[]
            {
                new Tuple<object, string>(OrderUsersStatPolicy.OrderByMaxRrcUsersDescending, "按照最大RRC连接用户数降序排列"),
                new Tuple<object, string>(OrderUsersStatPolicy.OrderByMaxActiveUsersDescending, "按照最大激活用户数降序排列"),
                new Tuple<object, string>(OrderUsersStatPolicy.OrderByDownlinkActiveUsersDescending, "按照下行平均激活用户数降序排列"),
                new Tuple<object, string>(OrderUsersStatPolicy.OrderByUplinkActiveUsersDescending, "按照上行平均激活用户数降序排列"),
                new Tuple<object, string>(OrderUsersStatPolicy.OrderByMaxCaUsersDescending, "按照小区最大CA能力用户数降序排列"),
            };
        }
    }
}
