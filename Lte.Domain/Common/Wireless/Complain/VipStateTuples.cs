using System;
using Lte.Domain.Common.Types;

namespace Lte.Domain.Common.Wireless.Complain
{
    [EnumTypeDescription(typeof(VipState), Begin)]
    public enum VipState : byte
    {
        Begin,
        Preprocessed,
        Test,
        TestEvaluation,
        NetworkOptimization,
        NewSite,
        EmergencyDemand,
        Conclusion
    }

    public class VipStateDescriptionTransform : DescriptionTransform<VipState>
    {

    }

    public class VipStateTransform : EnumTransform<VipState>
    {
        public VipStateTransform() : base(VipState.Begin)
        {
        }
    }

    internal static class VipStateTuples
    {
        public static Tuple<object, string>[] GetTuples()
        {
            return new[]
            {
                new Tuple<object, string>(VipState.Begin, "生成工单"),
                new Tuple<object, string>(VipState.Preprocessed, "预处理"),
                new Tuple<object, string>(VipState.Test, "现场测试"),
                new Tuple<object, string>(VipState.TestEvaluation, "测试评估"),
                new Tuple<object, string>(VipState.NetworkOptimization, "优化调整"),
                new Tuple<object, string>(VipState.NewSite, "新增资源"),
                new Tuple<object, string>(VipState.EmergencyDemand, "通信车需求"),
                new Tuple<object, string>(VipState.Conclusion, "保障结论"),
            };
        }
    }
}
