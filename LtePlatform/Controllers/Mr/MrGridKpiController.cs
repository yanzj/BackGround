using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Abp.EntityFramework.Entities;
using Lte.Domain.Common.Geo;
using Lte.Evaluations.DataService.Mr;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Mr
{
    [ApiControl("栅格覆盖查询控制器")]
    [ApiGroup("MR")]
    public class MrGridKpiController : ApiController
    {
        private readonly MrGridKpiService _service;

        public MrGridKpiController(MrGridKpiService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("查询地理范围内的栅格")]
        [ApiParameterDoc("west", "西边经度")]
        [ApiParameterDoc("east", "东边经度")]
        [ApiParameterDoc("south", "南边纬度")]
        [ApiParameterDoc("north", "北边纬度")]
        [ApiResponse("栅格列表")]
        public IEnumerable<MrGridKpiDto> Get(double west, double east, double south, double north)
        {
            return _service.QueryKpiDtos(west, east, south, north);
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
}