using System;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless.Complain;

namespace Lte.Domain.Common.Wireless.Station
{
    [EnumTypeDescription(typeof(OperatorUsage), Others)]
    public enum OperatorUsage : byte
    {
        Telecom,
        TelecomAndMobile,
        TelcomAndUnicom,
        All,
        None,
        Others,
        UnicomUseTelecom,
        TelecomUseUnicom
    }

    public class OperatorUsageDescriptionTransform : DescriptionTransform<OperatorUsage>
    {

    }

    public class OperatorUsageTransform : EnumTransform<OperatorUsage>
    {
        public OperatorUsageTransform() : base(OperatorUsage.Others)
        {
        }
    }

    internal static class OperatorUsageTuples
    {
        public static Tuple<object, string>[] GetTuples()
        {
            return new[]
            {
                new Tuple<object, string>(OperatorUsage.Telecom, "电信独有"),
                new Tuple<object, string>(OperatorUsage.Telecom, "电信产权独有"),
                new Tuple<object, string>(OperatorUsage.TelecomAndMobile, "电信移动"),
                new Tuple<object, string>(OperatorUsage.TelcomAndUnicom, "电信联通"),
                new Tuple<object, string>(OperatorUsage.All, "电信移动联通"),
                new Tuple<object, string>(OperatorUsage.None, "无机房"),
                new Tuple<object, string>(OperatorUsage.Others, "其他"),
                new Tuple<object, string>(OperatorUsage.UnicomUseTelecom, "电信产权联通共享"),
                new Tuple<object, string>(OperatorUsage.TelecomUseUnicom, "联通产权电信共享")
            };
        }
    }
}
