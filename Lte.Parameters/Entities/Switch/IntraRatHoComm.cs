using System;
using Abp.Domain.Entities;
using Abp.EntityFramework.Dependency;
using Lte.Domain.Common;
using Lte.Domain.Regular;
using MongoDB.Bson;

namespace Lte.Parameters.Entities.Switch
{
    public class IntraRatHoComm : IEntity<ObjectId>, IHuaweiMongo
    {
        public ObjectId Id { get; set; }

        public bool IsTransient()
        {
            return false;
        }

        public string iDate { get; set; }

        public DateTime? UpdateDate => iDate.GetDateFromFileName();

        public int eNodeB_Id { get; set; }

        public string eNodeBId_Name { get; set; }

        public int IntraFreqHoRprtInterval { get; set; }

        public int IntraRatHoRprtAmount { get; set; }

        public int FreqPriInterFreqHoA1TrigQuan { get; set; }

        public int IntraRatHoMaxRprtCell { get; set; }

        public int InterFreqHoA4RprtQuan { get; set; }

        public int InterFreqHoA4TrigQuan { get; set; }

        public int CovBasedIfHoWaitingTimer { get; set; }

        public int IntraFreqHoA3TrigQuan { get; set; }

        public int InterFreqHoA1A2TrigQuan { get; set; }

        public int objId { get; set; }

        public int InterFreqHoRprtInterval { get; set; }

        public int A3InterFreqHoA1A2TrigQuan { get; set; }

        public int IntraFreqHoA3RprtQuan { get; set; }

        public int? FreqPriHoCandidateUeSelPer { get; set; }
    }
}
