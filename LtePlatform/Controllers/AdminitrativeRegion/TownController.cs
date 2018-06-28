using System;
using System.Web.Http;
using Abp.EntityFramework.Entities;
using Lte.Evaluations.DataService.Basic;
using LtePlatform.Models;

namespace LtePlatform.Controllers.AdminitrativeRegion
{
    [ApiControl("镇区信息查询控制器")]
    public class TownController : ApiController
    {
        private readonly TownQueryService _service;

        public TownController(TownQueryService service)
        {
            _service = service;
        }

        [ApiDoc("根据LTE基站编号查询基站镇区信息")]
        [ApiParameterDoc("eNodebId", "LTE基站编号")]
        [ApiResponse("基站镇区信息，包括城市、区域、镇")]
        public Tuple<string, string, string> Get(int eNodebId)
        {
            return _service.GetTownNamesByENodebId(eNodebId);
        }

        [HttpGet]
        [ApiDoc("查询指定镇区信息")]
        [ApiParameterDoc("city", "城市")]
        [ApiParameterDoc("district", "区")]
        [ApiParameterDoc("town", "镇")]
        [ApiResponse("指定镇区信息")]
        public TownView Get(string city, string district, string town)
        {
            return _service.GetTown(city, district, town);
        }
    }
}