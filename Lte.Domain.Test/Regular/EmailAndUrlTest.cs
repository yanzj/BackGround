using Lte.Domain.Regular;
using NUnit.Framework;

namespace Lte.Domain.Test.Regular
{
    [TestFixture]
    public class EmailAndUrlTest
    {
        [TestCase("ouyh18@189.cn")]
        [TestCase("Ouyh18@gdtel.com.cn")]
        public void Test_CheckEmailByString_True(string source)
        {
            Assert.IsTrue(RegexService.CheckEmailByString(source));
        }

        [TestCase("#ouyh@189.cn")]
        [TestCase("ouyh18@gd.c")]
        public void Test_CheckEmailByString_False(string source)
        {
            Assert.IsFalse(RegexService.CheckEmailByString(source));
        }

        [TestCase("https://www.google.com.hk")]
        [TestCase("https://www.facebook.com")]
        [TestCase("http://219.128.254.41:2016")]
        public void Test_CheckURLByString_True(string source)
        {
            Assert.IsTrue(RegexService.CheckURLByString(source));
        }
    }
}
