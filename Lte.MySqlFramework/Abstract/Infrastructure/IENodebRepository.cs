using System.Collections.Generic;
using Abp.Domain.Repositories;
using Abp.EntityFramework.Entities.Infrastructure;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Excel;

namespace Lte.MySqlFramework.Abstract.Infrastructure
{
    public interface IENodebRepository : IRepository<ENodeb>, IMatchRepository<ENodeb, ENodebBaseExcel>, ISaveChanges
    {
        ENodeb GetByName(string name);

        List<ENodeb> GetAllInUseList();
    }
}