using Abp.EntityFramework.Dependency;
using Abp.EntityFramework.Repositories;
using Lte.Parameters.Abstract.Kpi;
using Lte.Parameters.Entities.Kpi;
using MongoDB.Bson;
using System;
using System.Collections.Generic;

namespace Lte.Parameters.Concrete.Kpi
{
    public class MrsPhrRepository : MongoDbRepositoryBase<MrsPhrStat, ObjectId>, IMrsPhrRepository
    {
        public MrsPhrRepository(IMongoDatabaseProvider databaseProvider) : base(databaseProvider)
        {
            CollectionName = "mrs_PowerHeadRoom_combined";
        }

        public MrsPhrRepository() : this(new MyMongoProvider("ouyh"))
        {

        }

        public MrsPhrStat Get(string cellId, DateTime statDate)
        {
            return this.QueryLastDate(cellId, statDate);
        }

        public IEnumerable<MrsPhrStat> GetList(string cellId, DateTime begin, DateTime end)
        {
            return this.QueryLastDate(cellId, begin, end);
        }
    }
}
