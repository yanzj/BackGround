using System;
using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless.Complain;
using Lte.Domain.Regular.Attributes;

namespace Abp.EntityFramework.Entities.Complain
{
    [AutoMapFrom(typeof(BranchDemand))]
    public class BranchDemandDto
    {
        [MemberDoc("��ʼ����")]
        public DateTime BeginDate { get; set; }

        [MemberDoc("��������")]
        public string SerialNumber { get; set; }

        [MemberDoc("������ţ����ڶ���һ����")]
        public int TownId { get; set; }

        [MemberDoc("����")]
        public string City { get; set; }

        [MemberDoc("����")]
        public string District { get; set; }

        [MemberDoc("����")]
        public string Town { get; set; }

        [MemberDoc("�������")]
        public string ComplainContents { get; set; }

        [MemberDoc("������Ϣ")]
        public string ProcessContents { get; set; }

        [AutoMapPropertyResolve("SolveFunction", typeof(BranchDemand), typeof(SolveFunctionDescriptionTransform))]
        [MemberDoc("�����ʩ")]
        public string SolveFunctionDescription { get; set; }

        [AutoMapPropertyResolve("IsSolved", typeof(BranchDemand), typeof(YesNoTransform))]
        [MemberDoc("�Ƿ��ѽ��")]
        public string IsSolvedDescription { get; set; }

        [MemberDoc("�ᵥ���ڣ����Ϊ�գ���˵���ù���δ���")]
        public DateTime? EndDate { get; set; }

        [MemberDoc("����")]
        public double Lontitute { get; set; }

        [MemberDoc("γ��")]
        public double Lattitute { get; set; }

        [MemberDoc("�û���Ϣ")]
        public string SubscriberInfo { get; set; }

        [MemberDoc("�ͻ�������Ϣ")]
        public string ManagerInfo { get; set; }
    }
}