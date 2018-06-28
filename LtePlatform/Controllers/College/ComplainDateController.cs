using System;
using System.Collections.Generic;
using System.Web.Http;
using Lte.Evaluations.DataService.College;
using Lte.Evaluations.ViewModels.RegionKpi;

namespace LtePlatform.Controllers.College
{
    [Cors("http://132.110.60.94:2018", "http://218.13.12.242:2018")]
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