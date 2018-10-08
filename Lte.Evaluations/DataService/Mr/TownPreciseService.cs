using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework.Entities.Mr;
using Abp.EntityFramework.Entities.RegionKpi;
using Lte.Domain.Common.Wireless.Cell;
using Lte.Domain.Regular;
using Lte.MySqlFramework.Abstract.RegionKpi;

namespace Lte.Evaluations.DataService.Mr
{
    public class TownPreciseService
    {
        private readonly ITownPreciseCoverageRepository _repository;

        public TownPreciseService(ITownPreciseCoverageRepository repository)
        {
            _repository = repository;
        }
        
        public List<TownPreciseStat> QueryTownViews(DateTime begin, DateTime end, int townId, FrequencyBandType frequency)
        {
            var query =
                _repository.GetAllList(
                        x =>
                            x.StatTime >= begin && x.StatTime < end && x.FrequencyBandType == frequency &&
                            x.TownId == townId)
                    .OrderBy(x => x.StatTime)
                    .ToList();
            return query;
        }

        public TownPreciseStat QueryOneDateBandStat(DateTime statDate, FrequencyBandType frequency)
        {
            var end = statDate.AddDays(1);
            var result = _repository
                .GetAllList(x => x.StatTime >= statDate && x.StatTime < end && x.FrequencyBandType == frequency);
            return result.Any() ? result.ArraySum() : null;
        }
    }
}
