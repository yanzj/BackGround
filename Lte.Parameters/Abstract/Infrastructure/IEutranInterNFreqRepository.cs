using System.Collections.Generic;
using Abp.Domain.Repositories;
using Lte.Parameters.Entities.Neighbor;
using MongoDB.Bson;

namespace Lte.Parameters.Abstract.Infrastructure
{
    public interface IEutranInterNFreqRepository : IRepository<EutranInterNFreq, ObjectId>
    {
        List<EutranInterNFreq> GetRecentList(int eNodebId);

        List<EutranInterNFreq> GetRecentList(int eNodebId, int localCellId);
    }
}