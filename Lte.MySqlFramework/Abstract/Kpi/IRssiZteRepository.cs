using Abp.Domain.Repositories;
using Abp.EntityFramework.Entities.Kpi;
using Abp.EntityFramework.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lte.MySqlFramework.Abstract.Kpi
{
    public interface IRssiZteRepository : IRepository<RssiZte>, ISaveChanges, IMatchRepository<RssiZte>
    {
    }
}
