﻿using Abp.EntityFramework.AutoMapper;
using Abp.Reflection;
using Lte.Evaluations.MockItems;
using Lte.Evaluations.Policy;
using Lte.Evaluations.TestService;
using Lte.Parameters.MockOperations;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Abp.EntityFramework.Entities.Region;
using Abp.EntityFramework.Entities.RegionKpi;
using Lte.MySqlFramework.Abstract.Region;
using Lte.MySqlFramework.Abstract.RegionKpi;

namespace Lte.Evaluations.DataService.Kpi
{
    [TestFixture]
    public class PreciseRegionStatServiceTest
    {
        private readonly ITypeFinder _typeFinder = new TypeFinder(new MyAssemblyFinder());
        private readonly Mock<ITownRepository> _townRepository = new Mock<ITownRepository>(); 
        private readonly Mock<ITownPreciseCoverageRepository> _statRepository =
            new Mock<ITownPreciseCoverageRepository>();

        private PreciseRegionStatTestService _testService;

        [TestFixtureSetUp]
        public void FixtureSetup()
        {
            var module = new AbpAutoMapperModule(_typeFinder);
            module.PostInitialize();
            _townRepository.MockGetId<ITownRepository, Town>();
        }

        [SetUp]
        public void SetUp()
        {
            _townRepository.MockQueryItems(new List<Town>
            {
                new Town {Id = 1, CityName = "city", DistrictName = "district1", TownName = "town1"},
                new Town {Id = 2, CityName = "city", DistrictName = "district2", TownName = "town2"},
                new Town {Id = 3, CityName = "city", DistrictName = "district2", TownName = "town3"}
            }.AsQueryable());
            _testService = new PreciseRegionStatTestService(_townRepository, _statRepository);
        }

        [TestCase("2015-5-1", 1, "2015-4-1", 10, 11, 12, 13)]
        [TestCase("2015-6-2", 2, "2015-4-20", 15, 21, 72, 44)]
        [TestCase("2015-6-2", 3, "2015-5-20", 15, 38, 41, 5)]
        public void TestQueryLastDateStat_Normal(string initialDate, int townId, string statDate, int totalMrs,
            int firstNeighbors, int secondNeighbors, int thirdNeighbors)
        {
            _testService.ImportPreciseRecord(townId, statDate, totalMrs, firstNeighbors, secondNeighbors, thirdNeighbors);
            var result = _testService.QueryLastDateStat(initialDate, "city");
            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatDate, DateTime.Parse(statDate));
            Assert.AreEqual(result.TownViews.Count(), 1);
            PreciseViewTestService.AssertEqual(result.TownViews.ElementAt(0), new TownPreciseView
            {
                City = "city",
                District = "district" + (townId == 1 ? 1 : 2),
                Town = "town" + townId,
                FirstNeighbors = firstNeighbors,
                SecondNeighbors = secondNeighbors,
                ThirdNeighbors = thirdNeighbors,
                TotalMrs = totalMrs
            });
            Assert.AreEqual(result.DistrictViews.Count(), 1);
            PreciseViewTestService.AssertEqual(result.DistrictViews.ElementAt(0), new DistrictPreciseView
            {
                City = "city",
                District = "district" + (townId == 1 ? 1 : 2),
                FirstNeighbors = firstNeighbors,
                SecondNeighbors = secondNeighbors,
                TotalMrs = totalMrs
            });
        }

        [TestCase("2015-5-1", 1, new[] { "2015-4-1", "2015-4-2" }, new[] { 10, 11 }, new[] { 12, 13 }, new[] { 15, 16 }, new[] { 21, 13 },
            "2015-4-2", 11, 13, 16, 13)]
        [TestCase("2015-6-2", 2, new[] { "2015-4-20", "2015-4-18" }, new[] { 10, 11 }, new[] { 12, 13 }, new[] { 15, 16 }, new[] { 21, 13 },
            "2015-4-20", 10, 12, 15, 21)]
        [TestCase("2015-6-2", 3, new[] { "2015-5-20", "2015-5-13", "2015-5-25" },
            new[] { 10, 11, 19 }, new[] { 16, 13, 17 }, new[] { 15, 16, 22 }, new[] { 27, 21, 13 },
            "2015-5-25", 19, 17, 22, 13)]
        public void TestQueryLastDateStat_SingleTown(string initialDate, int townId, string[] statDates, int[] totalMrs,
            int[] firstNeighbors, int[] secondNeighbors, int[] thirdNeighbors,
            string resultDate, int resultTotal, int resultFirst, int resultSecond, int resultThird)
        {
            _testService.ImportPreciseRecord(townId, statDates, totalMrs, firstNeighbors, secondNeighbors,
                thirdNeighbors);
            var result = _testService.QueryLastDateStat(initialDate, "city");
            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatDate, DateTime.Parse(resultDate));
            Assert.AreEqual(result.TownViews.Count(), 1);
            PreciseViewTestService.AssertEqual(result.TownViews.ElementAt(0), new TownPreciseView
            {
                City = "city",
                District = "district" + (townId == 1 ? 1 : 2),
                Town = "town" + townId,
                FirstNeighbors = resultFirst,
                SecondNeighbors = resultSecond,
                ThirdNeighbors = resultThird,
                TotalMrs = resultTotal
            });
            Assert.AreEqual(result.DistrictViews.Count(), 1);
            PreciseViewTestService.AssertEqual(result.DistrictViews.ElementAt(0), new DistrictPreciseView
            {
                City = "city",
                District = "district" + (townId == 1 ? 1 : 2),
                FirstNeighbors = resultFirst,
                SecondNeighbors = resultSecond,
                TotalMrs = resultTotal
            });
        }

        [TestCase("2015-5-1", new[] { 1, 2 }, new[] { "2015-4-1", "2015-4-2" }, 
            new[] { 10, 11 }, new[] { 12, 13 }, new[] { 15, 16 }, new[] { 21, 13 },
            "2015-4-2", new[] { 2 }, new[] { 11 }, new[] { 13 }, new[] { 16 }, new[] { 13 },
            new[] { 2 }, new[] { 11 }, new[] { 13 }, new[] { 16 })]
        [TestCase("2015-6-2", new[] { 2, 3 }, new[] { "2015-4-20", "2015-4-18" }, 
            new[] { 10, 11 }, new[] { 12, 13 }, new[] { 15, 16 }, new[] { 21, 13 },
            "2015-4-20", new[] { 2 }, new[] { 10 }, new[] { 12 }, new[] { 15 }, new[] { 21 },
            new[] { 2 }, new[] { 10 }, new[] { 12 }, new[] { 15 })]
        [TestCase("2015-6-2", new[] { 3, 2, 1}, new[] { "2015-5-20", "2015-5-13", "2015-5-25" },
            new[] { 10, 11, 19 }, new[] { 16, 13, 17 }, new[] { 15, 16, 22 }, new[] { 27, 21, 13 },
            "2015-5-25", new[] { 1 }, new[] { 19 }, new[] { 17 }, new[] { 22 }, new[] { 13 },
            new[] { 1 }, new[] { 19 }, new[] { 17 }, new[] { 22 })]
        public void TestQueryLastDateStat_MultiTowns(string initialDate, int[] townIds, string[] statDates, int[] totalMrs,
            int[] firstNeighbors, int[] secondNeighbors, int[] thirdNeighbors,
            string resultDate, int[] resultTownId, int[] resultTotal, int[] resultFirst, int[] resultSecond, int[] resultThird,
            int[] resultDistrictId, int[] districtTotal, int[] districtFirst, int[] districtSecond)
        {
            _testService.ImportPreciseRecord(townIds, statDates, totalMrs, firstNeighbors, secondNeighbors,
                thirdNeighbors);
            var result = _testService.QueryLastDateStat(initialDate, "city");
            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatDate, DateTime.Parse(resultDate));
            for (var i = 0; i < resultTownId.Length; i++)
            {
                PreciseViewTestService.AssertEqual(result.TownViews.ElementAt(i), new TownPreciseView
                {
                    City = "city",
                    District = "district" + (resultTownId[i] == 1 ? 1 : 2),
                    Town = "town" + resultTownId[i],
                    FirstNeighbors = resultFirst[i],
                    SecondNeighbors = resultSecond[i],
                    ThirdNeighbors = resultThird[i],
                    TotalMrs = resultTotal[i]
                });
            }
            for (var i = 0; i < resultDistrictId.Length; i++)
            {
                PreciseViewTestService.AssertEqual(result.DistrictViews.ElementAt(i), new DistrictPreciseView
            {
                City = "city",
                District = "district" + resultDistrictId[i],
                FirstNeighbors = districtFirst[i],
                SecondNeighbors = districtSecond[i],
                TotalMrs = districtTotal[i]
            });
            }
                
        }
    }
}
