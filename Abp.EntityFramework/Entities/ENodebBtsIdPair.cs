using System.Collections.Generic;
using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Common.Types;
using Lte.Domain.Excel;

namespace Abp.EntityFramework.Entities
{
    [AutoMapFrom(typeof(CellExcel))]
    public class ENodebBtsIdPair
    {
        public int ENodebId { get; set; }

        [AutoMapPropertyResolve("ShareCdmaInfo", typeof(CellExcel), typeof(SharedBtsIdTransform))]
        public int BtsId { get; set; }
    }

    public class ENodebBtsIdPairEquator : IEqualityComparer<ENodebBtsIdPair>
    {
        public bool Equals(ENodebBtsIdPair x, ENodebBtsIdPair y)
        {
            return x != null && y != null && x.ENodebId == y.ENodebId && x.BtsId == y.BtsId;
        }

        public int GetHashCode(ENodebBtsIdPair obj)
        {
            return obj.ENodebId.GetHashCode() * obj.BtsId.GetHashCode();
        }
    }
}