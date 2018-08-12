using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Kpi;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Common.Wireless.Kpi;

namespace Lte.MySqlFramework.Support
{
    public static class FlowQuerySupport
    {
        public static List<FlowView> QueryTopViewsByPolicy(this List<FlowView> source, int topCount,
            OrderDownSwitchPolicy policy)
        {
            var minDate = source.Min(x => x.StatTime);
            var maxDate = source.Max(x => x.StatTime);
            topCount *= (maxDate - minDate).Days + 1;
            switch (policy)
            {
                case OrderDownSwitchPolicy.OrderByDownSwitchCountsDescendings:
                    return source.OrderByDescending(x => x.RedirectCdma2000).Take(topCount).ToList();
                case OrderDownSwitchPolicy.OrderByDownSwitchRateDescending:
                    return source.OrderByDescending(x => x.DownSwitchRate).Take(topCount).ToList();
                default:
                    return new List<FlowView>();
            }

        }

        public static List<FlowView> QueryTopViewsByPolicy(this List<FlowView> source, int topCount,
            OrderFeelingRatePolicy policy)
        {
            var minDate = source.Min(x => x.StatTime);
            var maxDate = source.Max(x => x.StatTime);
            topCount *= (maxDate - minDate).Days + 1;
            switch (policy)
            {
                case OrderFeelingRatePolicy.OrderByDownlinkDurationDescendings:
                    return source.OrderByDescending(x => x.DownlinkFeelingDuration).Take(topCount).ToList();
                case OrderFeelingRatePolicy.OrderByDownlinkFeelingRateRate:
                    return source.OrderBy(x => x.DownlinkFeelingRate).Take(topCount).ToList();
                case OrderFeelingRatePolicy.OrderByUplinkDurationDescendings:
                    return source.OrderByDescending(x => x.UplinkFeelingDuration).Take(topCount).ToList();
                case OrderFeelingRatePolicy.OrderByUplinkFeelingRateRate:
                    return source.OrderBy(x => x.UplinkFeelingRate).Take(topCount).ToList();
                default:
                    return new List<FlowView>();
            }

        }

        public static List<FlowView> QueryTopViewsByPolicy(this List<FlowView> source, int topCount,
            OrderTopFlowPolicy policy)
        {
            var minDate = source.Min(x => x.StatTime);
            var maxDate = source.Max(x => x.StatTime);
            topCount *= (maxDate - minDate).Days + 1;
            switch (policy)
            {
                case OrderTopFlowPolicy.OrderByDownlinkFlowDescending:
                    return source.OrderByDescending(x => x.PdcpDownlinkFlow).Take(topCount).ToList();
                case OrderTopFlowPolicy.OrderByUplinkFlowDescending:
                    return source.OrderByDescending(x => x.PdcpUplinkFlow).Take(topCount).ToList();
                case OrderTopFlowPolicy.OrderByTotalFlowDescending:
                    return source.OrderByDescending(x => x.PdcpDownlinkFlow + x.PdcpUplinkFlow).Take(topCount).ToList();
                case OrderTopFlowPolicy.OrderByMaxUsersDescending:
                    return source.OrderByDescending(x => x.MaxUsers).Take(topCount).ToList();
                case OrderTopFlowPolicy.OrderByMaxActiveUsersDescending:
                    return source.OrderByDescending(x => x.MaxActiveUsers).Take(topCount).ToList();
                default:
                    return new List<FlowView>();
            }

        }

    }
}
