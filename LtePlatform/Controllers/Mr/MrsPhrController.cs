using Lte.Evaluations.DataService.Mr;
using Lte.Parameters.Entities.Kpi;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace LtePlatform.Controllers.Mr
{
    public class MrsPhrController : ApiController
    {
        private readonly MrsService _service;

        public MrsPhrController(MrsService service)
        {
            _service = service;
        }

        [HttpGet]
        public MrsPhrStat Get(int eNodebId, byte sectorId, DateTime statDate)
        {
            return _service.QueryPhrStat(eNodebId, sectorId, statDate);
        }

        public IEnumerable<MrsPhrStat> Get(int eNodebId, byte sectorId, DateTime begin, DateTime end)
        {
            return _service.QueryPhrStats(eNodebId, sectorId, begin, end);
        } 
    }
}