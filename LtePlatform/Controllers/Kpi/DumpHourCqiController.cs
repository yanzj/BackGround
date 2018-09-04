﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Lte.Evaluations.DataService.Kpi;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Kpi
{
    [ApiControl("导入忙时CQI优良比指标控制器")]
    [ApiGroup("导入")]
    public class DumpHourCqiController : ApiController
    {
        private readonly HourCqiService _service;

        public DumpHourCqiController(HourCqiService service)
        {
            _service = service;
        }

        [HttpPut]
        [ApiDoc("导入一条CQI优良比指标")]
        [ApiResponse("是否已经成功导入")]
        public Task<bool> Put()
        {
            return _service.DumpOneCqiStat();
        }

        [HttpGet]
        [ApiDoc("获得当前服务器中待导入的CQI优良比指标统计记录数")]
        [ApiResponse("当前服务器中待导入的CQI优良比指标统计记录数")]
        public int Get()
        {
            return _service.HourCqiCount;
        }

        [HttpDelete]
        [ApiDoc("清空待导入的CQI优良比指标统计记录")]
        public void Delete()
        {
            _service.ClearCqiStats();
        }
    }
}