using Lte.Evaluations.DataService.Basic;
using Lte.MySqlFramework.Entities;
using LtePlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Lte.MySqlFramework.Entities.Infrastructure;

namespace LtePlatform.Controllers.Parameters
{
    [ApiControl("集团天线信息查询控制器")]
    [ApiGroup("基础信息")]
    public class StationAntennaQueryController : ApiController
    {
        private readonly StationAntennaService _service;

        public StationAntennaQueryController(StationAntennaService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("查询所有记录")]
        [ApiResponse("所有记录")]
        public IEnumerable<StationAntennaView> QueryAll()
        {
            return _service.QueryAll();
        }

        [HttpGet]
        [ApiDoc("根据站点编号查询记录")]
        [ApiParameterDoc("stationNum", "站点编号")]
        [ApiResponse("符合条件的记录")]
        public IEnumerable<StationAntennaView> GetByStationNum(string stationNum)
        {
            return _service.QueryByStationNum(stationNum);
        }

        [HttpGet]
        [ApiDoc("查询经纬度范围内的记录")]
        [ApiParameterDoc("west", "西边经度")]
        [ApiParameterDoc("east", "东边经度")]
        [ApiParameterDoc("south", "南边纬度")]
        [ApiParameterDoc("north", "北边纬度")]
        [ApiResponse("经纬度范围内的记录")]
        public IEnumerable<StationAntennaView> QueryRange(double west, double east, double south, double north)
        {
            return _service.QueryRange(west, east, south, north);
        }
    }
}