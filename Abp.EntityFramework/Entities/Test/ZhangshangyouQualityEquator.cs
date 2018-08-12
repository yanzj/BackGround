using System.Collections.Generic;

namespace Abp.EntityFramework.Entities.Test
{
    public class ZhangshangyouQualityEquator : IEqualityComparer<ZhangshangyouQuality>
    {
        public bool Equals(ZhangshangyouQuality x, ZhangshangyouQuality y)
        {
            return x != null && y != null && x.SerialNumber == y.SerialNumber;
        }

        public int GetHashCode(ZhangshangyouQuality obj)
        {
            return obj.SerialNumber.GetHashCode();
        }
    }
}
