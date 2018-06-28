using System;
using Abp.Domain.Entities;
using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Excel;

namespace Abp.EntityFramework.Entities
{
    [AutoMapFrom(typeof(OnlineSustainExcel))]
    public class ComplainProcess : Entity
    {
        public string SerialNumber { get; set; }
        
        [AutoMapPropertyResolve("AreaTypeDescription", typeof(OnlineSustainExcel), typeof(ComplainSceneTransform))]
        public ComplainScene ComplainScene { get; set; }

        [AutoMapPropertyResolve("ComplainStateDescription", typeof(OnlineSustainExcel), typeof(ComplainStateTransform))]
        public ComplainState ComplainState { get; set; }
        
        [AutoMapPropertyResolve("WorkItemCause", typeof(OnlineSustainExcel), typeof(ComplainReasonTransform))]
        public ComplainReason MainCause { get; set; }

        [AutoMapPropertyResolve("WorkItemSubCause", typeof(OnlineSustainExcel), typeof(ComplainSubReasonTransform))]
        public ComplainSubReason SubCause { get; set; }
        
        public string ProcessSuggestion { get; set; }

        public string WorkItemInfo { get; set; }

        public DateTime BeginDate { get; set; }

        public DateTime? ComplainTime { get; set; }

        public DateTime? ReceiveTime { get; set; }

        public DateTime? ProcessDate { get; set; }

        public short? ReceiveLevel { get; set; }
        
        public short? TransmitLevel { get; set; }
        
        public short? Pn { get; set; }
        
        public double? EcIo { get; set; }
        
        public string BtsName { get; set; }
        
        public int? BtsId { get; set; }

        public byte? CoverageLevel { get; set; }

        [AutoMapPropertyResolve("CoverageTypeDescription", typeof(OnlineSustainExcel), typeof(ComplainCategoryTransform))]
        public ComplainCategory ComplainCategory { get; set; }

        public string ProcessPerson { get; set; }

        public bool IsResolved { get; set; }

        public string ResolveScheme { get; set; }
        
        public string ResolveCauseDescription { get; set; }
        
        public string PlanSite { get; set; }
        
        public DateTime? ResolveDate { get; set; }
    }
}