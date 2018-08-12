using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.EntityFramework;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Maintainence;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Common.Wireless.Work;
using Lte.MySqlFramework.Abstract;

namespace Lte.MySqlFramework.Concrete
{
    public class WorkItemRepository : PagingRepositoryBase<WorkItem>, IWorkItemRepository
    {
        public async Task<List<WorkItem>> GetAllListAsync(int eNodebId)
        {
            return await GetAllListAsync(x => x.ENodebId == eNodebId);
        }

        public async Task<List<WorkItem>> GetUnfinishedPreciseListAsync(DateTime begin, DateTime end)
        {
            return
                await
                    GetAllListAsync(
                        x => x.BeginTime >= begin && x.BeginTime < end && x.Subtype == WorkItemSubtype.PreciseRate
                             && x.State != WorkItemState.Finished);
        }

        public async Task<WorkItem> GetPreciseExistedAsync(int eNodebId, byte sectorId)
        {
            return
                await
                    FirstOrDefaultAsync(
                        x =>
                            x.ENodebId == eNodebId && x.SectorId == sectorId && x.Subtype == WorkItemSubtype.PreciseRate &&
                            x.State != WorkItemState.Finished);
        }

        public async Task<List<WorkItem>> GetAllListAsync(int eNodebId, byte sectorId)
        {
            return await GetAllListAsync(x => x.ENodebId == eNodebId && x.SectorId == sectorId);
        }

        public WorkItemRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}