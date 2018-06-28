using System;
using System.Collections.Generic;
using System.Web.Http;
using Abp.EntityFramework.Entities;
using Lte.Evaluations.DataService.Dt;
using Lte.MySqlFramework.Entities;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Dt
{
    [ApiControl("镇区测试文件联合信息查询控制器")]
    public class TownTestInfoController : ApiController
    {
        private readonly TownTestInfoService _service;

        public TownTestInfoController(TownTestInfoService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("查询指定数据文件（已知类型）在各个镇区的详细信息")]
        [ApiParameterDoc("csvFileName", "数据文件名称")]
        [ApiParameterDoc("type", "数据文件类型")]
        [ApiResponse("各个镇区DT统计的详细信息")]
        public IEnumerable<AreaTestInfo> Get(string csvFileName, string type)
        {
            return _service.CalculateAreaTestInfos(csvFileName, type);
        }

        [HttpGet]
        [ApiDoc("查询指定区域内指定时间范围内的数据文件列表信息")]
        [ApiParameterDoc("city", "城市")]
        [ApiParameterDoc("district", "区域")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiResponse("数据文件列表信息")]
        public IEnumerable<AreaTestFileView> Get(DateTime begin, DateTime end, string city, string district)
        {
            return _service.QueryDistrictTestInfos(begin, end, city, district);
        }

        [HttpGet]
        [ApiDoc("查询指定鎮内指定时间范围内的数据文件列表信息")]
        [ApiParameterDoc("city", "城市")]
        [ApiParameterDoc("district", "区域")]
        [ApiParameterDoc("town", "鎮")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiResponse("数据文件列表信息")]
        public IEnumerable<AreaTestFileView> Get(DateTime begin, DateTime end, string city, string district, string town)
        {
            return _service.QueryTownTestInfos(begin, end, city, district, town);
        }

        [HttpGet]
        public IEnumerable<AreaTestInfo> Get(int fileId)
        {
            return _service.QueryAreaTestInfos(fileId);
        }

        [HttpPut]
        public int Put(AreaTestInfo info)
        {
            return _service.UpdateAreaTestInfo(info);
        }
    }
}