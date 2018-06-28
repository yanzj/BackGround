using Abp.Domain.Entities;
using Abp.EntityFramework.Dependency;
using MongoDB.Bson;

namespace Lte.Parameters.Entities.Neighbor
{
    public class EutranInterFreqNCell : IEntity<ObjectId>, IHuaweiNeighborMongo
    {
        public bool IsTransient()
        {
            return false;
        }

        public ObjectId Id { get; set; }

        public string iDate { get; set; }

        public int eNodeB_Id { get; set; }

        public string eNodeBId_Name { get; set; }

        public int LocalCellId { get; set; }

        public int Mnc { get; set; }

        public int NoRmvFlag { get; set; }

        public int Mcc { get; set; }

        public int CellMeasPriority { get; set; }

        public int CtrlMode { get; set; }

        public int eNodeBId { get; set; }

        public int CellId { get; set; }

        public int AnrFlag { get; set; }

        public int BlindHoPriority { get; set; }

        public int NCellClassLabel { get; set; }

        public int CellQoffset { get; set; }

        public int CellIndividualOffset { get; set; }

        public int OverlapRange { get; set; }

        public string LocalCellName { get; set; }

        public int OverlapInd { get; set; }

        public int NoHoFlag { get; set; }

        public string NeighbourCellName { get; set; }

        public int? AggregationProperty { get; set; }

        public int? OverlapIndicatorExtension { get; set; }
    }
}