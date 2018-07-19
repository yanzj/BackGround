using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Common.Types;

namespace Lte.Parameters.Entities.Switch
{
    [AutoMapFrom(typeof(UeEUtranMeasurementZte), typeof(InterFreqHoGroup))]
    public class InterFreqEventA5 : IHoEventView
    {
        [AutoMapPropertyResolve("hysteresis", typeof(UeEUtranMeasurementZte))]
        [AutoMapPropertyResolve("InterFreqHoA4Hyst", typeof(InterFreqHoGroup))]
        public int Hysteresis { get; set; }

        [AutoMapPropertyResolve("timeToTrigger", typeof(UeEUtranMeasurementZte))]
        [AutoMapPropertyResolve("InterFreqHoA4TimeToTrig", typeof(InterFreqHoGroup))]
        public int TimeToTrigger { get; set; }

        [AutoMapPropertyResolve("thresholdOfRSRP", typeof(UeEUtranMeasurementZte))]
        [AutoMapPropertyResolve("InterFreqHoA4ThdRsrp", typeof(InterFreqHoGroup))]
        public int ThresholdOfRsrp { get; set; }

        [AutoMapPropertyResolve("thresholdOfRSRQ", typeof(UeEUtranMeasurementZte))]
        [AutoMapPropertyResolve("InterFreqHoA4ThdRsrq", typeof(InterFreqHoGroup))]
        public int ThresholdOfRsrq { get; set; }

        [AutoMapPropertyResolve("a5Threshold2OfRSRP", typeof(UeEUtranMeasurementZte))]
        [AutoMapPropertyResolve("InterFreqHoA5Thd1Rsrp", typeof(InterFreqHoGroup))]
        public int Threshold2OfRsrp { get; set; }

        [AutoMapPropertyResolve("a5Threshold2OfRSRQ", typeof(UeEUtranMeasurementZte))]
        [AutoMapPropertyResolve("InterFreqHoA5Thd1Rsrq", typeof(InterFreqHoGroup))]
        public int Threshold2OfRsrq { get; set; }
    }
}