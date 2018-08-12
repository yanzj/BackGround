using System;
using System.Collections.Generic;
using Abp.Domain.Repositories;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Kpi;
using Abp.EntityFramework.Repositories;

namespace Lte.MySqlFramework.Abstract
{
    public interface ICqiZteRepository : IRepository<CqiZte>, ISaveChanges, IMatchRepository<CqiZte>
    {
        List<CqiZte> FilterTopList(DateTime begin, DateTime end);
    }
}