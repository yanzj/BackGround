using Abp.EntityFramework;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Maintainence;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Excel;
using Lte.MySqlFramework.Abstract;

namespace Lte.MySqlFramework.Concrete
{
    public class AlarmWorkItemRepository : EfRepositorySave<MySqlContext, AlarmWorkItem>,
        IAlarmWorkItemRepository
    {
        public AlarmWorkItemRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public AlarmWorkItem Match(AlarmWorkItemExcel stat)
        {
            return FirstOrDefault(x => x.SerialNumber == stat.SerialNumber);
        }
    }
}
