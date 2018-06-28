using Lte.Evaluations.DataService.Basic;
using Lte.Evaluations.DataService.Kpi;
using Lte.Evaluations.DataService.Mr;
using LtePlatform.Models;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Lte.Evaluations.DataService.Dt;

namespace LtePlatform.Controllers
{
    [Authorize]
    public class ParametersController : Controller
    {
        private readonly BasicImportService _basicImportService;
        private readonly AlarmsService _alarmsService;
        private readonly NearestPciCellService _neighborService;
        private readonly FlowService _flowService;
        private readonly CoverageStatService _coverageService;
        private readonly ZhangshangyouQualityService _zhangshangyouQualityService;
        private readonly ZhangshangyouCoverageService _zhangshangyouCoverageService;

        public ParametersController(BasicImportService basicImportService, AlarmsService alarmsService,
            NearestPciCellService neighborService, FlowService flowService, CoverageStatService coverageService,
            ZhangshangyouQualityService zhangshangyouQualityService, ZhangshangyouCoverageService zhangshangyouCoverageService)
        {
            _basicImportService = basicImportService;
            _alarmsService = alarmsService;
            _neighborService = neighborService;
            _flowService = flowService;
            _coverageService = coverageService;
            _zhangshangyouQualityService = zhangshangyouQualityService;
            _zhangshangyouCoverageService = zhangshangyouCoverageService;
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
            ViewBag.Message = "共上传掌上优信号详单文件" + zhangshangyouQuality.Length + "个！";
            foreach (var file in zhangshangyouQuality)
            {
                _alarmsService.UploadHwAlarms(new StreamReader(file.InputStream, Encoding.GetEncoding("GB2312")));
            }

            return View("AlarmImport");
        }

        public ActionResult BasicImport()
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
                var count = _basicImportService.ImportStationDictionaries(dictPath);
                ViewBag.Message = "共上传集团站点记录" + count + "条";
            } 
            return View("BasicImport");
        }

        [HttpPost]
        public ActionResult ENodebBasePost()
        {
            var dictFile = Request.Files["eNodebBase"];
            if (dictFile != null && dictFile.FileName != "")
            {
                var dictPath = dictFile.UploadParametersFile();
                var count = _basicImportService.ImportStationENodebs(dictPath);
                ViewBag.Message = "共上传集团基站记录" + count + "条";
            }
            return View("BasicImport");
        }

        [HttpPost]
        public ActionResult ConstructionPost()
        {
            var dictFile = Request.Files["construction"];
            if (dictFile != null && dictFile.FileName != "")
            {
                var dictPath = dictFile.UploadParametersFile();
                var count = _basicImportService.ImportConstructions(dictPath);
                ViewBag.Message = "共上传集团小区记录" + count + "条";
            }
            return View("BasicImport");
        }

        [HttpPost]
        public ActionResult StationRruPost()
        {
            var dictFile = Request.Files["stationRru"];
            if (dictFile != null && dictFile.FileName != "")
            {
                var dictPath = dictFile.UploadParametersFile();
                var count = _basicImportService.ImportStationRrus(dictPath);
                ViewBag.Message = "共上传集团RRU记录" + count + "条";
            }
            return View("BasicImport");
        }

        [HttpPost]
        public ActionResult StationAntennaPost()
        {
            var dictFile = Request.Files["stationAntenna"];
            if (dictFile != null && dictFile.FileName != "")
            {
                var dictPath = dictFile.UploadParametersFile();
                var count = _basicImportService.ImportStationAntennas(dictPath);
                ViewBag.Message = "共上传集团天线记录" + count + "条";
            }
            return View("BasicImport");
        }

        [HttpPost]
        public ActionResult DistributionPost()
        {
            var distributionFile = Request.Files["distribution"];
            if (distributionFile!=null&&distributionFile.FileName!="")
            {
                var path = distributionFile.UploadParametersFile();
                var count = _basicImportService.ImportDistributions(path);
                ViewBag.Message = "共上传室内分布记录" + count + "条";
            }
            return View("BasicImport");
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
                    _neighborService.UploadMrGrids(new StreamReader(file.InputStream), file.FileName);
                }
            }
            return View("NeighborImport");
        }
        
        [HttpPost]
        public ActionResult HwFlowPost(HttpPostedFileBase[] flowHw)
        {
            if (flowHw != null && flowHw.Length > 0 && !string.IsNullOrEmpty(flowHw[0]?.FileName))
            {
                ViewBag.Message = "共上传华为流量信息文件" + flowHw.Length + "个！";
                foreach (var file in flowHw)
                {
                    _flowService.UploadFlowHuaweis(new StreamReader(file.InputStream, Encoding.GetEncoding("GB2312")));
                }
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
    }
}