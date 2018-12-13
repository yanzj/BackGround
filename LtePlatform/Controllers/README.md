# Restful API 接口

## 基本结构

### 调用机制

控制器调用业务服务类，采用注入依赖Ninject。

#### 业务服务类

#### 注入依赖机制

### 例子

#### MVC控制器例子

``` CSharp
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
......
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
.......
}
```

#### API控制器例子

```CSharp
namespace LtePlatform.Controllers.Dt
{
    [ApiControl("带有AGPS详细信息数据点查询控制器")]
    public class AgisDtPointsController : ApiController
    {
        private readonly NearestPciCellService _service;

        public AgisDtPointsController(NearestPciCellService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("查询日期范围内带有AGPS详细信息数据点")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiResponse("日期范围内带有AGPS详细信息数据点")]
        public IEnumerable<AgisDtPoint> Get(DateTime begin, DateTime end)
        {
            return _service.QueryAgisDtPoints(begin, end);
        }

        [HttpGet]
        [ApiDoc("查询日期范围内和指定主题带有AGPS详细信息数据点")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiParameterDoc("topic", "指定主题")]
        [ApiResponse("日期范围内和指定主题带有AGPS详细信息数据点")]
        public IEnumerable<AgisDtPoint> Get(DateTime begin, DateTime end, string topic)
        {
            return _service.QueryAgisDtPoints(begin, end, topic);
        }
    }
}
```

## MVC控制器

### 账号管理控制器

### 主页控制器

### KPI相关控制器

### 管理控制器

### 基础数据控制器

## API控制器

### 账号相关

### 管理区域

### （校园网）专题优化

### 路测相关

### 网管指标相关

### MR相关

### 基础参数