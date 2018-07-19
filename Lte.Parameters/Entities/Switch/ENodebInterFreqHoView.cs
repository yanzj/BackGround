using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Common.Types;

namespace Lte.Parameters.Entities.Switch
{
    [AutoMapFrom(typeof(IntraRatHoComm))]
    public class ENodebInterFreqHoView
    {
        [AutoMapPropertyResolve("eNodeB_Id", typeof(IntraRatHoComm))]
        public int ENodebId { get; set; }

        public int InterFreqHoA4RprtQuan { get; set; }

        public int InterFreqHoA4TrigQuan { get; set; }

        [AutoMapPropertyResolve("InterFreqHoA1A2TrigQuan", typeof(IntraRatHoComm))]
        public int InterFreqHoA1TrigQuan { get; set; }

        [AutoMapPropertyResolve("InterFreqHoA1A2TrigQuan", typeof(IntraRatHoComm))]
        public int InterFreqHoA2TrigQuan { get; set; }

        public int InterFreqHoRprtInterval { get; set; }

        [AutoMapPropertyResolve("A3InterFreqHoA1A2TrigQuan", typeof(IntraRatHoComm))]
        public int A3InterFreqHoA1TrigQuan { get; set; }

        [AutoMapPropertyResolve("A3InterFreqHoA1A2TrigQuan", typeof(IntraRatHoComm))]
        public int A3InterFreqHoA2TrigQuan { get; set; }
    }
}