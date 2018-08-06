using System;
using System.Collections.Generic;
using System.Web.Http;
using Lte.Evaluations.DataService.College;
using Lte.MySqlFramework.Support;

namespace LtePlatform.Controllers.College
{
    public class ComplainDateController : ApiController
    {
        private readonly ComplainService _service;

        public ComplainDateController(ComplainService service)
        {
            _service = service;
        }

        [HttpGet]
        public DistrictComplainDateView Get(DateTime initialDate)
        {
            return _service.QueryLastDateStat(initialDate);
        }

        [HttpGet]
        public List<DistrictComplainView> Get(DateTime begin, DateTime end)
        {
            return _service.QueryDateSpanStats(begin.Date, end.Date);
        }
    }
}