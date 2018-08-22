using Abp.EntityFramework.Entities.Kpi;
using Abp.EntityFramework.Entities.RegionKpi;
using Lte.MySqlFramework.Abstract.Infrastructure;
using Lte.MySqlFramework.Abstract.Kpi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lte.Evaluations.DataService.RegionKpi
{
    public class TownHourUsersService
    {
        private readonly IENodebRepository _eNodebRepository;
        private readonly IHourUsersRepository _usersRepository;

        public TownHourUsersService(IENodebRepository eNodebRepository, IHourUsersRepository usersRepository)
        {
            _eNodebRepository = eNodebRepository;
            _usersRepository = usersRepository;
        }

        public List<TownHourUsers> GetTownUsersStats(DateTime statDate)
        {
            var end = statDate.AddDays(1);
            var userses = _usersRepository.GetAllList(x => x.StatTime >= statDate && x.StatTime < end);
            var townStatList = userses.GetTownDateStats<HourUsers, TownHourUsers>(_eNodebRepository);
            return townStatList.ToList();
        }

    }
}
