using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities.Kpi;
using Lte.Domain.Common.Wireless.Kpi;
using Lte.MySqlFramework.Abstract.Infrastructure;
using Lte.MySqlFramework.Abstract.Kpi;
using Lte.MySqlFramework.Abstract.Region;
using Lte.MySqlFramework.Entities.Kpi;
using Lte.MySqlFramework.Support;

namespace Lte.Evaluations.DataService.Kpi
{
    public class HourPrbQueryService : FilterLocalDateSpanQuery<HourPrbView, IHourPrbRepository, HourPrb>
    {
        public HourPrbQueryService(IHourPrbRepository repository, IENodebRepository eNodebRepository,
            ITownRepository townRepository) : base(repository, eNodebRepository, townRepository)
        {
        }
        
        private List<HourPrbView> QueryTopViewsByPolicy(IEnumerable<HourPrbView> source, int topCount,
            OrderPrbStatPolicy policy)
        {
            switch (policy)
            {
                case OrderPrbStatPolicy.OrderByPdschDtchPrbRateDescending:
                    return source.OrderByDescending(x => x.PdschPrbRate).Take(topCount).ToList();
                case OrderPrbStatPolicy.OrderByPdschHighUsageDescending:
                    return source.OrderByDescending(x => x.PdschTotalPrbs).Take(topCount).ToList();
                case OrderPrbStatPolicy.OrderByPdschPrbRateDescending:
                    return source.OrderByDescending(x => x.DownlinkPrbRate).Take(topCount).ToList();
                case OrderPrbStatPolicy.OrderByPuschDtchPrbRateDescending:
                    return source.OrderByDescending(x => x.UplinkPrbRate).Take(topCount).ToList();
                case OrderPrbStatPolicy.OrderByPuschHighUsageDescending:
                    return source.OrderByDescending(x => x.PuschTotalPrbs).Take(topCount).ToList();
                case OrderPrbStatPolicy.OrderByPuschPrbRateDescending:
                    return source.OrderByDescending(x => x.PuschPrbRate).Take(topCount).ToList();
            }
            return new List<HourPrbView>();
        }
        
        public List<HourPrbView> QueryTopPrbViews(string city, string district, DateTime begin, DateTime end, int topCount)
        {
            var results = QueryDistrictViews(city, district, begin, end).ToList();
            var days = (results.Max(x => x.StatTime) - results.Min(x => x.StatTime)).Days + 1;
            return results.OrderByDescending(x => x.DownlinkPrbRate).Take(topCount * days).ToList();
        }

        public List<HourPrbView> QueryTopPrbViews(DateTime begin, DateTime end, int topCount, OrderPrbStatPolicy policy)
        {
            var stats = Repository.FilterTopList(begin, end);
            var joinViews = stats.MapTo<List<HourPrbView>>();
            var days = (joinViews.Max(x => x.StatTime) - joinViews.Min(x => x.StatTime)).Days + 1;
            return QueryTopViewsByPolicy(joinViews, topCount * days, policy);
        }

        public List<HourPrbView> QueryTopPrbViews(string city, string district, DateTime begin, DateTime end,
            int topCount, OrderPrbStatPolicy policy)
        {
            var joinViews = QueryDistrictViews(city, district, begin, end).ToList();
            var days = (joinViews.Max(x => x.StatTime) - joinViews.Min(x => x.StatTime)).Days + 1;
            return QueryTopViewsByPolicy(joinViews, topCount * days, policy);
        }
    }
}
