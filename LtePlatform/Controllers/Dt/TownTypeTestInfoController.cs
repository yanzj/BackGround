using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Lte.Evaluations.DataService;
using Lte.Evaluations.DataService.Dt;
using Lte.MySqlFramework.Entities;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Dt
{
    [ApiControl("镇区测试文件类型信息查询控制器")]
    [ApiGroup("测试")]
    public class TownTypeTestInfoController : ApiController
    {
        private readonly RasterInfoService _service;

        public TownTypeTestInfoController(RasterInfoService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("查询全市镇区路测数据指标统计")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiParameterDoc("dataType", "数据类型")]
        [ApiResponse("全市镇区路测数据指标统计")]
        public IEnumerable<DistrictFileView> Get(DateTime begin, DateTime end, string dataType)
        {
            return _service.QueryCityTestInfos(begin, end, dataType);
        }

        [HttpGet]
        [ApiDoc("查询指定区域下镇区路测数据指标统计")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiParameterDoc("dataType", "数据类型")]
        [ApiParameterDoc("city", "城市")]
        [ApiParameterDoc("district", "区域")]
        [ApiResponse("指定区域下镇区路测数据指标统计")]
        public DistrictFileView Get(DateTime begin, DateTime end, string dataType, 
            string city, string district)
        {
            return _service.QueryDistrictTestInfos(begin, end, dataType, city, district);
        }

        [HttpGet]
        [ApiDoc("查询指定镇区路测数据指标统计")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiParameterDoc("dataType", "数据类型")]
        [ApiParameterDoc("city", "城市")]
        [ApiParameterDoc("district", "区域")]
        [ApiParameterDoc("town", "镇区")]
        [ApiResponse("指定镇区路测数据指标统计")]
        public TownAreaTestFileView Get(DateTime begin, DateTime end, string dataType, 
            string city, string district, string town)
        {
            return _service.QueryTownTestInfos(begin, end, dataType, city, district, town);
        }
    }
}
