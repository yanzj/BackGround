using System.Collections.Generic;
using System.Web.Http;
using Lte.Evaluations.DataService.College;
using Lte.MySqlFramework.Entities;
using LtePlatform.Models;

namespace LtePlatform.Controllers.College
{
    [ApiControl("查询校园网LTE小区的控制器")]
    [Cors("http://132.110.60.94:2018", "http://218.13.12.242:2018")]
    public class CollegeCellsController : ApiController
    {
        private readonly CollegeCellViewService _viewService;

        public CollegeCellsController(CollegeCellViewService viewService)
        {
            _viewService = viewService;
        }

        [HttpGet]
        [ApiDoc("查询校园网LTE小区")]
        [ApiParameterDoc("collegeName", "校园名称")]
        [ApiResponse("校园网LTE小区列表")]
        public IEnumerable<CellRruView> Get(string collegeName)
        {
            return _viewService.GetCollegeViews(collegeName);
        }
    }
}