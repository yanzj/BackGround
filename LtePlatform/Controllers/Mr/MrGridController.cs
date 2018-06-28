using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Abp.EntityFramework.Entities;
using Lte.Domain.Common.Geo;
using Lte.Domain.Common.Wireless;
using Lte.Evaluations.DataService.Mr;
using Lte.MySqlFramework.Entities;
using Lte.Parameters.Entities.Channel;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Mr
{
    [ApiControl("MR栅格数据查询控制器")]
    public class MrGridController : ApiController
    {
        private readonly NearestPciCellService _service;
        private readonly AgpsService _agpsService;

        public MrGridController(NearestPciCellService service, AgpsService agpsService)
        {
            _service = service;
            _agpsService = agpsService;
        }

        [HttpGet]
        [ApiDoc("查询指定区域最近日期内的MR覆盖率栅格信息")]
        [ApiParameterDoc("statDate", "初始日期")]
        [ApiParameterDoc("district", "指定区域")]
        [ApiResponse("指定区域最近日期内的MR覆盖率栅格信息列表")]
        public IEnumerable<MrCoverageGridView> Get(DateTime statDate, string district)
        {
            return _service.QueryCoverageGridViews(statDate, district);
        }

        [HttpGet]
        [ApiDoc("查询指定区域最近日期内的栅格竞争信息")]
        [ApiParameterDoc("statDate", "初始日期")]
        [ApiParameterDoc("district", "指定区域")]
        [ApiParameterDoc("competeDescription", "竞争信息类型")]
        [ApiResponse("指定区域最近日期内的栅格竞争信息列表")]
        public IEnumerable<MrCompeteGridView> Get(DateTime statDate, string district, string competeDescription)
        {
            return _service.QueryCompeteGridViews(statDate, district, competeDescription);
        }
    }

    [ApiControl("镇区MR覆盖情况查询控制器")]
    public class TownMrGridController : ApiController
    {
        private readonly MrGridService _service;
        private readonly TownSupportService _supportService;

        public TownMrGridController(MrGridService service, TownSupportService supportService)
        {
            _service = service;
            _supportService = supportService;
        }
        
        [HttpGet]
        [ApiDoc("查询指定区域最近日期内的MR覆盖率栅格信息")]
        [ApiParameterDoc("statDate", "初始日期")]
        [ApiParameterDoc("district", "指定区域")]
        [ApiParameterDoc("town", "指定镇区")]
        [ApiResponse("指定区域最近日期内的MR覆盖率栅格信息列表")]
        public IEnumerable<MrCoverageGridView> Get(DateTime statDate, string district, string town)
        {
            var boundaries = _supportService.QueryTownBoundaries(district, town);
            if (boundaries == null) return new List<MrCoverageGridView>();
            return _service.QueryCoverageGridViews(statDate, boundaries, district);
        }

        [HttpGet]
        [ApiDoc("查询指定区域最近日期内的栅格竞争信息")]
        [ApiParameterDoc("statDate", "初始日期")]
        [ApiParameterDoc("district", "指定区域")]
        [ApiParameterDoc("town", "指定镇区")]
        [ApiParameterDoc("competeDescription", "竞争信息类型")]
        [ApiResponse("指定区域最近日期内的栅格竞争信息列表")]
        public IEnumerable<MrCompeteGridView> Get(DateTime statDate, string district, string town, string competeDescription)
        {
            var boundaries = _supportService.QueryTownBoundaries(district, town);
            if (boundaries == null) return new List<MrCompeteGridView>();
            var competeTuple =
                WirelessConstants.EnumDictionary["AlarmCategory"].FirstOrDefault(x => x.Item2 == competeDescription);
            var compete = (AlarmCategory?)competeTuple?.Item1;

            return _service.QueryCompeteGridViews(statDate, district, compete, boundaries);
        }
    }

    [ApiControl("AGPS电信覆盖情况查询控制器")]
    public class AgpsTelecomController : ApiController
    {
        private readonly TownSupportService _service;
        private readonly AgpsService _agpsService;

        public AgpsTelecomController(TownSupportService service, AgpsService agpsService)
        {
            _service = service;
            _agpsService = agpsService;
        }

        [HttpGet]
        [ApiDoc("查询指定日期范围内指定镇区电信AGPS覆盖情况")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiParameterDoc("district", "区域")]
        [ApiParameterDoc("town", "镇区")]
        [ApiResponse("指定镇区AGPS覆盖情况")]
        public IEnumerable<AgpsCoverageView> Get(DateTime begin, DateTime end, string district,
            string town)
        {
            var boundaries = _service.QueryTownBoundaries(district, town);
            return boundaries == null
                ? new List<AgpsCoverageView>()
                : _agpsService.QueryTelecomCoverageViews(begin, end, boundaries);
        }
        
        [HttpPost]
        [ApiDoc("更新一条镇区级别电信AGPS覆盖信息")]
        [ApiParameterDoc("view", "镇区级别电信AGPS覆盖信息")]
        [ApiResponse("更新结果")]
        public int Post(AgpsTownView view)
        {
            return view.Views.Sum(stat => _agpsService.UpdateTelecomAgisPoint(stat, view.District, view.Town));
        }
    }

    [ApiControl("AGPS移动覆盖情况查询控制器")]
    public class AgpsMobileController : ApiController
    {
        private readonly TownSupportService _service;
        private readonly AgpsService _agpsService;

        public AgpsMobileController(TownSupportService service, AgpsService agpsService)
        {
            _service = service;
            _agpsService = agpsService;
        }

        [HttpGet]
        [ApiDoc("查询指定日期范围内指定镇区移动AGPS覆盖情况")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiParameterDoc("district", "区域")]
        [ApiParameterDoc("town", "镇区")]
        [ApiResponse("指定镇区AGPS覆盖情况")]
        public IEnumerable<AgpsCoverageView> Get(DateTime begin, DateTime end, string district,
            string town)
        {
            var boundaries = _service.QueryTownBoundaries(district, town);
            return boundaries == null
                ? new List<AgpsCoverageView>()
                : _agpsService.QueryMobileCoverageViews(begin, end, boundaries);
        }
        
        [HttpPost]
        [ApiDoc("更新一条镇区级别移动AGPS覆盖信息")]
        [ApiParameterDoc("view", "镇区级别移动AGPS覆盖信息")]
        [ApiResponse("更新结果")]
        public int Post(AgpsTownView view)
        {
            return view.Views.Sum(stat => _agpsService.UpdateMobileAgisPoint(stat, view.District, view.Town));
        }
    }

    [ApiControl("AGPS联通覆盖情况查询控制器")]
    public class AgpsUnicomController : ApiController
    {
        private readonly TownSupportService _service;
        private readonly AgpsService _agpsService;

        public AgpsUnicomController(TownSupportService service, AgpsService agpsService)
        {
            _service = service;
            _agpsService = agpsService;
        }

        [HttpGet]
        [ApiDoc("查询指定日期范围内指定镇区AGPS覆盖情况")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiParameterDoc("district", "区域")]
        [ApiParameterDoc("town", "镇区")]
        [ApiResponse("指定镇区AGPS覆盖情况")]
        public IEnumerable<AgpsCoverageView> Get(DateTime begin, DateTime end, string district,
            string town)
        {
            var boundaries = _service.QueryTownBoundaries(district, town);
            return boundaries == null
                ? new List<AgpsCoverageView>()
                : _agpsService.QueryUnicomCoverageViews(begin, end, boundaries);
        }
        
        [HttpPost]
        [ApiDoc("更新一条镇区级别联通AGPS覆盖信息")]
        [ApiParameterDoc("view", "镇区级别联通AGPS覆盖信息")]
        [ApiResponse("更新结果")]
        public int Post(AgpsTownView view)
        {
            return view.Views.Sum(stat => _agpsService.UpdateUnicomAgisPoint(stat, view.District, view.Town));
        }
    }
    
    [ApiControl("栅格分簇查询控制器")]
    public class GridClusterController : ApiController
    {
        private readonly GridClusterService _service;

        public GridClusterController(GridClusterService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("查询某专题下的栅格分簇")]
        [ApiParameterDoc("theme", "专题名称")]
        [ApiResponse("栅格分簇列表")]
        public IEnumerable<GridClusterView> Get(string theme)
        {
            return _service.QueryClusterViews(theme);
        }

        [HttpGet]
        public IEnumerable<GridClusterView> Get(string theme, double west, double east, double south, double north)
        {
            return _service.QueryClusterViews(theme, west, east, south, north);
        }

        [HttpPost]
        public IEnumerable<MrGridKpiDto> Post(IEnumerable<GeoGridPoint> points)
        {
            return _service.QueryKpiDtos(points);
        }

        [HttpPut]
        public MrGridKpiDto Put(IEnumerable<GeoGridPoint> points)
        {
            return _service.QueryClusterKpi(points);
        }
    }

    [ApiControl("含有DPI指标信息的栅格查询控制器")]
    public class DpiGridKpiController : ApiController
    {
        private readonly DpiGridKpiService _service;

        public DpiGridKpiController(DpiGridKpiService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("查询指定栅格的DPI信息")]
        [ApiParameterDoc("x", "栅格横坐标")]
        [ApiParameterDoc("y", "栅格纵坐标")]
        public DpiGridKpiDto Get(int x, int y)
        {
            return _service.QueryKpi(x, y);
        }
    }
}
