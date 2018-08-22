using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Lte.Evaluations.DataService.Basic;
using Lte.MySqlFramework.Entities;
using Lte.MySqlFramework.Entities.Infrastructure;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Parameters
{
    [ApiControl("集团基站信息查询控制器")]
    [ApiGroup("基础信息")]
    public class StationENodebQueryController : ApiController
    {
        private readonly ENodebBaseService _service;

        public StationENodebQueryController(ENodebBaseService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("查询所有记录")]
        [ApiResponse("所有记录")]
        public IEnumerable<ENodebBaseView> QueryAll()
        {
            return _service.QueryAll();
        }

        [HttpGet]
        [ApiDoc("模糊查询记录")]
        [ApiParameterDoc("searchText", "查询字符串")]
        [ApiResponse("符合条件的记录")]
        public IEnumerable<ENodebBaseView> GetByText(string searchText)
        {
            return _service.QueryENodebBases(searchText);
        }

        [HttpGet]
        [ApiDoc("根据基站名称查询记录")]
        [ApiParameterDoc("eNodebName", "基站名称")]
        [ApiResponse("符合条件的记录")]
        public ENodebBaseView QueryByENodebName(string eNodebName)
        {
            return _service.QueryByENodebName(eNodebName);
        }

        [HttpGet]
        [ApiDoc("根据站点编号查询记录")]
        [ApiParameterDoc("stationNum", "站点编号")]
        [ApiResponse("符合条件的记录列表")]
        public IEnumerable<ENodebBaseView> GetByStationNum(string stationNum)
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
        public IEnumerable<ENodebBaseView> QueryRange(double west, double east, double south, double north)
        {
            return _service.QueryRange(west, east, south, north);
        }
    }
}