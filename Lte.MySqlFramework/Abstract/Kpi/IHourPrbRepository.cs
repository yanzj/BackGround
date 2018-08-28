using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.EntityFramework.Entities.Kpi;
using Abp.EntityFramework.Repositories;

namespace Lte.MySqlFramework.Abstract.Kpi
{
    public interface IHourPrbRepository : IRepository<HourPrb>, ISaveChanges, IMatchRepository<HourPrb>,
        IFilterTopRepository<HourPrb>
    {
    }
}
