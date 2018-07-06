using System;
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

namespace Lte.Evaluations.DataService.Dt
{
    public class ZhangshangyouQualityService
    {
        private readonly IZhangshangyouQualityRepository _repository;
        private readonly IZhangshangyouCoverageRepository _coverageRepository;
        private readonly ICellRepository _cellRepository;

        public ZhangshangyouQualityService(IZhangshangyouQualityRepository repository,
            IZhangshangyouCoverageRepository coverageRepository, ICellRepository cellRepository)
        {
            _repository = repository;
            _coverageRepository = coverageRepository;
            _cellRepository = cellRepository;
            if (Stats == null)
            {
                Stats = new Stack<ZhangshangyouQualityCsv>();
            }
        }

        private static Stack<ZhangshangyouQualityCsv> Stats { get; set; }

        public void UploadStats(StreamReader reader)
        {
            var stats = CsvContext.Read<ZhangshangyouQualityCsv>(reader, CsvFileDescription.CommaDescription)
                .ToList();
            foreach (var stat in stats)
            {
                Stats.Push(stat);
            }
        }

        public async Task<bool> DumpOneStat()
        {
            var stat = Stats.Pop();
            if (stat == null) throw new NullReferenceException("coverage stat is null!");
            await _repository
                .UpdateOne<IZhangshangyouQualityRepository, ZhangshangyouQuality, ZhangshangyouQualityCsv>(stat);
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

        public IEnumerable<ZhangshangyouQualityView> QueryByDateSpanAndRange(DateTime begin, DateTime end,
            double west, double east, double south, double north, double xOffset, double yOffset)
        {
            var coverageItems = _coverageRepository.GetAllList(
                    x => x.Longtitute >= west + xOffset && x.Longtitute < east + xOffset
                                                        && x.Lattitute >= south + yOffset &&
                                                        x.Lattitute < north + yOffset
                                                        && x.StatTime >= begin && x.StatTime < end);
            var dateSpanItems = _repository.GetAllList(x => x.StatTime >= begin && x.StatTime < end);
            return (from c in coverageItems join q in dateSpanItems on c.SerialNumber equals q.SerialNumber select q)
                .Distinct(new ZhangshangyouQualityEquator())
                .MapTo<IEnumerable<ZhangshangyouQualityView>>();
        }

        public IEnumerable<ZhangshangyouQualityView> QueryByDateSpanAndGeneralRange(DateTime begin, DateTime end,
            double generalWest, double generalEast, double generalSouth, double generalNorth,
            double xOffset, double yOffset)
        {
            var coverageItems = _coverageRepository.GetAllList(
                x => x.Longtitute >= generalWest + xOffset
                     && x.Longtitute < generalEast + xOffset
                     && x.Lattitute >= generalSouth + yOffset &&
                     x.Lattitute < generalNorth + yOffset
                     && x.StatTime >= begin && x.StatTime < end);
            var cells = _cellRepository.GetAllList(x => x.Longtitute >= generalWest && x.Longtitute < generalEast
                                                                                    && x.Lattitute >= generalSouth &&
                                                                                    x.Lattitute < generalNorth);

            foreach (var cell in cells)
            {
                var items = _coverageRepository.GetAllList(x =>
                    x.StatTime >= begin && x.StatTime < end && x.ENodebId == cell.ENodebId &&
                    x.SectorId == cell.SectorId);
                if (items.Any())
                {
                    coverageItems.AddRange(items);
                }
            }

            var dateSpanItems = _repository.GetAllList(x => x.StatTime >= begin && x.StatTime < end);
            return (from c in coverageItems.Distinct(new ZhangshangyouCoverageEquator())
                    join q in dateSpanItems on c.SerialNumber equals q.SerialNumber
                    select q)
                .Distinct(new ZhangshangyouQualityEquator())
                .MapTo<IEnumerable<ZhangshangyouQualityView>>();
        }

        public IEnumerable<ZhangshangyouQualityView> QueryLteRecordsByDateSpan(DateTime begin, DateTime end,
            int eNodebId, byte sectorId)
        {
            return _repository.GetAllList(
                    x => x.StatTime >= begin && x.StatTime < end && x.ENodebId == eNodebId && x.SectorId == sectorId)
                .MapTo<IEnumerable<ZhangshangyouQualityView>>();
        }

        public IEnumerable<ZhangshangyouQualityView> QueryCdmaRecordsByDateSpan(DateTime begin, DateTime end,
            int btsId, byte cdmaSectorId)
        {
            return _repository.GetAllList(
                    x => x.StatTime >= begin && x.StatTime < end && x.BtsId == btsId && x.CdmaSectorId == cdmaSectorId)
                .MapTo<IEnumerable<ZhangshangyouQualityView>>();
        }

    }
}
