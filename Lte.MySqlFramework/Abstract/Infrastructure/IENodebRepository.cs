using System.Collections.Generic;
using Abp.Domain.Repositories;
using Abp.EntityFramework.Entities.Infrastructure;

namespace Lte.MySqlFramework.Abstract.Infrastructure
{
    public interface IENodebRepository : IRepository<ENodeb>
    {
        ENodeb GetByName(string name);

        List<ENodeb> GetAllInUseList();

        int SaveChanges();
    }
}