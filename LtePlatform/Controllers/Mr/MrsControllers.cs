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

    public class MrsTadvController : ApiController
    {
        private readonly MrsService _service;

        public MrsTadvController(MrsService service)
        {
            _service = service;
        }

        [HttpGet]
        public MrsTadvStat Get(int eNodebId, byte sectorId, DateTime statDate)
        {
            return _service.QueryTadvStat(eNodebId, sectorId, statDate);
        }

        public IEnumerable<MrsTadvStat> Get(int eNodebId, byte sectorId, DateTime begin, DateTime end)
        {
            return _service.QueryTadvStats(eNodebId, sectorId, begin, end);
        }
    }

    public class MrsSinrUlController : ApiController
    {
        private readonly MrsService _service;

        public MrsSinrUlController(MrsService service)
        {
            _service = service;
        }

        [HttpGet]
        public MrsSinrUlStat Get(int eNodebId, byte sectorId, DateTime statDate)
        {
            return _service.QuerySinrUlStat(eNodebId, sectorId, statDate);
        }

        public IEnumerable<MrsSinrUlStat> Get(int eNodebId, byte sectorId, DateTime begin, DateTime end)
        {
            return _service.QuerySinrUlStats(eNodebId, sectorId, begin, end);
        }
    }

    public class MrsTadvRsrpController : ApiController
    {
        private readonly MrsService _service;

        public MrsTadvRsrpController(MrsService service)
        {
            _service = service;
        }

        public MrsTadvRsrpStat Get(int eNodebId, byte sectorId, DateTime statDate)
        {
            return _service.QueryTadvRsrpStat(eNodebId, sectorId, statDate);
        }

        public IEnumerable<MrsTadvRsrpStat> Get(int eNodebId, byte sectorId, DateTime begin, DateTime end)
        {
            return _service.QueryTadvRsrpStats(eNodebId, sectorId, begin, end);
        }
    }
}