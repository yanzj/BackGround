using Abp.Domain.Entities;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Dependency;
using Lte.Domain.Common;
using Lte.Domain.Common.Types;
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

    [AutoMapFrom(typeof(IntraRatHoComm), typeof(UeEUtranMeasurementZte))]
    public class ENodebIntraFreqHoView
    {
        [AutoMapPropertyResolve("eNodeB_Id", typeof(IntraRatHoComm))]
        [AutoMapPropertyResolve("eNodeB_Id", typeof(UeEUtranMeasurementZte))]
        public int ENodebId { get; set; }

        [AutoMapPropertyResolve("IntraFreqHoRprtInterval", typeof(IntraRatHoComm))]
        [AutoMapPropertyResolve("reportInterval", typeof(UeEUtranMeasurementZte))]
        public int ReportInterval { get; set; }

        [AutoMapPropertyResolve("IntraRatHoRprtAmount", typeof(IntraRatHoComm))]
        [AutoMapPropertyResolve("reportAmount", typeof(UeEUtranMeasurementZte))]
        public int ReportAmount { get; set; }

        [AutoMapPropertyResolve("IntraRatHoMaxRprtCell", typeof(IntraRatHoComm))]
        [AutoMapPropertyResolve("maxReportCellNum", typeof(UeEUtranMeasurementZte))]
        public int MaxReportCellNum { get; set; }

        [AutoMapPropertyResolve("IntraFreqHoA3TrigQuan", typeof(IntraRatHoComm))]
        [AutoMapPropertyResolve("triggerQuantity", typeof(UeEUtranMeasurementZte))]
        public int TriggerQuantity { get; set; }

        [AutoMapPropertyResolve("IntraFreqHoA3RprtQuan", typeof(IntraRatHoComm))]
        [AutoMapPropertyResolve("reportQuantity", typeof(UeEUtranMeasurementZte))]
        public int ReportQuantity { get; set; }
    }

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

    [AutoMapFrom(typeof(UeEUtranMeasurementZte), typeof(InterFreqHoGroup))]
    public class InterFreqEventA1 : IHoEventView
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

    [AutoMapFrom(typeof(UeEUtranMeasurementZte), typeof(InterFreqHoGroup))]
    public class InterFreqEventA4 : IHoEventView
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
    }

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

    public class CellInterFreqHoView
    {
        public int Earfcn { get; set; }

        public int InterFreqHoEventType { get; set; }

        public InterFreqEventA1 InterFreqEventA1 { get; set; }

        public InterFreqEventA2 InterFreqEventA2 { get; set; }

        public InterFreqEventA3 InterFreqEventA3 { get; set; }

        public InterFreqEventA4 InterFreqEventA4 { get; set; }

        public InterFreqEventA5 InterFreqEventA5 { get; set; }
    }

    public interface IHoEventView
    {
        int Hysteresis { get; set; }

        int TimeToTrigger { get; set; }
    }

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

    public class InterRatHoComm : IEntity<ObjectId>, IHuaweiMongo
    {
        public ObjectId Id { get; set; }

        public bool IsTransient()
        {
            return false;
        }

        public string iDate { get; set; }

        public int eNodeB_Id { get; set; }

        public string eNodeBId_Name { get; set; }

        public int InterRatHoMaxRprtCell { get; set; }

        public int Cdma20001XrttMeasTimer { get; set; }

        public int Cdma2000HrpdFreqSelMode { get; set; }

        public int InterRatCdmaHrpdRprtInterval { get; set; }

        public int InterRatHoA1A2TrigQuan { get; set; }

        public int InterRatHoCdma1xRttB1MeasQuan { get; set; }

        public int InterRatHoCdmaHrpdB1MeasQuan { get; set; }

        public int InterRatHoRprtAmount { get; set; }

        public int UtranCellNumForEmcRedirect { get; set; }

        public int Cdma20001XrttMeasMode { get; set; }

        public int Cdma20001XrttJudgePnNum { get; set; }

        public int IRatBlindRedirPlmnCfgSimSw { get; set; }

        public int CdmaHrpdSectorIdSelMode { get; set; }

        public int CellInfoMaxUtranCellNum { get; set; }

        public int GeranCellNumForEmcRedirect { get; set; }

        public int InterRatCdma1xRttRprtInterval { get; set; }

        public int CdmaEcsfbPsConcurrentMode { get; set; }

        public int InterRatHoEventType { get; set; }

        public int InterRatHoGeranRprtInterval { get; set; }

        public int InterRatHoUtranB1MeasQuan { get; set; }

        public int Cdma20001XrttFreqSelMode { get; set; }

        public int CellInfoMaxGeranCellNum { get; set; }

        public int objId { get; set; }

        public int Cdma1XrttSectorIdSelMode { get; set; }

        public int InterRatHoUtranRprtInterval { get; set; }
    }
}
