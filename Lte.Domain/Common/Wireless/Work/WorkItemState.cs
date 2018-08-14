using System;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless.Complain;

namespace Lte.Domain.Common.Wireless.Work
{
    [EnumTypeDescription(typeof(WorkItemState), ToBeSigned)]
    public enum WorkItemState : byte
    {
        Processing,
        Processed,
        Finished,
        ToBeSigned,
        Auditing,
        Received,
        Feedback
    }

    public class WorkItemStateDescriptionTransform : DescriptionTransform<WorkItemState>
    {

    }

    public class WorkItemStateTransform : EnumTransform<WorkItemState>
    {
        public WorkItemStateTransform() : base(WorkItemState.ToBeSigned)
        {
        }
    }

    internal static class WorkItemStateTuples
    {
        public static Tuple<object, string>[] GetTuples()
        {
            return new[]
            {
                new Tuple<object, string>(WorkItemState.Processing, "������"),
                new Tuple<object, string>(WorkItemState.Processed, "���鵵"),
                new Tuple<object, string>(WorkItemState.Finished, "�鵵"),
                new Tuple<object, string>(WorkItemState.Finished, "�ѹ鵵"),
                new Tuple<object, string>(WorkItemState.Finished, "�ѳ���"),
                new Tuple<object, string>(WorkItemState.ToBeSigned, "��ǩ��"),
                new Tuple<object, string>(WorkItemState.Processing, "����"),
                new Tuple<object, string>(WorkItemState.Processing, "���ص�"),
                new Tuple<object, string>(WorkItemState.Processing, "������"),
                new Tuple<object, string>(WorkItemState.Auditing, "��ȷ��"),
                new Tuple<object, string>(WorkItemState.Auditing, "���"),
                new Tuple<object, string>(WorkItemState.Auditing, "�ص����"),
                new Tuple<object, string>(WorkItemState.Received, "�ӵ�"), 
                new Tuple<object, string>(WorkItemState.Feedback, "�ص�"), 
            };
        }
    }
}