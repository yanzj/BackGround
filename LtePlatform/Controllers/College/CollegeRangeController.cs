using System.Web.Http;
using Lte.Domain.Common.Wireless;
using Lte.MySqlFramework.Abstract;
using LtePlatform.Models;

namespace LtePlatform.Controllers.College
{
    [ApiControl("校园网区域查询控制器")]
    [Cors("http://132.110.60.94:2018", "http://218.13.12.242:2018")]
    public class CollegeRangeController : ApiController
    {
        private readonly ICollegeRepository _repository;

        public CollegeRangeController(ICollegeRepository repository)
        {
            _repository = repository;
        }

        [ApiDoc("查询校园网区域范围")]
        [ApiParameterDoc("collegeName", "校园名称")]
        [ApiResponse("校园网区域信息，包括东南西北的坐标点")]
        [HttpGet]
        public RectangleRange Get(string collegeName)
        {
            return _repository.GetRange(collegeName);
        }
    }
}