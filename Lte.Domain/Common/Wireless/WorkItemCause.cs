using System;
using Lte.Domain.Common.Types;

namespace Lte.Domain.Common.Wireless
{
    [EnumTypeDescription(typeof(WorkItemCause), Others)]
    public enum WorkItemCause : short
    {
        Rssi,
        ParameterConfig,
        TrunkProblem,
        PilotPolution,
        Overload,
        InterferenceCoverage,
        ImproperPower,
        FeedAppliance,
        NeighborCell,
        Others,
        WeakCoverage,
        ApplianceProblem,
        IndoorDistribution,
        AntennaFeedline,
        Antenna,
        OuterInterference,
        WrongDownTilt,
        PagingChannelBusy,
        HardSwitch,
        Jamming,
        OverCoverage,
        InvisibleAlarm,
        MainAlarm,
        ResouceJamming,
        BaseStolen,
        NeedPlan,
        NothingWithNetwork,
        UserTerminal
    }

    public class WorkItemCauseDescriptionTransform : DescriptionTransform<WorkItemCause>
    {

    }

    public class WorkItemCauseTransform : EnumTransform<WorkItemCause>
    {
        public WorkItemCauseTransform() : base(WorkItemCause.Others)
        {
        }
    }

    internal static class WorkItemCauseTuples
    {
        public static Tuple<object, string>[] GetTuples()
        {
            return new[]
            {
                new Tuple<object, string>(WorkItemCause.Antenna, "天线问题"),
                new Tuple<object, string>(WorkItemCause.AntennaFeedline, "天馈器件异常"),
                new Tuple<object, string>(WorkItemCause.AntennaFeedline, "基站天馈故障"),
                new Tuple<object, string>(WorkItemCause.ApplianceProblem, "设备故障"),
                new Tuple<object, string>(WorkItemCause.FeedAppliance, "馈线链接器件问题"),
                new Tuple<object, string>(WorkItemCause.HardSwitch, "硬切换问题"),
                new Tuple<object, string>(WorkItemCause.ImproperPower, "功率不合理"),
                new Tuple<object, string>(WorkItemCause.IndoorDistribution, "室分器件异常"),
                new Tuple<object, string>(WorkItemCause.InterferenceCoverage, "内部干扰（MOD3干扰等）"),
                new Tuple<object, string>(WorkItemCause.InterferenceCoverage, "干扰覆盖问题"),
                new Tuple<object, string>(WorkItemCause.InvisibleAlarm, "主设备隐性故障"),
                new Tuple<object, string>(WorkItemCause.Jamming, "拥塞"),
                new Tuple<object, string>(WorkItemCause.MainAlarm, "主设备障碍告警"),
                new Tuple<object, string>(WorkItemCause.MainAlarm, "基站设备故障"),
                new Tuple<object, string>(WorkItemCause.NeighborCell, "邻区漏配"),
                new Tuple<object, string>(WorkItemCause.Others, "其他"),
                new Tuple<object, string>(WorkItemCause.Others, "其它"),
                new Tuple<object, string>(WorkItemCause.Others, "其它原因"),
                new Tuple<object, string>(WorkItemCause.OuterInterference, "外部干扰"),
                new Tuple<object, string>(WorkItemCause.OuterInterference, "外界干扰"),
                new Tuple<object, string>(WorkItemCause.OuterInterference, "网络外部干扰"),
                new Tuple<object, string>(WorkItemCause.OverCoverage, "越区覆盖"),
                new Tuple<object, string>(WorkItemCause.OverCoverage, "越区覆盖问题"),
                new Tuple<object, string>(WorkItemCause.Overload, "小区容量不足"),
                new Tuple<object, string>(WorkItemCause.Overload, "负荷过载"),
                new Tuple<object, string>(WorkItemCause.PagingChannelBusy, "寻呼信道负荷高"),
                new Tuple<object, string>(WorkItemCause.ParameterConfig, "参数配置错误"),
                new Tuple<object, string>(WorkItemCause.PilotPolution, "导频污染"),
                new Tuple<object, string>(WorkItemCause.ResouceJamming, "资源拥塞"),
                new Tuple<object, string>(WorkItemCause.Rssi, "RSSI异常"),
                new Tuple<object, string>(WorkItemCause.TrunkProblem, "传输故障"),
                new Tuple<object, string>(WorkItemCause.WeakCoverage, "弱覆盖"),
                new Tuple<object, string>(WorkItemCause.WeakCoverage, "弱覆盖问题"),
                new Tuple<object, string>(WorkItemCause.WrongDownTilt, "下倾角错误"),
                new Tuple<object, string>(WorkItemCause.BaseStolen, "基站设施被盗"),
                new Tuple<object, string>(WorkItemCause.NeedPlan, "需新建站未立项"),
                new Tuple<object, string>(WorkItemCause.NothingWithNetwork, "未发现网络侧问题"),
                new Tuple<object, string>(WorkItemCause.UserTerminal, "疑似终端或用户原因"),
            };
        }
    }
}