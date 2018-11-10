using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Abp.EntityFramework.Entities.Infrastructure;
using Lte.Domain.Common.Geo;
using Lte.Evaluations.DataService.College;
using LtePlatform.Models;

namespace LtePlatform.Controllers.College
{
    [ApiControl("校园网/热点小区批量更新控制器")]
    [ApiGroup("专题优化")]
    public class CollegeCdmaCellContainerController : ApiController
    {
        private readonly CollegeCdmaCellViewService _service;

        public CollegeCdmaCellContainerController(CollegeCdmaCellViewService serive)
        {
            _service = serive;
        }

        [HttpGet]
        [ApiDoc("查询校园网CDMA小区列表")]
        [ApiParameterDoc("collegeName", "校园名称")]
        [ApiResponse("校园网CDMA小区列表")]
        public IEnumerable<SectorView> Get(string collegeName)
        {
            return _service.Query(collegeName);
        }

        [HttpPost]
        [ApiDoc("校园网/热点小区批量更新")]
        [ApiParameterDoc("container", "批量更新信息")]
        public async Task<int> Post(CollegeCellNamesContainer container)
        {
            return await _service.UpdateHotSpotCells(container);
        }
    }
}