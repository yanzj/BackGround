using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.EntityFramework.Entities.RegionKpi;
using Abp.EntityFramework.Entities.Test;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Common.Wireless.Cell;
using Lte.Domain.Regular;
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
        private readonly ICellRepository _cellRepository;

        public TownCoverageService(IENodebRepository eNodebRepository, ICoverageStatRepository statRepository,
            ITownCoverageRepository townRepository, ICellRepository cellRepository)
        {
            _eNodebRepository = eNodebRepository;
            _statRepository = statRepository;
            _townRepository = townRepository;
            _cellRepository = cellRepository;
        }

        public async Task<int> GenerateTownStats(DateTime statDate, FrequencyBandType bandType)
        {
            var end = statDate.AddDays(1);
            var count = _townRepository.Count(x =>
                x.StatDate >= statDate && x.StatDate < end && x.FrequencyBandType == bandType);
            if (count != 0) return count;
            var townStatList = _statRepository.GetAllList(x => x.StatDate >= statDate && x.StatDate < end)
                .GetTownFrequencyQueryStats<CoverageStat, TownCoverageStat>(bandType, _cellRepository,
                    _eNodebRepository);
            var statList = townStatList.GetDateMergeStats(statDate).ToList();
            statList.ForEach(townStat =>
            {
                townStat.FrequencyBandType = bandType;
            });
            return await _townRepository.UpdateMany(statList);
        }

        public async Task<int> DumpTownStats(List<TownCoverageStat> stats)
        {
            await _townRepository.UpdateMany(stats);
            return stats.Count;
        }
        
        public List<TownCoverageStat> QueryTownViews(DateTime begin, DateTime end, int townId, FrequencyBandType frequency)
        {
            var query =
                _townRepository.GetAllList(
                        x =>
                            x.StatDate >= begin && x.StatDate < end && x.FrequencyBandType == frequency &&
                            x.TownId == townId)
                    .OrderBy(x => x.StatDate)
                    .ToList();
            return query;
        }

        public TownCoverageStat QueryOneDateBandStat(DateTime statDate, FrequencyBandType frequency)
        {
            var end = statDate.AddDays(1);
            var result = _townRepository
                .GetAllList(x => x.StatDate >= statDate && x.StatDate < end && x.FrequencyBandType == frequency);
            return result.Any() ? result.ArraySum() : null;
        }
    }
}
