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
    public class FileRecord3GRepository : EfRepositorySave<MasterTestContext, FileRecord3G>, IFileRecord3GRepository
    {
        public FileRecord3GRepository(IDbContextProvider<MasterTestContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public FileRecord3G Match(FileRecord3G stat)
        {
            return FirstOrDefault(x => x.FileId == stat.FileId && x.StatTime == stat.StatTime);
        }
    }
}
