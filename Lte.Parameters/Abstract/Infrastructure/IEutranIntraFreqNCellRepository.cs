using System.Collections.Generic;
using Abp.Domain.Repositories;
using Lte.Parameters.Entities.Neighbor;
using MongoDB.Bson;

namespace Lte.Parameters.Abstract.Infrastructure
{
    public interface IEutranIntraFreqNCellRepository : IRepository<EutranIntraFreqNCell, ObjectId>
    {
        List<EutranIntraFreqNCell> GetRecentList(int eNodebId, byte localCellId);

        List<EutranIntraFreqNCell> GetReverseList(int destENodebId, byte destSectorId);

        List<EutranIntraFreqNCell> GetAllReverseList(int destENodebId, byte destSectorId);
    }
}