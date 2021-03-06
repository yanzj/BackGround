﻿using System;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Dependency;
using Abp.EntityFramework.Entities.Mr;
using AutoMapper;
using Lte.Domain.Common.Wireless;
using Lte.MySqlFramework.Abstract.Infrastructure;

namespace Lte.Evaluations.ViewModels.Mr
{
    [AutoMapFrom(typeof(MrsRsrpStat))]
    [AutoMapTo(typeof(TopMrsRsrp))]
    public class CellMrsRsrpDto : IStatDate, ILteCellQuery, IENodebName
    {
        public DateTime StatDate { get; set; }

        public int ENodebId { get; set; }

        public byte SectorId { get; set; }

        public string ENodebName { get; set; }

        public string CellName => ENodebName + "-" + SectorId;

        public long RSRP_00 { get; set; }

        public long RSRP_01 { get; set; }

        public long RSRP_02 { get; set; }

        public long RSRP_03 { get; set; }

        public long RSRP_04 { get; set; }

        public long RSRP_05 { get; set; }

        public long RSRP_06 { get; set; }

        public long RSRP_07 { get; set; }

        public long RSRP_08 { get; set; }

        public long RSRP_09 { get; set; }

        public long RSRP_10 { get; set; }

        public long RSRP_11 { get; set; }

        public long RSRP_12 { get; set; }

        public long RSRP_13 { get; set; }

        public long RSRP_14 { get; set; }

        public long RSRP_15 { get; set; }

        public long RSRP_16 { get; set; }

        public long RSRP_17 { get; set; }

        public long RSRP_18 { get; set; }

        public long RSRP_19 { get; set; }

        public long RSRP_20 { get; set; }

        public long RSRP_21 { get; set; }

        public long RSRP_22 { get; set; }

        public long RSRP_23 { get; set; }

        public long RSRP_24 { get; set; }

        public long RSRP_25 { get; set; }

        public long RSRP_26 { get; set; }

        public long RSRP_27 { get; set; }

        public long RSRP_28 { get; set; }

        public long RSRP_29 { get; set; }

        public long RSRP_30 { get; set; }

        public long RSRP_31 { get; set; }

        public long RSRP_32 { get; set; }

        public long RSRP_33 { get; set; }

        public long RSRP_34 { get; set; }

        public long RSRP_35 { get; set; }

        public long RSRP_36 { get; set; }

        public long RSRP_37 { get; set; }

        public long RSRP_38 { get; set; }

        public long RSRP_39 { get; set; }

        public long RSRP_40 { get; set; }

        public long RSRP_41 { get; set; }

        public long RSRP_42 { get; set; }

        public long RSRP_43 { get; set; }

        public long RSRP_44 { get; set; }

        public long RSRP_45 { get; set; }

        public long RSRP_46 { get; set; }

        public long RSRP_47 { get; set; }

        public long RsrpBelow120 => RSRP_00;

        public long Rsrp120To115 => RSRP_01;

        public long Rsrp115To110 => RSRP_02 + RSRP_03 + RSRP_04 + RSRP_05 + RSRP_06;

        public long Rsrp110To105 => RSRP_07 + RSRP_08 + RSRP_09 + RSRP_10 + RSRP_11;

        public long Rsrp105To100 => RSRP_12 + RSRP_13 + RSRP_14 + RSRP_15 + RSRP_16;

        public long Rsrp100To95 => RSRP_17 + RSRP_18 + RSRP_19 + RSRP_20 + RSRP_21;

        public long Rsrp95To90 => RSRP_22 + RSRP_23 + RSRP_24 + RSRP_25 + RSRP_26;

        public long Rsrp90To80
            => RSRP_27 + RSRP_28 + RSRP_29 + RSRP_30 + RSRP_31 + RSRP_32 + RSRP_33 + RSRP_34 + RSRP_35 + RSRP_36;

        public long Rsrp80To70 => RSRP_37 + RSRP_38 + RSRP_39 + RSRP_40 + RSRP_41;

        public long Rsrp70To60 => RSRP_42 + RSRP_43 + RSRP_44 + RSRP_45 + RSRP_46;

        public long RsrpAbove60 => RSRP_47;

        public long TotalMrs => RsrpBelow120 + Rsrp120To115 + Rsrp115To110 + Rsrp110To105 + Rsrp105To100 + Rsrp100To95
                                + Rsrp95To90 + Rsrp90To80 + Rsrp80To70 + Rsrp70To60 + RsrpAbove60;

        public double MrsBelow115 => RsrpBelow120 + Rsrp120To115;

        public double CoverageRate115 => 100 - 100 * MrsBelow115 / TotalMrs;

        public double MrsBelow110 => MrsBelow115 + Rsrp115To110;

        public double CoverageRate110 => 100 - 100 * MrsBelow110 / TotalMrs;

        public double MrsBelow105 => MrsBelow110 + Rsrp110To105;

        public double CoverageRate105 => 100 - 100 * MrsBelow105 / TotalMrs;

        public double MrsBelow100 => MrsBelow105 + Rsrp105To100;

        public double CoverageRate100 => 100 - 100 * MrsBelow100 / TotalMrs;

        public int TopDates { get; set; }

        public static CellMrsRsrpDto ConstructView(MrsRsrpStat stat, IENodebRepository repository)
        {
            var view = Mapper.Map<MrsRsrpStat, CellMrsRsrpDto>(stat);
            var eNodeb = repository.FirstOrDefault(x => x.ENodebId == stat.ENodebId);
            view.ENodebName = eNodeb?.Name;
            return view;
        }
    }
}