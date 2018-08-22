using System;
using System.Collections.Generic;
using System.Linq;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities.Infrastructure;
using Abp.EntityFramework.Entities.Test;
using Lte.Domain.Common.Wireless.Kpi;
using Lte.MySqlFramework.Abstract.Infrastructure;
using Lte.MySqlFramework.Abstract.Test;
using Lte.MySqlFramework.Entities;
using Lte.MySqlFramework.Entities.Mr;
using Lte.MySqlFramework.Support;
using Lte.MySqlFramework.Support.Container;

namespace Lte.Evaluations.DataService.Mr
{
    public class TopCoverageStatService
    {
        private readonly ICoverageStatRepository _repository;
        private readonly IENodebRepository _eNodebRepository;

        public TopCoverageStatService(ICoverageStatRepository repository, IENodebRepository eNodebRepository)
        {
            _repository = repository;
            _eNodebRepository = eNodebRepository;
        }

        private IEnumerable<TopCoverageStatView> GetTopViews(DateTime begin, DateTime end, int topCount,
            OrderMrsRsrpPolicy policy, IEnumerable<ENodeb> eNodebs, Func<DateTime, DateTime, List<CoverageStat>> queryFunc)
        {
            if (topCount <= 0) return new List<TopCoverageStatView>();
            var orderResult = new List<CoverageStat>();
            var beginDate = begin;
            var endDate = beginDate.AddDays(1);
            while (endDate < end)
            {
                var stats = queryFunc(beginDate, endDate);
                if (stats.Any())
                {
                    orderResult.AddRange(stats
                        .Where(x => (double) x.TelecomAbove110 / x.TelecomMrs < 0.8 && x.TelecomMrs > 10000)
                        .Order(policy, topCount));
                }
                beginDate = beginDate.AddDays(1);
                endDate = beginDate.AddDays(1);
            }
            var containers = orderResult.GenerateContainers<CoverageStat, TopCoverageStatContainer>();
            return containers.Select(x =>
            {
                var view = TopCoverageStatView.ConstructView(x.TopStat, eNodebs);
                view.TopDates = x.TopDates;
                return view;
            });
        }

        public IEnumerable<TopCoverageStatView> GetAllTopViews(DateTime begin, DateTime end, int topCount,
            OrderMrsRsrpPolicy policy)
        {
            return GetTopViews(begin, end, topCount, policy, _eNodebRepository.GetAllList(),
                (beginDate, endDate) =>
                    _repository.GetAllList(x => x.StatDate >= beginDate && x.StatDate < endDate));
        }

        public IEnumerable<TopCoverageStatView> GetPartialTopViews(DateTime begin, DateTime end, int topCount,
            OrderMrsRsrpPolicy policy, List<ENodeb> eNodebs)
        {
            return GetTopViews(begin, end, topCount, policy, eNodebs,
                (beginDate, endDate) =>
                {
                    var stats = _repository.GetAllList(x => x.StatDate >= beginDate && x.StatDate < endDate);
                    return (from q in stats
                            join e in eNodebs on q.ENodebId equals e.ENodebId
                            select q).ToList();
                });
        }

        public CoverageStatView GetOneDayView(int eNodebId, byte sectorId, DateTime statDate)
        {
            var end = statDate.AddDays(1);
            return
                CoverageStatView.ConstructView(
                    _repository.FirstOrDefault(
                        x =>
                            x.ENodebId == eNodebId && x.SectorId == sectorId && x.StatDate >= statDate &&
                            x.StatDate < end), _eNodebRepository);
        }

        public IEnumerable<CoverageStatView> GetDateSpanViews(int eNodebId, byte sectorId, DateTime begin, DateTime end)
        {
            var eNodeb = _eNodebRepository.FirstOrDefault(x => x.ENodebId == eNodebId);
            var list =
                _repository.GetAllList(
                        x => x.ENodebId == eNodebId && x.SectorId == sectorId && x.StatDate >= begin && x.StatDate < end)
                    .MapTo<List<CoverageStatView>>();
            list.ForEach(stat => stat.ENodebName = eNodeb?.Name);
            return list;

        }
    }
}
