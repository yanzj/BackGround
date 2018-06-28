using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Common.Types;

namespace Lte.Domain.Common.Wireless
{
    [EnumTypeDescription(typeof(AlarmLevel), Others)]
    public enum AlarmLevel : byte
    {
        Serious,
        Primary,
        Secondary,
        Warning,
        Urgent,
        Important,
        Tips,
        Between100And105,
        Between105And110,
        Below110,
        Others,
        BadCoverageCompeteLose,
        BadCoverageCompeteWin,
        GoodCoverageCompeteLose,
        GoodCoverageCompeteWin
    }

    public class AlarmLevelDescriptionTransform : DescriptionTransform<AlarmLevel>
    {

    }

    public class AlarmLevelTransform : EnumTransform<AlarmLevel>
    {
        public AlarmLevelTransform() : base(AlarmLevel.Others)
        {
        }
    }

    internal static class AlarmLevelTuples
    {
        public static Tuple<object, string>[] GetTuples()
        {
            return new[]
            {
                new Tuple<object, string>(AlarmLevel.Serious, "严重"),
                new Tuple<object, string>(AlarmLevel.Primary, "主要"),
                new Tuple<object, string>(AlarmLevel.Secondary, "次要"),
                new Tuple<object, string>(AlarmLevel.Warning, "警告"),
                new Tuple<object, string>(AlarmLevel.Urgent, "紧急"),
                new Tuple<object, string>(AlarmLevel.Important, "重要"),
                new Tuple<object, string>(AlarmLevel.Tips, "提示"),
                new Tuple<object, string>(AlarmLevel.Between100And105, "-100dbm到-105dbm"),
                new Tuple<object, string>(AlarmLevel.Between105And110, "-105dbm到-110dbm"),
                new Tuple<object, string>(AlarmLevel.Below110, "-110dbm以下"),
                new Tuple<object, string>(AlarmLevel.Between100And105, "黄色 -100dbm到-105dbm"),
                new Tuple<object, string>(AlarmLevel.Between105And110, "黄色 -105dbm到-110dbm"),
                new Tuple<object, string>(AlarmLevel.Below110, "红色 -110dbm以下"),
                new Tuple<object, string>(AlarmLevel.Others, "其他"),
                new Tuple<object, string>(AlarmLevel.GoodCoverageCompeteWin, "覆盖率大于80%且高于对方5%"),
                new Tuple<object, string>(AlarmLevel.GoodCoverageCompeteLose, "覆盖率大于80%且低于对方5%"),
                new Tuple<object, string>(AlarmLevel.BadCoverageCompeteWin, "覆盖率小于于80%且比对方好"),
                new Tuple<object, string>(AlarmLevel.BadCoverageCompeteLose, "覆盖率小于于80%且比对方差"),
                new Tuple<object, string>(AlarmLevel.GoodCoverageCompeteWin, "绿色 级别4"),
                new Tuple<object, string>(AlarmLevel.GoodCoverageCompeteLose, "黄色 级别3"),
                new Tuple<object, string>(AlarmLevel.BadCoverageCompeteWin, "紫色 级别2"),
                new Tuple<object, string>(AlarmLevel.BadCoverageCompeteLose, "红色 级别1"),
            };
        }
    }
}
