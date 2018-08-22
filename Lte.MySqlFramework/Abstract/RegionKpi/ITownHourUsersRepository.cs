using Abp.Domain.Repositories;
using Abp.EntityFramework.Entities.RegionKpi;
using Abp.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lte.MySqlFramework.Abstract.RegionKpi
{
    public interface ITownHourUsersRepository : IRepository<TownHourUsers>, ISaveChanges, IMatchRepository<TownHourUsers>
    {
    }
}
