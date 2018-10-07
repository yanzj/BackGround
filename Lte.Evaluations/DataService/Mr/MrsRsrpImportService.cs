﻿using System;
using System.Collections.Generic;
using System.Linq;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities.Mr;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Common.Wireless.Cell;
using Lte.Evaluations.ViewModels.Precise;
using Lte.MySqlFramework.Abstract.Infrastructure;
using Lte.MySqlFramework.Abstract.Mr;
using Lte.Parameters.Abstract.Kpi;
using Lte.Evaluations.DataService.RegionKpi;

namespace Lte.Evaluations.DataService.Mr
{
    public class MrsRsrpImportService
    {
        private readonly IMrsRsrpRepository _mrsRsrpRepository;
        private readonly IENodebRepository _eNodebRepository;
        private readonly ITopMrsRsrpRepository _topMrsRsrpRepository;
        private readonly ICellRepository _cellRepository;

        private static Stack<TopMrsRsrp> TopStats { get; set; }

        public MrsRsrpImportService(IMrsRsrpRepository mrsRsrpRepository, IENodebRepository eNodebRepository,
            ICellRepository cellRepository, ITopMrsRsrpRepository topRepository)
        {
            _mrsRsrpRepository = mrsRsrpRepository;
            _eNodebRepository = eNodebRepository;
            _topMrsRsrpRepository = topRepository;
            _cellRepository = cellRepository;
            if (TopStats == null) TopStats = new Stack<TopMrsRsrp>();
        }

        public IEnumerable<TownMrsRsrp> GetMergeMrsStats(DateTime statTime, FrequencyBandType bandType)
        {
            var end = statTime.AddDays(1);
            var stats = _mrsRsrpRepository.GetAllList(x => x.StatDate >= statTime && x.StatDate < end);
            var townStats =
                stats.GetTownFrequencyStats<MrsRsrpStat, TownMrsRsrp>(bandType, _cellRepository, _eNodebRepository);

            var mergeStats = from stat in townStats
                group stat by stat.TownId
                into g
                select new TownMrsRsrp
                {
                    TownId = g.Key,
                    StatDate = statTime,
                    Rsrp100To95 = g.Sum(x => x.Rsrp100To95),
                    Rsrp105To100 = g.Sum(x => x.Rsrp105To100),
                    Rsrp110To105 = g.Sum(x => x.Rsrp110To105),
                    Rsrp115To110 = g.Sum(x => x.Rsrp115To110),
                    Rsrp120To115 = g.Sum(x => x.Rsrp120To115),
                    Rsrp70To60 = g.Sum(x => x.Rsrp70To60),
                    Rsrp80To70 = g.Sum(x => x.Rsrp80To70),
                    Rsrp90To80 = g.Sum(x => x.Rsrp90To80),
                    Rsrp95To90 = g.Sum(x => x.Rsrp95To90),
                    RsrpAbove60 = g.Sum(x => x.RsrpAbove60),
                    RsrpBelow120 = g.Sum(x => x.RsrpBelow120),
                    FrequencyBandType = bandType
                };
            return mergeStats;
        }

        public int GetTopMrsRsrps(DateTime statTime)
        {
            var end = statTime.AddDays(1);
            var stats = _mrsRsrpRepository.GetAllList(x => x.StatDate >= statTime && x.StatDate < end);
            var dtos = stats.Where(x => x.RSRP_00 + x.RSRP_01 > 5000).MapTo<List<CellMrsRsrpDto>>();
            var items = dtos.MapTo<IEnumerable<TopMrsRsrp>>();
            foreach (var topMrsRsrp in items)
            {
                TopStats.Push(topMrsRsrp);
            }

            return TopStats.Count;
        }

        public int GetStatsToBeDump()
        {
            return TopStats.Count;
        }

        public void ClearStats()
        {
            TopStats.Clear();
        }

        public bool DumpOneStat()
        {
            var stat = TopStats.Pop();
            if (stat == null) return false;
            _topMrsRsrpRepository.ImportOne(stat);
            return true;
        }

    }
}
