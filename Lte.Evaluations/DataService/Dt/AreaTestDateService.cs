using System;
using System.Collections.Generic;
using System.Linq;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Test;
using Lte.MySqlFramework.Abstract;
using Lte.MySqlFramework.Abstract.Region;
using Lte.MySqlFramework.Abstract.Test;
using Lte.MySqlFramework.Entities;

namespace Lte.Evaluations.DataService.Dt
{
    public class AreaTestDateService
    {
        private readonly IAreaTestDateRepository _repository;
        private readonly ITownRepository _townRepository;

        public AreaTestDateService(IAreaTestDateRepository repository, ITownRepository townRepository)
        {
            _repository = repository;
            _townRepository = townRepository;
        }

        public IEnumerable<AreaTestDateView> QueryAllList()
        {
            return
                _repository.GetAllList()
                    .Select(x => x.ConstructAreaView<AreaTestDate, AreaTestDateView>(_townRepository));
        }

        public int UpdateLastDate(DateTime testDate, string networkType, int townId)
        {
            var town = _townRepository.Get(townId);
            if (town == null) return 0;
            var item = _repository.FirstOrDefault(x => x.Area == town.TownName);
            if (item == null) return 0;
            switch (networkType)
            {
                case "2G":
                    if (testDate > item.LatestDate2G) item.LatestDate2G = testDate;
                    break;
                case "3G":
                    if (testDate > item.LatestDate3G) item.LatestDate3G = testDate;
                    break;
                case "4G":
                    if (testDate > item.LatestDate4G) item.LatestDate4G = testDate;
                    break;
                default:
                    if (testDate > item.LatestDateVolte) item.LatestDateVolte = testDate;
                    break;
            }
            return _repository.SaveChanges();
        }
    }
}
