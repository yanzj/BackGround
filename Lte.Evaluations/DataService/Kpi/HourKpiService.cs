using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lte.Domain.Common.Wireless.Cell;
using Lte.Domain.Regular;
using Lte.MySqlFramework.Abstract.Kpi;
using Lte.MySqlFramework.Abstract.RegionKpi;

namespace Lte.Evaluations.DataService.Kpi
{
    public class HourKpiService
    {
        private readonly IHourPrbRepository _prbRepository;
        private readonly ITownHourPrbRepository _townPrbRepository;
        private readonly IHourUsersRepository _usersRepository;
        private readonly ITownHourUsersRepository _townUsersRepository;
        private readonly IHourCqiRepository _cqiRepository;
        private readonly ITownHourCqiRepository _townCqiRepository;

        public HourKpiService(IHourPrbRepository prbRepository, ITownHourPrbRepository townPrbRepository,
            IHourUsersRepository usersRepository, ITownHourUsersRepository townUsersRepository,
            IHourCqiRepository cqiRepository, ITownHourCqiRepository townCqiRepository)
        {
            _prbRepository = prbRepository;
            _townPrbRepository = townPrbRepository;
            _usersRepository = usersRepository;
            _townUsersRepository = townUsersRepository;
            _cqiRepository = cqiRepository;
            _townCqiRepository = townCqiRepository;
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
                var cqiItems =
                    await _cqiRepository.CountAsync(x => x.StatTime >= beginDate && x.StatTime < endDate);
                var townCqis =
                    await _townCqiRepository.CountAsync(x => x.StatDate >= beginDate && x.StatDate < endDate && x.FrequencyBandType == FrequencyBandType.All);
                var townCqi800s =
                    await _townCqiRepository.CountAsync(x => x.StatDate >= beginDate && x.StatDate < endDate && x.FrequencyBandType == FrequencyBandType.Band800VoLte);
                var townCqi1800s =
                    await _townCqiRepository.CountAsync(x => x.StatDate >= beginDate && x.StatDate < endDate && x.FrequencyBandType == FrequencyBandType.Band1800);
                var townCqi2100s =
                    await _townCqiRepository.CountAsync(x => x.StatDate >= beginDate && x.StatDate < endDate && x.FrequencyBandType == FrequencyBandType.Band2100);
                results.Add(new HourKpiHistory
                {
                    DateString = begin.ToShortDateString(),
                    PrbItems = prbItems,
                    TownPrbs = townPrbs,
                    UsersItems = usersItems,
                    TownUserses = townUserses,
                    CqiItems = cqiItems,
                    TownCqis = townCqis,
                    TownCqi1800s = townCqi1800s,
                    TownCqi2100s = townCqi2100s,
                    TownCqi800s = townCqi800s
                });
                begin = begin.AddDays(1);
            }
            return results;
        }

    }
}
