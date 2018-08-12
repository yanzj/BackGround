using Abp.Domain.Repositories;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Maintainence;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Excel;

namespace Lte.MySqlFramework.Abstract
{
    public interface IAlarmWorkItemRepository : IRepository<AlarmWorkItem>, ISaveChanges,
        IMatchRepository<AlarmWorkItem, AlarmWorkItemExcel>
    {
    }
}
