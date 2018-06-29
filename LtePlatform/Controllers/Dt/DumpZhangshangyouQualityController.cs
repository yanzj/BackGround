using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Lte.Evaluations.DataService.Dt;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Dt
{
    [ApiControl("导入掌上优测试详单信息处理器")]
    [ApiGroup("导入")]
    public class DumpZhangshangyouQualityController : ApiController
    {
        private readonly ZhangshangyouQualityService _service;

        public DumpZhangshangyouQualityController(ZhangshangyouQualityService service)
        {
            _service = service;
        }

        [HttpPut]
        [ApiDoc("导入一条掌上优测试详单信息")]
        [ApiResponse("导入结果")]
        public async Task<bool> Put()
        {
            return await _service.DumpOneStat();
        }

        [HttpGet]
        [ApiDoc("获取当前等待导入掌上优测试详单数")]
        [ApiResponse("当前等待导入掌上优测试详单数")]
        public int Get()
        {
            return _service.GetStatsToBeDump();
        }

        [HttpDelete]
        [ApiDoc("清除已上传掌上优测试详单记录（未写入数据库）")]
        public void Delete()
        {
            _service.ClearStats();
        }
    }
}