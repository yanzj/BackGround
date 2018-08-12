using System;
using System.Collections.Generic;
using System.Linq;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Region;
using Abp.EntityFramework.Entities.Test;
using Lte.Domain.Common.Types;
using Lte.MySqlFramework.Abstract;
using Lte.MySqlFramework.Abstract.Region;
using Lte.MySqlFramework.Abstract.Test;
using Lte.MySqlFramework.Entities;

namespace Lte.Evaluations.DataService.Dt
{
    public class RasterInfoService
    {
        private readonly IRasterInfoRepository _repository;
        private readonly IRasterTestInfoRepository _testInfoRepository;
        private readonly IDtFileInfoRepository _dtFileInfoRepository;
        private readonly ITownRepository _townRepository;
        private readonly IAreaTestInfoRepository _areaTestInfoRepository;

        public RasterInfoService(IRasterInfoRepository repository,
            IRasterTestInfoRepository testInfoRepository, IDtFileInfoRepository dtFileInfoRepository,
            ITownRepository townRepository, IAreaTestInfoRepository areaTestInfoRepository)
        {
            _repository = repository;
            _testInfoRepository = testInfoRepository;
            _dtFileInfoRepository = dtFileInfoRepository;
            _townRepository = townRepository;
            _areaTestInfoRepository = areaTestInfoRepository;
        }

        public IEnumerable<RasterInfo> GetAllList()
        {
            return _repository.GetAllList();
        } 
        
        public IEnumerable<FileRasterInfoView> QueryFileNames(string dataType, double west, double east, double south,
            double north)
        {
            var infos =
                _repository.GetAllList(
                    x => x.Longtitute >= west && x.Longtitute < east && x.Lattitute >= south && x.Lattitute < north);
            return GetFileRasterInfoViews(dataType, infos);
        }

        public string QueryNetworkType(string csvFileName)
        {
            var info = _testInfoRepository.FirstOrDefault(x => x.CsvFilesName.Contains(csvFileName));
            return info?.NetworkType;
        }

        public IEnumerable<CsvFilesInfo> QueryFileNames(DateTime begin, DateTime end)
        {
            return _dtFileInfoRepository.GetAllList(x => x.TestDate >= begin && x.TestDate < end);
        }

        private IEnumerable<FileRasterInfoView> GetFileRasterInfoViews(string dataType, List<RasterInfo> infos)
        {
            if (!infos.Any())
                return new List<FileRasterInfoView>();

            var fileInfos =
                infos.Select(
                        info => _testInfoRepository.GetAllList(x => x.NetworkType == dataType && x.RasterNum == info.Id))
                    .Aggregate((x, y) => x.Concat(y).ToList());
            var query = fileInfos.Select(x => x.CsvFilesName.GetSplittedFields(';').Select(t => new
            {
                x.RasterNum,
                FileName = t
            }));
            var tuples = query.Aggregate((x, y) => x.Concat(y)).Distinct();

            return from tuple in tuples
                group tuple by tuple.FileName
                into g
                select new FileRasterInfoView
                {
                    CsvFileName = g.Key,
                    RasterNums = g.Select(x => x.RasterNum)
                };
        }

        public IEnumerable<FileRasterInfoView> QueryFileNames(string dataType, string town)
        {
            var infos = _repository.GetAllList(x=>x.Area==town);
            return GetFileRasterInfoViews(dataType, infos);
        }

        private IEnumerable<FileRasterInfoView> QueryFileNames(string dataType)
        {
            var infos = _repository.GetAllList();
            return GetFileRasterInfoViews(dataType, infos);
        }

        public IEnumerable<FileRasterInfoView> QueryFileNames(string dataType, string townName, DateTime begin,
            DateTime end)
        {
            var views = QueryFileNames(dataType, townName);
            return GetFileRasterInfoViews(begin, end, views);
        }

        public IEnumerable<DistrictFileView> QueryCityTestInfos(DateTime begin, DateTime end, string dataType)
        {
            var allInfos = QueryFileInfos(dataType, begin, end).ToList();
            var towns = _townRepository.GetAllList(x =>
                x.TownName != "×æÃí" && x.TownName != "Ê¯Íå" && x.TownName != "ÕÅé¶" && x.TownName != "Ð¡ÌÁ");
            var views = towns.Select(town => QueryAreaTestFileViewsInTowns(allInfos, town));
            return views.GroupBy(x => x.District).Select(g => new DistrictFileView
            {
                District = g.Key,
                Distance = g.Sum(x => x.Distance),
                Count = g.Sum(x => x.Count),
                CoverageCount = g.Sum(x => x.CoverageCount),
                TownViews = g
            });
        }

        public DistrictFileView QueryDistrictTestInfos(DateTime begin, DateTime end, string dataType, 
            string city, string district)
        {
            var allInfos = QueryFileInfos(dataType, begin, end).ToList();
            var towns = _townRepository.GetAllList(x => x.CityName == city && x.DistrictName == district
                && x.TownName != "×æÃí" && x.TownName != "Ê¯Íå" && x.TownName != "ÕÅé¶" && x.TownName != "Ð¡ÌÁ");
            var views = towns.Select(town => QueryAreaTestFileViewsInTowns(allInfos, town));
            return new DistrictFileView
            {
                District = district,
                Distance = views.Sum(x => x.Distance),
                Count = views.Sum(x => x.Count),
                CoverageCount = views.Sum(x => x.CoverageCount),
                TownViews = views
            };
        }

        public TownAreaTestFileView QueryTownTestInfos(DateTime begin, DateTime end, string dataType,
            string city, string district, string town)
        {
            var allInfos = QueryFileInfos(dataType, begin, end).ToList();
            var townItem = _townRepository.FirstOrDefault(x => x.CityName == city && x.DistrictName == district
                                                        && x.TownName == town);
            return townItem == null ? null : QueryAreaTestFileViewsInTowns(allInfos, townItem);
        }

        private TownAreaTestFileView QueryAreaTestFileViewsInTowns(List<CsvFilesInfo> allInfos, Town town)
        {
            var views = from info in allInfos
                join file in _areaTestInfoRepository.GetAllList(x=> x.TownId==town.Id)
                on info.Id equals file.FileId
                select new
                {
                    Info = info,
                    File = file
                };
            return new TownAreaTestFileView
            {
                District = town.DistrictName,
                Town = town.TownName,
                Distance = views.Sum(x => x.File.Distance),
                Count = views.Sum(x => x.File.Count),
                CoverageCount = views.Sum(x => x.File.CoverageCount),
                Views = views.Select(x =>
                {
                    var fileView = x.File.MapTo<AreaTestFileView>();
                    fileView.CsvFileName = x.Info.CsvFileName;
                    fileView.TestDate = x.Info.TestDate;
                    fileView.AreaName = town.CityName + town.DistrictName + town.TownName;
                    return fileView;
                })
            };
        }

        public IEnumerable<CsvFilesInfo> QueryFileInfos(string dataType, DateTime begin, DateTime end)
        {
            var files = _dtFileInfoRepository.GetAllList(x => x.TestDate >= begin && x.TestDate < end);
            var fileNames = _testInfoRepository.GetAllList(x => x.NetworkType == dataType)
                .Select(x => x.CsvFilesName.Split(';'))
                .Aggregate((x, y) => x.Concat(y).Distinct().ToArray());
            return from file in files join name in fileNames on file.CsvFileName.Split('.')[0] equals name select file;
        }

        private IEnumerable<FileRasterInfoView> GetFileRasterInfoViews(DateTime begin, DateTime end, IEnumerable<FileRasterInfoView> views)
        {
            if (!views.Any()) return new List<FileRasterInfoView>();

            var fileInfos = _dtFileInfoRepository.GetAllList(x => x.TestDate >= begin && x.TestDate < end);
            if (!fileInfos.Any()) return new List<FileRasterInfoView>();

            return from fileInfo in fileInfos
                join view in views on fileInfo.CsvFileName.Split('.')[0] equals view.CsvFileName.Split('.')[0]
                select view;
        }

        private IEnumerable<CsvFilesInfo> GetCsvFileInfos(DateTime begin, DateTime end, IEnumerable<FileRasterInfoView> views)
        {
            if (!views.Any()) return new List<CsvFilesInfo>();

            var fileInfos = _dtFileInfoRepository.GetAllList(x => x.TestDate >= begin && x.TestDate < end);
            if (!fileInfos.Any()) return new List<CsvFilesInfo>();

            return from fileInfo in fileInfos
                   join view in views on fileInfo.CsvFileName.Split('.')[0] equals view.CsvFileName.Split('.')[0]
                   select fileInfo;
        }

        public IEnumerable<FileRasterInfoView> QueryFileNames(string dataType, double west, double east, double south,
            double north, DateTime begin, DateTime end)
        {
            var views = QueryFileNames(dataType, west, east, south, north);
            return GetFileRasterInfoViews(begin, end, views);
        }
    }
}