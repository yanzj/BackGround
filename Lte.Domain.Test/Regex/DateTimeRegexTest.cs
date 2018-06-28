using System;
using Lte.Domain.Regular;
using NUnit.Framework;

namespace Lte.Domain.Test.Regex
{
    [TestFixture]
    public class DateTimeRegexTest
    {
        [TestCase("2015-11-4")]
        [TestCase("8888-12-27")]
        [TestCase("1999-12-33")]
        public void TestCheckDateByStringTrue(string source)
        {
            Assert.IsTrue(source.CheckDateByString());
        }

        [TestCase("1234-13-7")]
        [TestCase("11121-8-8")]
        public void Test_CheckDateByString_False(string source)
        {
            Assert.IsFalse(source.CheckDateByString());
        }

        [TestCase("a2015-11-4", "2015-11-4")]
        [TestCase("18888-12-274", "8888-12-2")]
        [TestCase("1999-12-33", "1999-12-3")]
        [TestCase("佛山2018-05-01扇区统计表", "2018-05-01")]
        public void Test_GetFirstDateByString(string source, string result)
        {
            Assert.AreEqual(source.GetFirstDateByString(), result);
        }
        
        [TestCase("17-07-19 11:04:06.507", "2017-7-19 11:04:06.507")]
        [TestCase("2017-07-02 15:00:49.140", "2017-7-2 15:00:49.140")]
        public void Test_ConvertToDateTime(string source, string dest)
        {
            Assert.AreEqual(source.ConvertToDateTime(DateTime.Now).ToString("yyyy-M-d HH:mm:ss.fff"), dest);
        }

        [TestCase("2012-12-12", "2012-12-12")]
        [TestCase("2011-11-1", "")]
        public void Test_GetStrictDateByString(string source, string result)
        {
            Assert.AreEqual(source.GetStrictDateByString(), result);
        }

        [TestCase("20160111121300", "20160111121300")]
        [TestCase("20160111001300", "20160111001300")]
        public void Test_GetPersistentDateTimeString(string source, string result)
        {
            Assert.AreEqual(source.GetPersistentDateTimeString(), result);
        }

        [TestCase("20160111", "20160111")]
        [TestCase("20160111", "20160111")]
        [TestCase("20160111_223", "20160111")]
        public void Test_GetPersistentDateString(string source, string result)
        {
            Assert.AreEqual(source.GetPersistentDateString(), result);
        }

        [TestCase("20160110221500小区干扰矩阵.txt", "2016/1/10 22:15:00")]
        public void Test_GetDateTimeFromFileName(string source, string result)
        {
            Assert.AreEqual(source.GetDateTimeFromFileName().ToString(), result);
        }

        [TestCase("20160110221500小区干扰矩阵.txt", "2016/1/10 0:00:00")]
        [TestCase("20180307_757_499712_1", "2018/3/7 0:00:00")]
        public void Test_GetDateFromFileName(string source, string result)
        {
            Assert.AreEqual(source.GetDateFromFileName().ToString(), result);
        }
    }
}
