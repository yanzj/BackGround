using System.Collections.Generic;
using Abp.Domain.Repositories;
using Abp.EntityFramework.Entities;

namespace Lte.MySqlFramework.Abstract
{
    public interface INearestPciCellRepository : IRepository<NearestPciCell>
    {
        List<NearestPciCell> GetAllList(int cellId, byte sectorId);

        NearestPciCell GetNearestPciCell(int cellId, byte sectorId, short pci);

        int SaveChanges();
    }
}