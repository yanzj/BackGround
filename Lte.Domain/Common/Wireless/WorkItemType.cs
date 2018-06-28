using System;
using Lte.Domain.Common.Types;

namespace Lte.Domain.Common.Wireless
{
    [EnumTypeDescription(typeof(WorkItemType), Others)]
    public enum WorkItemType : byte
    {
        Kpi2G,
        Kpi4G,
        Infrastructure4G,
        Interference4G,
        RrcConnection,
        NetworkProblem,
        Others,
        DailyTask,
        DailyReport,
        Yilutong,
        KeySite,
        SelfConstruction,
        Feeling
    }

    public class WorkItemTypeDescriptionTransform : DescriptionTransform<WorkItemType>
    {

    }

    public class WorkItemTypeTransform : EnumTransform<WorkItemType>
    {
        public WorkItemTypeTransform() : base(WorkItemType.Others)
        {
        }
    }

    internal static class WorkItemTypeTuples
    {
        public static Tuple<object, string>[] GetTuples()
        {
            return new[]
            {
                new Tuple<object, string>(WorkItemType.Infrastructure4G, "4G基础数据"),
                new Tuple<object, string>(WorkItemType.Interference4G, "4G干扰故障"),
                new Tuple<object, string>(WorkItemType.Kpi2G, "2G性能故障"),
                new Tuple<object, string>(WorkItemType.Kpi4G, "4G性能故障"),
                new Tuple<object, string>(WorkItemType.NetworkProblem, "网元故障"),
                new Tuple<object, string>(WorkItemType.RrcConnection, "RRC连接成功率恶化"),
                new Tuple<object, string>(WorkItemType.Others, "其他类型"),
                new Tuple<object, string>(WorkItemType.DailyTask, "日常网优作业计划"),
                new Tuple<object, string>(WorkItemType.DailyReport, "日报"),
                new Tuple<object, string>(WorkItemType.Yilutong, "翼路通"),
                new Tuple<object, string>(WorkItemType.KeySite, "省-集团测试保障-关键站点清单收集"),
                new Tuple<object, string>(WorkItemType.SelfConstruction, "自建工单"),
                new Tuple<object, string>(WorkItemType.Feeling, "/移动业务感知/无线/综合感知/"),
            };
        }
    }
}