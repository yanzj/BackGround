using System.Collections.Generic;
using Abp.Domain.Repositories;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Infrastructure;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Excel;

namespace Lte.MySqlFramework.Abstract
{
    public interface ICellRepository : IRepository<Cell>, IMatchRepository<Cell, CellExcel>, ISaveChanges
    {
        void AddCells(IEnumerable<Cell> cells);

        Cell GetBySectorId(int eNodebId, byte sectorId);

        Cell GetByFrequency(int eNodebId, int frequency);

        List<Cell> GetAllList(int eNodebId);

        List<Cell> GetAllList(double west, double east, double south, double north);

        List<Cell> GetAllInUseList();
    }
}