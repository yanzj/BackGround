using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Dependency;
using Lte.Domain.Common.Wireless;

namespace Lte.MySqlFramework.Entities.Mr
{
    [AutoMapFrom(typeof(TownMrsTadvView))]
    public class DistrictMrsTadvView : ICityDistrict, IStatDate
    {
        public string District { get; set; }

        public string City { get; set; }

        public DateTime StatDate { get; set; }
        
        public long TadvBelow1 { get; set; }

        public long Tadv1To2 { get; set; }

        public long Tadv2To3 { get; set; }

        public long Tadv3To4 { get; set; }

        public long Tadv4To6 { get; set; }

        public long Tadv6To8 { get; set; }

        public long Tadv8To10 { get; set; }

        public long Tadv10To12 { get; set; }

        public long Tadv12To14 { get; set; }

        public long Tadv14To16 { get; set; }

        public long Tadv16To18 { get; set; }

        public long Tadv18To20 { get; set; }

        public long Tadv20To24 { get; set; }

        public long Tadv24To28 { get; set; }

        public long Tadv28To32 { get; set; }

        public long Tadv32To36 { get; set; }

        public long Tadv36To42 { get; set; }

        public long Tadv42To48 { get; set; }

        public long Tadv48To54 { get; set; }

        public long Tadv54To60 { get; set; }

        public long Tadv60To80 { get; set; }

        public long Tadv80To112 { get; set; }

        public long Tadv112To192 { get; set; }

        public long TadvAbove192 { get; set; }
        
        public static DistrictMrsTadvView ConstructView(TownMrsTadvView townView)
        {
            return townView.MapTo<DistrictMrsTadvView>();
        }
    }
}
