using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;
using EntityFramework.Extensions;
using Lte.Parameters.Abstract;
using Lte.Parameters.Entities.Kpi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lte.Parameters.Abstract.Infrastructure;

namespace Lte.Parameters.Concrete.Kpi
{
    public class EFInterferenceMatrixRepository : EfRepositoryBase<EFParametersContext, InterferenceMatrixStat>, IInterferenceMatrixRepository
    {
        public EFInterferenceMatrixRepository(IDbContextProvider<EFParametersContext> dbContextProvider) : base(dbContextProvider)
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
