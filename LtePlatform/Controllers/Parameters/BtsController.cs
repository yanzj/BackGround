using System.Collections.Generic;
using System.Web.Http;
using Abp.EntityFramework.Entities;
using Lte.Domain.Common.Geo;
using Lte.Evaluations.DataService.Basic;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Parameters
{
    [ApiControl("查询CDMA基站的控制器")]
    public class BtsController : ApiController
    {
        private readonly BtsQueryService _service;

        public BtsController(BtsQueryService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("根据行政区域条件查询基站列表")]
        [ApiParameterDoc("city", "城市")]
        [ApiParameterDoc("district", "区域")]
        [ApiParameterDoc("town", "镇区")]
        [ApiResponse("查询得到的基站列表结果，如果没有则会报错")]
        public IEnumerable<CdmaBtsView> Get(string city, string district, string town)
        {
            return _service.GetByTownNames(city, district, town);
        }

        [HttpGet]
        [ApiDoc("使用名称模糊查询，可以先后匹配基站名称、基站编号和地址")]
        [ApiParameterDoc("name", "模糊查询的名称")]
        [ApiResponse("查询得到的基站列表结果，如果没有则会报错")]
        public IEnumerable<CdmaBtsView> Get(string name)
        {
            return _service.GetByGeneralName(name);
        }

        [HttpGet]
        [ApiDoc("根据基站编号条件查询基站")]
        [ApiParameterDoc("btsId", "基站编号")]
        [ApiResponse("查询得到的基站列表结果，如果没有则会报错")]
        public CdmaBtsView Get(int btsId)
        {
            return _service.GetByBtsId(btsId);
        }

        [HttpPost]
        [ApiDoc("获取经纬度范围内的除某些基站外的基站视图列表")]
        [ApiParameterDoc("container", "指定条件范围")]
        [ApiResponse("指定条件范围内的基站视图列表")]
        public IEnumerable<CdmaBtsView> Post(ENodebRangeContainer container)
        {
            return _service.QueryBtsViews(container);
        }
    }
}