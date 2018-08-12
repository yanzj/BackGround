using System;
using Abp.Domain.Entities;
using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Common;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;

namespace Abp.EntityFramework.Entities.Kpi
{
    [AutoMapFrom(typeof(FlowZteCsv))]
    public class RrcZte : Entity, ILteCellQuery, IStatTime
    {
        public DateTime StatTime { get; set; }

        public int ENodebId { get; set; }

        public byte SectorId { get; set; }

        public int MtAccessRrcSuccess { get; set; }

        public int MtAccessRrcFailTimer { get; set; }

        public int MtAccessRrcFailAllow { get; set; }

        public int MtAccessRrcFailOther { get; set; }

        public int MoSignallingRrcSuccess { get; set; }

        public int MoSignallingRrcFailTimer { get; set; }

        public int MoSignallingRrcFailAllow { get; set; }

        public int MoSignallingRrcFailOther { get; set; }

        public int MoDataRrcSuccess { get; set; }

        public int MoDataRrcFailTimer { get; set; }

        public int MoDataRrcFailAllow { get; set; }

        public int MoDataRrcFailOther { get; set; }

        public int RrcReleaseUserInactive { get; set; }

        public int RrcReleaseMmeContext { get; set; }

        public int RrcReleaseResourceLack { get; set; }

        public int RrcReleaseCellReset { get; set; }

        public int RrcReleaseOther { get; set; }
        
        public int HighPriorityAccessRrcSuccess { get; set; }

        public int HighPriorityRrcSuccess => HighPriorityAccessRrcSuccess;


        public int HighPriorityAccessRrcFailTimer { get; set; }
        
        public int HighPriorityAccessRrcFailAllow { get; set; }
        
        public int HighPriorityAccessRrcFailOther { get; set; }
        
        public int EmergencyRrcSuccess { get; set; }
        
        public int EmergencyRrcFailTimer { get; set; }
        
        public int EmergencyRrcFailAllow { get; set; }
        
        public int EmergencyRrcFailOther { get; set; }

        [AutoMapPropertyResolve("RrcTotalDurationInMs", typeof(FlowZteCsv), typeof(ThousandTransform))]
        public int RrcTotalDuration { get; set; }

        [AutoMapPropertyResolve("RrcMaxDurationInMs", typeof(FlowZteCsv), typeof(ThousandTransform))]
        public int RrcMaxDuration { get; set; }
        
        public int RrcReleaseTimer { get; set; }
        
        public int RrcReleaseUeContextTimer { get; set; }
        
        public int RrcReleaseBadRsrp { get; set; }
        
        public int RrcReleaseRlcMaxRetransmit { get; set; }
        
        public int RrcReleasePdcpIntegrationFail { get; set; }

        public int RrcReleaseGptu { get; set; }

        public int RrcReleasePathMalfunction { get; set; }

        public int RrcReleaseFiber { get; set; }

        public int RrcReleaseUeExit { get; set; }

        public int RrcReleaseInterSiteReconstruction { get; set; }

        public int RrcReleaseRedirect { get; set; }

        public int RrcReleaseRadioLink { get; set; }

        public int RrcReleaseReconstructionFail { get; set; }

        public int RrcReleaseS1 { get; set; }

        public int RrcReleaseMmeOther { get; set; }

        public int RrcReleaseSwitchFail { get; set; }

        public int MtAccessRrcRequest { get; set; }

        public int MoSignallingRrcRequest { get; set; }

        public int MoDataRrcRequest { get; set; }

        public int HighPriorityAccessRrcRequest { get; set; }

        public int HighPriorityRrcRequest => HighPriorityAccessRrcRequest;

        public int EmergencyRrcRequest { get; set; }

        public int TotalRrcRequest
            =>
                MtAccessRrcRequest + MoSignallingRrcRequest + MoDataRrcRequest + HighPriorityAccessRrcRequest +
                EmergencyRrcRequest;

        public int TotalRrcSuccess
            =>
                MtAccessRrcSuccess + MoSignallingRrcSuccess + MoDataRrcSuccess + HighPriorityAccessRrcSuccess +
                EmergencyRrcSuccess;

        public int RrcFailTimer
            =>
                MtAccessRrcFailTimer + MoSignallingRrcFailTimer + MoDataRrcFailTimer + HighPriorityAccessRrcFailTimer +
                EmergencyRrcFailTimer;

        public int RrcFailAllow
            =>
                MtAccessRrcFailAllow + MoSignallingRrcFailAllow + MoDataRrcFailAllow + HighPriorityAccessRrcFailAllow +
                EmergencyRrcFailAllow;

        public int RrcFailOther
            =>
                MtAccessRrcFailOther + MoSignallingRrcFailOther + MoDataRrcFailOther + HighPriorityAccessRrcFailOther +
                EmergencyRrcFailOther;

        public int MtAccessRrcFail => MtAccessRrcFailAllow + MtAccessRrcFailOther + MtAccessRrcFailTimer;

        public int MoSignallingRrcFail => MoSignallingRrcFailAllow + MoSignallingRrcFailOther + MoSignallingRrcFailTimer;

        public int MoDataRrcFail => MoDataRrcFailAllow + MoDataRrcFailOther + MoDataRrcFailTimer;

        public int HighPriorityRrcFail
            => HighPriorityAccessRrcFailAllow + HighPriorityAccessRrcFailOther + HighPriorityAccessRrcFailTimer;

        public int EmergencyRrcFail => EmergencyRrcFailAllow + EmergencyRrcFailOther + EmergencyRrcFailTimer;

        public int TotalRrcFail => RrcFailTimer + RrcFailAllow + RrcFailOther;

    }
}