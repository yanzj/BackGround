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
    public class SpecialAlarmWorkItemRepository : EfRepositorySave<MySqlContext, SpecialAlarmWorkItem>,
        ISpecialAlarmWorkItemRepository
    {
        public SpecialAlarmWorkItemRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public SpecialAlarmWorkItem Match(SpecialAlarmWorkItemExcel stat)
        {
            return FirstOrDefault(x => x.AlarmNumber == stat.AlarmNumber);
        }
    }
}
