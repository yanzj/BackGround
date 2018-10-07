using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Dependency;
using Abp.EntityFramework.Entities.RegionKpi;
using Lte.Domain.Regular.Attributes;

namespace Abp.EntityFramework.Entities.Mr
{
    [AutoMapFrom(typeof(TownCoverageStat))]
    [TypeDoc("聚合MRS覆盖统计视图")]
    public class AggregateCoverageView : IStatDate
    {
        [MemberDoc("小区个数")]
        public int CellCount { get; set; }
        
        public string Name { get; set; }
        
        public DateTime StatDate { get; set; }
        
        public long CoverageMrs { get; set; }

        public double CoverageSum { get; set; }

        public long CoverageAbove95 { get; set; }

        public long CoverageAbove100 { get; set; }

        public long CoverageAbove105 { get; set; }

        public long CoverageAbove110 { get; set; }

        public long CoverageAbove115 { get; set; }
        
        public double CoverageAbove95Rate => CoverageMrs == 0 ? 0 : (double)CoverageAbove95 * 100 / CoverageMrs;

        public double CoverageAbove100Rate => CoverageMrs == 0 ? 0 : (double)CoverageAbove100 * 100 / CoverageMrs;

        public double CoverageAbove105Rate => CoverageMrs == 0 ? 0 : (double)CoverageAbove105 * 100 / CoverageMrs;

        public double CoverageAbove110Rate => CoverageMrs == 0 ? 0 : (double)CoverageAbove110 * 100 / CoverageMrs;

        public double CoverageAbove115Rate => CoverageMrs == 0 ? 0 : (double)CoverageAbove115 * 100 / CoverageMrs;

        public double AverageCoverageRsrp => CoverageMrs == 0 ? 0 : CoverageSum / CoverageMrs;

        public long CoverageWeak { get; set; }

        public long CoverageTotal { get; set; }
        
        public double CoverageWeakRate => CoverageTotal == 0 ? 0 : CoverageWeak / CoverageTotal;

        ////////////////////aaa

        public long TelecomMrs { get; set; }

        public double TelecomSum { get; set; }

        public long TelecomAbove95 { get; set; }

        public long TelecomAbove100 { get; set; }

        public long TelecomAbove105 { get; set; }

        public long TelecomAbove110 { get; set; }

        public long TelecomAbove115 { get; set; }
        
        public double TelecomAbove95Rate => TelecomMrs == 0 ? 0 : (double)TelecomAbove95 * 100 / TelecomMrs;

        public double TelecomAbove100Rate => TelecomMrs == 0 ? 0 : (double)TelecomAbove100 * 100 / TelecomMrs;

        public double TelecomAbove105Rate => TelecomMrs == 0 ? 0 : (double)TelecomAbove105 * 100 / TelecomMrs;

        public double TelecomAbove110Rate => TelecomMrs == 0 ? 0 : (double)TelecomAbove110 * 100 / TelecomMrs;

        public double TelecomAbove115Rate => TelecomMrs == 0 ? 0 : (double)TelecomAbove115 * 100 / TelecomMrs;

        public double AverageTelecomRsrp => TelecomMrs == 0 ? 0 : TelecomSum / TelecomMrs;

        public long TelecomWeak { get; set; }

        public long TelecomTotal { get; set; }
        
        public double TelecomWeakRate => TelecomTotal == 0 ? 0 : TelecomWeak / TelecomTotal;

        ////////////////////aaa

        public long AccessMrs { get; set; }

        public double AccessSum { get; set; }

        public long AccessAbove95 { get; set; }

        public long AccessAbove100 { get; set; }

        public long AccessAbove105 { get; set; }

        public long AccessAbove110 { get; set; }

        public long AccessAbove115 { get; set; }
        
        public double AccessAbove95Rate => AccessMrs == 0 ? 0 : (double)AccessAbove95 * 100 / AccessMrs;

        public double AccessAbove100Rate => AccessMrs == 0 ? 0 : (double)AccessAbove100 * 100 / AccessMrs;

        public double AccessAbove105Rate => AccessMrs == 0 ? 0 : (double)AccessAbove105 * 100 / AccessMrs;

        public double AccessAbove110Rate => AccessMrs == 0 ? 0 : (double)AccessAbove110 * 100 / AccessMrs;

        public double AccessAbove115Rate => AccessMrs == 0 ? 0 : (double)AccessAbove115 * 100 / AccessMrs;

        public double AverageAccessRsrp => AccessMrs == 0 ? 0 : AccessSum / AccessMrs;

        public long AccessWeak { get; set; }

        public long AccessTotal { get; set; }
        
        public double AccessWeakRate => AccessTotal == 0 ? 0 : AccessWeak / AccessTotal;

        //////////////////aaaaav

        public long AgpsCoverageMrs { get; set; }

        public double AgpsCoverageSum { get; set; }

        public long AgpsCoverageAbove95 { get; set; }

        public long AgpsCoverageAbove100 { get; set; }

        public long AgpsCoverageAbove105 { get; set; }

        public long AgpsCoverageAbove110 { get; set; }

        public long AgpsCoverageAbove115 { get; set; }
        
        public double AgpsCoverageAbove95Rate => AgpsCoverageMrs == 0 ? 0 : (double)AgpsCoverageAbove95 * 100 / AgpsCoverageMrs;

        public double AgpsCoverageAbove100Rate => AgpsCoverageMrs == 0 ? 0 : (double)AgpsCoverageAbove100 * 100 / AgpsCoverageMrs;

        public double AgpsCoverageAbove105Rate => AgpsCoverageMrs == 0 ? 0 : (double)AgpsCoverageAbove105 * 100 / AgpsCoverageMrs;

        public double AgpsCoverageAbove110Rate => AgpsCoverageMrs == 0 ? 0 : (double)AgpsCoverageAbove110 * 100 / AgpsCoverageMrs;

        public double AgpsCoverageAbove115Rate => AgpsCoverageMrs == 0 ? 0 : (double)AgpsCoverageAbove115 * 100 / AgpsCoverageMrs;

        public double AverageAgpsCoverageRsrp => AgpsCoverageMrs == 0 ? 0 : AgpsCoverageSum / AgpsCoverageMrs;

        public long AgpsCoverageWeak { get; set; }

        public long AgpsCoverageTotal { get; set; }
        
        public double AgpsCoverageWeakRate => AgpsCoverageTotal == 0 ? 0 : AgpsCoverageWeak / AgpsCoverageTotal;

        ////////////////////aaa

        public long AgpsTelecomMrs { get; set; }

        public double AgpsTelecomSum { get; set; }

        public long AgpsTelecomAbove95 { get; set; }

        public long AgpsTelecomAbove100 { get; set; }

        public long AgpsTelecomAbove105 { get; set; }

        public long AgpsTelecomAbove110 { get; set; }

        public long AgpsTelecomAbove115 { get; set; }
        
        public double AgpsTelecomAbove95Rate => AgpsTelecomMrs == 0 ? 0 : (double)AgpsTelecomAbove95 * 100 / AgpsTelecomMrs;

        public double AgpsTelecomAbove100Rate => AgpsTelecomMrs == 0 ? 0 : (double)AgpsTelecomAbove100 * 100 / AgpsTelecomMrs;

        public double AgpsTelecomAbove105Rate => AgpsTelecomMrs == 0 ? 0 : (double)AgpsTelecomAbove105 * 100 / AgpsTelecomMrs;

        public double AgpsTelecomAbove110Rate => AgpsTelecomMrs == 0 ? 0 : (double)AgpsTelecomAbove110 * 100 / AgpsTelecomMrs;

        public double AgpsTelecomAbove115Rate => AgpsTelecomMrs == 0 ? 0 : (double)AgpsTelecomAbove115 * 100 / AgpsTelecomMrs;

        public double AverageAgpsTelecomRsrp => AgpsTelecomMrs == 0 ? 0 : AgpsTelecomSum / AgpsTelecomMrs;

        public long AgpsTelecomWeak { get; set; }

        public long AgpsTelecomTotal { get; set; }
        
        public double AgpsTelecomWeakRate => AgpsTelecomTotal == 0 ? 0 : AgpsTelecomWeak / AgpsTelecomTotal;

        ////////////////////aaa

        public long AgpsAccessMrs { get; set; }

        public double AgpsAccessSum { get; set; }

        public long AgpsAccessAbove95 { get; set; }

        public long AgpsAccessAbove100 { get; set; }

        public long AgpsAccessAbove105 { get; set; }

        public long AgpsAccessAbove110 { get; set; }

        public long AgpsAccessAbove115 { get; set; }
        
        public double AgpsAccessAbove95Rate => AgpsAccessMrs == 0 ? 0 : (double)AgpsAccessAbove95 * 100 / AgpsAccessMrs;

        public double AgpsAccessAbove100Rate => AgpsAccessMrs == 0 ? 0 : (double)AgpsAccessAbove100 * 100 / AgpsAccessMrs;

        public double AgpsAccessAbove105Rate => AgpsAccessMrs == 0 ? 0 : (double)AgpsAccessAbove105 * 100 / AgpsAccessMrs;

        public double AgpsAccessAbove110Rate => AgpsAccessMrs == 0 ? 0 : (double)AgpsAccessAbove110 * 100 / AgpsAccessMrs;

        public double AgpsAccessAbove115Rate => AgpsAccessMrs == 0 ? 0 : (double)AgpsAccessAbove115 * 100 / AgpsAccessMrs;

        public double AverageAgpsAccessRsrp => AgpsAccessMrs == 0 ? 0 : AgpsAccessSum / AgpsAccessMrs;

        public long AgpsAccessWeak { get; set; }

        public long AgpsAccessTotal { get; set; }
        
        public double AgpsAccessWeakRate => AgpsAccessTotal == 0 ? 0 : AgpsAccessWeak / AgpsAccessTotal;

    }
}
