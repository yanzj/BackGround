using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Mr;
using Abp.EntityFramework.Repositories;

namespace Lte.MySqlFramework.Abstract
{
    public interface ITownMrsSinrUlRepository : IRepository<TownMrsSinrUl>, ISaveChanges, IMatchRepository<TownMrsSinrUl>
    {
    }
}
