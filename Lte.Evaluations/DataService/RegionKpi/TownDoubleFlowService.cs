using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework.Entities.Kpi;
using Abp.EntityFramework.Entities.RegionKpi;
using Lte.MySqlFramework.Abstract.Infrastructure;
using Lte.MySqlFramework.Abstract.Kpi;

namespace Lte.Evaluations.DataService.RegionKpi
{
    public class TownDoubleFlowService
    {
        private readonly IENodebRepository _eNodebRepository;
        private readonly IDoubleFlowHuaweiRepository _doubleFlowHuaweiRepository;
        private readonly IDoubleFlowZteRepository _doubleFlowZteRepository;

        public TownDoubleFlowService(IENodebRepository eNodebRepository,
            IDoubleFlowHuaweiRepository doubleFlowHuaweiRepository, IDoubleFlowZteRepository doubleFlowZteRepository)
        {
            _eNodebRepository = eNodebRepository;
            _doubleFlowHuaweiRepository = doubleFlowHuaweiRepository;
            _doubleFlowZteRepository = doubleFlowZteRepository;
        }

        public List<TownDoubleFlow> GetTownDoubleFlowStats(DateTime statDate)
        {
            var end = statDate.AddDays(1);
            var townStatList = new List<TownDoubleFlow>();
            var huaweiDoubleFlows = _doubleFlowHuaweiRepository.GetAllList(x => x.StatTime >= statDate && x.StatTime < end);
            townStatList.AddRange(huaweiDoubleFlows.GetTownStats<DoubleFlowHuawei, TownDoubleFlow>(_eNodebRepository));
            var zteDoubleFlows = _doubleFlowZteRepository.GetAllList(x => x.StatTime >= statDate && x.StatTime < end);
            townStatList.AddRange(zteDoubleFlows.GetTownStats<DoubleFlowZte, TownDoubleFlow>(_eNodebRepository));
            return townStatList;
        }

    }
}
