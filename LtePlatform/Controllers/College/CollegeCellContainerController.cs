using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using Lte.Domain.Common.Geo;
using Lte.Evaluations.DataService.College;
using Lte.MySqlFramework.Entities;
using LtePlatform.Models;

namespace LtePlatform.Controllers.College
{
    [ApiControl("校园网/热点小区批量更新控制器")]
    [Cors("http://132.110.60.94:2018", "http://218.13.12.242:2018")]
    public class CollegeCellContainerController : ApiController
    {
        private readonly CollegeCellViewService _service;

        public CollegeCellContainerController(CollegeCellViewService serive)
        {
            _service = serive;
        }

        [HttpGet]
        [ApiDoc("查询热点LTE小区")]
        [ApiParameterDoc("collegeName", "校园名称")]
        [ApiResponse("热点LTE小区列表")]
        public IEnumerable<SectorView> Get(string collegeName)
        {
            return _service.QueryCollegeSectors(collegeName);
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