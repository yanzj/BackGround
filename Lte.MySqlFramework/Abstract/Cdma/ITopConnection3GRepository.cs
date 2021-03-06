using System;
using System.Collections.Generic;
using Abp.Domain.Repositories;
using Abp.EntityFramework.Entities.Cdma;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Excel;

namespace Lte.MySqlFramework.Abstract.Cdma
{
    public interface ITopConnection3GRepository : IRepository<TopConnection3GCell>,
        IMatchRepository<TopConnection3GCell, TopConnection3GCellExcel>, ISaveChanges
    {
        List<TopConnection3GCell> GetAllList(string city, DateTime begin, DateTime end);
    }
}