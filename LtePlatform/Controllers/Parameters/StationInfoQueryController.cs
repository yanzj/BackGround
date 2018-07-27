using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Lte.Evaluations.DataService.Basic;
using Lte.MySqlFramework.Entities;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Parameters
{
    [ApiControl("集团站址信息查询控制器")]
    [ApiGroup("基础信息")]
    public class StationInfoQueryController : ApiController
    {
        private readonly StationInfoService _service;

        public StationInfoQueryController(StationInfoService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("查询所有记录")]
        [ApiResponse("所有记录")]
        public IEnumerable<StationDictionaryView> QueryAll()
        {
            return _service.QueryAll();
        }

        [HttpGet]
        [ApiDoc("查询区域内所有记录")]
        [ApiParameterDoc("district", "区")]
        [ApiResponse("区域内所有记录")]
        public IEnumerable<StationDictionaryView> QueryByDistrict(string district)
        {
            return _service.QueryByDistrict(district);
        }

        [HttpGet]
        [ApiDoc("查询经纬度范围内的记录")]
        [ApiParameterDoc("west", "西边经度")]
        [ApiParameterDoc("east", "东边经度")]
        [ApiParameterDoc("south", "南边纬度")]
        [ApiParameterDoc("north", "北边纬度")]
        [ApiResponse("经纬度范围内的记录")]
        public IEnumerable<StationDictionaryView> QueryRange(double west, double east, double south, double north)
        {
            return _service.QueryRange(west, east, south, north);
        }

        [HttpGet]
        [ApiDoc("按照站址名称匹配查询站址信息")]
        [ApiParameterDoc("name", "站址名称")]
        [ApiResponse("站址信息")]
        public StationDictionaryView QueryOne(string name)
        {
            return _service.QueryOneByStationName(name);
        }

        [HttpGet]
        [ApiDoc("按照基站编号匹配查询站址信息")]
        [ApiParameterDoc("eNodebId", "基站编号")]
        [ApiResponse("站址信息")]
        public StationDictionaryView QueryOneByENodebId(int eNodebId)
        {
            return _service.QueryOneByENodebId(eNodebId);
        }
    }
}