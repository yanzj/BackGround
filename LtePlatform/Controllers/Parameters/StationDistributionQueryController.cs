using Lte.Evaluations.DataService.Basic;
using Lte.MySqlFramework.Entities;
using LtePlatform.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace LtePlatform.Controllers.Parameters
{
    [ApiControl("集团室分信息查询控制器")]
    [ApiGroup("基础信息")]
    public class StationDistributionQueryController : ApiController
    {
        private readonly IndoorDistributionService _service;

        public StationDistributionQueryController(IndoorDistributionService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("查询所有记录")]
        [ApiResponse("所有记录")]
        public IEnumerable<IndoorDistributionView> QueryAll()
        {
            return _service.QueryAll();
        }

        [HttpGet]
        [ApiDoc("查询经纬度范围内的记录")]
        [ApiParameterDoc("west", "西边经度")]
        [ApiParameterDoc("east", "东边经度")]
        [ApiParameterDoc("south", "南边纬度")]
        [ApiParameterDoc("north", "北边纬度")]
        [ApiResponse("经纬度范围内的记录")]
        public IEnumerable<IndoorDistributionView> QueryRange(double west, double east, double south, double north)
        {
            return _service.QueryRange(west, east, south, north);
        }

        [HttpGet]
        [ApiDoc("根据RRU标识查询对应的记录")]
        [ApiParameterDoc("rruNum", "所属RRU标识")]
        [ApiResponse("所属RRU标识对应的记录")]
        public IndoorDistributionView GetByRruName(string rruNum)
        {
            return _service.QueryByRruNum(rruNum);
        }

        [HttpGet]
        [ApiDoc("根据小区标识查询对应的记录")]
        [ApiParameterDoc("cellNum", "所属小区标识")]
        [ApiResponse("所属小区标识对应的记录")]
        public IndoorDistributionView QueryByCellNum(string cellNum)
        {
            return _service.QueryByCellNum(cellNum);
        }

        [HttpGet]
        [ApiDoc("根据站点编号查询记录")]
        [ApiParameterDoc("stationNum", "站点编号")]
        [ApiResponse("符合条件的记录")]
        public IEnumerable<IndoorDistributionView> GetByStationNum(string stationNum)
        {
            return _service.QueryByStationNum(stationNum);
        }

    }
}