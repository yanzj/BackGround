using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.EntityFramework.Entities.Infrastructure;
using Abp.EntityFramework.Entities.Region;
using Abp.EntityFramework.Entities.Station;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless.Antenna;
using Lte.Domain.Excel;
using Lte.Domain.LinqToExcel;
using Lte.Domain.Regular;
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
        private readonly ICellRepository _cellRepository;
        private readonly ILteRruRepository _rruRepository;
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
            ITownRepository townRepository, ICellRepository cellRepository, ILteRruRepository rruRepository)
        {
            _distributionRepository = distributionRepository;
            _constructionInformation = constructionInformation;
            _stationRruRepository = stationRruRepository;
            _stationAntennaRepository = stationAntennaRepository;
            _eNodebBaseRepository = eNodebBaseRepository;
            _eNodebRepository = eNodebRepository;
            _stationDictionary = stationDictionary;
            _townRepository = townRepository;
            _cellRepository = cellRepository;
            _rruRepository = rruRepository;
           
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
                    if (excel.StationDistrict == "南海") return excel.StationTown.Replace("金沙", "丹灶").Replace("小塘", "狮山");
                    if (excel.StationDistrict != "禅城" || excel.StationTown == "南庄") return excel.StationTown;
                    return _stationDictionary.CalculateStationTown(excel);
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
            await _cellRepository
                .UpdateOneInUse<ICellRepository, Cell, ConstructionExcel>(stat,
                    (info, excel) =>
                    {
                        if (!excel.IsOutdoor)
                        {
                            var distribution = _distributionRepository.FirstOrDefault(x =>
                                x.IndoorSerialNum == excel.IndoorDistributionSerial);
                            if (distribution == null) return;
                            if (distribution.Longtitute > 112 && distribution.Lattitute > 22)
                            {
                                info.Longtitute = distribution.Longtitute;
                                info.Lattitute = distribution.Lattitute;
                            }
                        }
                        var antenna =
                            _stationAntennaRepository.FirstOrDefault(x => x.AntennaNum == excel.AntennaSerial);
                        if (antenna == null) return;
                        if (antenna.Longtitute > 112 && antenna.Lattitute > 22)
                        {
                            info.Longtitute = antenna.Longtitute;
                            info.Lattitute = antenna.Lattitute;
                            info.ETilt = antenna.ETilt;
                            info.MTilt = antenna.MTilt;
                            info.Height = antenna.Height;
                            info.Azimuth = antenna.Azimuth;
                            info.AntennaGain = excel.BandClass == 5 ? antenna.AntennaGainLow : antenna.AntennaGainHigh;
                        }
                        
                    });
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
            var cellSerials = stat.CellSerialNum.GetSplittedFields(',');
            foreach (var cellSerial in cellSerials)
            {
                var stationCell = _constructionInformation.FirstOrDefault(x => x.CellSerialNum == cellSerial);
                if (stationCell == null) continue;
                {
                    var cell = _cellRepository.FirstOrDefault(x =>
                        x.ENodebId == stationCell.ENodebId && x.SectorId == stationCell.SectorId);
                    if (cell != null)
                    {
                        if (cell.Longtitute < 112 && cell.Lattitute < 22 && stat.Longtitute > 112 && stat.Lattitute > 22)
                        {
                            cell.Longtitute = stat.Longtitute ?? 0;
                            cell.Lattitute=stat.Lattitute ?? 0;
                        }
                    }

                    _cellRepository.SaveChanges();
                    var rru = _rruRepository.FirstOrDefault(x =>
                        x.ENodebId == stationCell.ENodebId && x.LocalSectorId == stationCell.LocalCellId);
                    if (rru == null)
                    {
                        rru = new LteRru
                        {
                            ENodebId = stationCell.ENodebId,
                            LocalSectorId = stationCell.LocalCellId,
                            RruName = stat.Address
                        };
                        _rruRepository.Insert(rru);
                    }
                    else
                    {
                        rru.RruName = stat.Address;
                    }

                }
            }

            _rruRepository.SaveChanges();
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
            var stationCell = _constructionInformation.FirstOrDefault(x => x.CellSerialNum == stat.AntennaNum);
            if (stationCell != null)
            {
                var cell = _cellRepository.FirstOrDefault(x =>
                    x.ENodebId == stationCell.ENodebId && x.SectorId == stationCell.SectorId);
                if (cell != null)
                {
                    if (cell.Longtitute < 112 && cell.Lattitute < 22 && stat.Longtitute > 112 && stat.Lattitute > 22)
                    {
                        cell.Longtitute = stat.Longtitute;
                        cell.Lattitute = stat.Lattitute;
                        cell.ETilt = stat.ETilt;
                        cell.MTilt = stat.MTilt;
                        cell.Height = stat.Height;
                        cell.Azimuth = stat.Azimuth;
                        var antennaGainArray = string.IsNullOrEmpty(stat.AntennaGain)
                            ? new string[] { }
                            : stat.AntennaGain.GetSplittedFields('/');

                        cell.AntennaGain = cell.BandClass == 5
                            ? (antennaGainArray.Length > 1
                                ? antennaGainArray[1].ConvertToDouble(17.5)
                                : (antennaGainArray.Length > 0 ? antennaGainArray[0].ConvertToDouble(17.5) : 17.5))
                            : (antennaGainArray.Length > 0 ? antennaGainArray[0].ConvertToDouble(15) : 15);
                        _cellRepository.SaveChanges();
                    }
                }
                var rru = _rruRepository.FirstOrDefault(x =>
                    x.ENodebId == stationCell.ENodebId && x.LocalSectorId == stationCell.LocalCellId);
                if (rru == null)
                {
                    rru = new LteRru
                    {
                        ENodebId = stationCell.ENodebId,
                        LocalSectorId = stationCell.LocalCellId,
                        AntennaModel = stat.AntennaModel,
                        AntennaFactory = stat.AntennaFactoryDescription.GetEnumType<AntennaFactory>(),
                        AntennaInfo = stat.AntennaPorts + "端口天线;" +
                                      (stat.CommonAntennaWithCdma == "是" ? "与C网共用天线" : "独立天线"),
                        CanBeTilt = stat.ElectricAdjustable == "是",
                        RruName = stat.AntennaAddress
                    };
                    _rruRepository.Insert(rru);
                }
                else
                {
                    rru.AntennaModel = stat.AntennaModel;
                    rru.AntennaFactory = stat.AntennaFactoryDescription.GetEnumType<AntennaFactory>();
                    rru.AntennaInfo = stat.AntennaPorts + "端口天线;" +
                                      (stat.CommonAntennaWithCdma == "是" ? "与C网共用天线" : "独立天线");
                    rru.CanBeTilt = stat.ElectricAdjustable == "是";
                    rru.RruName = stat.AntennaAddress;
                }
            
                _rruRepository.SaveChanges();
            }
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
            var stationCell = _constructionInformation.FirstOrDefault(x => x.AntennaSerial == stat.IndoorSerialNum);
            if (stationCell != null)
            {
                var cell = _cellRepository.FirstOrDefault(x =>
                    x.ENodebId == stationCell.ENodebId && x.SectorId == stationCell.SectorId);
                if (cell != null)
                {
                    if (cell.Longtitute < 112 && cell.Lattitute < 22 && stat.Longtitute > 112 && stat.Lattitute > 22)
                    {
                        cell.Longtitute = stat.Longtitute;
                        cell.Lattitute = stat.Lattitute;
                        _cellRepository.SaveChanges();
                    }
                }
                var rru = _rruRepository.FirstOrDefault(x =>
                    x.ENodebId == stationCell.ENodebId && x.LocalSectorId == stationCell.LocalCellId);
                if (rru == null)
                {
                    rru = new LteRru
                    {
                        ENodebId = stationCell.ENodebId,
                        LocalSectorId = stationCell.LocalCellId,
                        RruName = stat.Address
                    };
                    _rruRepository.Insert(rru);
                }
                else
                {
                    rru.RruName = stat.Address;
                }

                _rruRepository.SaveChanges();
            }

            return true;
        }

    }
}
