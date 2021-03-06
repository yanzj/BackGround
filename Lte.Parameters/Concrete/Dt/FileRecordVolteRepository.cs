﻿using System;
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
    public class FileRecordVolteRepository : EfRepositorySave<MasterTestContext, FileRecordVolte>, IFileRecordVolteRepository
    {
        public FileRecordVolteRepository(IDbContextProvider<MasterTestContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public FileRecordVolte Match(FileRecordVolte stat)
        {
            return FirstOrDefault(x => x.FileId == stat.FileId && x.StatTime == stat.StatTime);
        }
    }
}
