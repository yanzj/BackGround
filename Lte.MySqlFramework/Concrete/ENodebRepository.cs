using System.Collections.Generic;
using System.Linq;
using Abp.EntityFramework;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Infrastructure;
using Abp.EntityFramework.Repositories;
using Lte.MySqlFramework.Abstract;

namespace Lte.MySqlFramework.Concrete
{
    public class ENodebRepository : EfRepositorySave<MySqlContext, ENodeb>, IENodebRepository
    {
        public ENodeb GetByName(string name)
        {
            return FirstOrDefault(x => x.Name == name);
        }

        public List<ENodeb> GetAllInUseList()
        {
            return GetAll().Where(x => x.IsInUse).ToList();
        }

        public ENodebRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}