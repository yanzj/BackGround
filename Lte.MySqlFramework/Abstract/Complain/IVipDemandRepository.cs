using Abp.Domain.Repositories;
using Abp.EntityFramework.Entities.Complain;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Excel;
using Lte.MySqlFramework.Entities;

namespace Lte.MySqlFramework.Abstract.Complain
{
    public interface IVipDemandRepository 
        : IRepository<VipDemand>, 
            IMatchRepository<VipDemand, VipDemandExcel>, 
            IMatchRepository<VipDemand, VipDemandDto>,
            IDateSpanRepository<VipDemand>,
            ISaveChanges
    {
    }
}