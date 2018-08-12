using System;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless.Complain;

namespace Lte.Domain.Common.Wireless.Alarm
{
    [EnumTypeDescription(typeof(AlarmCategory), Qos)]
    public enum AlarmCategory : byte
    {
        Communication,
        Qos,
        ProcessError,
        Environment,
        Apparatus,
        Huawei,
        Others,
        Self,
        OverallCompete,
        MobileCompete,
        UnicomCompete,
        AlarmOmc
    }

    public class AlarmCategoryDescriptionTransform : DescriptionTransform<AlarmCategory>
    {

    }

    public class AlarmCategoryTransform : EnumTransform<AlarmCategory>
    {
        public AlarmCategoryTransform() : base(AlarmCategory.Huawei)
        {
        }
    }

    internal static class AlarmCategoryTuples
    {
        public static Tuple<object, string>[] GetTuples()
        {
            return new[]
            {
                new Tuple<object, string>(AlarmCategory.Communication, "通信告警"),
                new Tuple<object, string>(AlarmCategory.Qos, "服务质量告警"),
                new Tuple<object, string>(AlarmCategory.ProcessError, "处理错误告警"),
                new Tuple<object, string>(AlarmCategory.Environment, "环境告警"),
                new Tuple<object, string>(AlarmCategory.Apparatus, "设备告警"),
                new Tuple<object, string>(AlarmCategory.Huawei, "华为告警"),
                new Tuple<object, string>(AlarmCategory.Others, "其他"),
                new Tuple<object, string>(AlarmCategory.Self, "自身覆盖"),
                new Tuple<object, string>(AlarmCategory.OverallCompete, "竞对总体"),
                new Tuple<object, string>(AlarmCategory.MobileCompete, "移动竞对"),
                new Tuple<object, string>(AlarmCategory.UnicomCompete, "联通竞对"),
                new Tuple<object, string>(AlarmCategory.AlarmOmc, "网管系统告警"),
            };
        }
    }
}
