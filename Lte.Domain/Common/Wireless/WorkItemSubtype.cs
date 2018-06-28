using System;
using Lte.Domain.Common.Types;

namespace Lte.Domain.Common.Wireless
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
                new Tuple<object, string>(WorkItemSubtype.CallSetup, "小区级呼叫建立成功率异常"),
                new Tuple<object, string>(WorkItemSubtype.DataMaintainence, "数据维护"),
                new Tuple<object, string>(WorkItemSubtype.Drop2G, "小区级掉话率异常"),
                new Tuple<object, string>(WorkItemSubtype.ErabConnection, "小区级E-RAB建立成功率异常"),
                new Tuple<object, string>(WorkItemSubtype.ErabDrop, "小区级E-RAB掉线率异常"),
                new Tuple<object, string>(WorkItemSubtype.PrbUplinkInterference, "PRB上行控制信道干扰"),
                new Tuple<object, string>(WorkItemSubtype.PrbUplinkSevereInterference, "PRB上行控制信道严重干扰"),
                new Tuple<object, string>(WorkItemSubtype.PreciseRate, "小区级精确覆盖率异常"),
                new Tuple<object, string>(WorkItemSubtype.RrcConnection, "小区级RRC连接成功率恶化"),
                new Tuple<object, string>(WorkItemSubtype.Rssi, "RSSI故障"),
                new Tuple<object, string>(WorkItemSubtype.UplinkInterference, "小区级上行干扰"),
                new Tuple<object, string>(WorkItemSubtype.UplinkSevereInterference, "小区级上行严重干扰"),
                new Tuple<object, string>(WorkItemSubtype.Others, "其他类型"),
                new Tuple<object, string>(WorkItemSubtype.AutomaticDt, "自动路测系统管理"),
                new Tuple<object, string>(WorkItemSubtype.ResourceOptimize, "资源调优管理"),
                new Tuple<object, string>(WorkItemSubtype.ProjectOptimization, "专题专项优化"),
                new Tuple<object, string>(WorkItemSubtype.CommunicationSustain, "重大通信保障"),
                new Tuple<object, string>(WorkItemSubtype.OptimizationWorkItem, "优化工单处理"),
                new Tuple<object, string>(WorkItemSubtype.KpiAlarm, "性能监控预警"),
                new Tuple<object, string>(WorkItemSubtype.RectifyDemand, "网优整改需求管理"),
                new Tuple<object, string>(WorkItemSubtype.NetworkPlan, "网络规划选址"),
                new Tuple<object, string>(WorkItemSubtype.SpecialData, "特殊数据更新"),
                new Tuple<object, string>(WorkItemSubtype.Dispossessed, "逼迁应急优化"),
                new Tuple<object, string>(WorkItemSubtype.ParameterCheck, "参数核查优化"),
                new Tuple<object, string>(WorkItemSubtype.ClusterRf, "簇射频优化"),
                new Tuple<object, string>(WorkItemSubtype.CoverageEvaluation, "覆盖系统评估"),
                new Tuple<object, string>(WorkItemSubtype.InterferenceCheck, "干扰排查整治"),
                new Tuple<object, string>(WorkItemSubtype.EngineeringOptimization, "工程优化管理"),
                new Tuple<object, string>(WorkItemSubtype.PlanDemandLibrary, "规划需求库管理"),
                new Tuple<object, string>(WorkItemSubtype.EngineeringParameters, "基站工参维护"),
                new Tuple<object, string>(WorkItemSubtype.MarketSustain, "市场支撑保障"),
                new Tuple<object, string>(WorkItemSubtype.CapacityEvaluation, "容量系统评估"),
                new Tuple<object, string>(WorkItemSubtype.CustomerComplain, "客户投诉处理"),
                new Tuple<object, string>(WorkItemSubtype.WeeklyAnalysis, "每周质量分析"),
                new Tuple<object, string>(WorkItemSubtype.DailyTest, "日常测试管理"),
                new Tuple<object, string>(WorkItemSubtype.BadFeeling, "小区连续3天感知差"),
                new Tuple<object, string>(WorkItemSubtype.HighFlowBadFeeling, "高流量小区连续3天感知差"),
                new Tuple<object, string>(WorkItemSubtype.TripleNetworkCell, "三网对比质差小区"),
                new Tuple<object, string>(WorkItemSubtype.TripleNetworkBuilding, "三网对比质差楼群"),
                new Tuple<object, string>(WorkItemSubtype.HighCqiLowRank2, "小区级高CQI低双流比异常"),
            };
        }
    }
}