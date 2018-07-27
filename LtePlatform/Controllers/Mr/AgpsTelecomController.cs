using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Abp.EntityFramework.Channel;
using Lte.Evaluations.DataService.Mr;
using Lte.Parameters.Entities.Channel;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Mr
{
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
}