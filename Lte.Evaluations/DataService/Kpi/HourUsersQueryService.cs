using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Dependency;
using Abp.EntityFramework.Entities.Kpi;
using Lte.Domain.Common.Wireless.Kpi;
using Lte.MySqlFramework.Abstract.Infrastructure;
using Lte.MySqlFramework.Abstract.Kpi;
using Lte.MySqlFramework.Abstract.Region;
using Lte.MySqlFramework.Entities.Kpi;
using Lte.MySqlFramework.Query;
using Lte.MySqlFramework.Support;

namespace Lte.Evaluations.DataService.Kpi
{
    public class HourUsersQueryService : FilterLocalDateSpanQuery<HourUsersView, IHourUsersRepository, HourUsers>
    {
        public HourUsersQueryService(IHourUsersRepository repository, IENodebRepository eNodebRepository,
            ITownRepository townRepository) : base(repository, eNodebRepository, townRepository)
        {
        }

        protected override IDateSpanQuery<List<HourUsersView>> GenerateQuery(int eNodebId, byte sectorId)
        {
            return new HourUsersDateSpanQuery(Repository, eNodebId, sectorId);
        }
        
        private List<HourUsersView> QueryTopViewsByPolicy(IEnumerable<HourUsersView> source, int topCount,
            OrderUsersStatPolicy policy)
        {
            switch (policy)
            {
                case OrderUsersStatPolicy.OrderByMaxRrcUsersDescending:
                    return source.OrderByDescending(x => x.MaxRrcUsers).Take(topCount).ToList();
                case OrderUsersStatPolicy.OrderByMaxActiveUsersDescending:
                    return source.OrderByDescending(x => x.MaxActiveUsers).Take(topCount).ToList();
                case OrderUsersStatPolicy.OrderByDownlinkActiveUsersDescending:
                    return source.OrderByDescending(x => x.DownlinkAverageActiveUsers).Take(topCount).ToList();
                case OrderUsersStatPolicy.OrderByUplinkActiveUsersDescending:
                    return source.OrderByDescending(x => x.UplinkAverageActiveUsers).Take(topCount).ToList();
                case OrderUsersStatPolicy.OrderByMaxCaUsersDescending:
                    return source.OrderByDescending(x => x.MaxCaUsers).Take(topCount).ToList();
            }
            return new List<HourUsersView>();
        }
        
        public List<HourUsersView> QueryTopUsersViews(string city, string district, DateTime begin, DateTime end, int topCount)
        {
            var results = QueryDistrictViews(city, district, begin, end).ToList();
            var days = (results.Max(x => x.StatTime) - results.Min(x => x.StatTime)).Days + 1;
            return results.OrderByDescending(x => x.MaxRrcUsers).Take(topCount * days).ToList();
        }

        public List<HourUsersView> QueryTopUsersViews(DateTime begin, DateTime end, int topCount, OrderUsersStatPolicy policy)
        {
            var stats = Repository.FilterTopList(begin, end);
            var joinViews = stats.MapTo<List<HourUsersView>>();
            var days = (joinViews.Max(x => x.StatTime) - joinViews.Min(x => x.StatTime)).Days + 1;
            return QueryTopViewsByPolicy(joinViews, topCount * days, policy);
        }

        public List<HourUsersView> QueryTopUsersViews(string city, string district, DateTime begin, DateTime end,
            int topCount, OrderUsersStatPolicy policy)
        {
            var joinViews = QueryDistrictViews(city, district, begin, end).ToList();
            var days = (joinViews.Max(x => x.StatTime) - joinViews.Min(x => x.StatTime)).Days + 1;
            return QueryTopViewsByPolicy(joinViews, topCount * days, policy);
        }
    }
}
