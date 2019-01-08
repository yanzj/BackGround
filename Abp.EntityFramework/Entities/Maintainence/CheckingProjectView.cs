using System;
using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless.Work;
using Lte.Domain.Regular.Attributes;

namespace Abp.EntityFramework.Entities.Maintainence
{
    [AutoMapFrom(typeof(CheckingProject))]
    public class CheckingProjectView
    {
        [MemberDoc("区县")]
        public string District { get; set; }

        [MemberDoc("站址编码")]
        public string StationSerialNumber { get; set; }

        [MemberDoc("维护单位")]
        public string Company { get; set; }

        [MemberDoc("基站维护人")]
        public string Maintainer { get; set; }

        [MemberDoc("巡检流水号")]
        public string CheckingFlowNumber { get; set; }

        [MemberDoc("计划开始日期")]
        public DateTime BeginDate { get; set; }

        [MemberDoc("计划结束日期")]
        public DateTime EndDate { get; set; }

        [MemberDoc("计划名称")]
        public string ProjectName { get; set; }

        [MemberDoc("归档时间")]
        public DateTime? FinalDate { get; set; }

        [MemberDoc("巡检单状态")]
        [AutoMapPropertyResolve("WorkItemState", typeof(CheckingProject), typeof(WorkItemStateDescriptionTransform))]
        public string WorkItemStateDescription { get; set; }

        [MemberDoc("合同站点名称")]
        public string StationName { get; set; }

        [MemberDoc("备注")]
        public string Comments { get; set; }
    }
}
