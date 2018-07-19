using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Regular;
using NUnit.Framework;

namespace Lte.Domain.Test.Regular
{
    [TestFixture]
    public class NumberRegexTest
    {
        [TestCase("440602197807150616", "440602197807150616")]
        [TestCase("44060219780715061X", "44060219780715061X")]
        [TestCase("12345", "")]
        public void GetIdCardByStringTest(string source, string result)
        {
            Assert.AreEqual(RegexService.GetIdCardByString(source), result);
        }

        [TestCase("", 0)]
        [TestCase("D:\\", 1)]
        [TestCase("D:\\aaa.txt", 1)]
        [TestCase("D:\\abc.txt_1", 1)]
        [TestCase("D:\\aaa_1.txt", 2)]
        [TestCase("D:\\abc_2.txt_3", 3)]
        public void GetNumberAffixTest(string path, int number)
        {
            Assert.AreEqual(path.GetNumberAffix(), number);
        }

        [Test]
        public void TypeTest()
        {
            Assert.AreEqual(typeof(string), Type.GetType("System.String"));
        }

        [TestCase("OMC_4407.ENB_868575.RRU_51-1-1", "中兴", 51)]
        [TestCase("OMC_4405.ENB_501642.RRU_1", "大唐", 1)]
        [TestCase("OMC_4412.ENB_870204.RRU_0-60-0", "华为", 60)]
        public void RackIdTest(string rruNumber, string factory, int rackId)
        {
            var resultId = rruNumber.GetRruRackId(factory.GetEnumType<ENodebFactory>());
            Assert.AreEqual(rackId, resultId);
        }
    }
}
