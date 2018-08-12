using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Cdma;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Excel;

namespace Lte.MySqlFramework.Abstract
{
    public interface ICdmaRegionStatRepository : IRepository<CdmaRegionStat>,
        IMatchRepository<CdmaRegionStat, CdmaRegionStatExcel>, ISaveChanges
    {
        List<CdmaRegionStat> GetAllList(DateTime begin, DateTime end);

        Task<List<CdmaRegionStat>> GetAllListAsync(DateTime begin, DateTime end);
    }
}