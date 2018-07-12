using System.Collections.Generic;
using Lte.Domain.Regular.Attributes;

namespace Lte.Domain.Common.Geo
{
    [TypeDoc("指定扇区查询范围条件")]
    public class SectorRangeContainer
    {
        [MemberDoc("西边经度")]
        public double West { get; set; }

        [MemberDoc("东边经度")]
        public double East { get; set; }

        [MemberDoc("南边纬度")]
        public double South { get; set; }

        [MemberDoc(("北边纬度"))]
        public double North { get; set; }

        [MemberDoc("需要排除的小区列表")]
        public IEnumerable<CellIdPair> ExcludedCells { get; set; }
    }
}