using System;
using System.Collections.Generic;
using System.Linq;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities;
using Lte.MySqlFramework.Abstract;
using Lte.MySqlFramework.Entities;
using Lte.Parameters.Abstract.Infrastructure;

namespace Lte.Evaluations.DataService.Dt
{
    public class TownTestInfoService
    {
        private readonly IFileRecordRepository _repository;
        private readonly ITownBoundaryRepository _boundaryRepository;
        private readonly IDtFileInfoRepository _fileInfoRepository;
        private readonly IAreaTestInfoRepository _areaTestInfoRepository;
        private readonly ITownRepository _townRepository;

        public TownTestInfoService(IFileRecordRepository repository, ITownBoundaryRepository boundaryRepository,
            IDtFileInfoRepository fileInfoRepository, IAreaTestInfoRepository areaTestInfoRepository,
            ITownRepository townRepository)
        {
            _repository = repository;
            _boundaryRepository = boundaryRepository;
            _fileInfoRepository = fileInfoRepository;
            _areaTestInfoRepository = areaTestInfoRepository;
            _townRepository = townRepository;
        }

        public IEnumerable<AreaTestInfo> QueryAreaTestInfos(int fileId)
        {
            var townIds =
                _boundaryRepository.GetAllList(x => x.AreaName == null).Select(x => x.TownId).Distinct().ToList();
            return from info in _areaTestInfoRepository.GetAllList(x => x.FileId == fileId)
                join id in townIds on info.TownId equals id
                select info;
        }
        
        public IEnumerable<AreaTestInfo> QueryRoadTestInfos(int fileId)
        {
            var townIds =
                _boundaryRepository.GetAllList(
                        x => x.AreaName != null && (x.AreaName.StartsWith("S") || x.AreaName.StartsWith("G")))
                    .Select(x => x.TownId)
                    .Distinct()
                    .ToList();
            return from info in _areaTestInfoRepository.GetAllList(x => x.FileId == fileId)
                join id in townIds on info.TownId equals id
                select info;
        }

        public IEnumerable<AreaTestFileView> QueryRoadTestInfos(DateTime begin, DateTime end, InfrastructureInfo road)
        {
            var allInfos =
                _areaTestInfoRepository.GetAllList(x => x.TownId == road.Id);
            var views = from info in allInfos
                join file in _fileInfoRepository.GetAllList() on info.FileId equals file.Id
                where file.TestDate >= begin && file.TestDate < end
                select new
                {
                    Info = info,
                    File = file
                };
            return views.Select(v =>
            {
                var view = v.Info.MapTo<AreaTestFileView>();
                view.AreaName = road.HotspotName;
                view.CsvFileName = v.File.CsvFileName;
                view.TestDate = v.File.TestDate;
                return view;
            });
        }

        public IEnumerable<AreaTestFileView> QueryDistrictTestInfos(DateTime begin, DateTime end, string city, string district)
        {
            var allInfos =
                _areaTestInfoRepository.GetAllList();
            var towns = _townRepository.GetAllList(x => x.CityName == city && x.DistrictName == district);
            var views = from info in allInfos
                join file in _fileInfoRepository.GetAllList(x => x.TestDate > begin && x.TestDate < end) 
                on info.FileId equals file.Id
                join town in towns on info.TownId equals town.Id
                select new
                {
                    Info = info,
                    File = file,
                    Town = town
                };
            return views.Select(v =>
            {
                var view = v.Info.MapTo<AreaTestFileView>();
                view.AreaName = v.Town.CityName + v.Town.DistrictName + v.Town.TownName;
                view.CsvFileName = v.File.CsvFileName;
                view.TestDate = v.File.TestDate;
                return view;
            });
        }

        public IEnumerable<AreaTestFileView> QueryTownTestInfos(DateTime begin, DateTime end, string city,
            string district, string townName)
        {
            var town =
                _townRepository.FirstOrDefault(
                    x => x.CityName == city && x.DistrictName == district && x.TownName == townName);
            var townInfos =
                _areaTestInfoRepository.GetAllList(x => x.TownId == town.Id);
            if (town == null) return new List<AreaTestFileView>();
            var views = from info in townInfos
                join file in _fileInfoRepository.GetAllList(x => x.TestDate > begin && x.TestDate < end)
                on info.FileId equals file.Id
                select new
                {
                    Info = info,
                    File = file,
                    Town = town
                };
            return views.Select(v =>
            {
                var view = v.Info.MapTo<AreaTestFileView>();
                view.AreaName = v.Town.CityName + v.Town.DistrictName + v.Town.TownName;
                view.CsvFileName = v.File.CsvFileName;
                view.TestDate = v.File.TestDate;
                return view;
            });
        }

        public IEnumerable<AreaTestInfo> CalculateAreaTestInfos(string csvFileName, string type)
        {
            var file = _fileInfoRepository.FirstOrDefault(x => x.CsvFileName == csvFileName + ".csv");
            if (file == null) return new List<AreaTestInfo>();
            var townIds =
                _boundaryRepository.GetAllList(x => x.AreaName == null).Select(x => x.TownId).Distinct().ToList();
            AreaTestInfoQuery query;
            switch (type)
            {
                case "2G":
                    query = new AreaTestInfo2GQuery(_repository, _boundaryRepository);
                    break;
                case "3G":
                    query = new AreaTestInfo3GQuery(_repository, _boundaryRepository);
                    break;
                case "Volte":
                    query = new AreaTestInfoVolteQuery(_repository, _boundaryRepository);
                    break;
                default:
                    query = new AreaTestInfo4GQuery(_repository, _boundaryRepository);
                    break;
            }  
            return query.QueryAreaTestInfos(townIds, csvFileName, file.Id);
        }

        public IEnumerable<AreaTestInfo> CalculateRoadTestInfos(string csvFileName, string type)
        {
            var file = _fileInfoRepository.FirstOrDefault(x => x.CsvFileName == csvFileName + ".csv");
            if (file == null) return new List<AreaTestInfo>();
            var townIds =
                _boundaryRepository.GetAllList(
                        x => x.AreaName != null && (x.AreaName.StartsWith("S") || x.AreaName.StartsWith("G")))
                    .Select(x => x.TownId)
                    .Distinct()
                    .ToList();
            
            AreaTestInfoQuery query;
            switch (type)
            {
                case "2G":
                    query = new AreaTestInfo2GQuery(_repository, _boundaryRepository);
                    break;
                case "3G":
                    query = new AreaTestInfo3GQuery(_repository, _boundaryRepository);
                    break;
                case "Volte":
                    query = new AreaTestInfoVolteQuery(_repository, _boundaryRepository);
                    break;
                default:
                    query = new AreaTestInfo4GQuery(_repository, _boundaryRepository);
                    break;
            }
            return query.QueryRoadTestInfos(townIds, csvFileName, file.Id);
        }

        public int UpdateAreaTestInfo(AreaTestInfo info)
        {
            var item = _areaTestInfoRepository.FirstOrDefault(x => x.TownId == info.TownId && x.FileId == info.FileId);
            if (item != null)
            {
                item.Count = info.Count;
                item.CoverageCount = info.CoverageCount;
                item.Distance = info.Distance;
            }
            else
            {
                _areaTestInfoRepository.Insert(info);
            }
            return _areaTestInfoRepository.SaveChanges();
        }
    }
}