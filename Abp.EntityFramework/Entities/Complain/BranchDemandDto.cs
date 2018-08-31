using System;
using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Common.Transform;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless.Complain;
using Lte.Domain.Regular.Attributes;

namespace Abp.EntityFramework.Entities.Complain
{
    [AutoMapFrom(typeof(BranchDemand))]
    public class BranchDemandDto
    {
        [MemberDoc("开始日期")]
        public DateTime BeginDate { get; set; }

        [MemberDoc("工单编码")]
        public string SerialNumber { get; set; }

        [MemberDoc("镇区编号，用于定义一个镇")]
        public int TownId { get; set; }

        [MemberDoc("城市")]
        public string City { get; set; }

        [MemberDoc("区域")]
        public string District { get; set; }

        [MemberDoc("镇区")]
        public string Town { get; set; }

        [MemberDoc("申告内容")]
        public string ComplainContents { get; set; }

        [MemberDoc("过程信息")]
        public string ProcessContents { get; set; }

        [AutoMapPropertyResolve("SolveFunction", typeof(BranchDemand), typeof(SolveFunctionDescriptionTransform))]
        [MemberDoc("解决措施")]
        public string SolveFunctionDescription { get; set; }

        [AutoMapPropertyResolve("IsSolved", typeof(BranchDemand), typeof(YesNoTransform))]
        [MemberDoc("是否已解决")]
        public string IsSolvedDescription { get; set; }

        [MemberDoc("结单日期，如果为空，则说明该工单未解决")]
        public DateTime? EndDate { get; set; }

        [MemberDoc("经度")]
        public double Lontitute { get; set; }

        [MemberDoc("纬度")]
        public double Lattitute { get; set; }

        [MemberDoc("用户信息")]
        public string SubscriberInfo { get; set; }

        [MemberDoc("客户经理信息")]
        public string ManagerInfo { get; set; }
    }
}