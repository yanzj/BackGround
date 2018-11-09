using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities.Mr;
using AutoMapper;
using Lte.Domain.Common.Wireless;
using Lte.MySqlFramework.Abstract.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lte.MySqlFramework.Entities.Mr
{
    [AutoMapFrom(typeof(TopMrsSinrUl))]
    public class TopMrsSinrUlView : ILteCellQuery
    {
        public int ENodebId { get; set; }

        public byte SectorId { get; set; }

        public string ENodebName { get; set; }

        public string CellName => ENodebName + "-" + SectorId;

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

        public long SinrUlAbove15 => SinrUlAbove24 + SinrUl21To24 + SinrUl18To21 + SinrUl15To18;

        public long SinrUlAbove9 => SinrUlAbove15 + SinrUl12To15 + SinrUl9To12;

        public long SinrUlAbove3 => SinrUlAbove9 + SinrUl6To9 + SinrUl3To6;

        public long SinrUlAbove0 => SinrUlAbove3 + SinrUl0To3;

        public long SinrUlAboveM3 => SinrUlAbove0 + SinrUlM3To0;

        public long TotalMrs => SinrUlAboveM3 + SinrUlM6ToM3 + SinrUlM9ToM6 + SinrUlBelowM9;

        public double CoverageAbove15 => TotalMrs == 0 ? 0 : (double)SinrUlAbove15 / TotalMrs;

        public double CoverageAbove9 => TotalMrs == 0 ? 0 : (double)SinrUlAbove9 / TotalMrs;

        public double CoverageAbove3 => TotalMrs == 0 ? 0 : (double)SinrUlAbove3 / TotalMrs;

        public double CoverageAbove0 => TotalMrs == 0 ? 0 : (double)SinrUlAbove0 / TotalMrs;

        public double CoverageAboveM3 => TotalMrs == 0 ? 0 : (double)SinrUlAboveM3 / TotalMrs;

        public int TopDates { get; set; }

        public static TopMrsSinrUlView ConstructView(TopMrsSinrUl stat, IENodebRepository repository)
        {
            var view = Mapper.Map<TopMrsSinrUl, TopMrsSinrUlView>(stat);
            var eNodeb = repository.FirstOrDefault(x => x.ENodebId == stat.ENodebId);
            view.ENodebName = eNodeb?.Name;
            return view;
        }
    }
}
