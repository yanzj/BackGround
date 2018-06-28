using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Common;
using Lte.Domain.Common.Types;
using NUnit.Framework;

namespace Lte.Domain.Test.Common
{
    [TestFixture]
    public class GeneralTextTest
    {
        [Test]
        public void TestSplittedFields()
        {
            var source = "/新增资源/已立项待建设/";
            var fields = source.GetSplittedFields('/');
            Assert.AreEqual(fields.Length, 2);
            Assert.AreEqual(fields[0], "新增资源");
        }

        [Test]
        public void TestSplittedFields4()
        {
            var source = "/网络侧故障/无线网络/3G网络/基站/小区退服/";
            var fields = source.GetSplittedFields('/');
            Assert.AreEqual(fields.Length, 5);
            Assert.AreEqual(fields[0], "网络侧故障");
        }
    }
}
