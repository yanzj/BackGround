using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.EntityFramework.Entities.Infrastructure;
using Abp.EntityFramework.Entities.Region;
using Abp.EntityFramework.Entities.Station;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Excel;
using Lte.Domain.LinqToExcel;
using Lte.MySqlFramework.Abstract.Infrastructure;
using Lte.MySqlFramework.Abstract.Region;
using Lte.MySqlFramework.Abstract.Station;

namespace Lte.Evaluations.DataService.Basic
{
    public class StationImportService
    {
        private readonly IDistributionRepository _distributionRepository;
        private readonly IConstructionInformationRepository _constructionInformation;
        private readonly IStationRruRepository _stationRruRepository;
        private readonly IStationAntennaRepository _stationAntennaRepository;
        private readonly IENodebBaseRepository _eNodebBaseRepository;
        private readonly IStationDictionaryRepository _stationDictionary;
        private readonly IENodebRepository _eNodebRepository;
        private readonly ITownRepository _townRepository;
        
        private static Stack<StationDictionaryExcel> Stations { get; set; }

        public int StationsCount => Stations.Count;

        private static Stack<ENodebBaseExcel> ENodebBases { get; set; }

        public int StationENodebCount => ENodebBases.Count;

        private static Stack<ConstructionExcel> StationCells { get; set; }

        public int StationCellCount => StationCells.Count;

        private static Stack<StationRruExcel> StationRrus { get; set; }

        public int StationRruCount => StationRrus.Count;

        private static Stack<StationAntennaExcel> StationAntennas { get; set; }

        public int StationAntennaCount => StationAntennas.Count;

        private static Stack<IndoorDistributionExcel> StationDistributions { get; set; }

        public int StationDistributionCount => StationDistributions.Count;

        private static List<Town> Towns { get; set; }

        public StationImportService(IDistributionRepository distributionRepository,
            IENodebBaseRepository eNodebBaseRepository, IENodebRepository eNodebRepository,
            IConstructionInformationRepository constructionInformation, IStationRruRepository stationRruRepository,
            IStationAntennaRepository stationAntennaRepository, IStationDictionaryRepository stationDictionary,
            ITownRepository townRepository)
        {
            _distributionRepository = distributionRepository;
            _constructionInformation = constructionInformation;
            _stationRruRepository = stationRruRepository;
            _stationAntennaRepository = stationAntennaRepository;
            _eNodebBaseRepository = eNodebBaseRepository;
            _eNodebRepository = eNodebRepository;
            _stationDictionary = stationDictionary;
            _townRepository = townRepository;
           
            if (Stations == null) Stations = new Stack<StationDictionaryExcel>();
            if (ENodebBases == null) ENodebBases = new Stack<ENodebBaseExcel>();
            if (StationCells == null) StationCells = new Stack<ConstructionExcel>();
            if (StationRrus == null) StationRrus = new Stack<StationRruExcel>();
            if (StationAntennas == null) StationAntennas = new Stack<StationAntennaExcel>();
            if (StationDistributions == null) StationDistributions = new Stack<IndoorDistributionExcel>();
            if (Towns == null) Towns = _townRepository.GetAllList();
        }
        
        public int ImportStationDictionaries(string path)
        {
            var repo = new ExcelQueryFactory { FileName = path };
            var excels = (from c in repo.Worksheet<StationDictionaryExcel>("Sheet1") select c).ToList();
            foreach (var stationDictionaryExcel in excels)
            {
                Stations.Push(stationDictionaryExcel);
            }

            return Stations.Count;
        }

        public async Task<bool> DumpOneStationInfo()
        {
            var stat = Stations.Pop();
            if (stat == null) throw new NullReferenceException("stat is null!");
            await _stationDictionary
                .UpdateOneInUse<IStationDictionaryRepository, StationDictionary, StationDictionaryExcel>(stat);
            return true;
        }

        public int ImportStationENodebs(string path)
        {
            var repo = new ExcelQueryFactory { FileName = path };
            var excels = (from c in repo.Worksheet<ENodebBaseExcel>("Sheet1") select c).ToList();
            foreach (var eNodebBaseExcel in excels)
            {
                ENodebBases.Push(eNodebBaseExcel);
            }

            return ENodebBases.Count;
        }

        public async Task<bool> DumpOneStationENodeb()
        {
            var stat = ENodebBases.Pop();
            if (stat == null) throw new NullReferenceException("stat is null!");
            await _eNodebBaseRepository
                .UpdateOneInUse<IENodebBaseRepository, ENodebBase, ENodebBaseExcel>(stat);
            await _eNodebRepository
                .UpdateOneInUse<IENodebRepository, ENodeb, ENodebBaseExcel, Town>(stat, Towns, excel =>
                {
                    if (excel.StationTown != "城区") return excel.StationTown.Replace("金沙", "丹灶");
                    var candidates = new[] {"石湾", "张槎", "祖庙"};
                    var result = candidates.FirstOrDefault(x => excel.ENodebName.Contains(x));
                    if (result != null) return result;
                    var station = _stationDictionary.FirstOrDefault(x => x.StationNum == excel.StationNum);
                    if (station == null) return "石湾";
                    result = candidates.FirstOrDefault(x =>
                        station.ElementName.Contains(x) || station.Address.Contains(x));
                    return result ?? "石湾";
                }, (info, excel) =>
                {
                    var station = _stationDictionary.FirstOrDefault(x => x.StationNum == excel.StationNum);
                    if (station == null) return;
                    info.Address = station.Address;
                    if (string.IsNullOrEmpty(info.PlanNum)) info.PlanNum = excel.ProjectSerial;
                }, excel =>
                {
                    var candidates = Towns.Select(x => new {x.TownName, x.Id}).ToList();
                    var result = candidates.FirstOrDefault(x => excel.ENodebName.Contains(x.TownName));
                    if (result != null) return result.Id;
                    var station = _stationDictionary.FirstOrDefault(x => x.StationNum == excel.StationNum);
                    if (station == null) return -1;
                    result = candidates.FirstOrDefault(x =>
                        station.ElementName.Contains(x.TownName) || station.Address.Contains(x.TownName));
                    if (result != null) return result.Id;
                    return -1;
                });
            return true;
        }

        public int ImportConstructions(string path)
        {
            var repo = new ExcelQueryFactory { FileName = path };
            var excels = (from c in repo.Worksheet<ConstructionExcel>("Sheet1") select c).ToList();
            foreach (var constructionExcel in excels)
            {
                StationCells.Push(constructionExcel);
            }

            return StationCells.Count;
        }

        public async Task<bool> DumpOneStationCell()
        {
            var stat = StationCells.Pop();
            if (stat == null) throw new NullReferenceException("stat is null!");
            await _constructionInformation
                .UpdateOneInUse<IConstructionInformationRepository, ConstructionInformation, ConstructionExcel>(stat);
            return true;
        }

        public int ImportStationRrus(string path)
        {
            var repo = new ExcelQueryFactory { FileName = path };
            var excels = (from c in repo.Worksheet<StationRruExcel>("Sheet1") select c).ToList();
            foreach (var stationRruExcel in excels)
            {
                StationRrus.Push(stationRruExcel);
            }

            return StationRrus.Count;
        }

        public async Task<bool> DumpOneStationRru()
        {
            var stat = StationRrus.Pop();
            if (stat == null) throw new NullReferenceException("stat is null!");
            await _stationRruRepository
                .UpdateOneInUse<IStationRruRepository, StationRru, StationRruExcel>(stat);
            return true;
        }

        public int ImportStationAntennas(string path)
        {
            var repo = new ExcelQueryFactory { FileName = path };
            var excels = (from c in repo.Worksheet<StationAntennaExcel>("Sheet1") select c).ToList();
            foreach (var stationAntennaExcel in excels)
            {
                StationAntennas.Push(stationAntennaExcel);
            }

            return StationAntennas.Count;
        }

        public async Task<bool> DumpOneStationAntenna()
        {
            var stat = StationAntennas.Pop();
            if (stat == null) throw new NullReferenceException("stat is null!");
            await _stationAntennaRepository
                .UpdateOneInUse<IStationAntennaRepository, StationAntenna, StationAntennaExcel>(stat);
            return true;
        }

        public int ImportDistributions(string path)
        {
            var repo = new ExcelQueryFactory { FileName = path };
            var excels = (from c in repo.Worksheet<IndoorDistributionExcel>("Sheet1") select c).ToList();
            foreach (var indoorDistributionExcel in excels)
            {
                StationDistributions.Push(indoorDistributionExcel);
            }

            return StationDistributions.Count;
        }

        public async Task<bool> DumpOneStationDistribution()
        {
            var stat = StationDistributions.Pop();
            if (stat == null) throw new NullReferenceException("stat is null!");
            await _distributionRepository
                .UpdateOneInUse<IDistributionRepository, IndoorDistribution, IndoorDistributionExcel>(stat);
            return true;
        }

    }
}
