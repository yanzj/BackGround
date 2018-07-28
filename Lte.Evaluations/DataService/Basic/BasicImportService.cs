using System;
using Lte.Domain.LinqToExcel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.EntityFramework.Entities;
using Lte.Domain.Common.Geo;
using Lte.MySqlFramework.Abstract;
using Lte.MySqlFramework.Entities;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Excel;

namespace Lte.Evaluations.DataService.Basic
{
    public class BasicImportService
    {
        private readonly IENodebRepository _eNodebRepository;
        private readonly ICellRepository _cellRepository;
        private readonly IBtsRepository _btsRepository;
        private readonly ICdmaCellRepository _cdmaCellRepository;
        private readonly IStationDictionaryRepository _stationDictionary;
        private readonly IENodebBaseRepository _eNodebBaseRepository;
        private readonly IDistributionRepository _distributionRepository;
        private readonly IHotSpotCellRepository _hotSpotCellRepository;
        private readonly IConstructionInformationRepository _constructionInformation;
        private readonly IStationRruRepository _stationRruRepository;
        private readonly IStationAntennaRepository _stationAntennaRepository;

        public BasicImportService(IENodebRepository eNodebRepository, ICellRepository cellRepository,
            IBtsRepository btsRepository, ICdmaCellRepository cdmaCellRepository, 
            IStationDictionaryRepository stationDictionary, IDistributionRepository distributionRepository,
            IHotSpotCellRepository hotSpotCellRepository, IENodebBaseRepository eNodebBaseRepository,
            IConstructionInformationRepository constructionInformation, IStationRruRepository stationRruRepository,
            IStationAntennaRepository stationAntennaRepository)
        {
            _eNodebRepository = eNodebRepository;
            _cellRepository = cellRepository;
            _btsRepository = btsRepository;
            _cdmaCellRepository = cdmaCellRepository;
            _stationDictionary = stationDictionary;
            _distributionRepository = distributionRepository;
            _hotSpotCellRepository = hotSpotCellRepository;
            _eNodebBaseRepository = eNodebBaseRepository;
            _constructionInformation = constructionInformation;
            _stationRruRepository = stationRruRepository;
            _stationAntennaRepository = stationAntennaRepository;
            if (Stations == null) Stations = new Stack<StationDictionaryExcel>();
            if (ENodebBases == null) ENodebBases = new Stack<ENodebBaseExcel>();
            if (StationCells == null) StationCells = new Stack<ConstructionExcel>();
            if (StationRrus == null) StationRrus = new Stack<StationRruExcel>();
        }

        public static List<BtsExcel> BtsExcels { get; set; } = new List<BtsExcel>();

        private static Stack<StationDictionaryExcel> Stations { get; set; }

        public int StationsCount => Stations.Count;

        private static Stack<ENodebBaseExcel> ENodebBases { get; set; }

        public int StationENodebCount => ENodebBases.Count;

        private static Stack<ConstructionExcel> StationCells { get; set; }

        public int StationCellCount => StationCells.Count;

        private static Stack<StationRruExcel> StationRrus { get; set; }

        public int StationRruCount => StationRrus.Count;
        
        public List<ENodebExcel> ImportENodebExcels(string path)
        {
            var repo = new ExcelQueryFactory { FileName = path };
            return (from c in repo.Worksheet<ENodebExcel>("基站级") select c).ToList();
        }

        public List<CellExcel> ImportCellExcels(string path)
        {
            var repo = new ExcelQueryFactory { FileName = path };
            return (from c in repo.Worksheet<CellExcel>("小区级") select c).ToList();
        } 

        public List<CdmaCellExcel> ImportCdmaParameters(string path)
        {
            var repo = new ExcelQueryFactory { FileName = path };
            BtsExcels = (from c in repo.Worksheet<BtsExcel>("基站级")
                select c).Where(x => x.BtsId > 0).ToList();
            return (from c in repo.Worksheet<CdmaCellExcel>("小区级")
                select c).ToList();
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
                .UpdateOne<IStationDictionaryRepository, StationDictionary, StationDictionaryExcel>(stat);
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
                .UpdateOne<IENodebBaseRepository, ENodebBase, ENodebBaseExcel>(stat);
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
                .UpdateOne<IConstructionInformationRepository, ConstructionInformation, ConstructionExcel>(stat);
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
                .UpdateOne<IStationRruRepository, StationRru, StationRruExcel>(stat);
            return true;
        }

        public int ImportStationAntennas(string path)
        {
            var repo = new ExcelQueryFactory { FileName = path };
            var excels = (from c in repo.Worksheet<StationAntennaExcel>("Sheet1") select c).ToList();
            return
                _stationAntennaRepository
                    .Import<IStationAntennaRepository, StationAntenna, StationAntennaExcel>(excels);
        }

        public int ImportDistributions(string path)
        {
            var repo = new ExcelQueryFactory { FileName = path };
            var excels = (from c in repo.Worksheet<IndoorDistributionExcel>("Sheet1") select c).ToList();
            return
                _distributionRepository.Import<IDistributionRepository, IndoorDistribution, IndoorDistributionExcel>(
                    excels);
        }

        public int ImportHotSpots(string path)
        {
            var repo = new ExcelQueryFactory { FileName = path };
            var excels = (from c in repo.Worksheet<HotSpotCellExcel>("基础信息") select c).ToList();
            return _hotSpotCellRepository.Import<IHotSpotCellRepository, HotSpotCellId, HotSpotCellExcel>(excels);
        }

        public IEnumerable<ENodebExcel> GetNewENodebExcels()
        {
            if (!BasicImportContainer.ENodebExcels.Any()) return new List<ENodebExcel>();
            return from info in BasicImportContainer.ENodebExcels
                join eNodeb in _eNodebRepository.GetAllInUseList()
                    on new {info.ENodebId, info.Name} equals new {eNodeb.ENodebId, eNodeb.Name} into eNodebQuery
                from eq in eNodebQuery.DefaultIfEmpty()
                where eq == null
                select info;
        }

        public IEnumerable<int> GetVanishedENodebIds()
        {
            if (!BasicImportContainer.ENodebExcels.Any()) return new List<int>();
            return from eNodeb in _eNodebRepository.GetAllInUseList()
                join info in BasicImportContainer.ENodebExcels
                    on new { eNodeb.ENodebId, eNodeb.Name } equals new { info.ENodebId, info.Name } into eNodebQuery
                from eq in eNodebQuery.DefaultIfEmpty()
                where eq == null
                select eNodeb.ENodebId;
        }

        public IEnumerable<int> GetVanishedBtsIds()
        {
            if (!BtsExcels.Any()) return new List<int>();
            return from bts in _btsRepository.GetAllInUseList()
                   join info in BtsExcels
                       on bts.BtsId equals info.BtsId into btsQuery
                   from eq in btsQuery.DefaultIfEmpty()
                   where eq == null
                   select bts.BtsId;
        }

        public IEnumerable<CellExcel> GetNewCellExcels()
        {
            if (!BasicImportContainer.CellExcels.Any()) return new List<CellExcel>();
            return from info in BasicImportContainer.CellExcels
                join cell in _cellRepository.GetAllInUseList()
                    on new {info.ENodebId, info.SectorId, info.Pci} equals 
                    new {cell.ENodebId, cell.SectorId, cell.Pci} into cellQuery
                from cq in cellQuery.DefaultIfEmpty()
                where cq == null
                select info;
        }

        public IEnumerable<CellIdPair> GetVanishedCellIds()
        {
            if (!BasicImportContainer.CellExcels.Any()) return new List<CellIdPair>();
            return from cell in _cellRepository.GetAllInUseList()
                join info in BasicImportContainer.CellExcels
                    on new {cell.ENodebId, cell.SectorId} equals new {info.ENodebId, info.SectorId}
                    into cellQuery
                from cq in cellQuery.DefaultIfEmpty()
                where cq == null
                select new CellIdPair {CellId = cell.ENodebId, SectorId = cell.SectorId};
        }

        public IEnumerable<CdmaCellIdPair> GetVanishedCdmaCellIds()
        {
            if (!BasicImportContainer.CdmaCellExcels.Any()) return new List<CdmaCellIdPair>();
            return from cell in _cdmaCellRepository.GetAllInUseList()
                   join info in BasicImportContainer.CdmaCellExcels
                       on new { cell.BtsId, cell.SectorId, cell.CellType } equals new { info.BtsId, info.SectorId, info.CellType }
                       into cellQuery
                   from cq in cellQuery.DefaultIfEmpty()
                   where cq == null
                   select new CdmaCellIdPair { CellId = cell.BtsId, SectorId = cell.SectorId, CellType = cell.CellType };
        }

        public IEnumerable<BtsExcel> GetNewBtsExcels()
        {
            if (!BtsExcels.Any()) return new List<BtsExcel>();
            return from info in BtsExcels
                join bts in _btsRepository.GetAllList()
                    on info.BtsId equals bts.BtsId into btsQuery
                from bq in btsQuery.DefaultIfEmpty()
                where bq == null
                select info;
        }

        public IEnumerable<CdmaCellExcel> GetNewCdmaCellExcels()
        {
            if (!BasicImportContainer.CdmaCellExcels.Any()) return new List<CdmaCellExcel>();
            return from info in BasicImportContainer.CdmaCellExcels
                join cell in _cdmaCellRepository.GetAllList()
                    on new {info.BtsId, info.SectorId} equals new {cell.BtsId, cell.SectorId} into cellQuery
                from cq in cellQuery.DefaultIfEmpty()
                where cq == null
                select info;
        }
    }
}
