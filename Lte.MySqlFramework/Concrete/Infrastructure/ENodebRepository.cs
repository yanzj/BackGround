using System.Collections.Generic;
using System.Linq;
using Abp.EntityFramework;
using Abp.EntityFramework.Entities.Infrastructure;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Excel;
using Lte.MySqlFramework.Abstract.Infrastructure;

namespace Lte.MySqlFramework.Concrete.Infrastructure
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

        public ENodeb Match(ENodebBaseExcel stat)
        {
            return FirstOrDefault(x => x.ENodebId == stat.ENodebId);
        }
    }
}