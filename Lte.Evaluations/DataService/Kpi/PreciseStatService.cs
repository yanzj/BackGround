using Lte.Evaluations.Properties;
using Lte.Parameters.Abstract.Kpi;
using System;
using System.Collections.Generic;
using System.Linq;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Infrastructure;
using Abp.EntityFramework.Entities.Kpi;
using Lte.Domain.Common.Wireless.Kpi;
using Lte.Evaluations.ViewModels.Precise;
using Lte.MySqlFramework.Abstract.Infrastructure;
using Lte.MySqlFramework.Support;

namespace Lte.Evaluations.DataService.Kpi
{
    public class PreciseStatService
    {
        private readonly IPreciseCoverage4GRepository _repository;
        private readonly IENodebRepository _eNodebRepository;

        public PreciseStatService(IPreciseCoverage4GRepository repository, IENodebRepository eNodebRepository)
        {
            _repository = repository;
            _eNodebRepository = eNodebRepository;
        }

        public IEnumerable<Precise4GView> GetTopCountViews(DateTime begin, DateTime end, int topCount,
            OrderPreciseStatPolicy policy)
        {
            if (topCount <= 0)
                return new List<Precise4GView>();
            var orderResult = GetTopCountStats(begin, end, topCount, policy);
            return orderResult.Select(x =>
            {
                var view = Precise4GView.ConstructView(x.PreciseCoverage4G, _eNodebRepository);
                view.TopDates = x.TopDates;
                return view;
            });
        }

        public IEnumerable<Precise4GView> GetTopCountViews(DateTime begin, DateTime end, int topCount,
            OrderPreciseStatPolicy policy, IEnumerable<ENodeb> eNodebs)
        {
            if (topCount <= 0)
                return new List<Precise4GView>();
            var orderResult = GetTopCountStats(begin, end, topCount, policy, eNodebs);
            return orderResult.Select(x =>
            {
                var view = Precise4GView.ConstructView(x.PreciseCoverage4G, _eNodebRepository);
                view.TopDates = x.TopDates;
                return view;
            });
        }

        public List<TopPrecise4GContainer> GetTopCountStats(DateTime begin, DateTime end, int topCount,
            OrderPreciseStatPolicy policy)
        {
            var query =
                _repository.GetAllList(x => x.StatTime >= begin && x.StatTime < end && x.TotalMrs > Settings.Default.TotalMrsThreshold);
            var result = query.GenerateContainers();

            var orderResult = result.Order(policy, topCount);
            return orderResult;
        }

        public List<TopPrecise4GContainer> GetTopCountStats(DateTime begin, DateTime end, int topCount,
            OrderPreciseStatPolicy policy, IEnumerable<ENodeb> eNodebs)
        {    ;
            var districtList =
                from q in
                    _repository.GetAllList(
                        x => x.StatTime >= begin && x.StatTime < end && x.TotalMrs > Settings.Default.TotalMrsThreshold)
                join e in eNodebs on q.CellId equals e.ENodebId
                select q;
            var result = districtList.GenerateContainers();
            
            var orderResult = result.Order(policy, topCount);
            return orderResult;
        }

        public Precise4GView GetOneWeekStats(int cellId, byte sectorId, DateTime date)
        {
            var begin = date.AddDays(-7);
            var end = date;
            var stats = GetTimeSpanStats(cellId, sectorId, begin, end).ToArray();
            var sumStat = new PreciseCoverage4G
            {
                CellId = cellId,
                SectorId = sectorId,
                FirstNeighbors = stats.Sum(q => q.FirstNeighbors),
                SecondNeighbors = stats.Sum(q => q.SecondNeighbors),
                ThirdNeighbors = stats.Sum(q => q.ThirdNeighbors),
                TotalMrs = stats.Sum(q => q.TotalMrs)
            };
            return Precise4GView.ConstructView(sumStat, _eNodebRepository);
        }

        public IEnumerable<PreciseCoverage4G> GetTimeSpanStats(int cellId, byte sectorId, DateTime begin, DateTime end)
        {
            return _repository.GetAllList(cellId, sectorId, begin, end).OrderBy(x => x.StatTime);
        }
        
        public IEnumerable<PreciseCoverage4G> GetTimeSpanStats(DateTime begin, DateTime end)
        {
            return _repository.GetAllList(x => x.StatTime >= begin && x.StatTime < end);
        }
    }
}
