using System;
using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;

namespace Abp.EntityFramework.Entities
{
    [AutoMapFrom(typeof(OnlineSustain))]
    public class OnlineSustainDto : ICityDistrictTown
    {
        public DateTime BeginDate { get; set; }

        public int TownId { get; set; }

        public string City { get; set; }

        public string District { get; set; }

        public string Town { get; set; }

        public string ContactPhone { get; set; }

        public int StaffId { get; set; }

        public string Phenomenon { get; set; }

        public string SerialNumber { get; set; }

        public string DutyStaff { get; set; }

        [AutoMapPropertyResolve("ComplainCategory", typeof(OnlineSustain), typeof(ComplainCategoryDescriptionTransform))]
        public string ComplainCategoryDescription { get; set; }

        [AutoMapPropertyResolve("ComplainSource", typeof(OnlineSustain), typeof(ComplainSourceDescriptionTransform))]
        public string ComplainSourceDescription { get; set; }

        [AutoMapPropertyResolve("ComplainReason", typeof(OnlineSustain), typeof(ComplainReasonDescriptionTransform))]
        public string ComplainReasonDescription { get; set; }

        [AutoMapPropertyResolve("ComplainSubReason", typeof(OnlineSustain), typeof(ComplainSubReasonDescriptionTransform))]
        public string ComplainSubReasonDescription { get; set; }

        public string Address { get; set; }

        public string Issue { get; set; }

        public string SpecialResponse { get; set; }

        [AutoMapPropertyResolve("IsPreProcessed", typeof(OnlineSustain), typeof(YesNoTransform))]
        public string IsPreProcessedDescription { get; set; }

        public string WorkItemNumber { get; set; }

        public double Longtitute { get; set; }

        public double Lattitute { get; set; }

        public string FollowInfo { get; set; }

        public string FeedbackInfo { get; set; }

        public string Site { get; set; }
    }
}