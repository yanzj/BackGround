using Lte.Domain.Common.Wireless;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using LtePlatform.Models;

namespace LtePlatform.Controllers.Kpi
{
    [ApiControl("KPI选项控制器")]
    [ApiGroup("KPI")]
    public class KpiOptionsController : ApiController
    {
        [HttpGet]
        public IEnumerable<string> Get(string key)
        {
            return WirelessConstants.EnumDictionary.ContainsKey(key)
                ? WirelessConstants.EnumDictionary[key].Select(x => x.Item2)
                : new List<string>();
        }
    }
}
