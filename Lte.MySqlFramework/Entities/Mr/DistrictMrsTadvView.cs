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
        
        public long TadvAbove112 => Tadv112To192 + TadvAbove192;

        public long TadvAbove60 => TadvAbove112 + Tadv60To80 + Tadv80To112;

        public long TadvAbove42 => TadvAbove60 + Tadv42To48 + Tadv48To54 + Tadv54To60;

        public long TadvAbove24 => TadvAbove42 + Tadv24To28 + Tadv28To32 + Tadv32To36 + Tadv36To42;

        public long TadvAbove16 => TadvAbove24 + Tadv16To18 + Tadv18To20 + Tadv20To24;

        public long TadvAbove10 => TadvAbove16 + Tadv10To12 + Tadv12To14 + Tadv14To16;

        public long TadvAbove6 => TadvAbove10 + Tadv6To8 + Tadv8To10;

        public long TotalMrs => TadvAbove6 + TadvBelow1 + Tadv1To2 + Tadv2To3 + Tadv3To4 + Tadv4To6;
        
        public double CoverageAbove112 => TotalMrs == 0 ? 0 : (double) TadvAbove112 / TotalMrs;
        
        public double CoverageAbove60 => TotalMrs == 0 ? 0 : (double) TadvAbove60 / TotalMrs;

        public double CoverageAbove42 => TotalMrs == 0 ? 0 : (double) TadvAbove42 / TotalMrs;

        public double CoverageAbove24 => TotalMrs == 0 ? 0 : (double) TadvAbove24 / TotalMrs;

        public double CoverageAbove16 => TotalMrs == 0 ? 0 : (double) TadvAbove16 / TotalMrs;
        
        public double CoverageAbove10 => TotalMrs == 0 ? 0 : (double) TadvAbove10 / TotalMrs;
        
        public double CoverageAbove6 => TotalMrs == 0 ? 0 : (double) TadvAbove6 / TotalMrs;

        public static DistrictMrsTadvView ConstructView(TownMrsTadvView townView)
        {
            return townView.MapTo<DistrictMrsTadvView>();
        }
    }
}
