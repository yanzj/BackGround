﻿using Abp.EntityFramework.AutoMapper;
using Abp.Reflection;
using AutoMapper;
using Lte.Evaluations.DataService.Queries;
using Lte.Evaluations.MockItems;
using Lte.Evaluations.Policy;
using Lte.Parameters.MockOperations;
using Moq;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using Abp.EntityFramework.Entities.Maintainence;
using Lte.Domain.Common.Wireless.Alarm;
using Lte.Evaluations.DataService.Basic;
using Lte.Evaluations.DataService.Maintainance;
using Lte.MySqlFramework.Abstract.Maintainence;
using Lte.MySqlFramework.Entities.Maintainence;

namespace Lte.Evaluations.DataService.Kpi
{
    [TestFixture]
    public class AlarmsServiceTest
    {
        private readonly Mock<IAlarmRepository> _repository = new Mock<IAlarmRepository>();
        private AlarmsService _service;
        private readonly ITypeFinder _typeFinder = new TypeFinder(new MyAssemblyFinder());

        [TestFixtureSetUp]
        public void FixtureSetup()
        {
            var module = new AbpAutoMapperModule(_typeFinder);
            module.PostInitialize();
            _repository.MockOperations();
            _service = new AlarmsService(_repository.Object, null, null, null, null);
        }

        [TestCase(1, "aaieqwigiowi", "2015-1-1 12:33", "2015-1-1 0:05", "2015-1-1 13:45", true)]
        [TestCase(2, "aai33igiowi", "2015-1-1 16:33", "2015-1-1 0:05", "2015-1-1 13:45", false)]
        [TestCase(1, "aaieqqgiowi", "2015-1-1 12:33", "2014-12-31 10:05", "2015-1-1 13:45", true)]
        [TestCase(13, "aaierweigiowi", "2015-1-1 12:33", "2015-1-1 0:05", "2015-1-2 13:45", true)]
        [TestCase(111, "aaieqwigiowi", "2015-1-1 12:33", "2015-1-1 14:05", "2015-1-1 17:45", false)]
        public void Test_GetAllList(int eNodebId, string details,
            string happenedTime, string begin, string end, bool matched)
        {
            _repository.MockQueryItems(new List<AlarmStat>
            {
                new AlarmStat
                {
                    ENodebId = eNodebId,
                    Details = details,
                    HappenTime = DateTime.Parse(happenedTime)
                }
            }.AsQueryable());
            var items = _repository.Object.GetAllList(DateTime.Parse(begin), DateTime.Parse(end), eNodebId);
            items.ShouldNotBeNull();
            items.Count.ShouldBe(matched ? 1 : 0);
            var results = Mapper.Map<IEnumerable<AlarmStat>, IEnumerable<AlarmView>>(items);
            results.Count().ShouldBe(matched ? 1 : 0);
            var views = _service.Get(eNodebId, DateTime.Parse(begin), DateTime.Parse(end));
            views.Count().ShouldBe(matched ? 1 : 0);
        }

        [TestCase(1, "aaieqwigiowi", "2015-1-1 12:33", "2015-1-1 0:05", "2015-1-1 13:45", true)]
        [TestCase(2, "aai33igiowi", "2015-1-1 16:33", "2015-1-1 0:05", "2015-1-1 13:45", false)]
        [TestCase(1, "aaieqqgiowi", "2015-1-1 12:33", "2014-12-31 10:05", "2015-1-1 13:45", true)]
        [TestCase(13, "aaierweigiowi", "2015-1-1 12:33", "2015-1-1 0:05", "2015-1-2 13:45", true)]
        [TestCase(111, "aaieqwigiowi", "2015-1-1 12:33", "2015-1-1 14:05", "2015-1-1 17:45", false)]
        public void Test_Get_SingleAlarms_BasicParameters(int eNodebId, string details,
            string happenedTime, string begin, string end, bool matched)
        {
            _repository.MockQueryItems(new List<AlarmStat>
            {
                new AlarmStat
                {
                    ENodebId = eNodebId,
                    Details = details,
                    HappenTime = DateTime.Parse(happenedTime)
                }
            }.AsQueryable());
            var views = _service.Get(eNodebId, DateTime.Parse(begin), DateTime.Parse(end));
            views.Count().ShouldBe(matched ? 1 : 0);
            var count = _service.GetCounts(eNodebId, DateTime.Parse(begin), DateTime.Parse(end));
            count.ShouldBe(matched ? 1 : 0);
            if (matched)
            {
                views.ElementAt(0).AssertBasicParameters(eNodebId, details);
            }
        }

        [TestCase(1, 2, 3, "2015-1-1 12:33", "2015-1-1 12:35", 
            "2015-1-1 0:05", "2015-1-1 13:45", true, "Cell-2", 2.0)]
        [TestCase(2, 3, 0, "2015-1-1 16:33", "2015-1-1 17:35", 
            "2015-1-1 0:05", "2015-1-1 13:45", false, "", 0.0)]
        [TestCase(1, 255, 1, "2015-1-1 12:33", "2015-1-1 13:33",
            "2014-12-31 10:05", "2015-1-1 13:45", true, "基站级", 60.0)]
        [TestCase(13, 6, 5, "2015-1-1 12:33", "2015-1-1 12:45",
            "2015-1-1 0:05", "2015-1-2 13:45", true, "基站级", 12.0)]
        [TestCase(111, 7, 2, "2015-1-1 12:33", "2015-1-1 12:45", 
            "2015-1-1 14:05", "2015-1-1 17:45", false, "", 0.0)]
        public void Test_Get_SingleAlarms_Position(int eNodebId, byte sectorId, byte alarmCategory,
            string happenedTime, string recoveryTime, string begin, string end, bool matched, string position, double duration)
        {
            _repository.MockQueryItems(new List<AlarmStat>
            {
                new AlarmStat
                {
                    ENodebId = eNodebId,
                    SectorId = sectorId,
                    HappenTime = DateTime.Parse(happenedTime),
                    AlarmCategory = (AlarmCategory)alarmCategory,
                    RecoverTime = DateTime.Parse(recoveryTime)
                }
            }.AsQueryable());
            var views = _service.Get(eNodebId, DateTime.Parse(begin), DateTime.Parse(end));
            var count = _service.GetCounts(eNodebId, DateTime.Parse(begin), DateTime.Parse(end));
            if (matched)
            {
                Assert.AreEqual(views.Count(), 1);
                Assert.AreEqual(count, 1);
                views.ElementAt(0).AssertPosition(position, duration);
            }
            else
            {
                Assert.AreEqual(views.Count(), 0);
                Assert.AreEqual(count, 0);
            }
        }

        [TestCase(1, 0, 1, 4, "2015-1-1 12:33", "2015-1-1 0:05", "2015-1-1 13:45", true, "小区退服")]
        [TestCase(1, 0, 2, 9, "2015-1-1 12:33", "2015-1-1 0:05", "2015-1-1 13:45", true, "X2断链告警(198094421)")]
        [TestCase(2, 1, 1, 17, "2015-1-1 12:33", "2015-1-1 0:05", "2015-1-1 13:45", true, "基站退出服务(198094422)")]
        [TestCase(1, 2, 3, 22, "2015-1-1 12:33", "2015-1-1 0:05", "2015-1-1 13:45", true, "RX通道异常(198098469)")]
        [TestCase(2, 1, 2, 5, "2015-1-1 16:33", "2015-1-1 0:05", "2015-1-1 13:45", false, "")]
        [TestCase(1, 2, 8, 10, "2015-1-1 12:33", "2014-12-31 10:05", "2015-1-1 13:45", true, "X2用户面路径不可用(198094467)")]
        [TestCase(13, 3, 6, 29, "2015-1-1 12:33", "2015-1-1 0:05", "2015-1-2 13:45", true, "单板通讯链路断(198097060)")]
        [TestCase(13, 3, 6, 36, "2015-1-1 12:33", "2015-1-1 0:05", "2015-1-2 13:45", true, "没有可用的空口时钟源(198092217)")]
        [TestCase(13, 3, 6, 44, "2015-1-1 12:33", "2015-1-1 0:05", "2015-1-2 13:45", true, "其他告警")]
        [TestCase(13, 3, 6, 51, "2015-1-1 12:33", "2015-1-1 0:05", "2015-1-2 13:45", true, "射频单元ALD电流异常告警")]
        [TestCase(13, 3, 6, 56, "2015-1-1 12:33", "2015-1-1 0:05", "2015-1-2 13:45", true, "射频单元时钟异常告警")]
        [TestCase(111, 2, 7, 57, "2015-1-1 12:33", "2015-1-1 14:05", "2015-1-1 17:45", false, "射频单元ALD电流异常告警")]
        public void Test_Get_SingleAlarms_Types(int eNodebId, byte level, byte category, short type,
            string happenedTime, string begin, string end, bool matched, string typeDescription)
        {
            _repository.MockQueryItems(new List<AlarmStat>
            {
                new AlarmStat
                {
                    ENodebId = eNodebId,
                    AlarmLevel = (AlarmLevel)level,
                    AlarmCategory = (AlarmCategory)category,
                    AlarmType = (AlarmType)type,
                    HappenTime = DateTime.Parse(happenedTime)
                }
            }.AsQueryable());
            var views = _service.Get(eNodebId, DateTime.Parse(begin), DateTime.Parse(end));
            var count = _service.GetCounts(eNodebId, DateTime.Parse(begin), DateTime.Parse(end));
            if (matched)
            {
                Assert.AreEqual(views.Count(), 1);
                Assert.AreEqual(count, 1);
                views.ElementAt(0).AssertTypes((AlarmLevel)level, (AlarmCategory)category, (AlarmType)type, typeDescription);
            }
            else
            {
                Assert.AreEqual(views.Count(), 0);
                Assert.AreEqual(count, 0);
            }
        }
    }
}
