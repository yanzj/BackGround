using Abp.EntityFramework.Repositories;
using Lte.Domain.LinqToExcel;
using Lte.Parameters.Abstract.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities.Cdma;
using Abp.EntityFramework.Entities.Complain;
using Abp.EntityFramework.Entities.Infrastructure;
using Abp.EntityFramework.Entities.Maintainence;
using Abp.EntityFramework.Entities.Region;
using Lte.Domain.Common.Geo;
using Lte.Domain.Excel;
using Lte.Domain.LinqToCsv.Context;
using Lte.Domain.LinqToCsv.Description;
using Lte.Evaluations.DataService.Dt;
using Lte.Evaluations.Policy;
using Lte.MySqlFramework.Abstract.Cdma;
using Lte.MySqlFramework.Abstract.Complain;
using Lte.MySqlFramework.Abstract.Infrastructure;
using Lte.MySqlFramework.Abstract.Maintainence;
using Lte.MySqlFramework.Abstract.Region;
using Lte.MySqlFramework.Abstract.Test;
using Lte.Parameters.Entities.Dt;

namespace Lte.Evaluations.DataService.Kpi
{
    public class KpiImportService
    {
        private readonly ICdmaRegionStatRepository _regionStatRepository;
        private readonly ITopDrop2GCellRepository _top2GRepository;
        private readonly ITopConnection3GRepository _top3GRepository;
        private readonly ITopConnection2GRepository _topConnection2GRepository;
        private readonly IVipDemandRepository _vipDemandRepository;
        private readonly IComplainItemRepository _complainItemRepository;
        private readonly IBranchDemandRepository _branchDemandRepository;
        private readonly IOnlineSustainRepository _onlineSustainRepository;
        private readonly IPlanningSiteRepository _planningSiteRepository;
        private readonly IComplainProcessRepository _processRepository;
        private readonly IFileRecordRepository _fileRecordRepository;
        private readonly IDtFileInfoRepository _dtFileInfoRepository;
        private readonly IRasterTestInfoRepository _rasterTestInfoRepository;
        private readonly ILteProblemRepository _lteProblemRepository;
        private readonly IVipProcessRepository _vipProcessRepository;
        private readonly List<Town> _towns; 

        public KpiImportService(ICdmaRegionStatRepository regionStatRepository,
            ITopDrop2GCellRepository top2GRepository, ITopConnection3GRepository top3GRepository,
            ITopConnection2GRepository topConnection2GRepository, IVipDemandRepository vipDemandRepository,
            IComplainItemRepository complainItemRepository, IBranchDemandRepository branchDemandRepository,
            IOnlineSustainRepository onlineSustainRepository, IPlanningSiteRepository planningSiteRepository, 
            IComplainProcessRepository processRepository, ITownRepository townRepository,
            IFileRecordRepository fileRecordRepository, IDtFileInfoRepository dtFileInfoRepository,
            IRasterTestInfoRepository rasterTestInfoRepository, ILteProblemRepository lteProblemRepository,
            IVipProcessRepository vipProcessRepository)
        {
            _regionStatRepository = regionStatRepository;
            _top2GRepository = top2GRepository;
            _top3GRepository = top3GRepository;
            _topConnection2GRepository = topConnection2GRepository;
            _vipDemandRepository = vipDemandRepository;
            _complainItemRepository = complainItemRepository;
            _branchDemandRepository = branchDemandRepository;
            _onlineSustainRepository = onlineSustainRepository;
            _planningSiteRepository = planningSiteRepository;
            _processRepository = processRepository;
            _fileRecordRepository = fileRecordRepository;
            _dtFileInfoRepository = dtFileInfoRepository;
            _rasterTestInfoRepository = rasterTestInfoRepository;
            _lteProblemRepository = lteProblemRepository;
            _vipProcessRepository = vipProcessRepository;
            _towns = townRepository.GetAllList();
        }
        public List<string> Import(string path, IEnumerable<string> regions)
        {
            var factory = new ExcelQueryFactory {FileName = path};
            var message = (from region in regions
                let stats = (from c in factory.Worksheet<CdmaRegionStatExcel>(region)
                    where c.StatDate > DateTime.Today.AddDays(-30) && c.StatDate <= DateTime.Today
                    select c).ToList()
                let count = _regionStatRepository.Import<ICdmaRegionStatRepository, CdmaRegionStat, CdmaRegionStatExcel>(stats)
                select "完成导入区域：'" + region + "'的日常指标导入" + count + "条").ToList();

            var topDrops = (from c in factory.Worksheet<TopDrop2GCellExcel>(TopDrop2GCellExcel.SheetName)
                            select c).ToList();
            var drops = _top2GRepository.Import<ITopDrop2GCellRepository, TopDrop2GCell, TopDrop2GCellExcel>(topDrops);
            message.Add("完成TOP掉话小区导入" + drops + "个");

            var topConnections = (from c in factory.Worksheet<TopConnection3GCellExcel>(TopConnection3GCellExcel.SheetName)
                                  select c).ToList();
            var connections =
                _top3GRepository.Import<ITopConnection3GRepository, TopConnection3GCell, TopConnection3GCellExcel>(
                    topConnections);
            message.Add("完成TOP连接小区导入" + connections + "个");

            var topConnection2Gs = (from c in factory.Worksheet<TopConnection2GExcel>(TopConnection2GExcel.SheetName)
                select c).ToList();
            var connection2Gs =
                _topConnection2GRepository.Import<ITopConnection2GRepository, TopConnection2GCell, TopConnection2GExcel>
                    (topConnection2Gs);
            message.Add("完成TOP呼建小区导入" + connection2Gs + "个");

            return message;
        }
        
        public string ImportVipDemand(string path)
        {
            var factory = new ExcelQueryFactory {FileName = path};
            var stats = (from c in factory.Worksheet<VipDemandExcel>("在线求助")
                select c).ToList();
            foreach (var stat in stats)
            {
                if (string.IsNullOrEmpty(stat.SerialNumber))
                {
                    stat.SerialNumber = stat.ContactPerson + "-" + stat.PhoneNumber + "-" + stat.BelongedCity + "-" +
                                        stat.Phenomenon;
                }
            }
            var count1 =
                _vipDemandRepository.Import<IVipDemandRepository, VipDemand, VipDemandExcel, Town>(
                    stats, _towns);
            var count2 =
                _vipProcessRepository.Import<IVipProcessRepository, VipProcess, VipDemandExcel>(stats);
            return "完成政企客户支撑信息导入" + count1 + "条；" + "过程信息导入" + count2 + "条";
        }

        public string ImportComplain(string path)
        {
            var factory = new ExcelQueryFactory { FileName = path };
            var stats = (from c in factory.Worksheet<ComplainExcel>("当月累积受理工单详单")
                         select c).ToList();
            foreach (var stat in stats)
            {
                var town =
                    _towns.FirstOrDefault(
                        x => stat.District.Contains(x.DistrictName)
                             && (stat.RoadName ?? "" + (stat.BuildingName ?? "")).Contains(x.TownName));
                if (town != null)
                    stat.TownId = town.Id;
            }
            var count =
                _complainItemRepository.Import<IComplainItemRepository, ComplainItem, ComplainExcel>(stats);
            return "完成抱怨量信息导入" + count + "条";
        }

        public string ImportComplainSupport(string path)
        {
            var factory = new ExcelQueryFactory { FileName = path };
            var stats = (from c in factory.Worksheet<ComplainSupplyExcel>("Sheet1")
                select c).ToList();
            var count =
                _complainItemRepository.UpdateOnlyMany<IComplainItemRepository, ComplainItem, ComplainSupplyExcel, Town>(
                    stats, _towns);
            return "完成抱怨量补充信息导入" + count + "条";
        }

        public string ImportBranchDemand(string path)
        {
            var factory = new ExcelQueryFactory {FileName = path};
            var stats = (from c in factory.Worksheet<BranchDemandExcel>("Sheet1") select c).ToList();
            var count =
                _branchDemandRepository.Import<IBranchDemandRepository, BranchDemand, BranchDemandExcel, Town>(stats,
                    _towns);
            return "完成分公司需求信息导入" + count + "条";
        }

        public string ImportOnlineDemand(string path)
        {
            var factory = new ExcelQueryFactory { FileName = path };
            var stats = (from c in factory.Worksheet<OnlineSustainExcel>("汇总表") select c).ToList();
            var count =
                _onlineSustainRepository.Import<IOnlineSustainRepository, OnlineSustain, OnlineSustainExcel, Town>(stats, _towns,
                    (towns, stat) =>
                    {
                        if (!string.IsNullOrEmpty(stat.Town))
                        {
                            var candidateTown =
                                towns.FirstOrDefault(x => x.DistrictName == stat.District && x.TownName == stat.Town);
                            if (candidateTown != null)
                            {
                                return candidateTown.Id;
                            }
                        }
                        var candidateTowns = towns.Where(x => x.DistrictName == stat.District).ToList();
                        if (!candidateTowns.Any()) candidateTowns = _towns;

                        var town = ((string.IsNullOrEmpty(stat.Site)
                            ? null
                            : candidateTowns.FirstOrDefault(x => stat.Site.Contains(x.TownName))) ??
                                    (string.IsNullOrEmpty(stat.Address)
                                        ? null
                                        : candidateTowns.FirstOrDefault(x => stat.Address.Contains(x.TownName)))) ??
                                   (string.IsNullOrEmpty(stat.Phenomenon)
                                       ? null
                                       : candidateTowns.FirstOrDefault(x => stat.Phenomenon.Contains(x.TownName)));
                        return town?.Id ?? candidateTowns.First().Id;
                    });
            var count2 =
                _processRepository.Import<IComplainProcessRepository, ComplainProcess, OnlineSustainExcel>(stats);
            return "完成电子运维投诉信息导入" + count + "条; " + "处理记录" + count2 + "条";
        }

        public string ImportPlanningSite(string path)
        {
            var factory = new ExcelQueryFactory { FileName = path };
            var stats = (from c in factory.Worksheet<PlanningSiteExcel>("谈点清单") select c).ToList();
            var count =
                _planningSiteRepository.Import<IPlanningSiteRepository, PlanningSite, PlanningSiteExcel, Town>(stats, _towns);
            return "完成规则站点信息导入" + count + "条";
        }

        public string ImportDt2GFile(string path)
        {
            bool fileExisted;
            var tableName = _fileRecordRepository.GetFileNameExisted(path, out fileExisted);
            if (fileExisted) return "数据文件已存在于数据库中。请确认是否正确。";
            var reader = new StreamReader(path, Encoding.GetEncoding("GB2312"));
            var infos = CsvContext.Read<FileRecord2GCsv>(reader, CsvFileDescription.CommaDescription).ToList();
            if (infos.FirstOrDefault(x => x.EcIo != null) == null)
            {
                var dingliInfos= CsvContext.Read<FileRecord2GDingli>(reader, CsvFileDescription.CommaDescription).ToList();
                if (dingliInfos.FirstOrDefault(x => x.EcIo != null) == null)
                    throw new Exception("不是有效的2G数据文件！");
                infos = dingliInfos.MapTo<List<FileRecord2GCsv>>();
            }
            reader.Close();
            var filterInfos =
                infos.GetFoshanGeoPoints().ToList();
            if (!filterInfos.Any()) throw new Exception("无数据或格式错误！");
            _dtFileInfoRepository.UpdateCsvFileInfo(tableName, filterInfos[0].StatTime, "2G");
            var stats = filterInfos.MergeRecords();
            _rasterTestInfoRepository.UpdateRasterInfo(stats, tableName, "2G");
            var count = _fileRecordRepository.InsertFileRecord2Gs(stats, tableName);
            return "完成2G路测文件导入：" + path + "(" + tableName + ")" + count + "条";
        }

        public string ImportDt3GFile(string path)
        {
            bool fileExisted;
            var tableName = _fileRecordRepository.GetFileNameExisted(path, out fileExisted);
            if (fileExisted) return "数据文件已存在于数据库中。请确认是否正确。";
            var reader = new StreamReader(path, Encoding.GetEncoding("GB2312"));
            var infos = CsvContext.Read<FileRecord3GCsv>(reader, CsvFileDescription.CommaDescription).ToList();
            reader.Close();
            var filterInfos =
                infos.GetFoshanGeoPoints().ToList();
            if (!filterInfos.Any()) throw new Exception("无数据或格式错误！");
            _dtFileInfoRepository.UpdateCsvFileInfo(tableName, filterInfos[0].StatTime, "3G");
            var stats = filterInfos.MergeRecords();
            _rasterTestInfoRepository.UpdateRasterInfo(stats, tableName, "3G");
            var count = _fileRecordRepository.InsertFileRecord3Gs(stats, tableName);
            return "完成3G路测文件导入：" + path + "(" + tableName + ")" + count + "条";
        }

        public string ImportDtVolteFile(string path)
        {
            bool fileExisted;
            var tableName = _fileRecordRepository.GetFileNameExisted(path, out fileExisted);
            //if (fileExisted) return "数据文件已存在于数据库中。请确认是否正确。";
            var reader = new StreamReader(path, Encoding.GetEncoding("GB2312"));
            var infos = CsvContext.Read<FileRecordVolteCsv>(reader, CsvFileDescription.CommaDescription).ToList();
            reader.Close();
            var filterInfos =
                infos.GetFoshanGeoPoints().ToList();
            if (!filterInfos.Any()) throw new Exception("无数据或格式错误！");
            _dtFileInfoRepository.UpdateCsvFileInfo(tableName, filterInfos[0].StatTime, "Volte");
            var stats = filterInfos.MergeRecords();
            if (!stats.Any()) throw new Exception("无数据或格式错误！");
            _rasterTestInfoRepository.UpdateRasterInfo(stats, tableName, "Volte");
            var count = _fileRecordRepository.InsertFileRecordVoltes(stats, tableName);
            return "完成VoLTE路测文件导入：" + path + "(" + tableName + ")" + count + "条";
        }

        public List<FileRecord4GCsv> ReadFileRecord4GCsvs(string path)
        {
            var reader = new StreamReader(path, Encoding.GetEncoding("GB2312"));
            var infos = reader.GetFileRecord4GCsvs();
            if (infos == null)
            {
                infos = reader.GetFileRecord4GByZte();
                if (infos == null)
                {
                    infos = reader.GetFileRecord4GByHuawei();
                        
                }
            }
            reader.Close();
            return infos != null ? infos.GetFoshanGeoPoints().ToList() : new List<FileRecord4GCsv>();
        }

        public string ImportDt4GFile(List<FileRecord4GCsv> filterInfos, List<string> paths, DateTime statDate)
        {
            if (!filterInfos.Any()) return "无数据或格式错误！";
            var filePath = string.Empty;
            var tableName = "";
            foreach (var path in paths)
            {
                tableName = _fileRecordRepository.GetFileNameExisted(path, out var fileExisted);
                if (!fileExisted)
                {
                    filePath = path;
                    break;
                }
            }

            if (string.IsNullOrEmpty(filePath)) return "所有表格均存在于数据库中";
            
            var statTime = filterInfos[0].StatTime.AddDays((statDate - DateTime.Today).Days);
            _dtFileInfoRepository.UpdateCsvFileInfo(tableName, statTime, "4G");
            var stats = filterInfos.MergeRecords();
            _rasterTestInfoRepository.UpdateRasterInfo(stats, tableName, "4G");
            var count = _fileRecordRepository.InsertFileRecord4Gs(stats, tableName);
            return "完成4G路测文件导入：" + filePath + "(" + tableName + ")" + count + "条";
        }

        public string ImportDt4GDingli(string path)
        {
            bool fileExisted;
            var tableName = _fileRecordRepository.GetFileNameExisted(path, out fileExisted);
            if (fileExisted) return "数据文件已存在于数据库中。请确认是否正确。";
            var reader = new StreamReader(path, Encoding.GetEncoding("GB2312"));
            var infos = CsvContext.Read<FileRecord4GDingli>(reader, CsvFileDescription.CommaDescription).ToList();
            var filterInfos =
                infos.GetFoshanGeoPoints().ToList();
            if (!filterInfos.Any()) return "无数据或格式错误！";
            _dtFileInfoRepository.UpdateCsvFileInfo(tableName, filterInfos[0].StatTime, "4G");
            var stats = filterInfos.MergeRecords();
            _rasterTestInfoRepository.UpdateRasterInfo(stats, tableName, "4G");
            var count = _fileRecordRepository.InsertFileRecord4Gs(stats, tableName);
            return "完成4G路测文件导入：" + path + "(" + tableName + ")" + count + "条";
        }

        public string ImportStandardProblem(string path)
        {
            var factory = new ExcelQueryFactory { FileName = path };
            var stats = (from c in factory.Worksheet<StandarProblemExcel>("标准题目") select c).ToList();
            var count =
                _lteProblemRepository.Import<ILteProblemRepository, LteProblem, StandarProblemExcel>(stats);
            return "完成标准考试题目导入" + count + "条";
        }

        public string ImportChoiceProblem(string path)
        {
            var factory = new ExcelQueryFactory { FileName = path };
            var stats = (from c in factory.Worksheet<ChoiceProblemExcel>("标准题目") select c).ToList();
            var count =
                _lteProblemRepository.Import<ILteProblemRepository, LteProblem, ChoiceProblemExcel>(stats);
            return "完成不定项选择题目导入" + count + "条";
        }
    }
}
