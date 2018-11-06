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

    }
}
