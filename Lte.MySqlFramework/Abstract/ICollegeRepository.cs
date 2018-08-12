using Abp.Domain.Repositories;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.College;
using Abp.EntityFramework.Repositories;
using Lte.MySqlFramework.Entities;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Common.Wireless.Region;

namespace Lte.MySqlFramework.Abstract
{
    public interface ICollegeRepository : IRepository<CollegeInfo>, ISaveChanges
    {
        CollegeRegion GetRegion(int id);

        CollegeInfo GetByName(string name);

        RectangleRange GetRange(string name);

    }
}
