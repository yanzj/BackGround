using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Dependency;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Mr;
using AutoMapper;
using Lte.Domain.Common.Wireless;
using Lte.MySqlFramework.Abstract;
using Lte.Parameters.Entities.Kpi;

namespace Lte.Evaluations.ViewModels.Precise
{
    [AutoMapFrom(typeof(MrsSinrUlStat))]
    [AutoMapTo(typeof(TopMrsSinrUl))]
    public class CellMrsSinrUlDto : IStatDate, ILteCellQuery
    {
        public DateTime StatDate { get; set; }

        public int ENodebId { get; set; }

        public byte SectorId { get; set; }

        public string ENodebName { get; set; }

        public string CellName => ENodebName + "-" + SectorId;

        public long SinrUL_00 { get; set; }

        public long SinrUL_01 { get; set; }

        public long SinrUL_02 { get; set; }

        public long SinrUL_03 { get; set; }

        public long SinrUL_04 { get; set; }

        public long SinrUL_05 { get; set; }

        public long SinrUL_06 { get; set; }

        public long SinrUL_07 { get; set; }

        public long SinrUL_08 { get; set; }

        public long SinrUL_09 { get; set; }

        public long SinrUL_10 { get; set; }

        public long SinrUL_11 { get; set; }

        public long SinrUL_12 { get; set; }

        public long SinrUL_13 { get; set; }

        public long SinrUL_14 { get; set; }

        public long SinrUL_15 { get; set; }

        public long SinrUL_16 { get; set; }

        public long SinrUL_17 { get; set; }

        public long SinrUL_18 { get; set; }

        public long SinrUL_19 { get; set; }

        public long SinrUL_20 { get; set; }

        public long SinrUL_21 { get; set; }

        public long SinrUL_22 { get; set; }

        public long SinrUL_23 { get; set; }

        public long SinrUL_24 { get; set; }

        public long SinrUL_25 { get; set; }

        public long SinrUL_26 { get; set; }

        public long SinrUL_27 { get; set; }

        public long SinrUL_28 { get; set; }

        public long SinrUL_29 { get; set; }

        public long SinrUL_30 { get; set; }

        public long SinrUL_31 { get; set; }

        public long SinrUL_32 { get; set; }

        public long SinrUL_33 { get; set; }

        public long SinrUL_34 { get; set; }

        public long SinrUL_35 { get; set; }

        public long SinrUL_36 { get; set; }

        public long SinrUlBelowM9 => SinrUL_00 + SinrUL_01;

        public long SinrUlM9ToM6 => SinrUL_02 + SinrUL_03 + SinrUL_04;

        public long SinrUlM6ToM3 => SinrUL_05 + SinrUL_06 + SinrUL_07;

        public long SinrUlM3To0 => SinrUL_08 + SinrUL_09 + SinrUL_10;

        public long SinrUl0To3 => SinrUL_11 + SinrUL_12 + SinrUL_13;

        public long SinrUl3To6 => SinrUL_14 + SinrUL_15 + SinrUL_16;

        public long SinrUl6To9 => SinrUL_17 + SinrUL_18 + SinrUL_19;

        public long SinrUl9To12 => SinrUL_20 + SinrUL_21 + SinrUL_22;

        public long SinrUl12To15 => SinrUL_23 + SinrUL_24 + SinrUL_25;

        public long SinrUl15To18 => SinrUL_26 + SinrUL_27 + SinrUL_18;

        public long SinrUl18To21 => SinrUL_29 + SinrUL_30 + SinrUL_31;

        public long SinrUl21To24 => SinrUL_32 + SinrUL_33 + SinrUL_34;

        public long SinrUlAbove24 => SinrUL_35 + SinrUL_36;

        public long TotalMrs => SinrUlBelowM9 + SinrUlM9ToM6 + SinrUlM6ToM3 +
                                SinrUlM3To0 + SinrUl0To3 + SinrUl3To6 + SinrUl6To9 + SinrUl9To12 + SinrUl12To15 +
                                SinrUl15To18 + SinrUl18To21 + SinrUl21To24 + SinrUlAbove24;

        public int TopDates { get; set; }

        public static CellMrsSinrUlDto ConstructView(MrsSinrUlStat stat, IENodebRepository repository)
        {
            var view = Mapper.Map<MrsSinrUlStat, CellMrsSinrUlDto>(stat);
            var eNodeb = repository.FirstOrDefault(x => x.ENodebId == stat.ENodebId);
            view.ENodebName = eNodeb?.Name;
            return view;
        }
    }
}
