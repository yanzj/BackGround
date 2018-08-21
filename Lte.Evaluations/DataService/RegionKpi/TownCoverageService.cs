using System;
using Abp.EntityFramework.Entities.RegionKpi;
using Abp.EntityFramework.Entities.Test;
using Lte.Evaluations.DataService.Kpi;
using Lte.MySqlFramework.Abstract.Infrastructure;
using Lte.MySqlFramework.Abstract.RegionKpi;
using Lte.MySqlFramework.Abstract.Test;

namespace Lte.Evaluations.DataService.RegionKpi
{
    public class TownCoverageService
    {
        private readonly IENodebRepository _eNodebRepository;
        private readonly ICoverageStatRepository _statRepository;
        private readonly ITownCoverageRepository _townRepository;

        public TownCoverageService(IENodebRepository eNodebRepository, ICoverageStatRepository statRepository,
            ITownCoverageRepository townRepository)
        {
            _eNodebRepository = eNodebRepository;
            _statRepository = statRepository;
            _townRepository = townRepository;
        }

        public int GenerateTownStats(DateTime statDate)
        {
            var end = statDate.AddDays(1);
            var count = _townRepository.Count(x => x.StatDate >= statDate && x.StatDate < end);
            if (count == 0)
            {
                var townStatList = _statRepository.GetAllList(x => x.StatDate >= statDate && x.StatDate < end)
                    .GetTownStats<CoverageStat, TownCoverageStat>(_eNodebRepository);
                foreach (var townStat in townStatList.GetDateMergeStats(statDate))
                {
                    _townRepository.Insert(townStat);
                }
                count = _townRepository.SaveChanges();
            }
            return count;
        }
    }
}
