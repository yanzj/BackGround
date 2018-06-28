using Abp.EntityFramework.Dependency;
using Abp.EntityFramework.Repositories;
using Lte.Parameters.Abstract.Basic;
using Lte.Parameters.Entities.Channel;
using MongoDB.Bson;
using System.Collections.Generic;

namespace Lte.Parameters.Concrete.Channel
{
    public class CellDlpcPdschPaRepository : MongoDbRepositoryBase<CellDlpcPdschPa, ObjectId>, ICellDlpcPdschPaRepository
    {
        public CellDlpcPdschPaRepository(IMongoDatabaseProvider databaseProvider) : base(databaseProvider)
        {
            CollectionName = "CellDlpcPdschPa";
        }

        public CellDlpcPdschPaRepository() : this(new MyMongoProvider("fangww"))
        {

        }

        public CellDlpcPdschPa GetRecent(int eNodebId, int localCellId)
        {
            return this.QueryRecent(eNodebId, localCellId);
        }
    }

    public class CellUlpcCommRepository : MongoDbRepositoryBase<CellUlpcComm, ObjectId>, ICellUlpcCommRepository
    {
        public CellUlpcCommRepository(IMongoDatabaseProvider databaseProvider) : base(databaseProvider)
        {
            CollectionName = "CellUlpcComm";
        }

        public CellUlpcCommRepository() : this(new MyMongoProvider("fangww"))
        {

        }

        public CellUlpcComm GetRecent(int eNodebId, int localCellId)
        {
            return this.QueryRecent(eNodebId, localCellId);
        }
    }

    public class PowerControlDLZteRepository : MongoDbRepositoryBase<PowerControlDLZte, ObjectId>, IPowerControlDLZteRepository
    {
        public PowerControlDLZteRepository(IMongoDatabaseProvider databaseProvider) : base(databaseProvider)
        {
            CollectionName = "PowerControlDL";
        }

        public PowerControlDLZteRepository() : this(new MyMongoProvider("fangww"))
        {

        }

        public List<PowerControlDLZte> GetRecentList(int eNodebId)
        {
            return this.QueryRecentList(eNodebId);
        }

        public PowerControlDLZte GetRecent(int eNodebId, byte sectorId)
        {
            return this.QueryRecent(eNodebId, sectorId);
        }
    }

    public class PowerControlULZteRepository : MongoDbRepositoryBase<PowerControlULZte, ObjectId>,
        IPowerControlULZteRepository
    {
        public PowerControlULZteRepository(IMongoDatabaseProvider databaseProvider) : base(databaseProvider)
        {
            CollectionName = "PowerControlUL";
        }

        public PowerControlULZteRepository() : this(new MyMongoProvider("fangww"))
        {

        }

        public PowerControlULZte GetRecent(int eNodebId, byte sectorId)
        {
            return this.QueryRecent(eNodebId, sectorId);
        }
    }
}
