using System.Collections.Generic;
using System.Web.Http;
using Lte.Domain.Regular;
using Lte.Evaluations.DataService.Basic;
using LtePlatform.Models;

namespace LtePlatform.Controllers.AdminitrativeRegion
{
    [ApiControl("��ԃ悅^߅������")]
    public class AreaBoundaryController : ApiController
    {
        private readonly TownBoundaryService _service;

        public AreaBoundaryController(TownBoundaryService service)
        {
            _service = service;
        }

        [HttpGet]
        public IEnumerable<AreaBoundaryView> Get()
        {
            return _service.GetAreaBoundaryViews();
        }
    }
}