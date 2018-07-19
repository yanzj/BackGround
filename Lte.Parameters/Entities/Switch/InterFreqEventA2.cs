using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Common.Types;

namespace Lte.Parameters.Entities.Switch
{
    [AutoMapFrom(typeof(UeEUtranMeasurementZte), typeof(InterFreqHoGroup))]
    public class InterFreqEventA2 : IHoEventView
    {
        [AutoMapPropertyResolve("hysteresis", typeof(UeEUtranMeasurementZte))]
        [AutoMapPropertyResolve("InterFreqHoA1A2Hyst", typeof(InterFreqHoGroup))]
        public int Hysteresis { get; set; }

        [AutoMapPropertyResolve("timeToTrigger", typeof(UeEUtranMeasurementZte))]
        [AutoMapPropertyResolve("InterFreqHoA1A2TimeToTrig", typeof(InterFreqHoGroup))]
        public int TimeToTrigger { get; set; }

        [AutoMapPropertyResolve("thresholdOfRSRP", typeof(UeEUtranMeasurementZte))]
        public int ThresholdOfRsrp { get; set; }

        [AutoMapPropertyResolve("thresholdOfRSRQ", typeof(UeEUtranMeasurementZte))]
        public int ThresholdOfRsrq { get; set; }
    }
}