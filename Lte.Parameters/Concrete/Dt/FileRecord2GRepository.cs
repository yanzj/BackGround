using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;
using Lte.Parameters.Abstract.Dt;
using Lte.Parameters.Entities.Dt;

namespace Lte.Parameters.Concrete.Dt
{
    public class FileRecord2GRepository : EfRepositorySave<MasterTestContext, FileRecord2G>, IFileRecord2GRepository
    {
        public FileRecord2GRepository(IDbContextProvider<MasterTestContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public FileRecord2G Match(FileRecord2G stat)
        {
            return FirstOrDefault(x => x.FileId == stat.FileId && x.StatTime == stat.StatTime);
        }
    }
}
