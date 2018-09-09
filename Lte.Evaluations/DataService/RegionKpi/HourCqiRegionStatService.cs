﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework.Dependency;
using Abp.EntityFramework.Entities.RegionKpi;
using Lte.MySqlFramework.Abstract.Region;
using Lte.MySqlFramework.Abstract.RegionKpi;
using Lte.MySqlFramework.Entities.RegionKpi;
using Lte.MySqlFramework.Support.View;

namespace Lte.Evaluations.DataService.RegionKpi
{
    public class HourCqiRegionStatService
    {
        private readonly ITownHourCqiRepository _statRepository;
        private readonly ITownRepository _townRepository;

        public HourCqiRegionStatService(ITownHourCqiRepository statRepository, ITownRepository townRepository)
        {
            _statRepository = statRepository;
            _townRepository = townRepository;
        }

        public HourCqiRegionDateView QueryLastDateStat(DateTime initialDate, string city)
        {
            var stats = _statRepository.QueryDate(initialDate, (repository, beginDate, endDate) =>
            {
                var query =
                    _statRepository.GetAllList(x => x.StatDate >= beginDate & x.StatDate < endDate);
                return query.FilterTownList(_townRepository.GetAllList().Where(x => x.CityName == city).ToList());
            });
            var townViews = stats.ConstructViews<TownHourCqi, TownHourCqiView>(_townRepository);
            return townViews.QueryRegionDateDateView<HourCqiRegionDateView, DistrictHourCqiView, TownHourCqiView>(initialDate,
                DistrictHourCqiView.ConstructView);
        }

        public IEnumerable<HourCqiRegionDateView> QueryDateViews(DateTime begin, DateTime end, string city)
        {
            var query = _statRepository.GetAllList(x => x.StatDate >= begin & x.StatDate < end);
            var townViews = query.QueryTownStat<TownHourCqi, TownHourCqiView>(_townRepository, city);
            return
                townViews.QueryDateDateViews<HourCqiRegionDateView, DistrictHourCqiView, TownHourCqiView>(
                    DistrictHourCqiView.ConstructView);
        }
    }
}