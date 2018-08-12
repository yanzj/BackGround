using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Regular.Attributes;
using Lte.MySqlFramework.Entities;

namespace Lte.MySqlFramework.Support.View
{
    [AutoMapFrom(typeof(WorkItemView))]
    public class WorkItemChartView
    {
        [MemberDoc("工单类型")]
        public string WorkItemType { get; set; }

        [MemberDoc("工单子类型")]
        public string WorkItemSubType { get; set; }

        [MemberDoc("区域")]
        public string District { get; set; }

        [MemberDoc("镇区")]
        public string Town { get; set; }

        [MemberDoc("工单状态")]
        public string WorkItemState { get; set; }
    }
}