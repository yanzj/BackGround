using System.Collections.Generic;
using Abp.Domain.Repositories;
using Lte.Parameters.Entities.Neighbor;
using MongoDB.Bson;

namespace Lte.Parameters.Abstract.Infrastructure
{
    public interface IEutranInterFreqNCellRepository : IRepository<EutranInterFreqNCell, ObjectId>
    {
        List<EutranInterFreqNCell> GetRecentList(int eNodebId, byte localCellId);

        List<EutranInterFreqNCell> GetReverseList(int destENodebId, byte destSectorId);

        List<EutranInterFreqNCell> GetAllReverseList(int destENodebId, byte destSectorId);
    }
}