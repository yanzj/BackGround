using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Maintainence;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Excel;
using Lte.MySqlFramework.Abstract;

namespace Lte.MySqlFramework.Concrete
{
    public class CheckingBasicRepository : EfRepositorySave<MySqlContext, CheckingBasic>, ICheckingBasicRepository
    {
        public CheckingBasicRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public CheckingBasic Match(CheckingBasicExcel stat)
        {
            return FirstOrDefault(x => x.CheckingFlowNumber == stat.CheckingFlowNumber);
        }
    }
}
