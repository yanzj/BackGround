using System;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Dependency;
using Abp.EntityFramework.Entities.Test;
using AutoMapper;
using Lte.Domain.Common.Wireless;
using Lte.MySqlFramework.Abstract.Infrastructure;

namespace Lte.MySqlFramework.Entities.Mr
{
    [AutoMapFrom(typeof(CoverageStat))]
    public class CoverageStatView : IStatDate, ILteCellQuery, IENodebName
    {
        public DateTime StatDate { get; set; }

        public int ENodebId { get; set; }

        public byte SectorId { get; set; }

        ////////aaaa

        public int CoverageMrs { get; set; }
        
        public double CoverageSum { get; set; }

        public int CoverageAbove95 { get; set; }

        public int CoverageAbove100 { get; set; }

        public int CoverageAbove105 { get; set; }

        public int CoverageAbove110 { get; set; }

        public int CoverageAbove115 { get; set; }

        public double CoverageAbove95Rate => CoverageMrs == 0 ? 0 : (double)CoverageAbove95 * 100 / CoverageMrs;

        public double CoverageAbove100Rate => CoverageMrs == 0 ? 0 : (double)CoverageAbove100 * 100 / CoverageMrs;

        public double CoverageAbove105Rate => CoverageMrs == 0 ? 0 : (double)CoverageAbove105 * 100 / CoverageMrs;
        
        public double CoverageAbove110Rate => CoverageMrs == 0 ? 0 : (double) CoverageAbove110 * 100 / CoverageMrs;
        
        public double CoverageAbove115Rate => CoverageMrs == 0 ? 0 : (double)CoverageAbove115 * 100 / CoverageMrs;
        
        public double AverageCoverageRsrp => CoverageMrs == 0 ? 0 : CoverageSum / CoverageMrs;

        public int CoverageWeak { get; set; }

        public int CoverageTotal { get; set; }

        public double CoverageWeakRate => CoverageTotal == 0 ? 0 : CoverageWeak / CoverageTotal;

        ////////aaaa

        public int TelecomMrs { get; set; }

        public double TelecomSum { get; set; }

        public int TelecomAbove95 { get; set; }

        public int TelecomAbove100 { get; set; }

        public int TelecomAbove105 { get; set; }

        public int TelecomAbove110 { get; set; }

        public int TelecomAbove115 { get; set; }

        public double TelecomAbove95Rate => TelecomMrs == 0 ? 0 : (double)TelecomAbove95 * 100 / TelecomMrs;

        public double TelecomAbove100Rate => TelecomMrs == 0 ? 0 : (double)TelecomAbove100 * 100 / TelecomMrs;

        public double TelecomAbove105Rate => TelecomMrs == 0 ? 0 : (double)TelecomAbove105 * 100 / TelecomMrs;

        public double TelecomAbove110Rate => TelecomMrs == 0 ? 0 : (double)TelecomAbove110 * 100 / TelecomMrs;

        public double TelecomAbove115Rate => TelecomMrs == 0 ? 0 : (double)TelecomAbove115 * 100 / TelecomMrs;

        public double AverageTelecomRsrp => TelecomMrs == 0 ? 0 : TelecomSum / TelecomMrs;

        public int TelecomWeak { get; set; }

        public int TelecomTotal { get; set; }

        public double TelecomWeakRate => TelecomTotal == 0 ? 0 : TelecomWeak / TelecomTotal;

        ////////aaaa

        public int AccessMrs { get; set; }

        public double AccessSum { get; set; }

        public int AccessAbove95 { get; set; }

        public int AccessAbove100 { get; set; }

        public int AccessAbove105 { get; set; }

        public int AccessAbove110 { get; set; }

        public int AccessAbove115 { get; set; }

        public double AccessAbove95Rate => AccessMrs == 0 ? 0 : (double)AccessAbove95 * 100 / AccessMrs;

        public double AccessAbove100Rate => AccessMrs == 0 ? 0 : (double)AccessAbove100 * 100 / AccessMrs;

        public double AccessAbove105Rate => AccessMrs == 0 ? 0 : (double)AccessAbove105 * 100 / AccessMrs;

        public double AccessAbove110Rate => AccessMrs == 0 ? 0 : (double)AccessAbove110 * 100 / AccessMrs;

        public double AccessAbove115Rate => AccessMrs == 0 ? 0 : (double)AccessAbove115 * 100 / AccessMrs;

        public double AverageAccessRsrp => AccessMrs == 0 ? 0 : AccessSum / AccessMrs;

        public int AccessWeak { get; set; }

        public int AccessTotal { get; set; }

        public double AccessWeakRate => AccessTotal == 0 ? 0 : AccessWeak / AccessTotal;

        ////////aaaa

        public int AgpsCoverageMrs { get; set; }

        public double AgpsCoverageSum { get; set; }

        public int AgpsCoverageAbove95 { get; set; }

        public int AgpsCoverageAbove100 { get; set; }

        public int AgpsCoverageAbove105 { get; set; }

        public int AgpsCoverageAbove110 { get; set; }

        public int AgpsCoverageAbove115 { get; set; }

        public double AgpsCoverageAbove95Rate => AgpsCoverageMrs == 0 ? 0 : (double)AgpsCoverageAbove95 * 100 / AgpsCoverageMrs;

        public double AgpsCoverageAbove100Rate => AgpsCoverageMrs == 0 ? 0 : (double)AgpsCoverageAbove100 * 100 / AgpsCoverageMrs;

        public double AgpsCoverageAbove105Rate => AgpsCoverageMrs == 0 ? 0 : (double)AgpsCoverageAbove105 * 100 / AgpsCoverageMrs;

        public double AgpsCoverageAbove110Rate => AgpsCoverageMrs == 0 ? 0 : (double)AgpsCoverageAbove110 * 100 / AgpsCoverageMrs;

        public double AgpsCoverageAbove115Rate => AgpsCoverageMrs == 0 ? 0 : (double)AgpsCoverageAbove115 * 100 / AgpsCoverageMrs;

        public double AverageAgpsCoverageRsrp => AgpsCoverageMrs == 0 ? 0 : AgpsCoverageSum / AgpsCoverageMrs;

        public int AgpsCoverageWeak { get; set; }

        public int AgpsCoverageTotal { get; set; }

        public double AgpsCoverageWeakRate => AgpsCoverageTotal == 0 ? 0 : AgpsCoverageWeak / AgpsCoverageTotal;

        ////////aaaa

        public int AgpsTelecomMrs { get; set; }

        public double AgpsTelecomSum { get; set; }

        public int AgpsTelecomAbove95 { get; set; }

        public int AgpsTelecomAbove100 { get; set; }

        public int AgpsTelecomAbove105 { get; set; }

        public int AgpsTelecomAbove110 { get; set; }

        public int AgpsTelecomAbove115 { get; set; }

        public double AgpsTelecomAbove95Rate => AgpsTelecomMrs == 0 ? 0 : (double)AgpsTelecomAbove95 * 100 / AgpsTelecomMrs;

        public double AgpsTelecomAbove100Rate => AgpsTelecomMrs == 0 ? 0 : (double)AgpsTelecomAbove100 * 100 / AgpsTelecomMrs;

        public double AgpsTelecomAbove105Rate => AgpsTelecomMrs == 0 ? 0 : (double)AgpsTelecomAbove105 * 100 / AgpsTelecomMrs;

        public double AgpsTelecomAbove110Rate => AgpsTelecomMrs == 0 ? 0 : (double)AgpsTelecomAbove110 * 100 / AgpsTelecomMrs;

        public double AgpsTelecomAbove115Rate => AgpsTelecomMrs == 0 ? 0 : (double)AgpsTelecomAbove115 * 100 / AgpsTelecomMrs;

        public double AverageAgpsTelecomRsrp => AgpsTelecomMrs == 0 ? 0 : AgpsTelecomSum / AgpsTelecomMrs;

        public int AgpsTelecomWeak { get; set; }

        public int AgpsTelecomTotal { get; set; }

        public double AgpsTelecomWeakRate => AgpsTelecomTotal == 0 ? 0 : AgpsTelecomWeak / AgpsTelecomTotal;

        ////////aaaa

        public int AgpsAccessMrs { get; set; }

        public double AgpsAccessSum { get; set; }

        public int AgpsAccessAbove95 { get; set; }

        public int AgpsAccessAbove100 { get; set; }

        public int AgpsAccessAbove105 { get; set; }

        public int AgpsAccessAbove110 { get; set; }

        public int AgpsAccessAbove115 { get; set; }

        public double AgpsAccessAbove95Rate => AgpsAccessMrs == 0 ? 0 : (double)AgpsAccessAbove95 * 100 / AgpsAccessMrs;

        public double AgpsAccessAbove100Rate => AgpsAccessMrs == 0 ? 0 : (double)AgpsAccessAbove100 * 100 / AgpsAccessMrs;

        public double AgpsAccessAbove105Rate => AgpsAccessMrs == 0 ? 0 : (double)AgpsAccessAbove105 * 100 / AgpsAccessMrs;

        public double AgpsAccessAbove110Rate => AgpsAccessMrs == 0 ? 0 : (double)AgpsAccessAbove110 * 100 / AgpsAccessMrs;

        public double AgpsAccessAbove115Rate => AgpsAccessMrs == 0 ? 0 : (double)AgpsAccessAbove115 * 100 / AgpsAccessMrs;

        public double AverageAgpsAccessRsrp => AgpsAccessMrs == 0 ? 0 : AgpsAccessSum / AgpsAccessMrs;

        public int AgpsAccessWeak { get; set; }

        public int AgpsAccessTotal { get; set; }

        public double AgpsAccessWeakRate => AgpsAccessTotal == 0 ? 0 : AgpsAccessWeak / AgpsAccessTotal;

        public string ENodebName { get; set; }

        public static CoverageStatView ConstructView(CoverageStat stat, IENodebRepository repository)
        {
            var view = Mapper.Map<CoverageStat, CoverageStatView>(stat);
            var eNodeb = repository.FirstOrDefault(x => x.ENodebId == stat.ENodebId);
            view.ENodebName = eNodeb?.Name;
            return view;
        }

    }
}