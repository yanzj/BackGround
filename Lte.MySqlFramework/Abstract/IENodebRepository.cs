using System.Collections.Generic;
using Abp.Domain.Repositories;
using Abp.EntityFramework.Entities;
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