using System.Collections.Generic;
using System.Web.Http;
using Abp.EntityFramework.Entities;
using Lte.Evaluations.DataService.Dump;
using LtePlatform.Models;

namespace LtePlatform.Controllers.AdminitrativeRegion
{
    [ApiControl("练习题目查询控制器")]
    public class LteProblemController : ApiController
    {
        private readonly LteProblemService _service;

        public LteProblemController(LteProblemService service)
        {
            _service = service;
        }

        [HttpGet]
        public IEnumerable<LteProblem> Get(string type)
        {
            return _service.QueryProblems(type);
        }

        [HttpGet]
        [ApiDoc("随机生成题目")]
        [ApiParameterDoc("type", "题目类型")]
        [ApiParameterDoc("count", "题目数量")]
        [ApiResponse("题目列表")]
        public IEnumerable<LteProblem> Get(string type, int count)
        {
            return _service.QueryRandomProblems(type, count);
        }

        [HttpGet]
        [ApiDoc("随机生成题目")]
        [ApiParameterDoc("type", "题目类型")]
        [ApiParameterDoc("count", "题目数量")]
        [ApiParameterDoc("keyword1", "第一关键字")]
        [ApiResponse("题目列表")]
        public IEnumerable<LteProblem> Get(string type, int count, string keyword1)
        {
            return _service.QueryRandomProblems(type, count, keyword1);
        }

        [HttpGet]
        [ApiDoc("随机生成题目")]
        [ApiParameterDoc("type", "题目类型")]
        [ApiParameterDoc("count", "题目数量")]
        [ApiParameterDoc("keyword1", "第一关键字")]
        [ApiParameterDoc("keyword2", "第二关键字")]
        [ApiResponse("题目列表")]
        public IEnumerable<LteProblem> Get(string type, int count, string keyword1, string keyword2)
        {
            return _service.QueryRandomProblems(type, count, keyword1, keyword2);
        }

        [HttpGet]
        [ApiDoc("更新题目关键字")]
        [ApiResponse("更新是否成功")]
        public bool Get(int id, string keyword1, string keyword2)
        {
            return _service.UpdateKeywords(id, keyword1, keyword2);
        }
    }
}