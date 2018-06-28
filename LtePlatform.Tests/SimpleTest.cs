using NUnit.Framework;
using System;

namespace LtePlatform.Tests
{
    [TestFixture]
    public class SimpleTest
    {
        [Test]
        public void Test_1()
        {
            Assert.IsTrue("20160201".StartsWith("201602"));
        }

        [Test]
        public void Test_2()
        {
            Assert.AreEqual(new DateTime(2016, 2, 5, 11, 5, 44).ToString("yyyyMMddHH"), "2016020511");
        }
    }
}
