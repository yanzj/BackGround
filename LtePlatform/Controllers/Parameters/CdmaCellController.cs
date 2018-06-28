using System.Collections.Generic;
using System.Web.Http;
using Lte.Evaluations.DataService.Basic;
using Lte.MySqlFramework.Entities;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Parameters
{
    [ApiControl("CDMA小区有关的控制器")]
    public class CdmaCellController : ApiController
    {
        private readonly CdmaCellService _service;

        public CdmaCellController(CdmaCellService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("给定基站编号对定的扇区视图对象列表")]
        [ApiParameterDoc("btsId", "基站编号")]
        [ApiResponse("对定的扇区视图对象列表")]
        public IEnumerable<CdmaSectorView> Get(int btsId)
        {
            return _service.QuerySectors(btsId);
        }

        [HttpGet]
        [ApiDoc("获取经纬度范围内的小区列表")]
        [ApiParameterDoc("west", "西边经度")]
        [ApiParameterDoc("east", "东边经度")]
        [ApiParameterDoc("south", "南边纬度")]
        [ApiParameterDoc("north", "北边纬度")]
        [ApiResponse("经纬度范围内的小区列表")]
        public IEnumerable<CdmaSectorView> Get(double west, double east, double south, double north)
        {
            return _service.GetCells(west, east, south, north);
        }

        [HttpGet]
        [ApiDoc("给定基站名对应的小区扇区编号列表")]
        [ApiParameterDoc("btsName", "基站名")]
        [ApiResponse("对应的小区扇区编号列表，如果找不到则会返回错误")]
        public IEnumerable<byte> Get(string btsName)
        {
            return _service.GetSectorIds(btsName);
        }

        [HttpGet]
        [ApiDoc("给定基站名对应的小区列表")]
        [ApiParameterDoc("name", "基站名")]
        [ApiResponse("对应的小区列表，如果找不到则会返回错误")]
        public IHttpActionResult GetViews(string name)
        {
            var query = _service.GetCellViews(name);
            return query == null ? (IHttpActionResult)BadRequest("Wrong ENodeb Name!") : Ok(query);
        }

        [HttpGet]
        [ApiDoc("给定基站编号和扇区编号查询CDMA复合小区（同时包括1X和DO信息）")]
        [ApiParameterDoc("btsId", "基站编号")]
        [ApiParameterDoc("sectorId", "扇区编号")]
        [ApiResponse("CDMA小区，如果找不到则会返回错误")]
        public IHttpActionResult Get(int btsId, byte sectorId)
        {
            var item = _service.QueryCell(btsId, sectorId);
            return item == null ? (IHttpActionResult)BadRequest("The cell cannot be found!") : Ok(item);
        }

        [HttpGet]
        [ApiDoc("给定基站编号和扇区编号以及小区类型查询CDMA复合小区")]
        [ApiParameterDoc("btsId", "基站编号")]
        [ApiParameterDoc("sectorId", "扇区编号")]
        [ApiParameterDoc("cellType", "小区类型")]
        [ApiResponse("CDMA小区，如果找不到则会返回错误")]
        public IHttpActionResult Get(int btsId, byte sectorId, string cellType)
        {
            var item = _service.QueryCell(btsId, sectorId, cellType);
            return item == null ? (IHttpActionResult)BadRequest("The cell cannot be found!") : Ok(item);
        }
    }
}