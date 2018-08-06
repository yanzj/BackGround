using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework.Dependency;
using Abp.EntityFramework.Entities;
using Lte.MySqlFramework.Abstract;
using Lte.MySqlFramework.Entities;
using Lte.MySqlFramework.Support;

namespace Lte.Evaluations.DataService.Kpi
{
    public class DoubleFlowRegionStatService
    {
        private readonly ITownDoubleFlowRepository _statRepository;
        private readonly ITownRepository _townRepository;

        public DoubleFlowRegionStatService(ITownDoubleFlowRepository statRepository, ITownRepository townRepository)
        {
            _statRepository = statRepository;
            _townRepository = townRepository;
        }

        public DoubleFlowRegionDateView QueryLastDateStat(DateTime initialDate, string city)
        {
            var stats = _statRepository.QueryLastDate(initialDate, (repository, beginDate, endDate) =>
            {
                var query =
                    _statRepository.GetAllList(x => x.StatTime >= beginDate & x.StatTime < endDate);
                return query.FilterTownList(_townRepository.GetAllList().Where(x => x.CityName == city).ToList());
            });
            var townViews = stats.ConstructViews<TownDoubleFlow, TownDoubleFlowView>(_townRepository);
            return townViews.QueryRegionDateView<DoubleFlowRegionDateView, DistrictDoubleFlowView, TownDoubleFlowView>(initialDate,
                DistrictDoubleFlowView.ConstructView);
        }

        public IEnumerable<DoubleFlowRegionDateView> QueryDateViews(DateTime begin, DateTime end, string city)
        {
            var query = _statRepository.GetAllList(x => x.StatTime >= begin & x.StatTime < end);
            var townViews = query.QueryTownStat<TownDoubleFlow, TownDoubleFlowView>(_townRepository, city);
            return
                townViews.QueryDateSpanViews<DoubleFlowRegionDateView, DistrictDoubleFlowView, TownDoubleFlowView>(
                    DistrictDoubleFlowView.ConstructView);
        }
    }
}
