using Abp.EntityFramework.Entities.Infrastructure;
using Abp.EntityFramework.Entities.Mr;
using Lte.Domain.Common.Wireless.Kpi;
using Lte.MySqlFramework.Abstract.Infrastructure;
using Lte.MySqlFramework.Abstract.Mr;
using Lte.MySqlFramework.Entities.Mr;
using Lte.MySqlFramework.Support;
using Lte.MySqlFramework.Support.Container;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lte.Evaluations.DataService.Kpi
{
    public class TopMrsTadvService
    {
        private readonly ITopMrsTadvRepository _topMrsTadvRepository;
        private readonly IENodebRepository _eNodebRepository;

        public TopMrsTadvService(ITopMrsTadvRepository topMrsTadvRepository, IENodebRepository eNodebRepository)
        {
            _topMrsTadvRepository = topMrsTadvRepository;
            _eNodebRepository = eNodebRepository;
        }

        private IEnumerable<TopMrsTadvView> GetTopViews(DateTime begin, DateTime end, int topCount,
            OrderMrsTadvPolicy policy, Func<DateTime, DateTime, List<TopMrsTadv>> queryFunc)
        {
            if (topCount <= 0) return new List<TopMrsTadvView>();
            var orderResult = new List<TopMrsTadv>();
            var beginDate = begin;
            var endDate = beginDate.AddDays(1);
            while (endDate < end)
            {
                var stats = queryFunc(beginDate, endDate);
                if (stats.Any())
                {
                    orderResult.AddRange(stats.Where(x => (double)(x.TadvAbove192 + x.Tadv112To192 + x.Tadv80To112 + x.Tadv60To80 + x.Tadv54To60)
                                                     /
                                                     (x.TadvBelow1 + x.Tadv1To2 + x.Tadv2To3 + x.Tadv3To4 + x.Tadv4To6 + x.Tadv6To8 +
                                                      x.Tadv8To10 + x.Tadv10To12 + x.Tadv12To14 + x.Tadv14To16 + x.Tadv16To18
                                                      + x.Tadv18To20 + x.Tadv20To24 + x.Tadv24To28 + x.Tadv28To32 + x.Tadv32To36
                                                      + x.Tadv36To42 + x.Tadv42To48 + x.Tadv48To54) > 0.2).Order(policy, topCount));
                }
                beginDate = beginDate.AddDays(1);
                endDate = beginDate.AddDays(1);
            }
            var containers = orderResult.GenerateContainers<TopMrsTadv, TopMrsTadvContainer>();
            return containers.Select(x =>
            {
                var view = TopMrsTadvView.ConstructView(x.TopStat, _eNodebRepository);
                view.TopDates = x.TopDates;
                return view;
            });
        }

        public IEnumerable<TopMrsTadvView> GetAllTopViews(DateTime begin, DateTime end, int topCount,
            OrderMrsTadvPolicy policy)
        {
            return GetTopViews(begin, end, topCount, policy,
                (beginDate, endDate) =>
                    _topMrsTadvRepository.GetAllList(x => x.StatDate >= beginDate && x.StatDate < endDate));
        }

        public IEnumerable<TopMrsTadvView> GetPartialTopViews(DateTime begin, DateTime end, int topCount,
            OrderMrsTadvPolicy policy, IEnumerable<ENodeb> eNodebs)
        {
            return GetTopViews(begin, end, topCount, policy,
                (beginDate, endDate) =>
                {
                    var stats = _topMrsTadvRepository.GetAllList(x => x.StatDate >= beginDate && x.StatDate < endDate);
                    return (from q in stats
                            join e in eNodebs on q.ENodebId equals e.ENodebId
                            select q).ToList();
                });
        }
    }
}
