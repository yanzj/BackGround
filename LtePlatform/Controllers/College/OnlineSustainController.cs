using System;
using System.Collections.Generic;
using System.Web.Http;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Complain;
using Lte.Evaluations.DataService.College;
using LtePlatform.Models;

namespace LtePlatform.Controllers.College
{
    [ApiControl("在线支撑查询控制器")]
    public class OnlineSustainController : ApiController
    {
        private readonly OnlineSustainService _service;

        public OnlineSustainController(OnlineSustainService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("查询指定日期范围内的在线支撑记录")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiResponse("指定日期范围内的在线支撑记录")]
        public List<OnlineSustainDto> Get(DateTime begin, DateTime end)
        {
            return _service.QueryList(begin, end);
        }

        [HttpGet]
        [ApiDoc("查询指定日期的前一个月的在线支撑记录")]
        [ApiParameterDoc("today", "指定日期")]
        [ApiResponse("指定日期的前一个月的在线支撑记录")]
        public IEnumerable<OnlineSustainDto> Get(DateTime today)
        {
            return _service.QueryList(today);
        }

        [HttpGet]
        [ApiDoc("查询指定区域内指定日期的前一个月的在线支撑记录")]
        [ApiParameterDoc("today", "指定日期")]
        [ApiParameterDoc("city", "城市")]
        [ApiParameterDoc("district", "区域")]
        [ApiResponse("指定区域内指定日期的前一个月的在线支撑记录")]
        public IEnumerable<OnlineSustainDto> Get(DateTime today, string city, string district)
        {
            return _service.QueryList(today, city, district);
        }

        [HttpGet]
        [ApiDoc("查询指定地理范围内的在线支撑记录")]
        [ApiParameterDoc("west", "西边经度")]
        [ApiParameterDoc("east", "东边经度")]
        [ApiParameterDoc("south", "南边纬度")]
        [ApiParameterDoc("north", "北边纬度")]
        [ApiResponse("指定地理范围内的在线支撑记录")]
        public IEnumerable<OnlineSustainDto> Get(double west, double east, double south, double north)
        {
            return _service.QueryList(west, east, south, north);
        } 
    }
}