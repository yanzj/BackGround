using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Regular.Attributes;
using Lte.MySqlFramework.Entities;

namespace Lte.MySqlFramework.Support.View
{
    [AutoMapFrom(typeof(WorkItemView))]
    public class WorkItemChartView
    {
        [MemberDoc("��������")]
        public string WorkItemType { get; set; }

        [MemberDoc("����������")]
        public string WorkItemSubType { get; set; }

        [MemberDoc("����")]
        public string District { get; set; }

        [MemberDoc("����")]
        public string Town { get; set; }

        [MemberDoc("����״̬")]
        public string WorkItemState { get; set; }
    }
}