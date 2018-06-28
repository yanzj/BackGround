using Lte.Evaluations.DataService.Switch;
using Lte.MySqlFramework.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using Abp.EntityFramework.Entities;
using Lte.Domain.Common.Wireless;

namespace Lte.Evaluations.DataService.Kpi
{
    public class FlowQueryService : DateSpanQuery<FlowView, IFlowHuaweiRepository, IFlowZteRepository>
    {
        private IEnumerable<FlowView> QueryDistrictDownSwitchViews(string city, string district, DateTime begin, DateTime end)
        {
            var zteStats =
                ZteRepository.GetAllList(
                    x => x.StatTime >= begin && x.StatTime < end && x.RedirectA2 + x.RedirectB2 > 2000);
            var huaweiStats =
                HuaweiRepository.GetAllList(
                    x => x.StatTime >= begin && x.StatTime < end && x.RedirectCdma2000 > 2000);
            var results = HuaweiCellRepository.QueryDistrictFlowViews<FlowView, FlowZte, FlowHuawei>(city, district,
                zteStats,
                huaweiStats,
                TownRepository, ENodebRepository);
            return results;
        }

        private IEnumerable<FlowView> QueryDistrictViews(string city, string district, DateTime begin, DateTime end)
        {
            var zteStats =
                ZteRepository.GetAllList(
                    x =>
                        x.StatTime >= begin && x.StatTime < end && x.DownlinkPdcpFlow + x.PdcpUplinkDuration > 200000 &&
                        x.MaxRrcUsers > 10);
            var huaweiStats =
                HuaweiRepository.GetAllList(
                    x => x.StatTime >= begin && x.PdcpDownlinkFlow + x.PdcpUplinkFlow > 200000 && x.MaxUsers > 10);
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

        private List<FlowView> QueryTopViewsByPolicy(List<FlowView> source, int topCount,
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

        private List<FlowView> QueryTopViewsByPolicy(List<FlowView> source, int topCount,
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

        public List<FlowView> QueryTopDownSwitchViews(string city, string district, DateTime begin, DateTime end, int topCount)
        {
            var results = QueryDistrictViews(city, district, begin, end);
            return results.OrderByDescending(x => x.RedirectCdma2000).Take(topCount).ToList();
        }

        public List<FlowView> QueryTopDownSwitchViews(DateTime begin, DateTime end, int topCount, OrderDownSwitchPolicy policy)
        {
            var zteStats =
                ZteRepository.GetAllList(
                        x => x.StatTime >= begin && x.StatTime < end && x.RedirectA2 + x.RedirectB2 > 2000);
            var huaweiStats =
                HuaweiRepository.GetAllList(x => x.StatTime >= begin && x.StatTime < end && x.RedirectCdma2000 > 2000);
            var joinViews = HuaweiCellRepository.QueryAllFlowViews<FlowView, FlowZte, FlowHuawei>(zteStats, huaweiStats);
            return QueryTopViewsByPolicy(joinViews.ToList(), topCount, policy);
        }

        public List<FlowView> QueryTopFlowViews(DateTime begin, DateTime end, int topCount, OrderTopFlowPolicy policy)
        {
            var zteStats =
                ZteRepository.GetAllList(
                    x =>
                        x.StatTime >= begin && x.StatTime < end && x.DownlinkPdcpFlow + x.PdcpUplinkDuration > 200000 &&
                        x.MaxRrcUsers > 10);
            var huaweiStats =
                HuaweiRepository.GetAllList(
                    x => x.StatTime >= begin && x.PdcpDownlinkFlow + x.PdcpUplinkFlow > 200000 && x.MaxUsers > 10);
            var joinViews = HuaweiCellRepository.QueryAllFlowViews<FlowView, FlowZte, FlowHuawei>(zteStats, huaweiStats);
            return QueryTopViewsByPolicy(joinViews.ToList(), topCount, policy);
        }

        public List<FlowView> QueryTopDownSwitchViews(string city, string district, DateTime begin, DateTime end,
            int topCount, OrderDownSwitchPolicy policy)
        {
            var joinViews = QueryDistrictDownSwitchViews(city, district, begin, end);
            return QueryTopViewsByPolicy(joinViews.ToList(), topCount, policy);
        }

        public List<FlowView> QueryTopFlowViews(string city, string district, DateTime begin, DateTime end,
            int topCount, OrderTopFlowPolicy policy)
        {
            var joinViews = QueryDistrictViews(city, district, begin, end);
            return QueryTopViewsByPolicy(joinViews.ToList(), topCount, policy);
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

        public FlowQueryService(IFlowHuaweiRepository huaweiRepository, IFlowZteRepository zteRepository,
            IENodebRepository eNodebRepository, ICellRepository huaweiCellRepository, ITownRepository townRepository)
            : base(huaweiRepository, zteRepository, eNodebRepository, huaweiCellRepository, townRepository)
        {
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
