using System;
using Abp.Domain.Entities;
using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Common;
using Lte.Domain.Common.Wireless;

namespace Abp.EntityFramework.Entities
{
    [AutoMapFrom(typeof(FlowHuaweiCsv))]
    public class RrcHuawei : Entity, ILocalCellQuery
    {
        public DateTime StatTime { get; set; }

        public int ENodebId { get; set; }

        public byte LocalCellId { get; set; }
        
        public int EmergencyRrcRequest { get; set; }
        
        public int EmergencyRrcRequestAll { get; set; }
        
        public int EmergencyRrcSuccess { get; set; }
        
        public int HighPriorityRrcRequest { get; set; }
        
        public int HighPriorityRrcRequestAll { get; set; }
        
        public int HighPriorityRrcSuccess { get; set; }
        
        public int MoDataRrcRequest { get; set; }
        
        public int MoDataRrcRequestAll { get; set; }
        
        public int MoDataRrcSuccess { get; set; }
        
        public int MoSignallingRrcRequest { get; set; }
        
        public int MoSignallingRrcRequestAll { get; set; }
        
        public int MoSignallingRrcSuccess { get; set; }
        
        public int MtAccessRrcRequest { get; set; }
        
        public int MtAccessRrcRequestAll { get; set; }
        
        public int MtAccessRrcSuccess { get; set; }
        
        public int RrcFailOtherResource { get; set; }
        
        public int RrcFailUserLimit { get; set; }
        
        public int RrcRejectFail { get; set; }
        
        public int RrcRejectOverload { get; set; }
        
        public int RrcReconstructionLostFlowControl { get; set; }
        
        public int RrcRequestLostFlowControl { get; set; }
        
        public int RrcFailResourceAssignment { get; set; }
        
        public int RrcFailUeNoAnswer { get; set; }
        
        public int RrcFailSrsAssignment { get; set; }
        
        public int RrcFailPucchAssignment { get; set; }
        
        public int RrcRejectFlowControl { get; set; }

        public int TotalRrcRequest
            =>
                MtAccessRrcRequest + MoSignallingRrcRequest + MoDataRrcRequest + HighPriorityRrcRequest +
                EmergencyRrcRequest;

        public int TotalRrcSuccess
            =>
                MtAccessRrcSuccess + MoSignallingRrcSuccess + MoDataRrcSuccess + HighPriorityRrcSuccess +
                EmergencyRrcSuccess;

        public int TotalRrcFail => TotalRrcRequest - TotalRrcSuccess;

        public int MtAccessRrcFail => MtAccessRrcRequest - MtAccessRrcSuccess;

        public int MoSignallingRrcFail => MoSignallingRrcRequest - MoSignallingRrcSuccess;

        public int MoDataRrcFail => MoDataRrcRequest - MoDataRrcSuccess;

        public int HighPriorityRrcFail => HighPriorityRrcRequest - HighPriorityRrcSuccess;

        public int EmergencyRrcFail => EmergencyRrcRequest - EmergencyRrcSuccess;
    }
}