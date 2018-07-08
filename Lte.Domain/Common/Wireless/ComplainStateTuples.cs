using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Common.Types;

namespace Lte.Domain.Common.Wireless
{
    [EnumTypeDescription(typeof(ComplainState), Begin)]
    public enum ComplainState : byte
    {
        Begin,
        Preprocessed,
        PlanTest,
        Test,
        ProcessIssues,
        Feedback,
        Archive,
        Resolved,
        Resolving,
        NoResolve,
        Normal,
        Abnormal,
        NoAppliance
    }

    public class ComplainStateDescriptionTransform : DescriptionTransform<ComplainState>
    {

    }

    public class ComplainStateTransform : EnumTransform<ComplainState>
    {
        public ComplainStateTransform() : base(ComplainState.Begin)
        {
        }
    }

    internal static class ComplainStateTuples
    {
        public static Tuple<object, string>[] GetTuples()
        {
            return new[]
            {
                new Tuple<object, string>(ComplainState.Begin, "生成工单"),
                new Tuple<object, string>(ComplainState.Preprocessed, "预处理"),
                new Tuple<object, string>(ComplainState.PlanTest, "预约测试"),
                new Tuple<object, string>(ComplainState.Test, "现场测试"),
                new Tuple<object, string>(ComplainState.ProcessIssues, "问题处理"),
                new Tuple<object, string>(ComplainState.Feedback, "回访用户"),
                new Tuple<object, string>(ComplainState.Archive, "工单归档"),
                new Tuple<object, string>(ComplainState.Resolved, "已解决"),
                new Tuple<object, string>(ComplainState.Resolving, "处理中"),
                new Tuple<object, string>(ComplainState.NoResolve, "无法解决"),
                new Tuple<object, string>(ComplainState.Normal, "正常"),
                new Tuple<object, string>(ComplainState.Abnormal, "异常"),
                new Tuple<object, string>(ComplainState.NoAppliance, "无此设备"), 
            };
        }
    }
}
