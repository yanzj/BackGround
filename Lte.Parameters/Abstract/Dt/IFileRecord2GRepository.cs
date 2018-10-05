using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.EntityFramework.Repositories;
using Lte.Parameters.Entities.Dt;

namespace Lte.Parameters.Abstract.Dt
{
    public interface IFileRecord2GRepository : IRepository<FileRecord2G>, ISaveChanges, IMatchRepository<FileRecord2G>
    {
    }
}
