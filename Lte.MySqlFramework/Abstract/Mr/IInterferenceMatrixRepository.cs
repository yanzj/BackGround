using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.EntityFramework.Entities.Mr;

namespace Lte.MySqlFramework.Abstract.Mr
{
    public interface IInterferenceMatrixRepository : IRepository<InterferenceMatrixStat>
    {
        int SaveChanges();

        Task<int> UpdateItemsAsync(int eNodebId, byte sectorId, short destPci, int destENodebId, byte destSectorId);

        List<InterferenceMatrixStat> GetAllList(DateTime begin, DateTime end, int cellId, byte sectorId);

        List<InterferenceMatrixStat> GetAllVictims(DateTime begin, DateTime end, int cellId, byte sectorId);
    }
}