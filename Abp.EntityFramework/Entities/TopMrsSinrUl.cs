using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using Abp.EntityFramework.Dependency;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Regular.Attributes;

namespace Abp.EntityFramework.Entities
{
    public class TopMrsSinrUl : Entity, IStatDate, ILteCellQuery
    {
        [ArraySumProtection]
        public DateTime StatDate { get; set; }

        [ArraySumProtection]
        public int ENodebId { get; set; }

        [ArraySumProtection]
        public byte SectorId { get; set; }
        
        public long SinrUlBelowM9 { get; set; }

        public long SinrUlM9ToM6 { get; set; }

        public long SinrUlM6ToM3 { get; set; }

        public long SinrUlM3To0 { get; set; }

        public long SinrUl0To3 { get; set; }

        public long SinrUl3To6 { get; set; }

        public long SinrUl6To9 { get; set; }

        public long SinrUl9To12 { get; set; }

        public long SinrUl12To15 { get; set; }

        public long SinrUl15To18 { get; set; }

        public long SinrUl18To21 { get; set; }

        public long SinrUl21To24 { get; set; }

        public long SinrUlAbove24 { get; set; }

    }
}
