using System.Collections.Generic;
using Lte.Domain.Regular.Attributes;

namespace Lte.Domain.Common.Geo
{
    [TypeDoc("基站编号容器")]
    public class ENodebIdsContainer
    {
        [MemberDoc("基站编号列表")]
        public IEnumerable<int> ENodebIds { get; set; }
    }
}