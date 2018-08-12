using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.EntityFramework.Entities.Maintainence;
using Abp.EntityFramework.Repositories;

namespace Lte.MySqlFramework.Abstract.Maintainence
{
    public interface IWorkItemRepository : IPagingRepository<WorkItem>, ISaveChanges
    {
        Task<List<WorkItem>> GetAllListAsync(int eNodebId, byte sectorId);

        Task<List<WorkItem>> GetAllListAsync(int eNodebId);

        Task<List<WorkItem>> GetUnfinishedPreciseListAsync(DateTime begin, DateTime end);

        Task<WorkItem> GetPreciseExistedAsync(int eNodebId, byte sectorId);
    }
}