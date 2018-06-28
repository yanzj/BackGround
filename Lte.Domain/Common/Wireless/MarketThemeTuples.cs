using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Common.Types;

namespace Lte.Domain.Common.Wireless
{
    [EnumTypeDescription(typeof(MarketTheme), OpenChannel)]
    public enum MarketTheme : byte
    {
        ElectricGauge,
        OpenChannel,
        HappyNewYear,
        CollegeSpring,
        CollegeAutumn,
        Others
    }

    public class MarketThemeDescriptionTransform : DescriptionTransform<MarketTheme>
    {

    }

    public class MarketThemeTransform : EnumTransform<MarketTheme>
    {
        public MarketThemeTransform() : base(MarketTheme.OpenChannel)
        {
        }
    }

    internal static class MarketThemeTuples
    {
        public static Tuple<object, string>[] GetTuples()
        {
            return new[]
            {
                new Tuple<object, string>(MarketTheme.CollegeAutumn, "校园秋营"),
                new Tuple<object, string>(MarketTheme.CollegeSpring, "校园春营"),
                new Tuple<object, string>(MarketTheme.ElectricGauge, "电力抄表"),
                new Tuple<object, string>(MarketTheme.HappyNewYear, "岁末年初"),
                new Tuple<object, string>(MarketTheme.OpenChannel, "开放渠道"),
                new Tuple<object, string>(MarketTheme.Others, "其他")
            };
        }
    }
}
