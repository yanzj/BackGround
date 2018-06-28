using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lte.Domain.Common.Wireless
{
    public enum OrderTopFlowPolicy : byte
    {
        OrderByDownlinkFlowDescending,
        OrderByUplinkFlowDescending,
        OrderByTotalFlowDescending,
        OrderByMaxUsersDescending,
        OrderByMaxActiveUsersDescending
    }

    internal static class OrderTopFlowPolicyTuples
    {
        public static Tuple<object, string>[] GetTuples()
        {
            return new[]
            {
                new Tuple<object, string>(OrderTopFlowPolicy.OrderByDownlinkFlowDescending, "按照下行流量降序"),
                new Tuple<object, string>(OrderTopFlowPolicy.OrderByUplinkFlowDescending, "按照上行流量降序"),
                new Tuple<object, string>(OrderTopFlowPolicy.OrderByTotalFlowDescending, "按照总流量降序"),
                new Tuple<object, string>(OrderTopFlowPolicy.OrderByMaxUsersDescending, "按照最大用户数降序"),
                new Tuple<object, string>(OrderTopFlowPolicy.OrderByMaxActiveUsersDescending, "按照最大激活用户数排序")
            };
        }
    }
}
