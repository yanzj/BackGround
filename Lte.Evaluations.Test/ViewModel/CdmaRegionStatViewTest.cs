using Abp.EntityFramework.AutoMapper;
using Abp.Reflection;
using Lte.Evaluations.DataService.Kpi;
using Lte.Evaluations.Policy;
using Lte.MySqlFramework.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Cdma;
using Lte.Evaluations.DataService.Queries;

namespace Lte.Evaluations.ViewModel
{
    [TestFixture]
    public class CdmaRegionStatViewTest
    {
        private AbpAutoMapperModule _module;
        private TypeFinder _typeFinder;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _typeFinder = new TypeFinder(new MyAssemblyFinder());
            _module = new AbpAutoMapperModule(_typeFinder);
            _module.PostInitialize();
        }

        [TestCase("Region-1", "2015-1-1", 12.1, 2, 3, 4, 5)]
        [TestCase("Region-2", "2015-11-1", 12.7, 2, 0, 4, 5)]
        [TestCase("Region-3", "2015-1-1", 12.1, 2, 3, 9, 0)]
        [TestCase("Region-1", "2015-1-15", -22.1, 7, 0, 4, 0)]
        public void Test_Constructor(string region, string dateString, double erlang, int drop2GNum, int drop2GDem,
            int ecioNum, int ecioDem)
        {
            var stat = new CdmaRegionStat
            {
                Region = region,
                StatDate = DateTime.Parse(dateString),
                ErlangIncludingSwitch = erlang,
                Drop2GNum = drop2GNum,
                Drop2GDem = drop2GDem,
                EcioNum = ecioNum,
                EcioDem = ecioDem
            };
            var view = stat.MapTo<CdmaRegionStatView>();
            Assert.AreEqual(view.Region, region);
            Assert.AreEqual(view.ErlangIncludingSwitch, erlang);
            Assert.AreEqual(view.Drop2GRate, drop2GDem == 0 ? 0 : (double)drop2GNum / drop2GDem);
            Assert.AreEqual(view.Ecio, ecioDem == 0 ? 1 : (double)ecioNum / ecioDem);
        }

        [TestCase("2015-1-2", "region1", 12.3)]
        [TestCase("2015-3-2", "region3", 20.3)]
        public void Test_OneElement_Matched(string date, string region, double erlang2G)
        {
            var statList = new List<CdmaRegionStat>
            {
                new CdmaRegionStat
                {
                    ErlangIncludingSwitch = erlang2G,
                    Region = region,
                    StatDate = DateTime.Parse(date)
                }
            };
            var dates = new List<DateTime>
            {
                DateTime.Parse(date)
            };
            var regionList = new List<string>
            {
                region
            };
            var viewList = CdmaRegionStatService.GenerateViewList(statList, dates, regionList);
            Assert.IsNotNull(viewList);
            Assert.AreEqual(viewList.Count, 2);
            Assert.IsNotNull(viewList[0]);
            Assert.AreEqual(viewList[0].Count(), 1);
            Assert.AreEqual(viewList[1].Count(), 1);
            viewList[0].ElementAt(0).AssertErlang2G(erlang2G);
            viewList[1].ElementAt(0).AssertErlang2G(erlang2G);
        }

        [TestCase(new[] { "2015-1-2", "2015-2-4" }, "region1", new[] { 12.8, 22.3 }, 0)]
        [TestCase(new[] { "2015-1-2", "2015-2-4" }, "region1", new[] { 12.8, 22.3 }, 1)]
        [TestCase(new[] { "2015-8-2", "2015-7-4" }, "region1", new[] { 12.8, 22.3 }, 0)]
        [TestCase(new[] { "2015-8-2", "2015-7-4", "2015-3-2" }, "region1", new[] { 12.8, 22.3, 32.1 }, 1)]
        public void Test_SingleRegion_MultiDates_OnlyOneDateMatched(string[] dates,
            string region, double[] erlang2Gs, int matchIndex)
        {
            var statList = dates.Select((t, i) => new CdmaRegionStat
            {
                ErlangIncludingSwitch = erlang2Gs[i],
                Region = region,
                StatDate = DateTime.Parse(t)
            }).ToList();
            var singleDate = new List<DateTime>
            {
                DateTime.Parse(dates[matchIndex])
            };
            var regionList = new List<string>
            {
                region
            };
            var viewList = CdmaRegionStatService.GenerateViewList(statList,
                singleDate, regionList);
            Assert.IsNotNull(viewList);
            Assert.AreEqual(viewList.Count, 2);
            Assert.IsNotNull(viewList[0]);
            Assert.AreEqual(viewList[0].Count(), 1);
            Assert.AreEqual(viewList[1].Count(), 1);
            viewList[0].ElementAt(0).AssertErlang2G(erlang2Gs[matchIndex]);
            viewList[1].ElementAt(0).AssertErlang2G(erlang2Gs[matchIndex]);
        }

        [TestCase(new[] { "2015-1-2", "2015-2-4" }, "region1", new[] { 12.8, 22.3 }, 0)]
        [TestCase(new[] { "2015-1-2", "2015-2-4" }, "region1", new[] { 12.8, 22.3 }, 1)]
        [TestCase(new[] { "2015-8-2", "2015-7-4" }, "region1", new[] { 12.8, 22.3 }, 2)]
        [TestCase(new[] { "2015-8-2", "2015-7-4", "2015-3-2" }, "region1", new[] { 12.8, 22.3, 32.1 }, 3)]
        public void Test_SingleRegion_MultiDates_AllDatesMatched(string[] dates,
            string region, double[] erlang2Gs, int testNo)
        {
            var statList = dates.Select((t, i) => new CdmaRegionStat
            {
                ErlangIncludingSwitch = erlang2Gs[i],
                Region = region,
                StatDate = DateTime.Parse(t)
            }).ToList();
            var regionList = new List<string>
            {
                region
            };
            var viewList = CdmaRegionStatService.GenerateViewList(statList, dates.Select(DateTime.Parse), regionList);
            Assert.IsNotNull(viewList);
            Assert.AreEqual(viewList.Count, 2);
            Assert.IsNotNull(viewList[0]);
            Assert.AreEqual(viewList[0].Count(), dates.Length);
            Assert.AreEqual(viewList[1].Count(), dates.Length);
        }

        [Test]
        public void Test_GetAssmebly()
        {
            var asm = Assembly.Load("Lte.Evaluations");
            Assert.IsNotNull(asm);
            Assert.AreEqual(asm.FullName, "Lte.Evaluations, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
            asm = Assembly.Load("Lte.Parameters");
            Assert.IsNotNull(asm);
            Assert.AreEqual(asm.FullName, "Lte.Parameters, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null");
        }
    }
}
