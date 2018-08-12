using Abp.EntityFramework.AutoMapper;
using Lte.MySqlFramework.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Abp.EntityFramework.Dependency;
using Abp.EntityFramework.Entities.Kpi;
using Lte.Domain.Common.Wireless.Kpi;
using Lte.MySqlFramework.Abstract.Infrastructure;
using Lte.MySqlFramework.Abstract.Kpi;
using Lte.MySqlFramework.Abstract.Region;
using Lte.MySqlFramework.Query;
using Lte.MySqlFramework.Support;

namespace Lte.Evaluations.DataService.Kpi
{
    public class CqiReportService 
        : FilterDateSpanQuery<CqiView, ICqiHuaweiRepository, ICqiZteRepository, CqiZte, CqiHuawei>
    {
        public CqiReportService(ICqiHuaweiRepository huaweiRepository, ICqiZteRepository zteRepository,
            IENodebRepository eNodebRepository, ITownRepository townRepository)
            : base(huaweiRepository, zteRepository, eNodebRepository, null, townRepository)
        {
        }

        protected override IDateSpanQuery<List<CqiView>> GenerateHuaweiQuery(int eNodebId, byte sectorId)
        {
            return new HuaweiCqiQuery(HuaweiRepository, eNodebId, sectorId);
        }

        protected override IDateSpanQuery<List<CqiView>> GenerateZteQuery(int eNodebId, byte sectorId)
        {
            return new ZteCqiQuery(ZteRepository, eNodebId, sectorId);
        }

        private List<CqiView> QueryTopViewsByPolicy(IEnumerable<CqiView> source, int topCount,
            OrderCqiPolicy policy)
        {
            switch (policy)
            {
                case OrderCqiPolicy.OrderByCqiRate:
                    return source.OrderBy(x => x.CqiRate).Take(topCount).ToList();
                case OrderCqiPolicy.OrderByPoorCqiDescending:
                    return source.OrderByDescending(x => x.CqiCounts.Item1 - x.CqiCounts.Item2).Take(topCount).ToList();
            }
            return new List<CqiView>();
        }

        public List<CqiView> QueryTopCqiViews(string city, string district, DateTime begin, DateTime end, int topCount)
        {
            var results = QueryDistrictViews(city, district, begin, end).ToList();
            var days = (results.Max(x => x.StatTime) - results.Min(x => x.StatTime)).Days + 1;
            return results.OrderByDescending(x => x.CqiCounts.Item1 - x.CqiCounts.Item2).Take(topCount * days).ToList();
        }

        public List<CqiView> QueryTopCqiViews(DateTime begin, DateTime end, int topCount, OrderCqiPolicy policy)
        {
            var zteStats = ZteRepository.FilterTopList(begin, end);
            var huaweiStats = HuaweiRepository.FilterTopList(begin, end);
            var joinViews = zteStats.MapTo<IEnumerable<CqiView>>().Concat(huaweiStats.MapTo<IEnumerable<CqiView>>()).ToList();
            var days = (joinViews.Max(x => x.StatTime) - joinViews.Min(x => x.StatTime)).Days + 1;
            return QueryTopViewsByPolicy(joinViews, topCount * days, policy);
        }

        public List<CqiView> QueryTopCqiViews(string city, string district, DateTime begin, DateTime end,
            int topCount, OrderCqiPolicy policy)
        {
            var joinViews = QueryDistrictViews(city, district, begin, end).ToList();
            var days = (joinViews.Max(x => x.StatTime) - joinViews.Min(x => x.StatTime)).Days + 1;
            return QueryTopViewsByPolicy(joinViews, topCount * days, policy);
        }
    }
}

