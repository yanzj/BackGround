using System;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless.Complain;

namespace Lte.Domain.Common.Wireless.Work
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
                new Tuple<object, string>(WorkItemCause.Antenna, "��������"),
                new Tuple<object, string>(WorkItemCause.AntennaFeedline, "���������쳣"),
                new Tuple<object, string>(WorkItemCause.AntennaFeedline, "��վ��������"),
                new Tuple<object, string>(WorkItemCause.ApplianceProblem, "�豸����"),
                new Tuple<object, string>(WorkItemCause.FeedAppliance, "����������������"),
                new Tuple<object, string>(WorkItemCause.HardSwitch, "Ӳ�л�����"),
                new Tuple<object, string>(WorkItemCause.ImproperPower, "���ʲ�����"),
                new Tuple<object, string>(WorkItemCause.IndoorDistribution, "�ҷ������쳣"),
                new Tuple<object, string>(WorkItemCause.InterferenceCoverage, "�ڲ����ţ�MOD3���ŵȣ�"),
                new Tuple<object, string>(WorkItemCause.InterferenceCoverage, "���Ÿ�������"),
                new Tuple<object, string>(WorkItemCause.InvisibleAlarm, "���豸���Թ���"),
                new Tuple<object, string>(WorkItemCause.Jamming, "ӵ��"),
                new Tuple<object, string>(WorkItemCause.MainAlarm, "���豸�ϰ��澯"),
                new Tuple<object, string>(WorkItemCause.MainAlarm, "��վ�豸����"),
                new Tuple<object, string>(WorkItemCause.NeighborCell, "����©��"),
                new Tuple<object, string>(WorkItemCause.Others, "����"),
                new Tuple<object, string>(WorkItemCause.Others, "����"),
                new Tuple<object, string>(WorkItemCause.Others, "����ԭ��"),
                new Tuple<object, string>(WorkItemCause.OuterInterference, "�ⲿ����"),
                new Tuple<object, string>(WorkItemCause.OuterInterference, "������"),
                new Tuple<object, string>(WorkItemCause.OuterInterference, "�����ⲿ����"),
                new Tuple<object, string>(WorkItemCause.OverCoverage, "Խ������"),
                new Tuple<object, string>(WorkItemCause.OverCoverage, "Խ����������"),
                new Tuple<object, string>(WorkItemCause.Overload, "С����������"),
                new Tuple<object, string>(WorkItemCause.Overload, "���ɹ���"),
                new Tuple<object, string>(WorkItemCause.PagingChannelBusy, "Ѱ���ŵ����ɸ�"),
                new Tuple<object, string>(WorkItemCause.ParameterConfig, "�������ô���"),
                new Tuple<object, string>(WorkItemCause.PilotPolution, "��Ƶ��Ⱦ"),
                new Tuple<object, string>(WorkItemCause.ResouceJamming, "��Դӵ��"),
                new Tuple<object, string>(WorkItemCause.Rssi, "RSSI�쳣"),
                new Tuple<object, string>(WorkItemCause.TrunkProblem, "�������"),
                new Tuple<object, string>(WorkItemCause.WeakCoverage, "������"),
                new Tuple<object, string>(WorkItemCause.WeakCoverage, "����������"),
                new Tuple<object, string>(WorkItemCause.WrongDownTilt, "����Ǵ���"),
                new Tuple<object, string>(WorkItemCause.BaseStolen, "��վ��ʩ����"),
                new Tuple<object, string>(WorkItemCause.NeedPlan, "���½�վδ����"),
                new Tuple<object, string>(WorkItemCause.NothingWithNetwork, "δ�������������"),
                new Tuple<object, string>(WorkItemCause.UserTerminal, "�����ն˻��û�ԭ��"),
            };
        }
    }
}