using Abp.Domain.Repositories;
using Abp.EntityFramework.Entities.College;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Common.Wireless.Region;

namespace Lte.MySqlFramework.Abstract.College
{
    public interface ICollegeRepository : IRepository<CollegeInfo>, ISaveChanges
    {
        CollegeRegion GetRegion(int id);

        CollegeInfo GetByName(string name);

        RectangleRange GetRange(string name);

    }
}
