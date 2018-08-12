using System;
using System.Collections.Generic;
using System.Linq;
using Abp.EntityFramework.Entities.Kpi;
using Lte.Domain.Common.Wireless.Kpi;
using Lte.Evaluations.DataService.Switch;
using Lte.Evaluations.Query;
using Lte.MySqlFramework.Abstract.Infrastructure;
using Lte.MySqlFramework.Abstract.Kpi;
using Lte.MySqlFramework.Abstract.Region;
using Lte.MySqlFramework.Entities;

namespace Lte.Evaluations.DataService.Kpi
{
    public class PrbQueryService : DateSpanQuery<PrbView, IPrbHuaweiRepository, IPrbZteRepository>
    {
        public PrbQueryService(IPrbHuaweiRepository huaweiRepository, IPrbZteRepository zteRepository,
            IENodebRepository eNodebRepository, ICellRepository huaweiCellRepository, ITownRepository townRepository)
            : base(huaweiRepository, zteRepository, eNodebRepository, huaweiCellRepository, townRepository)
        {
        }

        protected override IDateSpanQuery<List<PrbView>> GenerateHuaweiQuery(int eNodebId, byte sectorId)
        {
            return new HuaweiPrbQuery(HuaweiRepository, HuaweiCellRepository, eNodebId, sectorId);
        }

        protected override IDateSpanQuery<List<PrbView>> GenerateZteQuery(int eNodebId, byte sectorId)
        {
            return new ZtePrbQuery(ZteRepository, eNodebId, sectorId);
        }

        private IEnumerable<PrbView> QueryDistrictViews(string city, string district, DateTime begin, DateTime end)
        {
            var zteStats = ZteRepository.FilterTopList(begin, end);
            var huaweiStats = HuaweiRepository.FilterTopList(begin, end);
            var results = HuaweiCellRepository.QueryDistrictFlowViews<PrbView, PrbZte, PrbHuawei>(city, district,
                zteStats,
                huaweiStats,
                TownRepository, ENodebRepository);
            return results;
        }

        private List<PrbView> QueryTopViewsByPolicy(IEnumerable<PrbView> source, int topCount,
            OrderPrbStatPolicy policy)
        {
            switch (policy)
            {
                case OrderPrbStatPolicy.OrderByPdschDtchPrbRateDescending:
                    return source.OrderByDescending(x => x.PdschDtchPrbRate).Take(topCount).ToList();
                case OrderPrbStatPolicy.OrderByPdschHighUsageDescending:
                    return source.OrderByDescending(x => x.PdschHighUsageSeconds).Take(topCount).ToList();
                case OrderPrbStatPolicy.OrderByPdschPrbRateDescending:
                    return source.OrderByDescending(x => x.PdschPrbRate).Take(topCount).ToList();
                case OrderPrbStatPolicy.OrderByPuschDtchPrbRateDescending:
                    return source.OrderByDescending(x => x.PuschDtchPrbRate).Take(topCount).ToList();
                case OrderPrbStatPolicy.OrderByPuschHighUsageDescending:
                    return source.OrderByDescending(x => x.PuschHighUsageSeconds).Take(topCount).ToList();
                case OrderPrbStatPolicy.OrderByPuschPrbRateDescending:
                    return source.OrderByDescending(x => x.PuschPrbRate).Take(topCount).ToList();
            }
            return new List<PrbView>();
        }

        public List<PrbView> QueryTopPrbViews(string city, string district, DateTime begin, DateTime end, int topCount)
        {
            var results = QueryDistrictViews(city, district, begin, end).ToList();
            var days = (results.Max(x => x.StatTime) - results.Min(x => x.StatTime)).Days + 1;
            return results.OrderByDescending(x => x.PdschPrbRate).Take(topCount * days).ToList();
        }

        public List<PrbView> QueryTopPrbViews(DateTime begin, DateTime end, int topCount, OrderPrbStatPolicy policy)
        {
            var zteStats = ZteRepository.FilterTopList(begin, end);
            var huaweiStats = HuaweiRepository.FilterTopList(begin, end);
            var joinViews = HuaweiCellRepository.QueryAllFlowViews<PrbView, PrbZte, PrbHuawei>(zteStats, huaweiStats).ToList();
            var days = (joinViews.Max(x => x.StatTime) - joinViews.Min(x => x.StatTime)).Days + 1;
            return QueryTopViewsByPolicy(joinViews, topCount * days, policy);
        }

        public List<PrbView> QueryTopPrbViews(string city, string district, DateTime begin, DateTime end,
            int topCount, OrderPrbStatPolicy policy)
        {
            var joinViews = QueryDistrictViews(city, district, begin, end).ToList();
            var days = (joinViews.Max(x => x.StatTime) - joinViews.Min(x => x.StatTime)).Days + 1;
            return QueryTopViewsByPolicy(joinViews, topCount * days, policy);
        }
    }
}
