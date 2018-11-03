using System;
using System.Collections.Generic;
using Abp.EntityFramework.Dependency;
using Abp.EntityFramework.Entities.Channel;
using Abp.EntityFramework.Repositories;
using Lte.Parameters.Abstract.Kpi;
using MongoDB.Bson;

namespace Lte.Parameters.Concrete.Kpi
{
    public class TelecomAgpsRepository : MongoDbRepositoryBase<AgpsMongo, ObjectId>, ITelecomAgpsRepository
    {
        public TelecomAgpsRepository(IMongoDatabaseProvider databaseProvider) : base(databaseProvider)
        {
            CollectionName = "position_telecom_combined";
        }

        public TelecomAgpsRepository() : this(new MyMongoProvider("ouyh"))
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