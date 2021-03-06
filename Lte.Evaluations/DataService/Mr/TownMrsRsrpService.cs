﻿using System;
using System.Collections.Generic;
using System.Linq;
using Abp.EntityFramework.Entities.Mr;
using Lte.Domain.Common.Wireless.Cell;
using Lte.Domain.Regular;
using Lte.MySqlFramework.Abstract.Mr;

namespace Lte.Evaluations.DataService.Mr
{
    public class TownMrsRsrpService
    {
        private readonly ITownMrsRsrpRepository _repository;

        public TownMrsRsrpService(ITownMrsRsrpRepository repository)
        {
            _repository = repository;
        }
        
        public List<TownMrsRsrp> QueryTownViews(DateTime begin, DateTime end, int townId, FrequencyBandType frequency)
        {
            var query =
                _repository.GetAllList(
                        x =>
                            x.StatDate >= begin && x.StatDate < end && x.FrequencyBandType == frequency &&
                            x.TownId == townId)
                    .OrderBy(x => x.StatDate)
                    .ToList();
            return query;
        }

        public TownMrsRsrp QueryOneDateBandStat(DateTime statDate, FrequencyBandType frequency)
        {
            var end = statDate.AddDays(1);
            var result = _repository
                .GetAllList(x => x.StatDate >= statDate && x.StatDate < end && x.FrequencyBandType == frequency);
            return result.Any() ? result.ArraySum() : null;
        }
    }
}
