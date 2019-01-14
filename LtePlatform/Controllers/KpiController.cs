using System;
using Lte.Domain.Common;
using Lte.Evaluations.DataService;
using Lte.Evaluations.DataService.Basic;
using Lte.Evaluations.DataService.Kpi;
using LtePlatform.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Lte.Domain.Common.Types;
using Lte.Domain.Regular;
using Lte.Evaluations.DataService.Dump;
using Lte.Parameters.Entities.Dt;

namespace LtePlatform.Controllers
{
    [Authorize(Roles = "指标导入")]
    public class KpiController : Controller
    {
        private readonly TownQueryService _townService;
        private readonly KpiImportService _importService;
        private readonly WorkItemService _workItemService;

        public KpiController(TownQueryService townService, KpiImportService importService, WorkItemService workItemService)
        {
            _townService = townService;
            _importService = importService;
            _workItemService = workItemService;
        }
        
        public ActionResult Import()
        {
            return View();
        }

        public ActionResult DtImport()
        {
            return View();
        }
        
        public ActionResult CollegeFlowImport()
        {
            return View();
        }
        
        public ActionResult MarketFlowImport()
        {
            return View();
        }

        public ActionResult TransportationFlowImport()
        {
            return View();
        }

        public ActionResult ManageDt()
        {
            return View();
        }

        public ActionResult ComplainAdjust()
        {
            return View();
        }

        [HttpPost]
        public ViewResult KpiImport()
        {
            var message = new List<string>();
            var httpPostedFileBase = Request.Files["dailyKpi"];
            if (httpPostedFileBase == null || httpPostedFileBase.FileName == "")
            {
                ViewBag.ErrorMessage = "上传文件为空！请先上传文件。";
            }
            else
            {
                var fields = httpPostedFileBase.FileName.GetSplittedFields(new [] {'.', '\\'});
                var city = fields[fields.Length - 2];
                var legalCities = _townService.GetCities();
                if (legalCities.Count > 0 && legalCities.FirstOrDefault(x => x == city) == null)
                {
                    ViewBag.WarningMessage = "上传文件名对应的城市" + city + "找不到。使用'" + legalCities[0] + "'代替";
                    city = legalCities[0];
                }
                var regions = _townService.GetRegions(city);
                var path = httpPostedFileBase.UploadKpiFile();
                message = _importService.Import(path, regions);
            }
            ViewBag.Message = message;
            return View("Import");
        }
        
        [HttpPost]
        public ViewResult VipDemandImport()
        {
            var httpPostedFileBase = Request.Files["vipDemand"];
            if (httpPostedFileBase == null || httpPostedFileBase.FileName == "")
            {
                ViewBag.ErrorMessage = "上传文件为空！请先上传文件。";
            }
            else
            {
                var path = httpPostedFileBase.UploadKpiFile();
                ViewBag.Message = _importService.ImportVipDemand(path);
            }
            return View("Import");
        }

        [HttpPost]
        public ViewResult ComplainImport()
        {
            var httpPostedFileBase = Request.Files["complain"];
            if (httpPostedFileBase == null || httpPostedFileBase.FileName == "")
            {
                ViewBag.ErrorMessage = "上传文件为空！请先上传文件。";
            }
            else
            {
                var path = httpPostedFileBase.UploadKpiFile();
                ViewBag.Message = _importService.ImportComplain(path);
            }
            return View("Import");
        }

        [HttpPost]
        public ViewResult ComplainSupportImport()
        {
            var httpPostedFileBase = Request.Files["complainSupply"];
            if (httpPostedFileBase == null || httpPostedFileBase.FileName == "")
            {
                ViewBag.ErrorMessage = "上传文件为空！请先上传文件。";
            }
            else
            {
                var path = httpPostedFileBase.UploadKpiFile();
                ViewBag.Message = _importService.ImportComplainSupport(path);
            }
            return View("Import");
        }

        [HttpPost]
        public ViewResult BranchImport()
        {
            var httpPostedFileBase = Request.Files["branch"];
            if (httpPostedFileBase == null || httpPostedFileBase.FileName == "")
            {
                ViewBag.ErrorMessage = "上传文件为空！请先上传文件。";
            }
            else
            {
                var path = httpPostedFileBase.UploadKpiFile();
                ViewBag.Message = _importService.ImportBranchDemand(path);
            }
            return View("Import");
        }

        [HttpPost]
        public ViewResult OnlineImport()
        {
            var httpPostedFileBase = Request.Files["online"];
            if (httpPostedFileBase == null || httpPostedFileBase.FileName == "")
            {
                ViewBag.ErrorMessage = "上传文件为空！请先上传文件。";
            }
            else
            {
                var path = httpPostedFileBase.UploadKpiFile();
                ViewBag.Message = _importService.ImportOnlineDemand(path);
            }
            return View("Import");
        }

        [HttpPost]
        public ViewResult PlanningImport()
        {
            var httpPostedFileBase = Request.Files["planning"];
            if (httpPostedFileBase == null || httpPostedFileBase.FileName == "")
            {
                ViewBag.ErrorMessage = "上传文件为空！请先上传文件。";
            }
            else
            {
                var path = httpPostedFileBase.UploadKpiFile();
                ViewBag.Message = _importService.ImportPlanningSite(path);
            }
            return View("Import");
        }

        [HttpPost]
        public async Task<ViewResult> Dt2GImport()
        {
            var httpPostedFileBase = Request.Files["dt2G"];
            if (httpPostedFileBase == null || httpPostedFileBase.FileName == "")
            {
                ViewBag.ErrorMessage = "上传文件为空！请先上传文件。";
            }
            else
            {
                try
                {
                    var path = httpPostedFileBase.UploadKpiFile();
                    ViewBag.Message = await _importService.ImportDt2GFile(path);
                }
                catch (Exception e)
                {
                    ViewBag.ErrorMessage = "读取文件出错，信息为：" + e.Message;
                }
            }
            return View("DtImport");
        }

        [HttpPost]
        public async Task<ViewResult> Dt3GImport()
        {
            var httpPostedFileBase = Request.Files["dt3G"];
            if (httpPostedFileBase == null || httpPostedFileBase.FileName == "")
            {
                ViewBag.ErrorMessage = "上传文件为空！请先上传文件。";
            }
            else
            {
                try
                {
                    var path = httpPostedFileBase.UploadKpiFile();
                    ViewBag.Message = await _importService.ImportDt3GFile(path);
                }
                catch (Exception e)
                {
                    ViewBag.ErrorMessage = "读取文件出错，信息为：" + e.Message;
                }
            }
            return View("DtImport");
        }

        [HttpPost]
        public async Task<ViewResult> DtVolteImport()
        {
            var httpPostedFileBase = Request.Files["dtVolte"];
            if (httpPostedFileBase == null || httpPostedFileBase.FileName == "")
            {
                ViewBag.ErrorMessage = "上传文件为空！请先上传文件。";
            }
            else
            {
                try
                {
                    var path = httpPostedFileBase.UploadKpiFile();
                    ViewBag.Message = await _importService.ImportDtVolteFile(path);
                }
                catch (Exception e)
                {
                    ViewBag.ErrorMessage = "读取文件出错，信息为：" + e.Message;
                }
            }
            return View("DtImport");
        }

        [HttpPost]
        public async Task<ViewResult> Dt4GImport(HttpPostedFileBase[] dt4G)
        {
            if (dt4G == null || dt4G.Length <= 0 || string.IsNullOrEmpty(dt4G[0]?.FileName))
            {
                ViewBag.ErrorMessage = "上传文件为空！请先上传文件。";
                return View("DtImport");
            }

            var date = DateTime.Today;
            var paths = new List<string>();
            var infos = new List<FileRecord4GCsv>();
            foreach (var fileBase in dt4G)
            {
                var path = fileBase.UploadKpiFile();
                paths.Add(path);
                var fields = path.GetSplittedFields('\\');
                var dir = fields[fields.Length - 1];
                var dirDate = dir.GetDateFromFileName();
                if (date == DateTime.Today && dirDate != null)
                    date = (DateTime) dirDate;
                var data = _importService.ReadFileRecord4GCsvs(path);
                if (data.Any())
                    infos.AddRange(data);
            }
            
            ViewBag.Message = await _importService.ImportDt4GFile(infos, paths, date);
            return View("DtImport");
        }

        [HttpPost]
        public async Task<ViewResult> Dt4GDingliImport()
        {
            var httpPostedFileBase = Request.Files["dt4GDingli"];
            if (httpPostedFileBase == null || httpPostedFileBase.FileName == "")
            {
                ViewBag.ErrorMessage = "上传文件为空！请先上传文件。";
            }
            else
            {
                try
                {
                    var path = httpPostedFileBase.UploadKpiFile();
                    ViewBag.Message = await _importService.ImportDt4GDingli(path);
                }
                catch (Exception e)
                {
                    ViewBag.ErrorMessage = "读取文件出错，信息为：" + e.Message;
                }
            }
            return View("DtImport");
        }

        [HttpPost]
        public ViewResult StandarProblemImport()
        {
            var httpPostedFileBase = Request.Files["standardProblem"];
            if (httpPostedFileBase == null || httpPostedFileBase.FileName == "")
            {
                ViewBag.ErrorMessage = "上传文件为空！请先上传文件。";
            }
            else
            {
                try
                {
                    var path = httpPostedFileBase.UploadKpiFile();
                    ViewBag.Message = _importService.ImportStandardProblem(path);
                }
                catch (Exception e)
                {
                    ViewBag.ErrorMessage = "读取文件出错，信息为：" + e.Message;
                }
            }
            return View("DtImport");
        }

        [HttpPost]
        public ViewResult ChoiceProblemImport()
        {
            var httpPostedFileBase = Request.Files["choiceProblem"];
            if (httpPostedFileBase == null || httpPostedFileBase.FileName == "")
            {
                ViewBag.ErrorMessage = "上传文件为空！请先上传文件。";
            }
            else
            {
                try
                {
                    var path = httpPostedFileBase.UploadKpiFile();
                    ViewBag.Message = _importService.ImportChoiceProblem(path);
                }
                catch (Exception e)
                {
                    ViewBag.ErrorMessage = "读取文件出错，信息为：" + e.Message;
                }
            }
            return View("Import");
        }

        public ActionResult PreciseImport()
        {
            return View();
        }
        
        public ActionResult WorkItemImport()
        {
            return View();
        }
        
        public ActionResult CollegeInfos()
        {
            return View();
        }

        [HttpPost]
        public ActionResult WorkItemPost()
        {
            var httpPostedFileBase = Request.Files["workItem"];
            if (httpPostedFileBase == null || httpPostedFileBase.FileName == "")
            {
                ViewBag.ErrorMessage = "上传文件为空！请先上传文件。";
            }
            else
            {
                var path = httpPostedFileBase.UploadKpiFile();
                ViewBag.Message = _workItemService.ImportExcelFiles(path);
            }
            return View("WorkItemImport");
        }

        [HttpPost]
        public ActionResult AlarmWorkItemPost()
        {
            var httpPostedFileBase = Request.Files["AlarmWorkItem"];
            if (httpPostedFileBase == null || httpPostedFileBase.FileName == "")
            {
                ViewBag.ErrorMessage = "上传文件为空！请先上传文件。";
            }
            else
            {
                var path = httpPostedFileBase.UploadKpiFile();
                ViewBag.Message = _workItemService.ImportAlarmExcelFiles(path);
            }
            return View("WorkItemImport");
        }
        
        [HttpPost]
        public ActionResult SpecialWorkItemPost()
        {
            var httpPostedFileBase = Request.Files["specialWorkItem"];
            if (httpPostedFileBase == null || httpPostedFileBase.FileName == "")
            {
                ViewBag.ErrorMessage = "上传文件为空！请先上传文件。";
            }
            else
            {
                var path = httpPostedFileBase.UploadKpiFile();
                ViewBag.Message = _workItemService.ImportSpecialAlarmExcelFiles(path);
            }
            return View("WorkItemImport");
        }

        [HttpPost]
        public ActionResult CheckingProjectPost()
        {
            var httpPostedFileBase = Request.Files["checkingProject"];
            if (httpPostedFileBase == null || httpPostedFileBase.FileName == "")
            {
                ViewBag.ErrorMessage = "上传文件为空！请先上传文件。";
            }
            else
            {
                var path = httpPostedFileBase.UploadKpiFile();
                ViewBag.Message = _workItemService.ImportCheckingProjectExcelFiles(path);
            }
            return View("WorkItemImport");
        }
        
        [HttpPost]
        public ActionResult CheckingProjectProvincePost()
        {
            var httpPostedFileBase = Request.Files["checkingProjectProvince"];
            if (httpPostedFileBase == null || httpPostedFileBase.FileName == "")
            {
                ViewBag.ErrorMessage = "上传文件为空！请先上传文件。";
            }
            else
            {
                var path = httpPostedFileBase.UploadKpiFile();
                ViewBag.Message = _workItemService.ImportCheckingProjectProvinceExcelFiles(path);
            }
            return View("WorkItemImport");
        }

        [HttpPost]
        public ActionResult CheckingResultPost()
        {
            var httpPostedFileBase = Request.Files["checkingResult"];
            if (httpPostedFileBase == null || httpPostedFileBase.FileName == "")
            {
                ViewBag.ErrorMessage = "上传文件为空！请先上传文件。";
            }
            else
            {
                var path = httpPostedFileBase.UploadKpiFile();
                ViewBag.Message = _workItemService.ImportCheckingResultExcelFiles(path);
            }
            return View("WorkItemImport");
        }

    }
}