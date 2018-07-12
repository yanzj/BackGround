using System.Collections.Generic;
using Lte.Domain.Regular.Attributes;

namespace Lte.Domain.Common.Geo
{
    public class ENodebRangeContainer
    {
        [MemberDoc("西边经度")]
        public double West { get; set; }

        [MemberDoc("东边经度")]
        public double East { get; set; }

        [MemberDoc("南边纬度")]
        public double South { get; set; }

        [MemberDoc(("北边纬度"))]
        public double North { get; set; }

        public IEnumerable<int> ExcludedIds { get; set; }
    }
}