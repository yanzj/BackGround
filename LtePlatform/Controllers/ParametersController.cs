using Lte.Evaluations.DataService.Basic;
using Lte.Evaluations.DataService.Kpi;
using Lte.Evaluations.DataService.Mr;
using LtePlatform.Models;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Lte.Domain.Common.Types;
using Lte.Evaluations.DataService.Dt;

namespace LtePlatform.Controllers
{
    [Authorize]
    public class ParametersController : Controller
    {
        private readonly BasicImportService _basicImportService;
        private readonly StationImportService _stationImportService;
        private readonly AlarmsService _alarmsService;
        private readonly NearestPciCellService _neighborService;
        private readonly MrGridService _mrGridService;
        private readonly FlowService _flowService;
        private readonly CoverageStatService _coverageService;
        private readonly ZhangshangyouQualityService _zhangshangyouQualityService;
        private readonly ZhangshangyouCoverageService _zhangshangyouCoverageService;
        private readonly HourPrbService _hourKpiService;
        private readonly HourUsersService _hourUsersService;
        private readonly HourCqiService _hourCqiService;

        public ParametersController(BasicImportService basicImportService, AlarmsService alarmsService,
            NearestPciCellService neighborService, MrGridService mrGridService,
            FlowService flowService, CoverageStatService coverageService, StationImportService stationImportService,
            ZhangshangyouQualityService zhangshangyouQualityService, ZhangshangyouCoverageService zhangshangyouCoverageService,
            HourPrbService hourKpiService, HourUsersService hourUsersService, HourCqiService hourCqiService)
        {
            _basicImportService = basicImportService;
            _alarmsService = alarmsService;
            _neighborService = neighborService;
            _mrGridService = mrGridService;
            _flowService = flowService;
            _coverageService = coverageService;
            _stationImportService = stationImportService;
            _zhangshangyouQualityService = zhangshangyouQualityService;
            _zhangshangyouCoverageService = zhangshangyouCoverageService;
            _hourKpiService = hourKpiService;
            _hourUsersService = hourUsersService;
            _hourCqiService = hourCqiService;
        }
        
        public ActionResult AlarmImport()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ZteAlarmPost(HttpPostedFileBase[] alarmZte)
        {
            if (alarmZte == null || alarmZte.Length <= 0 || string.IsNullOrEmpty(alarmZte[0]?.FileName))
                return View("AlarmImport");
            ViewBag.Message = "共上传中兴告警信息文件" + alarmZte.Length + "个！";
            foreach (var file in alarmZte)
            {
                _alarmsService.UploadZteAlarms(new StreamReader(file.InputStream, Encoding.GetEncoding("GB2312")));
            }
            return View("AlarmImport");
        }

        [HttpPost]
        public ActionResult HwAlarmPost(HttpPostedFileBase[] alarmHw)
        {
            if (alarmHw == null || alarmHw.Length <= 0 || string.IsNullOrEmpty(alarmHw[0]?.FileName))
                return View("AlarmImport");
            ViewBag.Message = "共上传华为告警信息文件" + alarmHw.Length + "个！";
            foreach (var file in alarmHw)
            {
                _alarmsService.UploadHwAlarms(new StreamReader(file.InputStream, Encoding.GetEncoding("GB2312")));
            }
            return View("AlarmImport");
        }

        [HttpPost]
        public ActionResult CoveragePost(HttpPostedFileBase[] alarmHw)
        {

            var httpPostedFileBase = Request.Files["coverage"];
            if (httpPostedFileBase == null || httpPostedFileBase.FileName == "")
            {
                ViewBag.ErrorMessage = "上传文件为空！请先上传文件。";
            }
            else
            {
                var path = httpPostedFileBase.UploadKpiFile();
                _coverageService.UploadStats(path, httpPostedFileBase.FileName.Split('.')[0]);
            }
            return View("AlarmImport");
        }

        [HttpPost]
        public ActionResult ZhangshangyouQualityPost(HttpPostedFileBase[] zhangshangyouQuality)
        {
            if (zhangshangyouQuality == null || zhangshangyouQuality.Length <= 0 ||
                string.IsNullOrEmpty(zhangshangyouQuality[0]?.FileName))
                return View("AlarmImport");
            ViewBag.Message = "共上传掌上优测试详单文件" + zhangshangyouQuality.Length + "个！";
            foreach (var file in zhangshangyouQuality)
            {
                _zhangshangyouQualityService.UploadStats(new StreamReader(file.InputStream, Encoding.GetEncoding("GB2312")));
            }

            return View("AlarmImport");
        }

        [HttpPost]
        public ActionResult ZhangshangyouCoveragePost(HttpPostedFileBase[] zhangshangyouCoverage)
        {
            if (zhangshangyouCoverage == null || zhangshangyouCoverage.Length <= 0 ||
                string.IsNullOrEmpty(zhangshangyouCoverage[0]?.FileName))
                return View("AlarmImport");
            ViewBag.Message = "共上传掌上优信号详单文件" + zhangshangyouCoverage.Length + "个！";
            foreach (var file in zhangshangyouCoverage)
            {
                _zhangshangyouCoverageService.UploadStats(new StreamReader(file.InputStream, Encoding.GetEncoding("GB2312")));
            }

            return View("AlarmImport");
        }

        public ActionResult BasicImport()
        {
            return View();
        }
        
        public ActionResult StationImport()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LteImportPost()
        {
            var lteFile = Request.Files["lteExcel"];
            if (lteFile != null && lteFile.FileName != "")
            {
                var ltePath = lteFile.UploadParametersFile();
                BasicImportContainer.ENodebExcels = _basicImportService.ImportENodebExcels(ltePath);
                BasicImportContainer.CellExcels = _basicImportService.ImportCellExcels(ltePath);
                BasicImportContainer.LteRruIndex = 0;
                BasicImportContainer.LteCellIndex = 0;
            }
            return RedirectToAction("BasicImport");
        }

        [HttpPost]
        public ActionResult CdmaImportPost()
        {
            var cdmaFile = Request.Files["cdmaExcel"];
            if (cdmaFile != null && cdmaFile.FileName != "")
            {
                var cdmaPath = cdmaFile.UploadParametersFile();
                BasicImportContainer.CdmaCellExcels = _basicImportService.ImportCdmaParameters(cdmaPath);
                BasicImportContainer.CdmaRruIndex = 0;
            }
            return RedirectToAction("BasicImport");
        }

        [HttpPost]
        public ActionResult StationDictionaryPost()
        {
            var dictFile = Request.Files["stationDictionary"];
            if (dictFile!=null&& dictFile.FileName!="")
            {
                var dictPath = dictFile.UploadParametersFile();
                var count = _stationImportService.ImportStationDictionaries(dictPath);
                ViewBag.Message = "共上传集团站点记录" + count + "条";
            } 
            return View("StationImport");
        }

        [HttpPost]
        public ActionResult ENodebBasePost()
        {
            var dictFile = Request.Files["eNodebBase"];
            if (dictFile != null && dictFile.FileName != "")
            {
                var dictPath = dictFile.UploadParametersFile();
                var count = _stationImportService.ImportStationENodebs(dictPath);
                ViewBag.Message = "共上传集团基站记录" + count + "条";
            }
            return View("StationImport");
        }

        [HttpPost]
        public ActionResult ConstructionPost(HttpPostedFileBase[] construction)
        {
            if (construction == null || construction.Length <= 0 ||
                string.IsNullOrEmpty(construction[0]?.FileName))
                return View("StationImport");
            var count = 0;
            foreach (var dictFile in construction)
            {
                var dictPath = dictFile.UploadParametersFile();
                count = _stationImportService.ImportConstructions(dictPath);
            }
            
            ViewBag.Message = "共上传集团小区记录" + count + "条";
            return View("StationImport");
        }

        [HttpPost]
        public ActionResult StationRruPost(HttpPostedFileBase[] stationRru)
        {
            if (stationRru == null || stationRru.Length <= 0 ||
                string.IsNullOrEmpty(stationRru[0]?.FileName))
                return View("StationImport");
            var count = 0;
            foreach (var dictFile in stationRru)
            {
                var dictPath = dictFile.UploadParametersFile();
                count = _stationImportService.ImportStationRrus(dictPath);
            }
            ViewBag.Message = "共上传集团RRU记录" + count + "条";

            return View("StationImport");
        }

        [HttpPost]
        public ActionResult StationAntennaPost(HttpPostedFileBase[] stationAntenna)
        {
            if (stationAntenna == null || stationAntenna.Length <= 0 ||
                string.IsNullOrEmpty(stationAntenna[0]?.FileName))
                return View("StationImport");
            var count = 0;
            foreach (var dictFile in stationAntenna)
            {
                var dictPath = dictFile.UploadParametersFile();
                count = _stationImportService.ImportStationAntennas(dictPath);
            }
            ViewBag.Message = "共上传集团天线记录" + count + "条";

            return View("StationImport");
        }

        [HttpPost]
        public ActionResult DistributionPost()
        {
            var distributionFile = Request.Files["distribution"];
            if (distributionFile!=null&&distributionFile.FileName!="")
            {
                var path = distributionFile.UploadParametersFile();
                var count = _stationImportService.ImportDistributions(path);
                ViewBag.Message = "共上传室内分布记录" + count + "条";
            }
            return View("StationImport");
        }

        public ActionResult HotSpotPost()
        {
            var hotSpotFile = Request.Files["hotSpot"];
            if (hotSpotFile != null && hotSpotFile.FileName != "")
            {
                var path = hotSpotFile.UploadParametersFile();
                var count = _basicImportService.ImportHotSpots(path);
                ViewBag.Message = "共上传热点基础数据记录" + count + "条";
            }
            return View("BasicImport");
        }

        [Authorize]
        public ActionResult NeighborImport()
        {
            return View();
        }

        [Authorize]
        public ActionResult HourKpiImport()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ZteNeighborPost(HttpPostedFileBase[] neighborZte)
        {
            if (neighborZte == null || neighborZte.Length <= 0 || string.IsNullOrEmpty(neighborZte[0]?.FileName))
                return View("NeighborImport");
            var count = 0;
            foreach (var file in neighborZte)
            {
                count +=
                    await
                        _neighborService.UploadMrGridKpiPoints(new StreamReader(file.InputStream,
                            Encoding.GetEncoding("GB2312")));
            }
            ViewBag.Message = "共上传MR指标数据文件" + neighborZte.Length + "个！数据" + count + "条";
            return View("NeighborImport");
        }

        [HttpPost]
        public ActionResult HwNeighborPost(HttpPostedFileBase[] neighborHw)
        {
            if (neighborHw != null && neighborHw.Length > 0 && !string.IsNullOrEmpty(neighborHw[0]?.FileName))
            {
                ViewBag.Message = "共上传MR栅格信息文件" + neighborHw.Length + "个！";
                foreach (var file in neighborHw)
                {
                    _mrGridService.UploadMrGrids(new StreamReader(file.InputStream), file.FileName);
                }
            }
            return View("NeighborImport");
        }
        
        [HttpPost]
        public ActionResult HwFlowPost(HttpPostedFileBase[] flowHw)
        {
            if (flowHw == null || flowHw.Length <= 0 || string.IsNullOrEmpty(flowHw[0]?.FileName))
                return View("NeighborImport");
            ViewBag.Message = "共上传华为流量信息文件" + flowHw.Length + "个！";
            foreach (var file in flowHw)
            {
                _flowService.UploadFlowHuaweis(new StreamReader(file.InputStream, Encoding.GetEncoding("GB2312")));
            }
            return View("NeighborImport");
        }

        [HttpPost]
        public ActionResult HwCqiPost(HttpPostedFileBase[] cqiHw)
        {
            if (cqiHw != null && cqiHw.Length > 0 && !string.IsNullOrEmpty(cqiHw[0]?.FileName))
            {
                ViewBag.Message = "共上传华为CQI信息文件" + cqiHw.Length + "个！";
                foreach (var file in cqiHw)
                {
                    _flowService.UploadCqiHuaweis(new StreamReader(file.InputStream, Encoding.GetEncoding("GB2312")));
                }
            }
            return View("NeighborImport");
        }

        [HttpPost]
        public ActionResult ZteFlowPost(HttpPostedFileBase[] flowZte)
        {
            if (flowZte != null && flowZte.Length > 0 && !string.IsNullOrEmpty(flowZte[0]?.FileName))
            {
                ViewBag.Message = "共上传中兴流量信息文件" + flowZte.Length + "个！";
                foreach (var file in flowZte)
                {
                    _flowService.UploadFlowZtes(new StreamReader(file.InputStream, Encoding.GetEncoding("GB2312")));
                }
            }
            return View("NeighborImport");
        }

        public ActionResult HourPrbPost(HttpPostedFileBase[] hourPrb)
        {
            if (hourPrb != null && hourPrb.Length > 0 && !string.IsNullOrEmpty(hourPrb[0]?.FileName))
            {
                ViewBag.Message = "共上传忙时PRB信息文件" + hourPrb.Length + "个！";
                foreach (var file in hourPrb)
                {
                    _hourKpiService.UploadPrbs(new StreamReader(file.InputStream, Encoding.GetEncoding("GB2312")));
                }
            }
            return View("HourKpiImport");
        }

        public ActionResult HourUsersPost(HttpPostedFileBase[] hourUsers)
        {
            if (hourUsers != null && hourUsers.Length > 0 && !string.IsNullOrEmpty(hourUsers[0]?.FileName))
            {
                ViewBag.Message = "共上传忙时用户数信息文件" + hourUsers.Length + "个！";
                foreach (var file in hourUsers)
                {
                    _hourUsersService.UploadUserses(new StreamReader(file.InputStream, Encoding.GetEncoding("GB2312")));
                }
            }
            return View("HourKpiImport");
        }
        
        public ActionResult HourCqiPost(HttpPostedFileBase[] hourCqi)
        {
            if (hourCqi != null && hourCqi.Length > 0 && !string.IsNullOrEmpty(hourCqi[0]?.FileName))
            {
                ViewBag.Message = "共上传忙时CQI优良比信息文件" + hourCqi.Length + "个！";
                foreach (var file in hourCqi)
                {
                    _hourCqiService.UploadCqis(new StreamReader(file.InputStream, Encoding.GetEncoding("GB2312")));
                }
            }
            return View("HourKpiImport");
        }
    }
}