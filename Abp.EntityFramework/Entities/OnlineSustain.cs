using System;
using Abp.Domain.Entities;
using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Excel;

namespace Abp.EntityFramework.Entities
{
    [AutoMapFrom(typeof(OnlineSustainExcel), typeof(OnlineSustainDto))]
    public class OnlineSustain: Entity, ITownId
    {
        public DateTime BeginDate { get; set; }

        public int TownId { get; set; }

        public string ContactPhone { get; set; }
        
        public int StaffId { get; set; }
        
        public string Phenomenon { get; set; }

        public string SerialNumber { get; set; }

        public string DutyStaff { get; set; }

        public string ComplainNumber { get; set; }

        [AutoMapPropertyResolve("ComplainCategoryDescription", typeof(OnlineSustainExcel), typeof(ComplainCategoryTransform))]
        [AutoMapPropertyResolve("ComplainCategoryDescription", typeof(OnlineSustainDto), typeof(ComplainCategoryTransform))]
        public ComplainCategory ComplainCategory { get; set; }

        [AutoMapPropertyResolve("FirstReasonClass", typeof(OnlineSustainExcel), typeof(ComplainSourceTransform))]
        [AutoMapPropertyResolve("ComplainSourceDescription", typeof(OnlineSustainDto), typeof(ComplainSourceTransform))]
        public ComplainSource ComplainSource { get; set; }

        [AutoMapPropertyResolve("SecondReasonClass", typeof(OnlineSustainExcel), typeof(ComplainReasonTransform))]
        [AutoMapPropertyResolve("ComplainReasonDescription", typeof(OnlineSustainDto), typeof(ComplainReasonTransform))]
        public ComplainReason ComplainReason { get; set; }

        [AutoMapPropertyResolve("ThirdReasonClass", typeof(OnlineSustainExcel), typeof(ComplainSubReasonTransform))]
        [AutoMapPropertyResolve("ComplainSubReasonDescription", typeof(OnlineSustainDto), typeof(ComplainSubReasonTransform))]
        public ComplainSubReason ComplainSubReason { get; set; }

        public string Address { get; set; }

        public string Issue { get; set; }

        public string SpecialResponse { get; set; }
        
        [AutoMapPropertyResolve("IsPreProcessedDescription", typeof(OnlineSustainDto), typeof(YesToBoolTransform))]
        public bool IsPreProcessed { get; set; }

        public string WorkItemNumber { get; set; }

        public double Longtitute { get; set; }

        public double Lattitute { get; set; }
        
        public string FeedbackInfo { get; set; }

        public string Site { get; set; }
    }
}
