using Abp.EntityFramework.Dependency;
using Abp.EntityFramework.Repositories;
using Lte.Parameters.Abstract.Kpi;
using Lte.Parameters.Entities.Kpi;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using Lte.Parameters.Entities.Channel;

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

    public class MrsRsrpRepository : MongoDbRepositoryBase<MrsRsrpStat, ObjectId>, IMrsRsrpRepository
    {
        public MrsRsrpRepository(IMongoDatabaseProvider databaseProvider) : base(databaseProvider)
        {
            CollectionName = "mrs_RSRP_combined";
        }

        public MrsRsrpRepository() : this(new MyMongoProvider("ouyh"))
        {

        }

        public MrsRsrpStat Get(string cellId, DateTime statDate)
        {
            return this.QueryLastDate(cellId, statDate);
        }

        public IEnumerable<MrsRsrpStat> GetList(string cellId, DateTime begin, DateTime end)
        {
            return this.QueryLastDate(cellId, begin, end);
        }
    }

    public class MrsSinrUlRepository : MongoDbRepositoryBase<MrsSinrUlStat, ObjectId>, IMrsSinrUlRepository
    {
        public MrsSinrUlRepository(IMongoDatabaseProvider databaseProvider) : base(databaseProvider)
        {
            CollectionName = "mrs_SinrUL_combined";
        }
        public MrsSinrUlRepository() : this(new MyMongoProvider("ouyh"))
        {

        }

        public MrsSinrUlStat Get(string cellId, DateTime statDate)
        {
            return this.QueryLastDate(cellId, statDate);
        }

        public IEnumerable<MrsSinrUlStat> GetList(string cellId, DateTime begin, DateTime end)
        {
            return this.QueryLastDate(cellId, begin, end);
        }
    }

    public class MrsTadvRsrpRepository : MongoDbRepositoryBase<MrsTadvRsrpStat, ObjectId>, IMrsTadvRsrpRepository
    {
        public MrsTadvRsrpRepository(IMongoDatabaseProvider databaseProvider) : base(databaseProvider)
        {
            CollectionName = "mrs_TadvRsrp_combined";
        }

        public MrsTadvRsrpRepository() : this(new MyMongoProvider("ouyh"))
        {

        }

        public MrsTadvRsrpStat Get(string cellId, DateTime statDate)
        {
            return this.QueryLastDate(cellId, statDate);
        }

        public IEnumerable<MrsTadvRsrpStat> GetList(string cellId, DateTime begin, DateTime end)
        {
            return this.QueryLastDate(cellId, begin, end);
        }
    }

    public class MrsTadvRepository : MongoDbRepositoryBase<MrsTadvStat, ObjectId>, IMrsTadvRepository
    {
        public MrsTadvRepository(IMongoDatabaseProvider databaseProvider) : base(databaseProvider)
        {
            CollectionName = "mrs_Tadv_combined";
        }

        public MrsTadvRepository() : this(new MyMongoProvider("ouyh"))
        {

        }

        public MrsTadvStat Get(string cellId, DateTime statDate)
        {
            return this.QueryLastDate(cellId, statDate);
        }

        public IEnumerable<MrsTadvStat> GetList(string cellId, DateTime begin, DateTime end)
        {
            return this.QueryLastDate(cellId, begin, end);
        }
    }

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

    public class UnicomAgpsRepository : MongoDbRepositoryBase<AgpsMongo, ObjectId>, IUnicomAgpsRepository
    {
        public UnicomAgpsRepository(IMongoDatabaseProvider databaseProvider) : base(databaseProvider)
        {
            CollectionName = "position_unicom_combined";
        }

        public UnicomAgpsRepository() : this(new MyMongoProvider("ouyh"))
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
