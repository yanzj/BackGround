using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Common.Types;

namespace Lte.Parameters.Entities.Switch
{
    [AutoMapFrom(typeof(IntraFreqHoGroup), typeof(UeEUtranMeasurementZte))]
    public class CellIntraFreqHoView : IHoEventView
    {
        [AutoMapPropertyResolve("eNodeB_Id", typeof(IntraFreqHoGroup))]
        [AutoMapPropertyResolve("eNodeB_Id", typeof(UeEUtranMeasurementZte))]
        public int ENodebId { get; set; }

        public int SectorId { get; set; }

        [AutoMapPropertyResolve("IntraFreqHoA3Hyst", typeof(IntraFreqHoGroup))]
        [AutoMapPropertyResolve("hysteresis", typeof(UeEUtranMeasurementZte), typeof(DoubleTransform))]
        public int Hysteresis { get; set; }

        [AutoMapPropertyResolve("IntraFreqHoA3TimeToTrig", typeof(IntraFreqHoGroup))]
        [AutoMapPropertyResolve("timeToTrigger", typeof(UeEUtranMeasurementZte))]
        public int TimeToTrigger { get; set; }

        [AutoMapPropertyResolve("IntraFreqHoA3Offset", typeof(IntraFreqHoGroup))]
        [AutoMapPropertyResolve("a3Offset", typeof(UeEUtranMeasurementZte), typeof(DoubleTransform))]
        public int A3Offset { get; set; }
    }
}