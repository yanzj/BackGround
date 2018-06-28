using System.Collections.Generic;
using Abp.Domain.Repositories;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Excel;
using Lte.MySqlFramework.Entities;

namespace Lte.MySqlFramework.Abstract
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