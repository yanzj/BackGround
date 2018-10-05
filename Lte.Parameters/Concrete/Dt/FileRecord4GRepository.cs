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
    public class FileRecord4GRepository : EfRepositorySave<MasterTestContext, FileRecord4G>, IFileRecord4GRepository
    {
        public FileRecord4GRepository(IDbContextProvider<MasterTestContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public FileRecord4G Match(FileRecord4G stat)
        {
            return FirstOrDefault(x => x.FileId == stat.FileId && x.StatTime == stat.StatTime);
        }
    }
}
