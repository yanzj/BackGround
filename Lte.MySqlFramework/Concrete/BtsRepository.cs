using System.Collections.Generic;
using System.Linq;
using Abp.EntityFramework;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Repositories;
using Lte.MySqlFramework.Abstract;

namespace Lte.MySqlFramework.Concrete
{
    public class BtsRepository : EfRepositorySave<MySqlContext, CdmaBts>, IBtsRepository
    {
        public CdmaBts GetByBtsId(int btsId)
        {
            return FirstOrDefault(x => x.BtsId == btsId);
        }

        public CdmaBts GetByName(string name)
        {
            return FirstOrDefault(x => x.Name == name);
        }

        public List<CdmaBts> GetAllInUseList()
        {
            return GetAll().Where(x => x.IsInUse).ToList();
        }

        public List<CdmaBts> GetAllList(double west, double east, double south, double north)
        {
            return GetAllList(x => x.Longtitute >= west && x.Longtitute <= east
                                   && x.Lattitute >= south && x.Lattitute <= north);
        }
        
        public BtsRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}