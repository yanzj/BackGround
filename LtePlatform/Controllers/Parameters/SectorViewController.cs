using System.Collections.Generic;
using System.Web.Http;
using Abp.EntityFramework.Entities.Infrastructure;
using Lte.Domain.Common.Geo;
using Lte.Evaluations.DataService.Basic;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Parameters
{
    [ApiControl("扇区视图查询控制器")]
    public class SectorViewController : ApiController
    {
        private readonly CellService _service;

        public SectorViewController(CellService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("获取经纬度范围内的扇区视图列表")]
        [ApiParameterDoc("west", "西边经度")]
        [ApiParameterDoc("east", "东边经度")]
        [ApiParameterDoc("south", "南边纬度")]
        [ApiParameterDoc("north", "北边纬度")]
        [ApiResponse("经纬度范围内的扇区视图列表")]
        public IEnumerable<SectorView> Get(double west, double east, double south, double north)
        {
            return _service.QuerySectors(west, east, south, north);
        }

        [HttpPost]
        [ApiDoc("获取经纬度范围内的除某些小区外的扇区视图列表")]
        [ApiParameterDoc("container", "指定条件范围")]
        [ApiResponse("指定条件范围内的扇区视图列表")]
        public IEnumerable<SectorView> Get(SectorRangeContainer container)
        {
            return _service.QuerySectors(container);
        }
    }
}