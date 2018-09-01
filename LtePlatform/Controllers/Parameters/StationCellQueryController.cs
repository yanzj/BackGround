using Lte.Evaluations.DataService.Basic;
using LtePlatform.Models;
using System.Collections.Generic;
using System.Web.Http;
using Lte.MySqlFramework.Entities.Infrastructure;

namespace LtePlatform.Controllers.Parameters
{
    [ApiControl("集团小区信息查询控制器")]
    [ApiGroup("基础信息")]
    public class StationCellQueryController : ApiController
    {
        private readonly ConstructionInformationService _service;

        public StationCellQueryController(ConstructionInformationService service)
        {
            _service = service;
        }

        [HttpGet]
        [ApiDoc("查询所有记录")]
        [ApiResponse("所有记录")]
        public IEnumerable<ConstructionView> QueryAll()
        {
            return _service.QueryAll();
        }

        [HttpGet]
        [ApiDoc("根据小区名称查询小区记录")]
        [ApiParameterDoc("cellName", "小区名称")]
        [ApiResponse("小区记录")]
        public ConstructionView Get(string cellName)
        {
            return _service.QueryByCellName(cellName);
        }

        [HttpGet]
        [ApiDoc("根据站点编号查询记录")]
        [ApiParameterDoc("stationNum", "站点编号")]
        [ApiResponse("符合条件的记录")]
        public IEnumerable<ConstructionView> GetByStationNum(string stationNum)
        {
            return _service.QueryByStationNum(stationNum);
        }

        [HttpGet]
        [ApiDoc("根据天线编号查询记录")]
        [ApiParameterDoc("antennaNum", "天线编号")]
        [ApiResponse("符合条件的记录")]
        public IEnumerable<ConstructionView> QueryByAntennaNum(string antennaNum)
        {
            return _service.QueryByAntennaNum(antennaNum);
        }

        [HttpGet]
        [ApiDoc("根据小区标识查询小区记录")]
        [ApiParameterDoc("cellNum", "小区标识")]
        [ApiResponse("小区记录")]
        public ConstructionView GetByCellNum(string cellNum)
        {
            return _service.QueryByCellNum(cellNum);
        }

    }
}