﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Test;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Common;
using Lte.Domain.LinqToCsv.Context;
using Lte.Domain.LinqToCsv.Description;
using Lte.MySqlFramework.Abstract;
using Lte.MySqlFramework.Abstract.Infrastructure;
using Lte.MySqlFramework.Abstract.Test;
using Lte.MySqlFramework.Entities;
using Lte.MySqlFramework.Entities.Dt;
using Remotion.Data.Linq.Utilities;

namespace Lte.Evaluations.DataService.Dt
{
    public class ZhangshangyouCoverageService
    {
        private readonly IZhangshangyouCoverageRepository _repository;
        private readonly ICellRepository _cellRepository;

        public ZhangshangyouCoverageService(IZhangshangyouCoverageRepository repository,
            ICellRepository cellRepository)
        {
            _repository = repository;
            _cellRepository = cellRepository;
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
            var results = _repository.GetAll().Where(
                    x => x.Longtitute >= west + xOffset && x.Longtitute < east + xOffset
                                                        && x.Lattitute >= south + yOffset &&
                                                        x.Lattitute < north + yOffset
                                                        && x.StatTime >= begin && x.StatTime < end);
            foreach (var item in results)
            {
                item.XOffset = xOffset;
                item.YOffset = yOffset;
            }

            _repository.SaveChanges();
            return results.ToList().MapTo<IEnumerable<ZhangshangyouCoverageView>>();
        }

        public IEnumerable<ZhangshangyouCoverageView> QueryByDateSpanAndGeneralRange(DateTime begin, DateTime end,
            double generalWest, double generalEast, double generalSouth, double generalNorth, 
            double xOffset, double yOffset)
        {
            var list = _repository.GetAllList(
                x => x.Longtitute >= generalWest + xOffset 
                     && x.Longtitute < generalEast + xOffset
                                                    && x.Lattitute >= generalSouth + yOffset &&
                                                    x.Lattitute < generalNorth + yOffset
                                                    && x.StatTime >= begin && x.StatTime < end);
            
            var cells = _cellRepository.GetAllList(x => x.Longtitute >= generalWest && x.Longtitute < generalEast
                                                                                    && x.Lattitute >= generalSouth &&
                                                                                    x.Lattitute < generalNorth);
            var rangeItems = _repository.GetAllList(x => x.StatTime >= begin && x.StatTime < end);
            var items = (from item in rangeItems
                join c in cells on new {item.ENodebId, item.SectorId} equals new {c.ENodebId, c.SectorId}
                select item).ToList();

            list.AddRange(items);

            var resultList = list.Distinct(new ZhangshangyouCoverageEquator());
            foreach (var item in resultList)
            {
                item.XOffset = xOffset;
                item.YOffset = yOffset;
                var o = _repository.Get(item.Id);
                o.XOffset = xOffset;
                o.YOffset = yOffset;
            }

            _repository.SaveChanges();

            return resultList.MapTo<IEnumerable<ZhangshangyouCoverageView>>();
        }

        public IEnumerable<ZhangshangyouCoverageView> QueryLteRecordsByDateSpan(DateTime begin, DateTime end,
            int eNodebId, byte sectorId)
        {
            var items = _repository.GetAll().Where(
                    x => x.StatTime >= begin && x.StatTime < end && x.ENodebId == eNodebId && x.SectorId == sectorId);
            foreach (var item in items)
            {
                if (Math.Abs(item.XOffset) < 1e-6) item.XOffset = 0.03;
                if (Math.Abs(item.YOffset) < 1e-6) item.YOffset = 0.01;
            }

            _repository.SaveChanges();
            return items.ToList().MapTo<IEnumerable<ZhangshangyouCoverageView>>();
        }

        public IEnumerable<ZhangshangyouCoverageView> QueryCdmaRecordsByDateSpan(DateTime begin, DateTime end,
            int btsId, byte cdmaSectorId)
        {
            var items = _repository.GetAll().Where(
                    x => x.StatTime >= begin && x.StatTime < end && x.BtsId == btsId && x.CdmaSectorId == cdmaSectorId);
            foreach (var item in items)
            {
                if (Math.Abs(item.XOffset) < 1e-6) item.XOffset = 0.03;
                if (Math.Abs(item.YOffset) < 1e-6) item.YOffset = 0.01;
            }

            _repository.SaveChanges();
            return items.ToList().MapTo<IEnumerable<ZhangshangyouCoverageView>>();
        }

    }
}
