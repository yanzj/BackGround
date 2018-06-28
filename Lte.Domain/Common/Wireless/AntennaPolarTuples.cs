using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Common.Types;

namespace Lte.Domain.Common.Wireless
{
    [EnumTypeDescription(typeof(AntennaPolar), AntennaPolar.Others)]
    public enum AntennaPolar : byte
    {
        Single,
        Double,
        Others
    }

    public class AntennaPolarDescriptionTransform : DescriptionTransform<AntennaPolar>
    {

    }

    public class AntennaPolarTransform : EnumTransform<AntennaPolar>
    {
        public AntennaPolarTransform() : base(AntennaPolar.Others)
        {
        }
    }

    internal static class AntennaPolarTuples
    {
        public static Tuple<object, string>[] GetTuples()
        {
            return new[]
            {
                new Tuple<object, string>(AntennaPolar.Single, "单极化"),
                new Tuple<object, string>(AntennaPolar.Double, "双极化")
            };
        }
    }
}
