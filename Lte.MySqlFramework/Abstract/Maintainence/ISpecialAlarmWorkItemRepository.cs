using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.EntityFramework.Entities.Maintainence;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Excel;

namespace Lte.MySqlFramework.Abstract.Maintainence
{
    public interface ISpecialAlarmWorkItemRepository : IRepository<SpecialAlarmWorkItem>, ISaveChanges,
        IMatchRepository<SpecialAlarmWorkItem, SpecialAlarmWorkItemExcel>
    {
    }
}
