﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Abp.EntityFramework.Entities;
using Lte.Evaluations.DataService.Mr;
using Lte.MySqlFramework.Entities;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Mongo
{
    [ApiControl("导入MRS-RSRP的控制器")]
    [ApiGroup("KPI")]
    public class MrsRsrpImportController : ApiController
    {
        private readonly MrsRsrpImportService _service;

        public MrsRsrpImportController(MrsRsrpImportService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("合并镇区指标")]
        [ApiParameterDoc("statDate", "统计日期")]
        [ApiResponse("合并结果")]
        public IEnumerable<TownMrsRsrp> GetMrs(DateTime statDate)
        {
            return _service.GetMergeMrsStats(statDate);
        }

        [HttpGet]
        [ApiDoc("获取TOP指标")]
        [ApiParameterDoc("topDate", "统计日期")]
        [ApiResponse("TOP指标条数")]
        public int GetTopMrs(DateTime topDate)
        {
            return _service.GetTopMrsRsrps(topDate);
        }

    }
}