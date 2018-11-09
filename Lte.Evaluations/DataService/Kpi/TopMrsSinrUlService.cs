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
    public class TopMrsSinrUlService
    {
        private readonly ITopMrsSinrUlRepository _topMrsSinrUlRepository;
        private readonly IENodebRepository _eNodebRepository;

        public TopMrsSinrUlService(ITopMrsSinrUlRepository topMrsSinrUlRepository, IENodebRepository eNodebRepository)
        {
            _topMrsSinrUlRepository = topMrsSinrUlRepository;
            _eNodebRepository = eNodebRepository;
        }

        private IEnumerable<TopMrsSinrUlView> GetTopViews(DateTime begin, DateTime end, int topCount,
            OrderMrsSinrUlPolicy policy, Func<DateTime, DateTime, List<TopMrsSinrUl>> queryFunc)
        {
            if (topCount <= 0) return new List<TopMrsSinrUlView>();
            var orderResult = new List<TopMrsSinrUl>();
            var beginDate = begin;
            var endDate = beginDate.AddDays(1);
            while (endDate < end)
            {
                var stats = queryFunc(beginDate, endDate);
                if (stats.Any())
                {
                    orderResult.AddRange(stats.Where(x => (double)(x.SinrUlBelowM9 + x.SinrUlM9ToM6 + x.SinrUlM6ToM3)
                                                     /
                                                     (x.SinrUlBelowM9 + x.SinrUlM9ToM6 + x.SinrUlM6ToM3 + x.SinrUlM3To0 +
                                                      x.SinrUl0To3+ x.SinrUl3To6 + x.SinrUl6To9 + x.SinrUl9To12 
                                                      + x.SinrUl12To15 + x.SinrUl15To18 + x.SinrUl18To21 
                                                      + x.SinrUl21To24 + x.SinrUlAbove24) > 0.1).Order(policy, topCount));
                }
                beginDate = beginDate.AddDays(1);
                endDate = beginDate.AddDays(1);
            }
            var containers = orderResult.GenerateContainers<TopMrsSinrUl, TopMrsSinrUlContainer>();
            return containers.Select(x =>
            {
                var view = TopMrsSinrUlView.ConstructView(x.TopStat, _eNodebRepository);
                view.TopDates = x.TopDates;
                return view;
            });
        }

        public IEnumerable<TopMrsSinrUlView> GetAllTopViews(DateTime begin, DateTime end, int topCount,
            OrderMrsSinrUlPolicy policy)
        {
            return GetTopViews(begin, end, topCount, policy,
                (beginDate, endDate) =>
                    _topMrsSinrUlRepository.GetAllList(x => x.StatDate >= beginDate && x.StatDate < endDate));
        }

        public IEnumerable<TopMrsSinrUlView> GetPartialTopViews(DateTime begin, DateTime end, int topCount,
            OrderMrsSinrUlPolicy policy, IEnumerable<ENodeb> eNodebs)
        {
            return GetTopViews(begin, end, topCount, policy,
                (beginDate, endDate) =>
                {
                    var stats = _topMrsSinrUlRepository.GetAllList(x => x.StatDate >= beginDate && x.StatDate < endDate);
                    return (from q in stats
                            join e in eNodebs on q.ENodebId equals e.ENodebId
                            select q).ToList();
                });
        }
    }
}
