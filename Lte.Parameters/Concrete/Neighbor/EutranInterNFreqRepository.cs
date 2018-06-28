using Abp.EntityFramework.Dependency;
using Abp.EntityFramework.Repositories;
using Lte.Parameters.Entities.Neighbor;
using MongoDB.Bson;
using System.Collections.Generic;
using Lte.Parameters.Abstract;
using Lte.Parameters.Abstract.Infrastructure;

namespace Lte.Parameters.Concrete.Neighbor
{
    public class EutranInterNFreqRepository : MongoDbRepositoryBase<EutranInterNFreq, ObjectId>, IEutranInterNFreqRepository
    {
        public EutranInterNFreqRepository(IMongoDatabaseProvider databaseProvider) : base(databaseProvider)
        {
            CollectionName = "EutranInterNFreq";
        }

        public EutranInterNFreqRepository() : this(new MyMongoProvider("fangww"))
        {

        }

        public List<EutranInterNFreq> GetRecentList(int eNodebId)
        {
            return this.QueryHuaweiRecentList(eNodebId);
        }

        public List<EutranInterNFreq> GetRecentList(int eNodebId, int localCellId)
        {
            return this.QueryRecentList(eNodebId, localCellId);
        }
    }
}
