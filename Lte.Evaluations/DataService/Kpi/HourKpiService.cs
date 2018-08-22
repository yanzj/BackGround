using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities.Kpi;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Common.Wireless.Kpi;
using Lte.MySqlFramework.Abstract.Kpi;
using Lte.MySqlFramework.Abstract.RegionKpi;
using Lte.Parameters.Entities.Kpi;

namespace Lte.Evaluations.DataService.Kpi
{
    public class HourKpiService
    {
        private readonly IHourPrbRepository _prbRepository;
        private readonly ITownHourPrbRepository _townPrbRepository;
        private readonly IHourUsersRepository _usersRepository;
        private readonly ITownHourUsersRepository _townUsersRepository;

        public HourKpiService(IHourPrbRepository prbRepository, ITownHourPrbRepository townPrbRepository,
            IHourUsersRepository usersRepository, ITownHourUsersRepository townUsersRepository)
        {
            _prbRepository = prbRepository;
            _townPrbRepository = townPrbRepository;
            _usersRepository = usersRepository;
            _townUsersRepository = townUsersRepository;
        }


        public async Task<IEnumerable<HourKpiHistory>> GetHourHistories(DateTime begin, DateTime end)
        {
            var results = new List<HourKpiHistory>();
            while (begin < end.AddDays(1))
            {
                var beginDate = begin;
                var endDate = begin.AddDays(1);
                var prbItems =
                    await _prbRepository.CountAsync(x => x.StatTime >= beginDate && x.StatTime < endDate);
                var townPrbs =
                    await _townPrbRepository.CountAsync(x => x.StatDate >= beginDate && x.StatDate < endDate);
                var usersItems =
                    await _usersRepository.CountAsync(x => x.StatTime >= beginDate && x.StatTime < endDate);
                var townUserses =
                    await _townUsersRepository.CountAsync(x => x.StatDate >= beginDate && x.StatDate < endDate);
                results.Add(new HourKpiHistory
                {
                    DateString = begin.ToShortDateString(),
                    PrbItems = prbItems,
                    TownPrbs = townPrbs,
                    UsersItems = usersItems,
                    TownUserses = townUserses
                });
                begin = begin.AddDays(1);
            }
            return results;
        }

    }
}
