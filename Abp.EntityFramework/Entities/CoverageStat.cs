using System;
using Abp.Domain.Entities;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Dependency;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Excel;
using Lte.Domain.Regular.Attributes;

namespace Abp.EntityFramework.Entities
{
    [AutoMapFrom(typeof(CoverageStatExcel))]
    public class CoverageStat: Entity, IStatDate, ILteCellQuery
    {
        [ArraySumProtection]
        public DateTime StatDate { get; set; }

        [ArraySumProtection]
        public int ENodebId { get; set; }

        [ArraySumProtection]
        public byte SectorId { get; set; }

        public int CoverageMrs { get; set; }

        public double CoverageSum { get; set; }

        public int CoverageAbove95 { get; set; }

        public int CoverageAbove100 { get; set; }

        public int CoverageAbove105 { get; set; }

        public int CoverageAbove110 { get; set; }

        public int CoverageAbove115 { get; set; }

        public int CoverageWeak { get; set; }

        public int CoverageTotal { get; set; }

        ////////////////////aaa

        public int TelecomMrs { get; set; }
        
        public double TelecomSum { get; set; }

        public int TelecomAbove95 { get; set; }

        public int TelecomAbove100 { get; set; }

        public int TelecomAbove105 { get; set; }
        
        public int TelecomAbove110 { get; set; }
        
        public int TelecomAbove115 { get; set; }

        public int TelecomWeak { get; set; }

        public int TelecomTotal { get; set; }

        ////////////////////aaa

        public int AccessMrs { get; set; }

        public double AccessSum { get; set; }

        public int AccessAbove95 { get; set; }

        public int AccessAbove100 { get; set; }

        public int AccessAbove105 { get; set; }

        public int AccessAbove110 { get; set; }

        public int AccessAbove115 { get; set; }

        public int AccessWeak { get; set; }

        public int AccessTotal { get; set; }

        //////////////////aaaaav
         
        public int AgpsCoverageMrs { get; set; }

        public double AgpsCoverageSum { get; set; }

        public int AgpsCoverageAbove95 { get; set; }

        public int AgpsCoverageAbove100 { get; set; }

        public int AgpsCoverageAbove105 { get; set; }

        public int AgpsCoverageAbove110 { get; set; }

        public int AgpsCoverageAbove115 { get; set; }

        public int AgpsCoverageWeak { get; set; }

        public int AgpsCoverageTotal { get; set; }

        ////////////////////aaa

        public int AgpsTelecomMrs { get; set; }

        public double AgpsTelecomSum { get; set; }

        public int AgpsTelecomAbove95 { get; set; }

        public int AgpsTelecomAbove100 { get; set; }

        public int AgpsTelecomAbove105 { get; set; }

        public int AgpsTelecomAbove110 { get; set; }

        public int AgpsTelecomAbove115 { get; set; }

        public int AgpsTelecomWeak { get; set; }

        public int AgpsTelecomTotal { get; set; }

        ////////////////////aaa

        public int AgpsAccessMrs { get; set; }

        public double AgpsAccessSum { get; set; }

        public int AgpsAccessAbove95 { get; set; }

        public int AgpsAccessAbove100 { get; set; }

        public int AgpsAccessAbove105 { get; set; }

        public int AgpsAccessAbove110 { get; set; }

        public int AgpsAccessAbove115 { get; set; }

        public int AgpsAccessWeak { get; set; }

        public int AgpsAccessTotal { get; set; }

    }
}
