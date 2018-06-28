using System;
using System.Collections.Generic;
using System.Linq;
using Lte.Domain.Common.Wireless;
using Lte.MySqlFramework.Abstract;
using Lte.Parameters.Abstract.Kpi;
using Lte.Parameters.Entities.Kpi;

namespace Lte.Evaluations.DataService.College
{
    public class CollegeAlarmService
    {
        private readonly IInfrastructureRepository _infrastructureRepository;
        private readonly IENodebRepository _eNodebRepository;
        private readonly IAlarmRepository _alarmRepository;

        public CollegeAlarmService(IInfrastructureRepository infrastructureRepository,
            IENodebRepository eNodebRepository, IAlarmRepository alarmRepository)
        {
            _infrastructureRepository = infrastructureRepository;
            _eNodebRepository = eNodebRepository;
            _alarmRepository = alarmRepository;
        }

        public IEnumerable<Tuple<string, IEnumerable<AlarmStat>>> QueryCollegeENodebAlarms(string collegeName,
            DateTime begin, DateTime end)
        {
            var ids = _infrastructureRepository.GetCollegeInfrastructureIds(collegeName, InfrastructureType.ENodeb);
            return (from id in ids
                select _eNodebRepository.Get(id)
                into eNodeb
                where eNodeb != null
                let stats = _alarmRepository.GetAllList(begin, end, eNodeb.ENodebId)
                select new Tuple<string, IEnumerable<AlarmStat>>(eNodeb.Name, stats)).ToList();
        }
    }
}