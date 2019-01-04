using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework;
using Abp.EntityFramework.Entities.Maintainence;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Excel;
using Lte.MySqlFramework.Abstract.Maintainence;

namespace Lte.MySqlFramework.Concrete.Maintainence
{
    public class CheckingProjectProvinceRepository : EfRepositorySave<MySqlContext, CheckingProjectProvince>, 
        ICheckingProjectProvinceRepository
    {
        public CheckingProjectProvinceRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public CheckingProjectProvince Match(CheckingProjectProvinceExcel stat)
        {
            return FirstOrDefault(x => x.WorkItemNumber == stat.WorkItemNumber);
        }
    }
}
