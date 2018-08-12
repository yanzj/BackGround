using System.Collections.Generic;
using Abp.Domain.Repositories;
using Abp.EntityFramework.Entities.Infrastructure;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Excel;

namespace Lte.MySqlFramework.Abstract.Infrastructure
{
    public interface ICellRepository : IRepository<Cell>, IMatchRepository<Cell, CellExcel>, ISaveChanges
    {
        Cell GetBySectorId(int eNodebId, byte sectorId);

        List<Cell> GetAllList(int eNodebId);

        List<Cell> GetAllList(double west, double east, double south, double north);

        List<Cell> GetAllInUseList();
    }
}