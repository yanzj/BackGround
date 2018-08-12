using System;
using Abp.Domain.Entities;
using Abp.EntityFramework.Dependency;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Regular.Attributes;

namespace Abp.EntityFramework.Entities.Mr
{
    public class TopMrsRsrp : Entity, IStatDate, ILteCellQuery
    {
        [ArraySumProtection]
        public DateTime StatDate { get; set; }

        [ArraySumProtection]
        public int ENodebId { get; set; }

        [ArraySumProtection]
        public byte SectorId { get; set; }

        public long RsrpBelow120 { get; set; }

        public long Rsrp120To115 { get; set; }

        public long Rsrp115To110 { get; set; }

        public long Rsrp110To105 { get; set; }

        public long Rsrp105To100 { get; set; }

        public long Rsrp100To95 { get; set; }

        public long Rsrp95To90 { get; set; }

        public long Rsrp90To80 { get; set; }

        public long Rsrp80To70 { get; set; }

        public long Rsrp70To60 { get; set; }

        public long RsrpAbove60 { get; set; }
    }
}
