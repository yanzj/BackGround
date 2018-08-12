using System;
using Abp.Domain.Entities;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Dependency;
using Abp.EntityFramework.Entities.Test;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Regular.Attributes;

namespace Abp.EntityFramework.Entities.RegionKpi
{
    [AutoMapFrom(typeof(CoverageStat))]
    public class TownCoverageStat: Entity, ITownId, IStatDate
    {
        [ArraySumProtection]
        public int TownId { get; set; }

        [ArraySumProtection]
        public DateTime StatDate { get; set; }

        public long CoverageMrs { get; set; }

        public double CoverageSum { get; set; }

        public long CoverageAbove95 { get; set; }

        public long CoverageAbove100 { get; set; }

        public long CoverageAbove105 { get; set; }

        public long CoverageAbove110 { get; set; }

        public long CoverageAbove115 { get; set; }

        public long CoverageWeak { get; set; }

        public long CoverageTotal { get; set; }

        ////////////////////aaa

        public long TelecomMrs { get; set; }

        public double TelecomSum { get; set; }

        public long TelecomAbove95 { get; set; }

        public long TelecomAbove100 { get; set; }

        public long TelecomAbove105 { get; set; }

        public long TelecomAbove110 { get; set; }

        public long TelecomAbove115 { get; set; }

        public long TelecomWeak { get; set; }

        public long TelecomTotal { get; set; }

        ////////////////////aaa

        public long AccessMrs { get; set; }

        public double AccessSum { get; set; }

        public long AccessAbove95 { get; set; }

        public long AccessAbove100 { get; set; }

        public long AccessAbove105 { get; set; }

        public long AccessAbove110 { get; set; }

        public long AccessAbove115 { get; set; }

        public long AccessWeak { get; set; }

        public long AccessTotal { get; set; }

        //////////////////aaaaav

        public long AgpsCoverageMrs { get; set; }

        public double AgpsCoverageSum { get; set; }

        public long AgpsCoverageAbove95 { get; set; }

        public long AgpsCoverageAbove100 { get; set; }

        public long AgpsCoverageAbove105 { get; set; }

        public long AgpsCoverageAbove110 { get; set; }

        public long AgpsCoverageAbove115 { get; set; }

        public long AgpsCoverageWeak { get; set; }

        public long AgpsCoverageTotal { get; set; }

        ////////////////////aaa

        public long AgpsTelecomMrs { get; set; }

        public double AgpsTelecomSum { get; set; }

        public long AgpsTelecomAbove95 { get; set; }

        public long AgpsTelecomAbove100 { get; set; }

        public long AgpsTelecomAbove105 { get; set; }

        public long AgpsTelecomAbove110 { get; set; }

        public long AgpsTelecomAbove115 { get; set; }

        public long AgpsTelecomWeak { get; set; }

        public long AgpsTelecomTotal { get; set; }

        ////////////////////aaa

        public long AgpsAccessMrs { get; set; }

        public double AgpsAccessSum { get; set; }

        public long AgpsAccessAbove95 { get; set; }

        public long AgpsAccessAbove100 { get; set; }

        public long AgpsAccessAbove105 { get; set; }

        public long AgpsAccessAbove110 { get; set; }

        public long AgpsAccessAbove115 { get; set; }

        public long AgpsAccessWeak { get; set; }

        public long AgpsAccessTotal { get; set; }

    }
}
