using System.Collections.Generic;
using Abp.Domain.Repositories;
using Abp.EntityFramework.Entities.Maintainence;
using Abp.EntityFramework.Repositories;

namespace Lte.MySqlFramework.Abstract.Maintainence
{
    public interface IPreciseWorkItemCellRepository : IRepository<PreciseWorkItemCell>, ISaveChanges
    {
        List<PreciseWorkItemCell> GetAllList(string serialNumber);

        PreciseWorkItemCell Get(string serialNumber, int eNodebId, byte sectorId);
    }
}