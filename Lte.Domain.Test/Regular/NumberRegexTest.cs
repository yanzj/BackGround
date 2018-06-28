using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Regular;
using NUnit.Framework;

namespace Lte.Domain.Test.Regular
{
    [TestFixture]
    public class NumberRegexTest
    {
        [TestCase("440602197807150616", "440602197807150616")]
        [TestCase("44060219780715061X", "44060219780715061X")]
        public void GetIDCardByStringTest(string source, string result)
        {
            Assert.AreEqual(RegexService.GetIDCardByString(source), result);
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
    }
}
