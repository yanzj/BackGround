﻿using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.RegionKpi;
using Lte.Parameters.Entities.Kpi;
using NUnit.Framework;

namespace Lte.Evaluations.TestService
{
    public static class PreciseViewTestService
    {
        public static void AssertEqual(DistrictPreciseView left, DistrictPreciseView right)
        {
            Assert.AreEqual(left.City, right.City);
            Assert.AreEqual(left.District, right.District);
            Assert.AreEqual(left.FirstNeighbors, right.FirstNeighbors);
            Assert.AreEqual(left.SecondNeighbors, right.SecondNeighbors);
            Assert.AreEqual(left.TotalMrs, right.TotalMrs);
        }

        public static void AssertEqual(TownPreciseView left, TownPreciseView right)
        {
            Assert.AreEqual(left.City, right.City);
            Assert.AreEqual(left.District, right.District);
            Assert.AreEqual(left.Town, right.Town);
            Assert.AreEqual(left.FirstNeighbors, right.FirstNeighbors);
            Assert.AreEqual(left.SecondNeighbors, right.SecondNeighbors);
            Assert.AreEqual(left.ThirdNeighbors, right.ThirdNeighbors);
            Assert.AreEqual(left.TotalMrs, right.TotalMrs);
        }
    }
}
