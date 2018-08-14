using System;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless.Complain;

namespace Lte.Domain.Common.Wireless.Work
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
                new Tuple<object, string>(WorkItemType.Infrastructure4G, "4G��������"),
                new Tuple<object, string>(WorkItemType.Interference4G, "4G���Ź���"),
                new Tuple<object, string>(WorkItemType.Kpi2G, "2G���ܹ���"),
                new Tuple<object, string>(WorkItemType.Kpi4G, "4G���ܹ���"),
                new Tuple<object, string>(WorkItemType.NetworkProblem, "��Ԫ����"),
                new Tuple<object, string>(WorkItemType.RrcConnection, "RRC���ӳɹ��ʶ�"),
                new Tuple<object, string>(WorkItemType.Others, "��������"),
                new Tuple<object, string>(WorkItemType.DailyTask, "�ճ�������ҵ�ƻ�"),
                new Tuple<object, string>(WorkItemType.DailyReport, "�ձ�"),
                new Tuple<object, string>(WorkItemType.Yilutong, "��·ͨ"),
                new Tuple<object, string>(WorkItemType.KeySite, "ʡ-���Ų��Ա���-�ؼ�վ���嵥�ռ�"),
                new Tuple<object, string>(WorkItemType.SelfConstruction, "�Խ�����"),
                new Tuple<object, string>(WorkItemType.Feeling, "/�ƶ�ҵ���֪/����/�ۺϸ�֪/"),
            };
        }
    }
}