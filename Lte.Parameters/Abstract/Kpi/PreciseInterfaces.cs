using Abp.Domain.Repositories;
using Lte.Parameters.Entities.Kpi;
using System;
using System.Collections.Generic;
using Abp.EntityFramework.Repositories;
using MongoDB.Bson;

namespace Lte.Parameters.Abstract.Kpi
{
    public interface IPreciseCoverage4GRepository : IRepository<PreciseCoverage4G>, ISaveChanges
    {
        List<PreciseCoverage4G> GetAllList(int cellId, byte sectorId, DateTime begin, DateTime end);

        List<PreciseCoverage4G> GetAllList(DateTime begin, DateTime end);
    }

    public interface ITownPreciseCoverage4GStatRepository : IRepository<TownPreciseCoverage4GStat>, ISaveChanges
    {
    }

    public interface IPreciseMongoRepository : IRepository<PreciseMongo, ObjectId>
    {
        List<PreciseMongo> GetAllList(DateTime statDate);
    }

    public interface IAlarmRepository : IRepository<AlarmStat>
    {
        List<AlarmStat> GetAllList(DateTime begin, DateTime end);

        List<AlarmStat> GetAllList(DateTime begin, DateTime end, int eNodebId);

        List<AlarmStat> GetAllList(DateTime begin, DateTime end, int eNodebId, byte localCellId);

        int Count(DateTime begin, DateTime end, int eNodebId);

        int SaveChanges();
    }

}
