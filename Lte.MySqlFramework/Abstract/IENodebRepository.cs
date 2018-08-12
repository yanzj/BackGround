using System.Collections.Generic;
using Abp.Domain.Repositories;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Infrastructure;
using Lte.MySqlFramework.Entities;

namespace Lte.MySqlFramework.Abstract
{
    public interface IENodebRepository : IRepository<ENodeb>
    {
        ENodeb GetByName(string name);

        List<ENodeb> GetAllInUseList();

        int SaveChanges();
    }
}