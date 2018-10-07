﻿using System;
using System.Collections.Generic;
using System.Linq;
using Abp.EntityFramework.Dependency;
using Abp.EntityFramework.Entities.RegionKpi;
using Lte.Domain.Common.Wireless.Cell;
using Lte.MySqlFramework.Abstract.Region;
using Lte.MySqlFramework.Abstract.RegionKpi;
using Lte.MySqlFramework.Entities.Mr;
using Lte.MySqlFramework.Support.View;

namespace Lte.Evaluations.DataService.RegionKpi
{
    public class CoverageRegionStatService
    {
        private readonly ITownCoverageRepository _statRepository;
        private readonly ITownRepository _townRepository;

        public CoverageRegionStatService(ITownCoverageRepository statRepository, ITownRepository townRepository)
        {
            _statRepository = statRepository;
            _townRepository = townRepository;
        }

        public CoverageRegionDateView QueryLastDateStat(DateTime initialDate, string city)
        {
            var stats = _statRepository.QueryDate(initialDate, (repository, beginDate, endDate) =>
            {
                var query =
                    _statRepository.GetAllList(x =>
                        x.StatDate >= beginDate & x.StatDate < endDate && x.FrequencyBandType == FrequencyBandType.All);
                return query.FilterTownList(_townRepository.GetAllList().Where(x => x.CityName == city).ToList());
            });
            var townViews = stats.ConstructViews<TownCoverageStat, TownCoverageView>(_townRepository);
            return townViews.QueryRegionDateDateView<CoverageRegionDateView, DistrictCoverageView, TownCoverageView>(initialDate,
                DistrictCoverageView.ConstructView);
        }

        public IEnumerable<CoverageRegionDateView> QueryDateViews(DateTime begin, DateTime end, string city)
        {
            var query = _statRepository.GetAllList(x =>
                x.StatDate >= begin & x.StatDate < end && x.FrequencyBandType == FrequencyBandType.All);
            var townViews = query.QueryTownStat<TownCoverageStat, TownCoverageView>(_townRepository, city);
            return
                townViews.QueryDateDateViews<CoverageRegionDateView, DistrictCoverageView, TownCoverageView>(
                    DistrictCoverageView.ConstructView);
        }
    }
}
