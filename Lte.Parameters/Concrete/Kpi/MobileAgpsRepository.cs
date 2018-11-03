using System;
using System.Collections.Generic;
using Abp.EntityFramework.Dependency;
using Abp.EntityFramework.Entities.Channel;
using Abp.EntityFramework.Repositories;
using Lte.Parameters.Abstract.Kpi;
using MongoDB.Bson;

namespace Lte.Parameters.Concrete.Kpi
{
    public class MobileAgpsRepository : MongoDbRepositoryBase<AgpsMongo, ObjectId>, IMobileAgpsRepository
    {
        public MobileAgpsRepository(IMongoDatabaseProvider databaseProvider) : base(databaseProvider)
        {
            CollectionName = "position_mobile_combined";
        }

        public MobileAgpsRepository() : this(new MyMongoProvider("ouyh"))
        {

        }

        public AgpsMongo Get(string cellId, DateTime statDate)
        {
            return this.QueryLastDate(cellId, statDate);
        }

        public IEnumerable<AgpsMongo> GetList(string cellId, DateTime begin, DateTime end)
        {
            return this.QueryLastDate(cellId, begin, end);
        }
    }
}