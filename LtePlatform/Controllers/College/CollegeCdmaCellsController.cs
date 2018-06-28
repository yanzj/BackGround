using System.Collections.Generic;
using System.Web.Http;
using Lte.Evaluations.DataService.College;
using Lte.MySqlFramework.Entities;
using LtePlatform.Models;

namespace LtePlatform.Controllers.College
{
    [ApiControl("查询校园网CDMA小区的控制器")]
    [Cors("http://132.110.60.94:2018", "http://218.13.12.242:2018")]
    public class CollegeCdmaCellsController : ApiController
    {
        private readonly CollegeCdmaCellViewService _viewService;

        public CollegeCdmaCellsController(CollegeCdmaCellViewService viewService)
        {
            _viewService = viewService;
        }

        [HttpGet]
        [ApiDoc("查询校园网CDMA小区列表")]
        [ApiParameterDoc("collegeName", "校园名称")]
        [ApiResponse("校园网CDMA小区列表")]
        public IEnumerable<CdmaCellView> Get(string collegeName)
        {
            return _viewService.GetViews(collegeName);
        }
    }
}