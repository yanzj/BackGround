using System;
using Lte.Domain.Common.Types;

namespace Lte.Domain.Common.Wireless.Complain
{
    [EnumTypeDescription(typeof(SolveFunction), Others)]
    public enum SolveFunction : byte
    {
        NewSiteUnplanned,
        NewSitePlanned,
        NewRruUnplanned,
        NewRruPlanned,
        NewDistributionPlanned,
        NewDistributionUnplanned,
        NewRepeaterPlanned,
        NewRepeaterUnplanned,
        NewDoPlanned,
        NewDoUnplanned,
        DistributionExpansion,
        SubscriberTerminal,
        BtsMalfunction,
        DistributionMalfunction,
        RepeaterMalfunction,
        NetworkOptimization,
        SelfRecoverage,
        NormalTest,
        NoContact,
        Others
    }

    public class SolveFunctionDescriptionTransform : DescriptionTransform<SolveFunction>
    {

    }

    public class SolveFunctionTransform : EnumTransform<SolveFunction>
    {
        public SolveFunctionTransform() : base(SolveFunction.Others)
        {
        }
    }

    internal static class SolveFunctionTuples
    {
        public static Tuple<object, string>[] GetTuples()
        {
            return new[]
            {
                new Tuple<object, string>(SolveFunction.NewSitePlanned, "新增基站（已规划）"),
                new Tuple<object, string>(SolveFunction.NewSiteUnplanned, "新增基站（未规划）"),
                new Tuple<object, string>(SolveFunction.NewRruPlanned, "新增RRU（已规划）"),
                new Tuple<object, string>(SolveFunction.NewRruUnplanned, "新增RRU（未规划）"),
                new Tuple<object, string>(SolveFunction.NewDistributionPlanned, "新增室分系统（已规划）"),
                new Tuple<object, string>(SolveFunction.NewDistributionUnplanned, "新增室分系统（未规划）"),
                new Tuple<object, string>(SolveFunction.NewRepeaterPlanned, "新增直放站（已规划）"),
                new Tuple<object, string>(SolveFunction.NewRepeaterUnplanned, "新增直放站（未规划）"),
                new Tuple<object, string>(SolveFunction.NewDoPlanned, "新增DO资源（有规划）"),
                new Tuple<object, string>(SolveFunction.NewDoUnplanned, "新增DO资源（无规划）"),
                new Tuple<object, string>(SolveFunction.DistributionExpansion, "室分系统（扩容）"),
                new Tuple<object, string>(SolveFunction.SubscriberTerminal, "用户终端问题"),
                new Tuple<object, string>(SolveFunction.BtsMalfunction, "基站故障处理"),
                new Tuple<object, string>(SolveFunction.DistributionMalfunction, "室分故障处理"),
                new Tuple<object, string>(SolveFunction.RepeaterMalfunction, "直放站故障处理"),
                new Tuple<object, string>(SolveFunction.NetworkOptimization, "网优调整"),
                new Tuple<object, string>(SolveFunction.SelfRecoverage, "查中自复户"),
                new Tuple<object, string>(SolveFunction.NormalTest, "测试正常"),
                new Tuple<object, string>(SolveFunction.NoContact, "联系不上用"),
                new Tuple<object, string>(SolveFunction.Others, "其他")
            };
        }
    }
}
