using Abp.Domain.Repositories;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Kpi;
using Abp.EntityFramework.Entities.Mr;
using Abp.EntityFramework.Repositories;

namespace Lte.MySqlFramework.Abstract
{
    public interface ITopMrsRsrpRepository : IRepository<TopMrsRsrp>, ISaveChanges, IMatchRepository<TopMrsRsrp>
    {
        
    }
}