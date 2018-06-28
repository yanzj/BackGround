using Abp.Domain.Repositories;
using Lte.Parameters.Entities.Switch;
using MongoDB.Bson;

namespace Lte.Parameters.Abstract.Switch
{
    public interface IInterFreqHoGroupRepository : IRepository<InterFreqHoGroup, ObjectId>
    {
        InterFreqHoGroup GetRecent(int eNodebId, int localCellId);
    }
}
