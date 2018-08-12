using System;
using Lte.Domain.Common.Types;

namespace Lte.Domain.Common.Wireless.Complain
{
    [EnumTypeDescription(typeof(ComplainCategory), Others)]
    public enum ComplainCategory : byte
    {
        LowSpeed3G,
        WeakCoverage3G,
        LowSpeed4G,
        WeakCoverage4G,
        BadQualityVoice,
        WeakCoverageVoice,
        Others,
        Voice,
        Web,
        ShortMessage,
        WeakCoverage,
        DeepCoverage,
        NetworkQuality,
        Appliance,
        LowFloor,
        MiddleFloor,
        HighFloor,
        Outdoor,
        VoiceAndWed,
        General4G,
        CallDrop
    }

    public class ComplainCategoryDescriptionTransform : DescriptionTransform<ComplainCategory>
    {

    }

    public class ComplainCategoryTransform : EnumTransform<ComplainCategory>
    {
        public ComplainCategoryTransform() : base(ComplainCategory.Others)
        {
        }
    }

    internal static class ComplainCategoryTuples
    {
        public static Tuple<object, string>[] GetTuples()
        {
            return new[]
            {
                new Tuple<object, string>(ComplainCategory.LowSpeed3G, "3G-网速慢"),
                new Tuple<object, string>(ComplainCategory.WeakCoverage3G, "3G-无信号或信号弱"),
                new Tuple<object, string>(ComplainCategory.LowSpeed4G, "4G-网速慢"),
                new Tuple<object, string>(ComplainCategory.LowSpeed4G, "本地上网质量-网速慢"),
                new Tuple<object, string>(ComplainCategory.LowSpeed4G, "上网质量-网速慢"),
                new Tuple<object, string>(ComplainCategory.WeakCoverage4G, "4G-无信号或信号弱"),
                new Tuple<object, string>(ComplainCategory.WeakCoverage4G, "上网质量-无信号或信号弱"),
                new Tuple<object, string>(ComplainCategory.BadQualityVoice, "语音-通话质量差"),
                new Tuple<object, string>(ComplainCategory.WeakCoverageVoice, "语音-无信号或信号弱"),
                new Tuple<object, string>(ComplainCategory.WeakCoverageVoice, "语音-无信号"),
                new Tuple<object, string>(ComplainCategory.WeakCoverageVoice, "语音-无信号或信号差"),
                new Tuple<object, string>(ComplainCategory.WeakCoverageVoice, "语音-信号弱/不稳定"),
                new Tuple<object, string>(ComplainCategory.Others, "其他"),
                new Tuple<object, string>(ComplainCategory.Voice, "移动语音类"),
                new Tuple<object, string>(ComplainCategory.Voice, "语音"),
                new Tuple<object, string>(ComplainCategory.Voice, "语音-回音/杂音/断续"), 
                new Tuple<object, string>(ComplainCategory.Web, "无线宽带类"),
                new Tuple<object, string>(ComplainCategory.Web, "无线宽带类"),
                new Tuple<object, string>(ComplainCategory.ShortMessage, "短信"),
                new Tuple<object, string>(ComplainCategory.WeakCoverage, "弱覆盖"),
                new Tuple<object, string>(ComplainCategory.WeakCoverage, "覆盖"),
                new Tuple<object, string>(ComplainCategory.DeepCoverage, "深度覆盖"),
                new Tuple<object, string>(ComplainCategory.NetworkQuality, "移动网络质量"),
                new Tuple<object, string>(ComplainCategory.Appliance, "局方设备"),
                new Tuple<object, string>(ComplainCategory.LowFloor, "低层（1-5楼）"),
                new Tuple<object, string>(ComplainCategory.MiddleFloor, "中层（6-9层）"),
                new Tuple<object, string>(ComplainCategory.HighFloor, "高层（10层以上）"),
                new Tuple<object, string>(ComplainCategory.Outdoor, "室外"),
                new Tuple<object, string>(ComplainCategory.VoiceAndWed, "语音上网"),
                new Tuple<object, string>(ComplainCategory.General4G, "4G业务"), 
                new Tuple<object, string>(ComplainCategory.CallDrop, "语音-掉话"), 
            };
        }
    }
}
