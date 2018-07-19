using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Common.Types;

namespace Lte.Parameters.Entities.Switch
{
    [AutoMapFrom(typeof(UeEUtranMeasurementZte), typeof(InterFreqHoGroup))]
    public class InterFreqEventA3 : IHoEventView
    {
        [AutoMapPropertyResolve("hysteresis", typeof(UeEUtranMeasurementZte))]
        public int Hysteresis { get; set; }

        [AutoMapPropertyResolve("timeToTrigger", typeof(UeEUtranMeasurementZte))]
        public int TimeToTrigger { get; set; }

        [AutoMapPropertyResolve("a3Offset", typeof(UeEUtranMeasurementZte))]
        [AutoMapPropertyResolve("InterFreqHoA3Offset", typeof(InterFreqHoGroup))]
        public int A3Offset { get; set; }
    }
}