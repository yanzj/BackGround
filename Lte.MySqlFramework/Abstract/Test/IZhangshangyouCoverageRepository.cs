using Abp.Domain.Repositories;
using Abp.EntityFramework.Entities.Test;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Common;

namespace Lte.MySqlFramework.Abstract.Test
{
    public interface IZhangshangyouCoverageRepository : IRepository<ZhangshangyouCoverage>, ISaveChanges,
        IMatchRepository<ZhangshangyouCoverage, ZhangshangyouCoverageCsv>
    {
    }
}
