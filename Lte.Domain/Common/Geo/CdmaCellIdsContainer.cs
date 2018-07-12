using System.Collections.Generic;
using Lte.Domain.Regular.Attributes;

namespace Lte.Domain.Common.Geo
{
    [TypeDoc("CDMA小区编号容器")]
    public class CdmaCellIdsContainer
    {
        [MemberDoc("小区编号列表")]
        public IEnumerable<CdmaCellIdPair> CellIdPairs { get; set; }
    }
}