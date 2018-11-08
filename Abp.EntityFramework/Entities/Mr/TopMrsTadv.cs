using Abp.Domain.Entities;
using Abp.EntityFramework.Dependency;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Regular.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abp.EntityFramework.Entities.Mr
{
    public class TopMrsTadv : Entity, IStatDate, ILteCellQuery
    {
        [ArraySumProtection]
        public DateTime StatDate { get; set; }

        [ArraySumProtection]
        public int ENodebId { get; set; }

        [ArraySumProtection]
        public byte SectorId { get; set; }

        public int TadvBelow1 { get; set; }

        public int Tadv1To2 { get; set; }

        public int Tadv2To3 { get; set; }

        public int Tadv3To4 { get; set; }

        public int Tadv4To6 { get; set; }

        public int Tadv6To8 { get; set; }

        public int Tadv8To10 { get; set; }

        public int Tadv10To12 { get; set; }

        public int Tadv12To14 { get; set; }

        public int Tadv14To16 { get; set; }

        public int Tadv16To18 { get; set; }

        public int Tadv18To20 { get; set; }

        public int Tadv20To24 { get; set; }

        public int Tadv24To28 { get; set; }

        public int Tadv28To32 { get; set; }

        public int Tadv32To36 { get; set; }

        public int Tadv36To42 { get; set; }

        public int Tadv42To48 { get; set; }

        public int Tadv48To54 { get; set; }

        public int Tadv54To60 { get; set; }

        public int Tadv60To80 { get; set; }

        public int Tadv80To112 { get; set; }

        public int Tadv112To192 { get; set; }

        public int TadvAbove192 { get; set; }
        
        public int TadvAbove112 => Tadv112To192 + TadvAbove192;

        public int TadvAbove60 => TadvAbove112 + Tadv60To80 + Tadv80To112;

        public int TadvAbove42 => TadvAbove60 + Tadv42To48 + Tadv48To54 + Tadv54To60;

        public int TadvAbove24 => TadvAbove42 + Tadv24To28 + Tadv28To32 + Tadv32To36 + Tadv36To42;

        public int TadvAbove16 => TadvAbove24 + Tadv16To18 + Tadv18To20 + Tadv20To24;

        public int TadvAbove10 => TadvAbove16 + Tadv10To12 + Tadv12To14 + Tadv14To16;

        public int TadvAbove6 => TadvAbove10 + Tadv6To8 + Tadv8To10;

        public int TotalMrs => TadvAbove6 + TadvBelow1 + Tadv1To2 + Tadv2To3 + Tadv3To4 + Tadv4To6;
        
        public double CoverageAbove112 => TotalMrs == 0 ? 0 : (double) TadvAbove112 / TotalMrs;
        
        public double CoverageAbove60 => TotalMrs == 0 ? 0 : (double) TadvAbove60 / TotalMrs;

        public double CoverageAbove42 => TotalMrs == 0 ? 0 : (double) TadvAbove42 / TotalMrs;

        public double CoverageAbove24 => TotalMrs == 0 ? 0 : (double) TadvAbove24 / TotalMrs;

        public double CoverageAbove16 => TotalMrs == 0 ? 0 : (double) TadvAbove16 / TotalMrs;
        
        public double CoverageAbove10 => TotalMrs == 0 ? 0 : (double) TadvAbove10 / TotalMrs;
        
        public double CoverageAbove6 => TotalMrs == 0 ? 0 : (double) TadvAbove6 / TotalMrs;

    }
}
