using System;
using Lte.Domain.Common;
using Lte.Domain.Common.Types;
using Lte.Domain.Regular;
using NUnit.Framework;

namespace Lte.Domain.Test.Regular
{
    [TestFixture]
    public class ContainsStringTest
    {
        [TestCase("123", "123")]
        [TestCase("123x", "123")]
        [TestCase("12.3x", "12")]
        [TestCase("x123", "123")]
        [TestCase("12x3456", "12")]
        [TestCase("abcd123x456", "123")]
        public void Test_GetFirstNumber(string source, string number)
        {
            Assert.AreEqual(RegexService.GetFirstNumberByString(source), number);
        }

        [TestCase("12", "12")]
        [TestCase("a456", "456")]
        [TestCase("12x34a56", "56")]
        public void Test_GetLastNumber(string source, string number)
        {
            Assert.AreEqual(RegexService.GetLastNumberByString(source), number);
        }

        [TestCase("123", 3)]
        [TestCase("493745723", 9)]
        public void Test_CheckLength(string source, int length)
        {
            Assert.IsTrue(RegexService.CheckLengthByString(source, length));
        }

        [TestCase("maaat", "m", "t", "maaa")]
        public void Test_Substring(string source, string startStr, string endStr, string result)
        {
            Assert.AreEqual(RegexService.Substring(source, startStr, endStr), result);
        }

        [Test]
        public void Test_GetBrackets()
        {
            var source = "[门口东边100米处]此次活动为佛山市南海区政府组织的一次大型文化活动，是宣传天翼品牌的重要场合。";
            var dest = source.GetSplittedFields(new[] {'[', ']'})[0];
            Assert.AreEqual(dest, "门口东边100米处");
        }

        [TestCase("A、逻辑信道与传输信道之间的映射  B、RLC协议数据单元的复用与解复用  C、根据传输块（TB）大小进行动态分段  D、同一个UE不同逻辑信道之间的优先级管理", "逻辑信道与传输信道之间的映射")]
        [TestCase("A:1;B:2;C:3;D:4", "1")]
        [TestCase("A. 包含PS与CS业务  ;B.仅包含CS业务 ;C.不确定", "包含PS与CS业务")]
        [TestCase(";A、PAPR问题;B、时间和频率同步;C、多小区多址", "PAPR问题")]
        [TestCase("1、0；2、-1.77；3、-3；-4.77", "1、0")]
        [TestCase("1.A0;2.A1;3.A2;4.B;5.C", "1.A0")]
        public void Test_Split(string problem, string part1)
        {
            var fields = problem.Split(new[] { "A.", "A）", ";", "A、", " ", "A:",
                "B.", "B）", "B、", "B:", "；",
                "C.", "C）", "C、", "C:",
                "D.", "D）", "D、", "D:",
                "E.", "E）", "E、", "E:",
                "F）", "F.", "F、", "F:"
            },
                StringSplitOptions.RemoveEmptyEntries);
            Assert.AreEqual(fields[0], part1);
        }
    }
}
