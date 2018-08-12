using System.Collections.Generic;
using Abp.Domain.Repositories;
using Abp.EntityFramework.Entities.Complain;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Excel;

namespace Lte.MySqlFramework.Abstract.Complain
{
    public interface IVipProcessRepository
        : IRepository<VipProcess>,
            IMatchRepository<VipProcess, VipProcessDto>,
            IMatchRepository<VipProcess, VipDemandExcel>,
            ISaveChanges
    {
        List<VipProcess> GetAllList(string serialNumber);
    }
}