using System.Collections.Generic;

namespace Abp.EntityFramework.Entities.Test
{
    public class ZhangshangyouCoverageEquator : IEqualityComparer<ZhangshangyouCoverage>
    {
        public bool Equals(ZhangshangyouCoverage x, ZhangshangyouCoverage y)
        {
            return x!=null && y != null && x.Id == y.Id;
        }

        public int GetHashCode(ZhangshangyouCoverage obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}