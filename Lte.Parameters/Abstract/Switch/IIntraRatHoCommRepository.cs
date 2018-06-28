using Abp.Domain.Repositories;
using Lte.Parameters.Entities.Switch;
using MongoDB.Bson;

namespace Lte.Parameters.Abstract.Switch
{
    public interface IRecent<out TStat>
    {
        TStat GetRecent(int eNodebId);
    }

    public interface IIntraRatHoCommRepository : IRepository<IntraRatHoComm, ObjectId>, IRecent<IntraRatHoComm>
    {
        
    }
}
