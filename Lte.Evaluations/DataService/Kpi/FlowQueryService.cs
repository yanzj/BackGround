using Lte.Evaluations.DataService.Switch;
using Lte.MySqlFramework.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using Abp.EntityFramework.Entities.Kpi;
using Lte.Domain.Common.Wireless.Cell;
using Lte.Domain.Common.Wireless.Kpi;
using Lte.Evaluations.Query;
using Lte.MySqlFramework.Abstract.Infrastructure;
using Lte.MySqlFramework.Abstract.Kpi;
using Lte.MySqlFramework.Abstract.Region;
using Lte.MySqlFramework.Support;

namespace Lte.Evaluations.DataService.Kpi
{
    public class FlowQueryService : DateSpanQuery<FlowView, IFlowHuaweiRepository, IFlowZteRepository>
    {
        private const int DownSwitchThreshold = 200;

        public FlowQueryService(IFlowHuaweiRepository huaweiRepository, IFlowZteRepository zteRepository,
            IENodebRepository eNodebRepository, ICellRepository huaweiCellRepository, ITownRepository townRepository)
            : base(huaweiRepository, zteRepository, eNodebRepository, huaweiCellRepository, townRepository)
        {
        }

        private IEnumerable<FlowView> QueryDistrictDownSwitchViews(string city, string district, DateTime begin, DateTime end)
        {
            var zteStats = ZteRepository.GetHighDownSwitchList(begin, end, DownSwitchThreshold);
            var huaweiStats = HuaweiRepository.GetHighDownSwitchList(begin, end, DownSwitchThreshold);
            var results = HuaweiCellRepository.QueryDistrictFlowViews<FlowView, FlowZte, FlowHuawei>(city, district,
                zteStats,
                huaweiStats,
                TownRepository, ENodebRepository);
            return results;
        }
        
        private IEnumerable<FlowView> QueryDistrictViews(string city, string district, DateTime begin, DateTime end)
        {
            var zteStats = ZteRepository.GetBusyList(begin, end);
            var huaweiStats = HuaweiRepository.GetBusyList(begin, end);
            var results = HuaweiCellRepository.QueryDistrictFlowViews<FlowView, FlowZte, FlowHuawei>(city, district,
                zteStats,
                huaweiStats,
                TownRepository, ENodebRepository);
            return results;
        }

        public IEnumerable<FlowView> QueryAllTownViews(string city, string district, string town,
            DateTime begin, DateTime end, FrequencyBandType frequency)
        {
            var zteStats =
                ZteRepository.QueryZteFlows<FlowZte, IFlowZteRepository>(frequency, begin, end);
            var huaweiStats =
                HuaweiRepository.GetAllList(x => x.StatTime >= begin && x.StatTime < end);
            var results = HuaweiCellRepository.QueryTownFlowViews<FlowView, FlowZte, FlowHuawei>(city, district,
                town, zteStats, huaweiStats, frequency,
                TownRepository, ENodebRepository);
            return results;
        }

        public List<FlowView> QueryTopDownSwitchViews(string city, string district, DateTime begin, DateTime end,
            int topCount)
        {
            var results = QueryDistrictDownSwitchViews(city, district, begin, end);
            return results.OrderByDescending(x => x.RedirectCdma2000).Take(topCount).ToList();
        }

        public List<FlowView> QueryTopFeelingRateViews(string city, string district, DateTime begin, DateTime end,
            int topCount)
        {
            var results = QueryDistrictViews(city, district, begin, end);
            return results.OrderBy(x => x.DownlinkFeelingRate).Take(topCount).ToList();
        }

        public List<FlowView> QueryTopDownSwitchViews(DateTime begin, DateTime end, int topCount,
            OrderDownSwitchPolicy policy)
        {
            var zteStats = ZteRepository.GetHighDownSwitchList(begin, end, DownSwitchThreshold);
            var huaweiStats = HuaweiRepository.GetHighDownSwitchList(begin, end, DownSwitchThreshold);
            var joinViews =
                HuaweiCellRepository.QueryAllFlowViews<FlowView, FlowZte, FlowHuawei>(zteStats, huaweiStats);
            return joinViews.ToList().QueryTopViewsByPolicy(topCount, policy);
        }

        public List<FlowView> QueryTopFeelingRateViews(DateTime begin, DateTime end, int topCount,
            OrderFeelingRatePolicy policy)
        {
            var zteStats = ZteRepository.GetBusyList(begin, end);
            var huaweiStats = HuaweiRepository.GetBusyList(begin, end);
            var joinViews =
                HuaweiCellRepository.QueryAllFlowViews<FlowView, FlowZte, FlowHuawei>(zteStats, huaweiStats);
            return joinViews.ToList().QueryTopViewsByPolicy(topCount, policy);
        }

        public List<FlowView> QueryTopFlowViews(DateTime begin, DateTime end, int topCount, OrderTopFlowPolicy policy)
        {
            var zteStats = ZteRepository.GetBusyList(begin, end);
            var huaweiStats = HuaweiRepository.GetBusyList(begin, end);
            var joinViews = HuaweiCellRepository.QueryAllFlowViews<FlowView, FlowZte, FlowHuawei>(zteStats, huaweiStats);
            return joinViews.ToList().QueryTopViewsByPolicy(topCount, policy);
        }

        public List<FlowView> QueryTopDownSwitchViews(string city, string district, DateTime begin, DateTime end,
            int topCount, OrderDownSwitchPolicy policy)
        {
            var joinViews = QueryDistrictDownSwitchViews(city, district, begin, end);
            return joinViews.ToList().QueryTopViewsByPolicy(topCount, policy);
        }

        public List<FlowView> QueryTopFeelingRateViews(string city, string district, DateTime begin, DateTime end,
            int topCount, OrderFeelingRatePolicy policy)
        {
            var joinViews = QueryDistrictViews(city, district, begin, end);
            return joinViews.ToList().QueryTopViewsByPolicy(topCount, policy);
        }

        public List<FlowView> QueryTopFlowViews(string city, string district, DateTime begin, DateTime end,
            int topCount, OrderTopFlowPolicy policy)
        {
            var joinViews = QueryDistrictViews(city, district, begin, end);
            return joinViews.ToList().QueryTopViewsByPolicy(topCount, policy);
        }

        public IEnumerable<FlowView> QueryTopRank2Views(string city, string district, DateTime begin, DateTime end,
            int topCount)
        {
            var results = HuaweiCellRepository.QueryDistrictFlowViews<FlowView, FlowZte, FlowHuawei>(city, district,
                ZteRepository.GetAllList(x => x.StatTime >= begin && x.StatTime < end 
                && x.SchedulingTm3 -x.SchedulingTm3Rank2 > 10000000),
                HuaweiRepository.GetAllList(
                    x => x.StatTime >= begin && x.StatTime < end && x.SchedulingRank1 > 1000000),
                TownRepository, ENodebRepository);
            return results.OrderBy(x => x.Rank2Rate).Take(topCount);
        }

        public IEnumerable<FlowView> QueryAllTopRank2Views(DateTime begin, DateTime end,
            int topCount)
        {
            var results = HuaweiCellRepository.QueryAllFlowViews<FlowView, FlowZte, FlowHuawei>(
                ZteRepository.GetAllList(x => x.StatTime >= begin && x.StatTime < end
                                              && x.SchedulingTm3 - x.SchedulingTm3Rank2 > 10000000),
                HuaweiRepository.GetAllList(
                    x => x.StatTime >= begin && x.StatTime < end && x.SchedulingRank1 > 1000000));
            return results.OrderBy(x => x.Rank2Rate).Take(topCount);
        }

        protected override IDateSpanQuery<List<FlowView>> GenerateHuaweiQuery(int eNodebId, byte sectorId)
        {
            return new HuaweiFlowQuery(HuaweiRepository, HuaweiCellRepository, eNodebId, sectorId);
        }

        protected override IDateSpanQuery<List<FlowView>> GenerateZteQuery(int eNodebId, byte sectorId)
        {
            return new ZteFlowQuery(ZteRepository, eNodebId, sectorId);
        }
    }
}
