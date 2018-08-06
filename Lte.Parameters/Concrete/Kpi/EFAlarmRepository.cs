using System;
using System.Collections.Generic;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;
using Lte.Parameters.Abstract.Kpi;
using Lte.Parameters.Entities.Kpi;

namespace Lte.Parameters.Concrete.Kpi
{
    public class EFAlarmRepository : EfRepositorySave<EFParametersContext, AlarmStat>, IAlarmRepository
    {
        public List<AlarmStat> GetAllList(DateTime begin, DateTime end)
        {
            return GetAllList(x => x.HappenTime >= begin && x.HappenTime < end);
        }

        public List<AlarmStat> GetAllList(DateTime begin, DateTime end, int eNodebId)
        {
            return GetAllList(x => x.HappenTime >= begin && x.HappenTime < end && x.ENodebId == eNodebId);
        }

        public List<AlarmStat> GetAllList(DateTime begin, DateTime end, int eNodebId, byte sectorId)
        {
            return
                GetAllList(
                    x => x.HappenTime >= begin && x.HappenTime < end && x.ENodebId == eNodebId && x.SectorId == sectorId);
        }

        public int Count(DateTime begin, DateTime end, int eNodebId)
        {
            return Count(x => x.HappenTime >= begin && x.HappenTime < end && x.ENodebId == eNodebId);
        }
        
        public EFAlarmRepository(IDbContextProvider<EFParametersContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}