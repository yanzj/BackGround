using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Common.Types;
using Lte.Domain.Regular;
using NUnit.Framework;

namespace Lte.Domain.Test.Regular
{
    [TestFixture]
    public class DateTimeConvertTest
    {
        [Test]
        public void SimpleTest()
        {
            Assert.AreEqual("2018-06-01 23:54:56".ConvertToDateTime(DateTime.Today), new DateTime(2018, 6, 1, 23, 54, 56));
        }

        [Test]
        public void MillisecondsTest()
        {
            Assert.AreEqual("2018-06-01 23:54:56.010".ConvertToDateTime(DateTime.Today), new DateTime(2018, 6, 1, 23, 54, 56, 10));
        }

        [Test]
        public void MillisecondsWithCalculationTest()
        {
            var origin = "2018-06-01 23:54:56:010";
            var fields = origin.GetSplittedFields(':');
            if (fields.Length == 4)
            {
                origin = origin.Replace(':' + fields[3], '.' + fields[3]);
            }
            Assert.AreEqual(origin.ConvertToDateTime(DateTime.Today), new DateTime(2018, 6, 1, 23, 54, 56, 10));
        }
    }
}
