using Abp.Domain.Repositories;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Excel;
using Lte.MySqlFramework.Entities;

namespace Lte.MySqlFramework.Abstract
{
    public interface IVipDemandRepository 
        : IRepository<VipDemand>, 
            IMatchRepository<VipDemand, VipDemandExcel>, 
            IMatchRepository<VipDemand, VipDemandDto>,
            IDateSpanQuery<VipDemand>,
            ISaveChanges
    {
    }
}