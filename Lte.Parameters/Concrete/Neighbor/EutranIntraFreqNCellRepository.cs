using Abp.EntityFramework.Dependency;
using Abp.EntityFramework.Repositories;
using Lte.Parameters.Entities.Neighbor;
using MongoDB.Bson;
using System.Collections.Generic;
using System.Linq;
using Lte.Parameters.Abstract;
using Lte.Parameters.Abstract.Infrastructure;

namespace Lte.Parameters.Concrete.Neighbor
{
    public class EutranIntraFreqNCellRepository : MongoDbRepositoryBase<EutranIntraFreqNCell, ObjectId>, 
        IEutranIntraFreqNCellRepository
    {
        public EutranIntraFreqNCellRepository(IMongoDatabaseProvider databaseProvider) : base(databaseProvider)
        {
            CollectionName = "EutranIntraFreqNCell";
        }

        public EutranIntraFreqNCellRepository() : this(new MyMongoProvider("fangww"))
        {

        }

        public List<EutranIntraFreqNCell> GetRecentList(int eNodebId, byte localCellId)
        {
            return this.QueryRecentList(eNodebId, localCellId);
        }

        public List<EutranIntraFreqNCell> GetReverseList(int destENodebId, byte destSectorId)
        {
            return this.QueryReverseList(destENodebId, destSectorId);
        }

        public List<EutranIntraFreqNCell> GetAllReverseList(int destENodebId, byte destSectorId)
        {
            var query =
                MongoDB.Driver.Builders.Query<EutranIntraFreqNCell>.Where(
                    e => e.eNodeBId == destENodebId && e.CellId == destSectorId);
            var list = Collection.Find(query).AsQueryable().ToList();
            return (from item in list
                group item by new
                {
                    item.eNodeB_Id,
                    item.LocalCellId,
                    item.eNodeBId,
                    item.CellId
                }
                into g
                select new EutranIntraFreqNCell
                {
                    eNodeB_Id = g.Key.eNodeB_Id,
                    LocalCellId = g.Key.LocalCellId,
                    eNodeBId = g.Key.eNodeBId,
                    CellId = g.Key.CellId,
                    NoHoFlag = g.First().NoHoFlag,
                    NoRmvFlag = g.First().NoRmvFlag,
                    AnrFlag = g.First().AnrFlag,
                    NCellClassLabel = g.First().NCellClassLabel,
                    AttachCellSwitch = g.First().AttachCellSwitch,
                    eNodeBId_Name = g.First().eNodeBId_Name,
                    CellRangeExpansion = g.First().CellRangeExpansion,
                    CtrlMode = g.First().CtrlMode,
                    CellMeasPriority = g.First().CellMeasPriority,
                    CellIndividualOffset = g.First().CellIndividualOffset,
                    CellQoffset = g.First().CellQoffset,
                    NeighbourCellName = g.First().NeighbourCellName,
                    LocalCellName = g.First().LocalCellName,
                    HighSpeedCellIndOffset = g.First().HighSpeedCellIndOffset,
                    VectorCellFlag = g.First().VectorCellFlag,
                    Mcc = g.First().Mcc,
                    Mnc = g.First().Mnc
                }).ToList();
        }
    }

    public class EutranInterFreqNCellRepository : MongoDbRepositoryBase<EutranInterFreqNCell, ObjectId>,
        IEutranInterFreqNCellRepository
    {
        public EutranInterFreqNCellRepository(IMongoDatabaseProvider databaseProvider) : base(databaseProvider)
        {
            CollectionName = "EutranInterFreqNCell";
        }

        public EutranInterFreqNCellRepository() : this(new MyMongoProvider("fangww"))
        {

        }

        public List<EutranInterFreqNCell> GetRecentList(int eNodebId, byte localCellId)
        {
            return this.QueryRecentList(eNodebId, localCellId);
        }

        public List<EutranInterFreqNCell> GetReverseList(int destENodebId, byte destSectorId)
        {
            return this.QueryReverseList(destENodebId, destSectorId);
        }

        public List<EutranInterFreqNCell> GetAllReverseList(int destENodebId, byte destSectorId)
        {
            var query =
                MongoDB.Driver.Builders.Query<EutranIntraFreqNCell>.Where(
                    e => e.eNodeBId == destENodebId && e.CellId == destSectorId);
            var list = Collection.Find(query).AsQueryable().ToList();
            return (from item in list
                    group item by new
                    {
                        item.eNodeB_Id,
                        item.LocalCellId,
                        item.eNodeBId,
                        item.CellId
                    }
                into g
                    select new EutranInterFreqNCell
                    {
                        eNodeB_Id = g.Key.eNodeB_Id,
                        LocalCellId = g.Key.LocalCellId,
                        eNodeBId = g.Key.eNodeBId,
                        CellId = g.Key.CellId,
                        NoHoFlag = g.First().NoHoFlag,
                        NoRmvFlag = g.First().NoRmvFlag,
                        AnrFlag = g.First().AnrFlag,
                        NCellClassLabel = g.First().NCellClassLabel,
                        BlindHoPriority = g.First().BlindHoPriority,
                        eNodeBId_Name = g.First().eNodeBId_Name,
                        CellMeasPriority = g.First().CellMeasPriority,
                        CellIndividualOffset = g.First().CellIndividualOffset,
                        CellQoffset = g.First().CellQoffset,
                        OverlapInd = g.First().OverlapInd,
                        OverlapRange = g.First().OverlapRange,
                        LocalCellName = g.First().LocalCellName,
                        Mnc = g.First().Mnc,
                        Mcc = g.First().Mcc,
                        CtrlMode = g.First().CtrlMode,
                        NeighbourCellName = g.First().NeighbourCellName
                    }).ToList();
        }
    }
}
