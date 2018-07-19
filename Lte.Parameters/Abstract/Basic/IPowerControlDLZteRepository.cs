using System.Collections.Generic;
using Abp.Domain.Repositories;
using Lte.Parameters.Entities.Channel;
using MongoDB.Bson;

namespace Lte.Parameters.Abstract.Basic
{
    public interface IPowerControlDLZteRepository : IRepository<PowerControlDLZte, ObjectId>
    {
        List<PowerControlDLZte> GetRecentList(int eNodebId);

        PowerControlDLZte GetRecent(int eNodebId, byte sectorId);
    }
}