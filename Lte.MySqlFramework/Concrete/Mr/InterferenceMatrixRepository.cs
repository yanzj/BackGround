using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.EntityFramework;
using Abp.EntityFramework.Entities.Mr;
using Abp.EntityFramework.Repositories;
using EntityFramework.Extensions;
using Lte.MySqlFramework.Abstract.Mr;

namespace Lte.MySqlFramework.Concrete.Mr
{
    public class InterferenceMatrixRepository : EfRepositoryBase<MySqlContext, InterferenceMatrixStat>, 
        IInterferenceMatrixRepository
    {
        public InterferenceMatrixRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public int SaveChanges()
        {
            return Context.SaveChanges();
        }

        public Task<int> UpdateItemsAsync(int eNodebId, byte sectorId, short destPci, int destENodebId, byte destSectorId)
        {
            return GetAll()
                .Where(x => x.CellId == eNodebId + "-" + sectorId && x.NeighborPci == destPci && x.DestENodebId == 0)
                .UpdateAsync(
                    t =>
                        new InterferenceMatrixStat
                        {
                            DestENodebId = destENodebId,
                            DestSectorId = destSectorId
                        });
        }

        public List<InterferenceMatrixStat> GetAllList(DateTime begin, DateTime end, int cellId, byte sectorId)
        {
            return
                GetAll()
                    .Where(
                        x =>
                            x.StatDate >= begin && x.StatDate < end && x.CellId == cellId + "-" + sectorId)
                    .ToList();
        }

        public List<InterferenceMatrixStat> GetAllVictims(DateTime begin, DateTime end, int cellId, byte sectorId)
        {
            return
                GetAll()
                    .Where(
                        x =>
                            x.StatDate >= begin && x.StatDate < end && x.DestENodebId == cellId &&
                            x.DestSectorId == sectorId)
                    .ToList();
        }
    }
}
