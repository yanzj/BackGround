using System.Collections.Generic;
using System.Linq;
using Abp.EntityFramework.Entities;
using Lte.Domain.Common.Geo;
using Lte.MySqlFramework.Abstract;
using Lte.MySqlFramework.Abstract.Region;
using Lte.MySqlFramework.Entities;

namespace Lte.MySqlFramework.Support
{
    public abstract class AreaTestInfoQuery
    {
        private int _currentTownId;
        private double _lastLon;
        private double _lastLat;
        private readonly ITownBoundaryRepository _boundaryRepository;

        protected AreaTestInfoQuery(ITownBoundaryRepository boundaryRepository)
        {
            _currentTownId = -1;
            _lastLon = -1.0;
            _lastLat = -1.0;
            _boundaryRepository = boundaryRepository;
        }

        protected void UpdateCurrentTownId(List<AreaTestInfo> results, int townId, GeoPoint point, int fileId,
            bool isCoverage)
        {
            var distance = 0.0;
            if (townId == _currentTownId)
            {
                if (_lastLon > 0 && _lastLat > 0)
                {
                    distance = point.Distance(new GeoPoint(_lastLon, _lastLat));
                }
                _lastLon = point.Longtitute;
                _lastLat = point.Lattitute;
            }
            else
            {
                _currentTownId = townId;
                _lastLon = -1.0;
                _lastLat = -1.0;
            }
            var item = results.FirstOrDefault(x => x.TownId == townId);
            if (item == null)
            {
                results.Add(new AreaTestInfo
                {
                    FileId = fileId,
                    TownId = townId,
                    Distance = distance,
                    Count = 1,
                    CoverageCount = isCoverage ? 1 : 0
                });
            }
            else
            {
                item.Distance += distance;
                item.Count++;
                if (isCoverage) item.CoverageCount++;
            }
        }

        private List<TownRectangle> GenerateRectangle(List<int> townIds)
        {
            return (from townId in townIds
                let coors = _boundaryRepository.GetAllList(x => x.TownId == townId)
                where coors.Any()
                let west = coors.Select(coor => coor.CoorList().Min(x => x.Longtitute)).Min()
                let east = coors.Select(coor => coor.CoorList().Max(x => x.Longtitute)).Max()
                let south = coors.Select(coor => coor.CoorList().Min(x => x.Lattitute)).Min()
                let north = coors.Select(coor => coor.CoorList().Max(x => x.Lattitute)).Max()
                select new TownRectangle
                {
                    TownId = townId, West = west, East = east, South = south, North = north,
                    Coors = coors
                }).ToList();
        }

        private List<RoadRectangle> GenerateRoadRectangle(List<int> townIds)
        {
            return (from townId in townIds
                    let coors = _boundaryRepository.FirstOrDefault(x => x.TownId == townId)
                    where coors != null
                    let west = coors.CoorList().Min(x => x.Longtitute)
                    let east = coors.CoorList().Max(x => x.Longtitute)
                    let south = coors.CoorList().Min(x => x.Lattitute)
                    let north = coors.CoorList().Max(x => x.Lattitute)
                    select new RoadRectangle
                    {
                        TownId = townId,
                        West = west,
                        East = east,
                        South = south,
                        North = north,
                        Coors = coors
                    }).ToList();
        }

        protected void UpdateTownRecords<TRecord>(List<int> townIds, int fileId, IEnumerable<TRecord> data, List<AreaTestInfo> results)
            where TRecord : IGeoPoint<double?>, ICoverage
        {
            var dataList = data.Where(x => x.Longtitute != null && x.Lattitute != null).ToList();
            var rectangles = GenerateRectangle(townIds);
            foreach (var record in dataList.Where(x => x.IsValid()))
            {
                foreach (var rect in rectangles.Where(rect => record.Longtitute > rect.West && record.Longtitute < rect.East
                                                           && record.Lattitute > rect.South && record.Lattitute < rect.North))
                {
                    var point = new GeoPoint(record.Longtitute ?? 0, record.Lattitute ?? 0);
                    if (!rect.Coors.IsInTownRange(point)) continue;
                    UpdateCurrentTownId(results, rect.TownId, point, fileId, record.IsCoverage());
                    break;
                }
            }
        }

        protected void UpdateRoadRecords<TRecord>(List<int> townIds, int fileId, IEnumerable<TRecord> data, List<AreaTestInfo> results)
            where TRecord : IGeoPoint<double?>, ICoverage
        {
            var dataList = data.Where(x => x.Longtitute != null && x.Lattitute != null).ToList();
            var rectangles = GenerateRoadRectangle(townIds);
            foreach (var record in dataList.Where(x => x.IsValid()))
            {
                foreach (var rect in rectangles.Where(rect => record.Longtitute > rect.West && record.Longtitute < rect.East
                                                           && record.Lattitute > rect.South && record.Lattitute < rect.North))
                {
                    var point = new GeoPoint(record.Longtitute ?? 0, record.Lattitute ?? 0);
                    if (!rect.Coors.IsInTownRange(point)) continue;
                    UpdateCurrentTownId(results, rect.TownId, point, fileId, record.IsCoverage());
                    break;
                }
            }
        }

        public abstract List<AreaTestInfo> QueryAreaTestInfos(List<int> townIds, string csvFileName, int fileId);

        public abstract List<AreaTestInfo> QueryRoadTestInfos(List<int> townIds, string csvFileName, int fileId);
    }
}