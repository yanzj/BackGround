using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Common.Types;

namespace Lte.Domain.Common.Wireless
{
    [EnumTypeDescription(typeof(ENodebFactory), ENodebFactory.Others)]
    public enum ENodebFactory : byte
    {
        Huawei,
        Zte,
        Bell,
        Erisson,
        Nokia,
        Datang,
        Others
    }

    public class ENodebFactoryDescriptionTransform : DescriptionTransform<ENodebFactory>
    {

    }

    public class ENodebFactoryTransform : EnumTransform<ENodebFactory>
    {
        public ENodebFactoryTransform() : base(ENodebFactory.Others)
        {
        }
    }

    internal static class ENodebFactoryTuples
    {
        public static Tuple<object, string>[] GetTuples()
        {
            return new[]
            {
                new Tuple<object, string>(ENodebFactory.Huawei, "华为"),
                new Tuple<object, string>(ENodebFactory.Zte, "中兴"),
                new Tuple<object, string>(ENodebFactory.Bell, "贝尔"),
                new Tuple<object, string>(ENodebFactory.Erisson, "爱立信"),
                new Tuple<object, string>(ENodebFactory.Nokia, "诺基亚"),
                new Tuple<object, string>(ENodebFactory.Datang, "大唐")
            };
        }
    }
}
