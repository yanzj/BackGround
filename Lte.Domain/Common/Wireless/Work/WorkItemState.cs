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
                new Tuple<object, string>(WorkItemState.Processing, "待处理"),
                new Tuple<object, string>(WorkItemState.Processed, "待归档"),
                new Tuple<object, string>(WorkItemState.Finished, "归档"),
                new Tuple<object, string>(WorkItemState.Finished, "已归档"),
                new Tuple<object, string>(WorkItemState.Finished, "已撤销"),
                new Tuple<object, string>(WorkItemState.ToBeSigned, "待签单"),
                new Tuple<object, string>(WorkItemState.Processing, "处理"),
                new Tuple<object, string>(WorkItemState.Processing, "待回单"),
                new Tuple<object, string>(WorkItemState.Processing, "任务处理"),
                new Tuple<object, string>(WorkItemState.Auditing, "待确认"),
                new Tuple<object, string>(WorkItemState.Auditing, "审核"),
                new Tuple<object, string>(WorkItemState.Auditing, "回单审核"),
                new Tuple<object, string>(WorkItemState.Received, "接单"), 
                new Tuple<object, string>(WorkItemState.Feedback, "回单"), 
            };
        }
    }
}