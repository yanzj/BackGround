using System.Collections.Generic;
using Lte.Domain.Regular.Attributes;

namespace Lte.Domain.Common.Geo
{
    [TypeDoc("小区编号容器")]
    public class CellIdsContainer
    {
        [MemberDoc("小区编号列表")]
        public IEnumerable<CellIdPair> CellIdPairs { get; set; }
    }
}