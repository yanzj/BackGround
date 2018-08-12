using System;
using System.Collections.Generic;
using Abp.Domain.Repositories;
using Abp.EntityFramework.Entities.Cdma;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Excel;

namespace Lte.MySqlFramework.Abstract.Cdma
{
    public interface ITopConnection2GRepository : IRepository<TopConnection2GCell>,
        IMatchRepository<TopConnection2GCell, TopConnection2GExcel>, ISaveChanges
    {
        List<TopConnection2GCell> GetAllList(string city, DateTime begin, DateTime end);
    }
}