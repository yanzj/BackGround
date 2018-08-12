using System.Collections.Generic;
using Abp.EntityFramework;
using Abp.EntityFramework.Entities.Complain;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Excel;
using Lte.MySqlFramework.Abstract.Complain;

namespace Lte.MySqlFramework.Concrete.Complain
{
    public class VipProcessRepository : EfRepositorySave<MySqlContext, VipProcess>, IVipProcessRepository
    {
        public VipProcessRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public VipProcess Match(VipProcessDto stat)
        {
            return FirstOrDefault(x => x.SerialNumber == stat.SerialNumber);
        }
        
        public List<VipProcess> GetAllList(string serialNumber)
        {
            return GetAllList(x => x.SerialNumber == serialNumber);
        }

        public VipProcess Match(VipDemandExcel stat)
        {
            return FirstOrDefault(x => x.SerialNumber == stat.SerialNumber);
        }
    }
}