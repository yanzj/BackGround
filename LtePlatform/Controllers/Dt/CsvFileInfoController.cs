using System;
using System.Collections.Generic;
using System.Web.Http;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Test;
using Lte.Evaluations.DataService.Dt;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Dt
{
    [ApiControl("DT测试数据文件查询控制器")]
    [ApiGroup("测试")]
    public class CsvFileInfoController : ApiController
    {
        private readonly CsvFileInfoService _service;

        public CsvFileInfoController(CsvFileInfoService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("获得指定日期范围内的DT测试数据文件信息")]
        [ApiParameterDoc("begin", "开始日期")]
        [ApiParameterDoc("end", "结束日期")]
        [ApiResponse("DT测试数据文件信息，包括测试时间、数据名称、存放目录、测试网络（2G3G4G）和测试距离等")]
        public IEnumerable<CsvFilesInfo> Get(DateTime begin, DateTime end)
        {
            return _service.QueryFilesInfos(begin, end);
        }

        [HttpGet]
        [ApiDoc("查询指定DT测试文件名的基本信息")]
        [ApiParameterDoc("fileName", "DT测试文件名")]
        [ApiResponse("DT测试文件的基本信息")]
        public CsvFilesInfo Get(string fileName)
        {
            return _service.QueryCsvFilesInfo(fileName);
        }

        [HttpPost]
        [ApiDoc("更新指定DT测试文件名的距离等信息")]
        [ApiParameterDoc("filesInfo", "DT测试文件信息")]
        [ApiResponse("更新结果")]
        public int Get(CsvFilesInfo filesInfo)
        {
            return _service.UpdateFileDistance(filesInfo);
        }
    }
}