using Abp.Domain.Repositories;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Excel;

namespace Lte.MySqlFramework.Abstract
{
    public interface IENodebBaseRepository : IRepository<ENodebBase>, ISaveChanges,
        IMatchRepository<ENodebBase, ENodebBaseExcel>
    {
    }
}