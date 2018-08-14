using System;
using Abp.Domain.Entities;
using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless.Work;
using Lte.Domain.Excel;
using Lte.Domain.Regular.Attributes;

namespace Abp.EntityFramework.Entities.Maintainence
{
    [TypeDoc("�洢�����ݿ�Ĺ�����Ϣ")]
    [AutoMapFrom(typeof(WorkItemExcel))]
    public class WorkItem : Entity
    {
        [MemberDoc("�������")]
        public string SerialNumber { get; set; }
        
        [MemberDoc("��������")]
        [AutoMapPropertyResolve("TypeDescription", typeof(WorkItemExcel), typeof(WorkItemTypeTransform))]
        public WorkItemType Type { get; set; }

        [MemberDoc("����������")]
        [AutoMapPropertyResolve("SubTypeDescription", typeof(WorkItemExcel), typeof(WorkItemSubtypeTransform))]
        public WorkItemSubtype Subtype { get; set; }

        [MemberDoc("��վ���")]
        public int ENodebId { get; set; }

        [MemberDoc("�������")]
        public byte SectorId { get; set; }

        [MemberDoc("�ɵ�ʱ��")]
        public DateTime BeginTime { get; set; }

        [MemberDoc("�ص�����")]
        public DateTime Deadline { get; set; }

        [MemberDoc("�ظ�����")]
        public short RepeatTimes { get; set; }

        [MemberDoc("���ش���")]
        public short RejectTimes { get; set; }

        [MemberDoc("������")]
        public string StaffName { get; set; }

        [MemberDoc("�������ʱ��")]
        public DateTime? FeedbackTime { get; set; }

        [MemberDoc("���ʱ��")]
        public DateTime? FinishTime { get; set; }

        [MemberDoc("��λԭ��")]
        [AutoMapPropertyResolve("CauseDescription", typeof(WorkItemExcel), typeof(WorkItemCauseTransform))]
        public WorkItemCause Cause { get; set; }

        [MemberDoc("����״̬")]
        [AutoMapPropertyResolve("StateDescription", typeof(WorkItemExcel), typeof(WorkItemStateTransform))]
        public WorkItemState State { get; set; }

        [MemberDoc("ʡ����ƽ̨������Ϣ")]
        public string Comments { get; set; }

        [MemberDoc("��ƽ̨������Ϣ")]
        public string FeedbackContents { get; set; }
    }
}