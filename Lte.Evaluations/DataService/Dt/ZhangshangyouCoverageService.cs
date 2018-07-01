﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Common;
using Lte.Domain.LinqToCsv.Context;
using Lte.Domain.LinqToCsv.Description;
using Lte.MySqlFramework.Abstract;
using Lte.MySqlFramework.Entities;
using Remotion.Data.Linq.Utilities;

namespace Lte.Evaluations.DataService.Dt
{
    public class ZhangshangyouCoverageService
    {
        private readonly IZhangshangyouCoverageRepository _repository;

        public ZhangshangyouCoverageService(IZhangshangyouCoverageRepository repository)
        {
            _repository = repository;
            if (Stats == null)
            {
                Stats = new Stack<ZhangshangyouCoverageCsv>();
            }
        }

        private static Stack<ZhangshangyouCoverageCsv> Stats { get; set; }

        public void UploadStats(StreamReader reader)
        {
            var stats = CsvContext.Read<ZhangshangyouCoverageCsv>(reader, CsvFileDescription.CommaDescription)
                .ToList();
            foreach (var stat in stats)
            {
                Stats.Push(stat);
            }
            if (Stats.Count == 0)
                throw new ArgumentEmptyException("aaaa");
        }

        public async Task<bool> DumpOneStat()
        {
            var stat = Stats.Pop();
            if (stat == null) throw new NullReferenceException("coverage stat is null!");
            await _repository
                .UpdateOne<IZhangshangyouCoverageRepository, ZhangshangyouCoverage, ZhangshangyouCoverageCsv>(stat);
            return true;
        }

        public int GetStatsToBeDump()
        {
            return Stats.Count;
        }

        public void ClearStats()
        {
            Stats.Clear();
        }

        public IEnumerable<ZhangshangyouCoverageView> QueryByDateSpanAndRange(DateTime begin, DateTime end,
            double west, double east, double south, double north, double xOffset, double yOffset)
        {
            return _repository.GetAllList(
                    x => x.Longtitute >= west + xOffset && x.Longtitute < east + xOffset
                                                        && x.Lattitute >= south + yOffset &&
                                                        x.Lattitute < north + yOffset
                                                        && x.StatTime >= begin && x.StatTime < end)
                .MapTo<IEnumerable<ZhangshangyouCoverageView>>();
        }

        public IEnumerable<ZhangshangyouCoverageView> QueryLteRecordsByDateSpan(DateTime begin, DateTime end,
            int eNodebId, byte sectorId)
        {
            return _repository.GetAllList(
                    x => x.StatTime >= begin && x.StatTime < end && x.ENodebId == eNodebId && x.SectorId == sectorId)
                .MapTo<IEnumerable<ZhangshangyouCoverageView>>();
        }

        public IEnumerable<ZhangshangyouCoverageView> QueryCdmaRecordsByDateSpan(DateTime begin, DateTime end,
            int btsId, byte cdmaSectorId)
        {
            return _repository.GetAllList(
                    x => x.StatTime >= begin && x.StatTime < end && x.BtsId == btsId && x.CdmaSectorId == cdmaSectorId)
                .MapTo<IEnumerable<ZhangshangyouCoverageView>>();
        }

    }
}
