﻿using Abp.EntityFramework.AutoMapper;
using Abp.Reflection;
using Lte.Evaluations.MockItems;
using Lte.Evaluations.Policy;
using Lte.Evaluations.TestService;
using Lte.MySqlFramework.Abstract;
using Lte.Parameters.MockOperations;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Region;
using Lte.Evaluations.DataService.Queries;
using Lte.MySqlFramework.Abstract.Cdma;
using Lte.MySqlFramework.Abstract.Region;

namespace Lte.Evaluations.DataService.College
{
    [TestFixture]
    public class CdmaRegionStatServiceTest
    {
        private AbpAutoMapperModule _module;
        private TypeFinder _typeFinder;
        private readonly Mock<IOptimzeRegionRepository> _regionRepository
            = new Mock<IOptimzeRegionRepository>();
        private readonly Mock<ICdmaRegionStatRepository> _statRepository
            = new Mock<ICdmaRegionStatRepository>();
        private CdmaRegionStatTestService _testService;

        [TestFixtureSetUp]
        public void FixtureSetup()
        {
            _typeFinder = new TypeFinder(new MyAssemblyFinder());
            _module = new AbpAutoMapperModule(_typeFinder);
            _module.PostInitialize();
            _statRepository.MockOperation();
            _regionRepository.MockOperation();
        }

        [SetUp]
        public void SetUp()
        {
            _regionRepository.MockQueryItems(new List<OptimizeRegion>
            {
                new OptimizeRegion { City = "city", Region = "region1" },
                new OptimizeRegion { City = "city", Region = "region2" },
                new OptimizeRegion { City = "city", Region = "region3" }
            }.AsQueryable());
            _testService = new CdmaRegionStatTestService(_regionRepository, _statRepository);
        }
        
        [TestCase("2015-5-1", "region1", "2015-4-1", 10)]
        [TestCase("2015-6-2", "region2", "2015-4-20", 15)]
        [TestCase("2015-6-2", "region3", "2015-5-20", 15)]
        public async Task TestQueryLastDateStat_Normal(string initialDate,
            string region, string recordDate, double erlang)
        {
            _testService.ImportElangRecord(region, recordDate, erlang);
            var result = await _testService.QueryLastDateStat(initialDate, "city");
            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatDate, DateTime.Parse(recordDate));
            Assert.AreEqual(result.StatViews.Count(), 2);
            result.StatViews.ElementAt(0).AssertRegionAndErlang2G(region, erlang);
            result.StatViews.ElementAt(1).AssertRegionAndErlang2G("city", erlang);
        }

        [TestCase("2015-3-30", "2015-5-1", "region1", "2015-4-1", 10)]
        [TestCase("2015-4-10", "2015-6-2", "region2", "2015-4-20", 15)]
        [TestCase("2015-5-4", "2015-6-2", "region3", "2015-5-20", 15)]
        public async Task TestQueryDateTrend_Normal(string beginDate, string endDate,
            string region, string recordDate, double erlang)
        {
            _testService.ImportElangRecord(region, recordDate, erlang);
            var result = await _testService.QueryDateTrend(beginDate, endDate, "city");
            Assert.IsNotNull(result);
            Assert.AreEqual(DateTime.Parse(result.StatDates.ElementAt(0)), DateTime.Parse(recordDate));
            Assert.AreEqual(result.ViewList.Count, 2);
        }

        [TestCase("2015-5-1", "region4", "2015-4-1", 10)]
        [TestCase("2015-6-2", "region5", "2015-4-10", 15)]
        [TestCase("2015-6-2", "region6", "2015-5-25", 15)]
        public async Task TestQueryLastDateStat_RegionNotFound(string initialDate,
            string region, string recordDate, double erlang)
        {
            _testService.ImportElangRecord(region, recordDate, erlang);
            var result = await _testService.QueryLastDateStat(initialDate, "city");
            Assert.IsNull(result);
        }

        [TestCase("2015-5-1", "region1", "2015-1-1", 10)]
        [TestCase("2015-6-2", "region2", "2015-2-10", 15)]
        [TestCase("2015-6-2", "region3", "2015-1-25", 15)]
        [TestCase("2015-6-3", "region4", "2015-1-25", 15)]
        public async Task TestQueryLastDateStat_DateOutOfRange(string initialDate,
            string region, string recordDate, double erlang)
        {
            _testService.ImportElangRecord(region, recordDate, erlang);
            var result = await _testService.QueryLastDateStat(initialDate, "city");
            Assert.IsNull(result);
        }

        [TestCase(1, "2015-5-1", "region1",
            new[] { "2015-4-1", "2015-4-6" }, new[] { 10.7, 12 }, 1)]
        [TestCase(2, "2015-6-2", "region2", 
            new[] { "2015-4-20", "2015-4-11" }, new[] { 15, 17.8 }, 0)]
        [TestCase(3, "2015-6-2", "region3", 
            new[] { "2015-5-20", "2015-5-19", "2015-5-21" }, new[] { 15, 26.2, 14 }, 2)]
        [TestCase(4, "2015-6-2", "region2",
            new[] { "2015-4-20", "2015-4-26", "2015-4-17" }, new[] { 15.1, 17, 18 }, 1)]
        public async Task TestQueryLastDateStat_SingleRegion_MultiDates(int testNo,
            string initialDate, string region, string[] recordDates, double[] erlangs,
            int matchedIndex)
        {
            _testService.ImportElangRecords(region, recordDates, erlangs);
            var result = await _testService.QueryLastDateStat(initialDate, "city");
            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatDate, DateTime.Parse(recordDates[matchedIndex]));
            Assert.AreEqual(result.StatViews.Count(), 2);
            result.StatViews.ElementAt(0).AssertRegionAndErlang2G(region, erlangs[matchedIndex]);
            result.StatViews.ElementAt(1).AssertRegionAndErlang2G("city", erlangs[matchedIndex]);
        }

        [TestCase(1, "2015-5-1", new[] { "region1", "region2" },
            "2015-4-1", new[] { 10.1, 12.4 })]
        [TestCase(2, "2015-6-2", new[] { "region2", "region3", "region1" },
            "2015-4-20", new[] { 15, 11.4, 12.3 })]
        [TestCase(3, "2015-6-2", new[] { "region3", "region2" },
            "2015-5-20", new[] { 15.9, 16.4 })]
        public async Task TestQueryLastDateStat_MultiRegions_SingleDate_AllRegionsMatched(int testNo,
            string initialDate, string[] regions, string recordDate, double[] erlangs)
        {
            _testService.ImportElangRecords(regions, recordDate, erlangs);
            var result = await _testService.QueryLastDateStat(initialDate, "city");
            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatDate, DateTime.Parse(recordDate));
            Assert.AreEqual(result.StatViews.Count(), regions.Length + 1);
            for (int i=0;i<regions.Length;i++)
                result.StatViews.ElementAt(i).AssertRegionAndErlang2G(regions[i], erlangs[i]);
            result.StatViews.ElementAt(regions.Length).AssertRegionAndErlang2G("city", erlangs.Sum());
        }

        [TestCase(1, "2015-5-1", new[] { "region1", "region2" },
            new[] { "2015-4-1", "2015-4-6" }, new[] { 10.1, 12.4 }, 1)]
        [TestCase(2, "2015-6-2", new[] { "region2", "region3", "region1" },
            new[] { "2015-5-20", "2015-5-19", "2015-5-21" }, new[] { 15, 11.4, 12.3 }, 2)]
        [TestCase(3, "2015-6-2", new[] { "region3", "region2" },
            new[] { "2015-4-20", "2015-4-26", "2015-4-17" }, new[] { 15.9, 16.4 }, 1)]
        public async Task TestQueryLastDateStat_MultiRegions_MultDates_AllRegionsMatched(int testNo,
            string initialDate, string[] regions, string[] recordDates, double[] erlangs, int matchedIndex)
        {
            _testService.ImportElangRecords(regions, recordDates, erlangs);
            var result = await _testService.QueryLastDateStat(initialDate, "city");
            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatDate, DateTime.Parse(recordDates[matchedIndex]));
            Assert.AreEqual(result.StatViews.Count(), 2);
            result.StatViews.ElementAt(0).AssertRegionAndErlang2G(regions[matchedIndex], erlangs[matchedIndex]);
            result.StatViews.ElementAt(1).AssertRegionAndErlang2G("city", erlangs[matchedIndex]);
        }

        [TestCase(1, "2015-5-1", new[] { "region1", "region2", "region3" },
            new[] { "2015-4-1", "2015-4-6", "2015-4-6" }, new[] { 10.1, 12.4, 15.7 }, new[] { 1, 2 })]
        [TestCase(2, "2015-6-2", new[] { "region2", "region3", "region1", "region2", "region3" },
            new[] { "2015-5-20", "2015-5-19", "2015-5-21", "2015-5-18", "2015-5-21" }, 
            new[] { 15, 11.4, 12.3, 21.2, 28.9 }, new[] { 2, 4 })]
        [TestCase(3, "2015-6-2", new[] { "region3", "region2", "region1", "region3", "region1" },
            new[] { "2015-4-20", "2015-4-26", "2015-4-17", "2015-4-26", "2015-4-26" }, 
            new[] { 15.9, 16.4, 20.7, 9.9, 2.3 }, new[] { 1, 3, 4 })]
        public async Task TestQueryLastDateStat_MultiRegions_MultDates_OneDateMatchedMultiRegions(int testNo,
            string initialDate, string[] regions, string[] recordDates, double[] erlangs, int[] matchedIndices)
        {
            _testService.ImportElangRecords(regions, recordDates, erlangs);
            var result = await _testService.QueryLastDateStat(initialDate, "city");
            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatDate, DateTime.Parse(recordDates[matchedIndices[0]]));
            Assert.AreEqual(result.StatViews.Count(), matchedIndices.Length + 1);
            result.StatViews.AssertRegionAndErlang2G(regions, erlangs, matchedIndices, "city");
        }

        [TestCase(1, "2015-5-1", new[] { "region1", "region2" },
            "2015-4-1", new[] { 10, 12 }, new[] { 11, 13 })]
        [TestCase(2, "2015-6-2", new[] { "region2", "region3", "region1" },
            "2015-4-20", new[] { 15, 11, 12 }, new[] { 11, 13, 17 })]
        [TestCase(3, "2015-6-2", new[] { "region3", "region2" },
            "2015-5-20", new[] { 15, 16 }, new[] { 11, 13 })]
        public async Task TestQueryLastDateStat_MultiRegions_SingleDate_DropRateConsidered(int testNo,
            string initialDate, string[] regions, string recordDate,
            int[] drop2GNums, int[] drop2GDems)
        {
            _testService.ImportDrop2Gs(regions, recordDate, drop2GNums, drop2GDems);
            var result = await _testService.QueryLastDateStat(initialDate, "city");
            Assert.IsNotNull(result);
            Assert.AreEqual(result.StatDate, DateTime.Parse(recordDate));
            Assert.AreEqual(result.StatViews.Count(), regions.Length + 1);
            for (var i = 0; i < regions.Length; i++)
                result.StatViews.ElementAt(i).AssertRegionAndDropRate(regions[i], (double) drop2GNums[i]/drop2GDems[i]);
            result.StatViews.ElementAt(regions.Length).AssertRegionAndDropRate("city",
                (double) drop2GNums.Sum()/drop2GDems.Sum());
        }
    }
}
