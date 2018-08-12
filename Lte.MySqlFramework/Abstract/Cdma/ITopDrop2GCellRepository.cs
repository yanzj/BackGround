using System;
using System.Collections.Generic;
using Abp.Domain.Repositories;
using Abp.EntityFramework.Entities.Cdma;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Excel;

namespace Lte.MySqlFramework.Abstract.Cdma
{
    public interface ITopDrop2GCellRepository : IRepository<TopDrop2GCell>,
        IMatchRepository<TopDrop2GCell, TopDrop2GCellExcel>, ISaveChanges
    {
        List<TopDrop2GCell> GetAllList(string city, DateTime begin, DateTime end);
    }
}