using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Excel;
using Lte.MySqlFramework.Abstract;

namespace Lte.MySqlFramework.Concrete
{
    public class CheckingDetailsRepository : EfRepositorySave<MySqlContext, CheckingDetails>, ICheckingDetailsRepository
    {
        public CheckingDetailsRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public CheckingDetails Match(CheckingDetailsExcel stat, string key)
        {
            return FirstOrDefault(x => x.CheckingFlowNumber == stat.CheckingFlowNumber && x.CheckingTheme == key);
        }
    }
}
