using Abp.Domain.Entities;
using Abp.EntityFramework.Dependency;
using MongoDB.Bson;

namespace Lte.Parameters.Entities.Switch
{
    public class IntraFreqHoGroup : IEntity<ObjectId>, IHuaweiCellMongo
    {
        public ObjectId Id { get; set; }

        public bool IsTransient()
        {
            return false;
        }

        public int IntraFreqHoA3Hyst { get; set; }

        public int IntraFreqHoA3TimeToTrig { get; set; }

        public int LocalCellId { get; set; }

        public int IntraFreqHoA3Offset { get; set; }

        public string iDate { get; set; }

        public int eNodeB_Id { get; set; }

        public string eNodeBId_Name { get; set; }

        public int IntraFreqHoGroupId { get; set; }

        public int? HighSpeedA3TimeToTrig { get; set; }

        public int? IntraFreqHoA2ThldRsrpCE { get; set; }
    }
}