using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;

namespace Abp.EntityFramework.Repositories
{
    public interface IFilterTopRepository<TEntity>
        where TEntity : Entity
    {
        List<TEntity> FilterTopList(DateTime begin, DateTime end);
    }
}
