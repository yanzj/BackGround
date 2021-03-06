using System.Collections.Generic;
using Abp.EntityFramework;
using Abp.EntityFramework.Entities.Maintainence;
using Abp.EntityFramework.Repositories;
using Lte.MySqlFramework.Abstract.Maintainence;

namespace Lte.MySqlFramework.Concrete.Maintainence
{
    public class PreciseWorkItemCellRepositroy : EfRepositorySave<MySqlContext, PreciseWorkItemCell>, IPreciseWorkItemCellRepository
    {
        public PreciseWorkItemCellRepositroy(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public List<PreciseWorkItemCell> GetAllList(string serialNumber)
        {
            return GetAllList(x => x.WorkItemNumber == serialNumber);
        }

        public PreciseWorkItemCell Get(string serialNumber, int eNodebId, byte sectorId)
        {
            return
                FirstOrDefault(x => x.WorkItemNumber == serialNumber && x.ENodebId == eNodebId && x.SectorId == sectorId);
        }
    }
}