using System;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless.Complain;

namespace Lte.Domain.Common.Wireless.Work
{
    [EnumTypeDescription(typeof(WorkItemSubtype), Others)]
    public enum WorkItemSubtype : short
    {
        Drop2G,
        CallSetup,
        PrbUplinkInterference,
        PrbUplinkSevereInterference,
        Rssi,
        DataMaintainence,
        ErabDrop,
        ErabConnection,
        RrcConnection,
        PreciseRate,
        UplinkInterference,
        UplinkSevereInterference,
        Others,
        AutomaticDt,
        ResourceOptimize,
        ProjectOptimization,
        CommunicationSustain,
        OptimizationWorkItem,
        KpiAlarm,
        RectifyDemand,
        NetworkPlan,
        SpecialData,
        Dispossessed,
        ParameterCheck,
        ClusterRf,
        CoverageEvaluation,
        InterferenceCheck,
        EngineeringOptimization,
        PlanDemandLibrary,
        EngineeringParameters,
        MarketSustain,
        CapacityEvaluation,
        CustomerComplain,
        WeeklyAnalysis,
        DailyTest,
        BadFeeling,
        HighFlowBadFeeling,
        TripleNetworkCell,
        TripleNetworkBuilding,
        HighCqiLowRank2
    }

    public class WorkItemSubtypeDescriptionTransform : DescriptionTransform<WorkItemSubtype>
    {

    }

    public class WorkItemSubtypeTransform : EnumTransform<WorkItemSubtype>
    {
        public WorkItemSubtypeTransform() : base(WorkItemSubtype.Others)
        {
        }
    }

    internal static class WorkItemSubtypeTuples
    {
        public static Tuple<object, string>[] GetTuples()
        {
            return new[]
            {
                new Tuple<object, string>(WorkItemSubtype.CallSetup, "С�������н����ɹ����쳣"),
                new Tuple<object, string>(WorkItemSubtype.DataMaintainence, "����ά��"),
                new Tuple<object, string>(WorkItemSubtype.Drop2G, "С�����������쳣"),
                new Tuple<object, string>(WorkItemSubtype.ErabConnection, "С����E-RAB�����ɹ����쳣"),
                new Tuple<object, string>(WorkItemSubtype.ErabDrop, "С����E-RAB�������쳣"),
                new Tuple<object, string>(WorkItemSubtype.PrbUplinkInterference, "PRB���п����ŵ�����"),
                new Tuple<object, string>(WorkItemSubtype.PrbUplinkSevereInterference, "PRB���п����ŵ����ظ���"),
                new Tuple<object, string>(WorkItemSubtype.PreciseRate, "С������ȷ�������쳣"),
                new Tuple<object, string>(WorkItemSubtype.RrcConnection, "С����RRC���ӳɹ��ʶ�"),
                new Tuple<object, string>(WorkItemSubtype.Rssi, "RSSI����"),
                new Tuple<object, string>(WorkItemSubtype.UplinkInterference, "С�������и���"),
                new Tuple<object, string>(WorkItemSubtype.UplinkSevereInterference, "С�����������ظ���"),
                new Tuple<object, string>(WorkItemSubtype.Others, "��������"),
                new Tuple<object, string>(WorkItemSubtype.AutomaticDt, "�Զ�·��ϵͳ����"),
                new Tuple<object, string>(WorkItemSubtype.ResourceOptimize, "��Դ���Ź���"),
                new Tuple<object, string>(WorkItemSubtype.ProjectOptimization, "ר��ר���Ż�"),
                new Tuple<object, string>(WorkItemSubtype.CommunicationSustain, "�ش�ͨ�ű���"),
                new Tuple<object, string>(WorkItemSubtype.OptimizationWorkItem, "�Ż���������"),
                new Tuple<object, string>(WorkItemSubtype.KpiAlarm, "���ܼ��Ԥ��"),
                new Tuple<object, string>(WorkItemSubtype.RectifyDemand, "���������������"),
                new Tuple<object, string>(WorkItemSubtype.NetworkPlan, "����滮ѡַ"),
                new Tuple<object, string>(WorkItemSubtype.SpecialData, "�������ݸ���"),
                new Tuple<object, string>(WorkItemSubtype.Dispossessed, "��ǨӦ���Ż�"),
                new Tuple<object, string>(WorkItemSubtype.ParameterCheck, "�����˲��Ż�"),
                new Tuple<object, string>(WorkItemSubtype.ClusterRf, "����Ƶ�Ż�"),
                new Tuple<object, string>(WorkItemSubtype.CoverageEvaluation, "����ϵͳ����"),
                new Tuple<object, string>(WorkItemSubtype.InterferenceCheck, "�����Ų�����"),
                new Tuple<object, string>(WorkItemSubtype.EngineeringOptimization, "�����Ż�����"),
                new Tuple<object, string>(WorkItemSubtype.PlanDemandLibrary, "�滮��������"),
                new Tuple<object, string>(WorkItemSubtype.EngineeringParameters, "��վ����ά��"),
                new Tuple<object, string>(WorkItemSubtype.MarketSustain, "�г�֧�ű���"),
                new Tuple<object, string>(WorkItemSubtype.CapacityEvaluation, "����ϵͳ����"),
                new Tuple<object, string>(WorkItemSubtype.CustomerComplain, "�ͻ�Ͷ�ߴ���"),
                new Tuple<object, string>(WorkItemSubtype.WeeklyAnalysis, "ÿ����������"),
                new Tuple<object, string>(WorkItemSubtype.DailyTest, "�ճ����Թ���"),
                new Tuple<object, string>(WorkItemSubtype.BadFeeling, "С������3���֪��"),
                new Tuple<object, string>(WorkItemSubtype.HighFlowBadFeeling, "������С������3���֪��"),
                new Tuple<object, string>(WorkItemSubtype.TripleNetworkCell, "�����Ա��ʲ�С��"),
                new Tuple<object, string>(WorkItemSubtype.TripleNetworkBuilding, "�����Ա��ʲ�¥Ⱥ"),
                new Tuple<object, string>(WorkItemSubtype.HighCqiLowRank2, "С������CQI��˫�����쳣"),
            };
        }
    }
}