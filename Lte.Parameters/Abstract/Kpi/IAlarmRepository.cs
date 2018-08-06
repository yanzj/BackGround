using System;
using System.Collections.Generic;
using Abp.Domain.Repositories;
using Lte.Parameters.Entities.Kpi;

namespace Lte.Parameters.Abstract.Kpi
{
    public interface IAlarmRepository : IRepository<AlarmStat>
    {
        List<AlarmStat> GetAllList(DateTime begin, DateTime end);

        List<AlarmStat> GetAllList(DateTime begin, DateTime end, int eNodebId);

        List<AlarmStat> GetAllList(DateTime begin, DateTime end, int eNodebId, byte localCellId);

        int Count(DateTime begin, DateTime end, int eNodebId);

        int SaveChanges();
    }
}