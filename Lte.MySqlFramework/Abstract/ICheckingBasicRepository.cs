using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Excel;

namespace Lte.MySqlFramework.Abstract
{
    public interface ICheckingBasicRepository : IRepository<CheckingBasic>, ISaveChanges, 
        IMatchRepository<CheckingBasic, CheckingBasicExcel>
    {
    }
}
