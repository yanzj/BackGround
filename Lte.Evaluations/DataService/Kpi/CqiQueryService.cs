using System;
using System.Collections.Generic;
using System.Linq;
using Abp.EntityFramework.Entities;
using Lte.Domain.Common.Wireless;
using Lte.Evaluations.DataService.Switch;
using Lte.MySqlFramework.Abstract;
using Lte.MySqlFramework.Entities;

namespace Lte.Evaluations.DataService.Kpi
{
    public class CqiQueryService : DateSpanQuery<QciView, IQciHuaweiRepository, IQciZteRepository>
    {
        public CqiQueryService(IQciHuaweiRepository huaweiRepository, IQciZteRepository zteRepository,
            IENodebRepository eNodebRepository, ICellRepository huaweiCellRepository, ITownRepository townRepository)
            : base(huaweiRepository, zteRepository, eNodebRepository, huaweiCellRepository, townRepository)
        {
        }

        protected override IDateSpanQuery<List<QciView>> GenerateHuaweiQuery(int eNodebId, byte sectorId)
        {
            return new HuaweiQciQuery(HuaweiRepository, HuaweiCellRepository, eNodebId, sectorId);
        }

        protected override IDateSpanQuery<List<QciView>> GenerateZteQuery(int eNodebId, byte sectorId)
        {
            return new ZteQciQuery(ZteRepository, eNodebId, sectorId);
        }

        private IEnumerable<QciView> QueryDistrictViews(string city, string district, DateTime begin, DateTime end)
        {
            var zteStats = ZteRepository.FilterTopList(begin, end);
            var huaweiStats = HuaweiRepository.FilterTopList(begin, end);
            var results = HuaweiCellRepository.QueryDistrictFlowViews<QciView, QciZte, QciHuawei>(city, district,
                zteStats,
                huaweiStats,
                TownRepository, ENodebRepository);
            return results;
        }

        private List<QciView> QueryTopViewsByPolicy(IEnumerable<QciView> source, int topCount,
            OrderCqiPolicy policy)
        {
            switch (policy)
            {
                case OrderCqiPolicy.OrderByCqiRate:
                    return source.OrderBy(x => x.CqiRate).Take(topCount).ToList();
                case OrderCqiPolicy.OrderByPoorCqiDescending:
                    return source.OrderByDescending(x => x.CqiCounts.Item1 - x.CqiCounts.Item2).Take(topCount).ToList();
            }
            return new List<QciView>();
        }

        public List<QciView> QueryTopCqiViews(string city, string district, DateTime begin, DateTime end, int topCount)
        {
            var results = QueryDistrictViews(city, district, begin, end).ToList();
            var days = (results.Max(x => x.StatTime) - results.Min(x => x.StatTime)).Days + 1;
            return results.OrderByDescending(x => x.CqiCounts.Item1 - x.CqiCounts.Item2).Take(topCount * days).ToList();
        }

        public List<QciView> QueryTopCqiViews(DateTime begin, DateTime end, int topCount, OrderCqiPolicy policy)
        {
            var zteStats = ZteRepository.FilterTopList(begin, end);
            var huaweiStats = HuaweiRepository.FilterTopList(begin, end);
            var joinViews = HuaweiCellRepository.QueryAllFlowViews<QciView, QciZte, QciHuawei>(zteStats, huaweiStats).ToList();
            var days = (joinViews.Max(x => x.StatTime) - joinViews.Min(x => x.StatTime)).Days + 1;
            return QueryTopViewsByPolicy(joinViews, topCount * days, policy);
        }

        public List<QciView> QueryTopCqiViews(string city, string district, DateTime begin, DateTime end,
            int topCount, OrderCqiPolicy policy)
        {
            var joinViews = QueryDistrictViews(city, district, begin, end).ToList();
            var days = (joinViews.Max(x => x.StatTime) - joinViews.Min(x => x.StatTime)).Days + 1;
            return QueryTopViewsByPolicy(joinViews, topCount * days, policy);
        }
    }
}