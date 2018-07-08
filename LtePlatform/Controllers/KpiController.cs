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
using System.Web.Mvc;
using Lte.Domain.Common.Types;
using Lte.Domain.Regular;
using Lte.Evaluations.DataService.Dump;

namespace LtePlatform.Controllers
{
    [Authorize(Roles = "指标导入")]
    public class KpiController : Controller
    {
        private readonly TownQueryService _townService;
        private readonly KpiImportService _importService;
        private readonly PreciseImportService _preciseImportService;
        private readonly WorkItemService _workItemService;

        public KpiController(TownQueryService townService, KpiImportService importService,
            PreciseImportService preciseImportService, WorkItemService workItemService)
        {
            _townService = townService;
            _importService = importService;
            _preciseImportService = preciseImportService;
            _workItemService = workItemService;
        }
        
        public ActionResult Import()
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
        public ViewResult Dt2GImport()
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
                    ViewBag.Message = _importService.ImportDt2GFile(path);
                }
                catch (Exception e)
                {
                    ViewBag.ErrorMessage = "读取文件出错，信息为：" + e.Message;
                }
            }
            return View("Import");
        }

        [HttpPost]
        public ViewResult Dt3GImport()
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
                    ViewBag.Message = _importService.ImportDt3GFile(path);
                }
                catch (Exception e)
                {
                    ViewBag.ErrorMessage = "读取文件出错，信息为：" + e.Message;
                }
            }
            return View("Import");
        }

        [HttpPost]
        public ViewResult DtVolteImport()
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
                    ViewBag.Message = _importService.ImportDtVolteFile(path);
                }
                catch (Exception e)
                {
                    ViewBag.ErrorMessage = "读取文件出错，信息为：" + e.Message;
                }
            }
            return View("Import");
        }

        [HttpPost]
        public ViewResult Dt4GImport()
        {
            var httpPostedFileBase = Request.Files["dt4G"];
            if (httpPostedFileBase == null || httpPostedFileBase.FileName == "")
            {
                ViewBag.ErrorMessage = "上传文件为空！请先上传文件。";
            }
            else
            {
                try
                {
                    var path = httpPostedFileBase.UploadKpiFile();
                    var fields = path.GetSplittedFields('\\');
                    var dir = fields[fields.Length - 1];
                    var date = dir.GetDateFromFileName() ?? DateTime.Today;
                    ViewBag.Message = _importService.ImportDt4GFile(path, date);
                }
                catch (Exception e)
                {
                    ViewBag.ErrorMessage = "读取文件出错，信息为：" + e.Message;
                }
            }
            return View("Import");
        }

        [HttpPost]
        public ViewResult Dt4GDingliImport()
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
                    ViewBag.Message = _importService.ImportDt4GDingli(path);
                }
                catch (Exception e)
                {
                    ViewBag.ErrorMessage = "读取文件出错，信息为：" + e.Message;
                }
            }
            return View("Import");
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
            return View("Import");
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

        [HttpPost]
        public ViewResult PrecisePost()
        {
            var message = new List<string>();
            var httpPostedFileBase = Request.Files["preciseFile"];
            if (httpPostedFileBase == null || httpPostedFileBase.FileName == "")
            {
                ViewBag.ErrorMessage = "上传文件为空！请先上传文件。";
            }
            else
            {
                var reader = new StreamReader(httpPostedFileBase.InputStream, Encoding.GetEncoding("GB2312"));
                _preciseImportService.UploadItems(reader);
                ViewBag.Message = "成功上传精确覆盖率文件" + httpPostedFileBase.FileName;
            }
            ViewBag.Message = message;
            return View("PreciseImport");
        }
        
        public ActionResult WorkItemImport()
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