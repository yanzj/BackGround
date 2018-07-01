using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abp.EntityFramework.Entities
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
