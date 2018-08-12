using System.Collections.Generic;
using Abp.EntityFramework;
using Abp.EntityFramework.Entities.Complain;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Excel;
using Lte.MySqlFramework.Abstract.Complain;

namespace Lte.MySqlFramework.Concrete.Complain
{
    public class ComplainProcessRepository : EfRepositorySave<MySqlContext, ComplainProcess>, IComplainProcessRepository
    {
        public ComplainProcessRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public ComplainProcess Match(OnlineSustainExcel stat)
        {
            return FirstOrDefault(x => x.SerialNumber == stat.SerialNumber);
        }
        
        public List<ComplainProcess> GetAllList(string serialNumber)
        {
            return GetAllList(x => x.SerialNumber == serialNumber);
        }
    }
}