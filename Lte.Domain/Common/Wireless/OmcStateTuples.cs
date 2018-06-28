using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Common.Types;

namespace Lte.Domain.Common.Wireless
{
    [EnumTypeDescription(typeof(OmcState), OmcState.Others)]
    public enum OmcState : byte
    {
        Normal,
        UnderConstruction,
        OutOfService,
        Others
    }

    public class OmcStateDescriptionTransform : DescriptionTransform<OmcState>
    {

    }

    public class OmcStateTransform : EnumTransform<OmcState>
    {
        public OmcStateTransform() : base(OmcState.Others)
        {
        }
    }

    internal static class OmcStateTuples
    {
        public static Tuple<object, string>[] GetTuples()
        {
            return new[]
            {
                new Tuple<object, string>(OmcState.Normal, "在网运行"),
                new Tuple<object, string>(OmcState.UnderConstruction, "工程状态"),
                new Tuple<object, string>(OmcState.OutOfService, "长期退服")
            };
        }
    }
}
