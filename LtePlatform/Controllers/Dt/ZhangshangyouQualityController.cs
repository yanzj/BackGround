using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Lte.Evaluations.DataService.Dt;
using Lte.MySqlFramework.Entities;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Dt
{
    [ApiControl("掌上优测试详单查询控制器")]
    [ApiGroup("测试")]
    public class ZhangshangyouQualityController : ApiController
    {
        private readonly ZhangshangyouQualityService _service;

        public ZhangshangyouQualityController(ZhangshangyouQualityService service)
        {
            _service = service;
        }

        [ApiDoc("查询日期范围和地理范围内的记录")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiParameterDoc("west", "西边经度")]
        [ApiParameterDoc("east", "东边经度")]
        [ApiParameterDoc("south", "南边纬度")]
        [ApiParameterDoc("north", "北边纬度")]
        [ApiParameterDoc("xOffset", "百度坐标经度偏移")]
        [ApiParameterDoc("yOffset", "百度坐标纬度偏移")]
        [ApiResponse("经纬度和日期范围内的记录")]
        [HttpGet]
        public IEnumerable<ZhangshangyouQualityView> Get(DateTime begin, DateTime end,
            double west, double east, double south, double north, double xOffset, double yOffset)
        {
            return _service.QueryByDateSpanAndRange(begin, end, west, east, south, north, xOffset, yOffset);
        }

        [ApiDoc("查询日期范围和地理范围（匹配测试点和小区）内的记录")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiParameterDoc("generalWest", "西边经度")]
        [ApiParameterDoc("generalEast", "东边经度")]
        [ApiParameterDoc("generalSouth", "南边纬度")]
        [ApiParameterDoc("generalNorth", "北边纬度")]
        [ApiParameterDoc("xOffset", "百度坐标经度偏移")]
        [ApiParameterDoc("yOffset", "百度坐标纬度偏移")]
        [ApiResponse("经纬度和日期范围内的记录")]
        [HttpGet]
        public IEnumerable<ZhangshangyouQualityView> GetGeneral(DateTime begin, DateTime end,
            double generalWest, double generalEast, double generalSouth, double generalNorth,
            double xOffset, double yOffset)
        {
            return _service.QueryByDateSpanAndGeneralRange(begin, end, generalWest, generalEast, generalSouth,
                generalNorth, xOffset, yOffset);
        }

        [ApiDoc("查询日期范围内的指定LTE小区记录")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiParameterDoc("eNodebId", "基站编号")]
        [ApiParameterDoc("sectorId", "小区编号")]
        [ApiResponse("日期范围内的指定LTE小区记录")]
        [HttpGet]
        public IEnumerable<ZhangshangyouQualityView> GetLte(DateTime begin, DateTime end,
            int eNodebId, byte sectorId)
        {
            return _service.QueryLteRecordsByDateSpan(begin, end, eNodebId, sectorId);
        }

        [ApiDoc("查询日期范围内的指定CDMA小区记录")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiParameterDoc("btsId", "基站编号")]
        [ApiParameterDoc("cdmaSectorId", "小区编号")]
        [ApiResponse("日期范围内的指定CDMA小区记录")]
        [HttpGet]
        public IEnumerable<ZhangshangyouQualityView> GetCdma(DateTime begin, DateTime end,
            int btsId, byte cdmaSectorId)
        {
            return _service.QueryCdmaRecordsByDateSpan(begin, end, btsId, cdmaSectorId);
        }
    }
}