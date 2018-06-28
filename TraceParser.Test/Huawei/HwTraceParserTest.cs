using NUnit.Framework;
using System;
using System.Diagnostics;
using System.IO;
using TraceParser.Huawei;

namespace TraceParser.Test.Huawei
{
    [TestFixture]
    public class HwTraceParserTest
    {
        [TestCase("B20150925.204044+0800-eNodeB.北滘碧江中学.49793", 10199)]
        [TestCase("B20150925.204044+0800-eNodeB.北滘新城区西.49789", 10404)]
        [TestCase("B20150925.204044+0800-eNodeB.大良南区电信LBBU10.49626", 116)]
        public void TestMethod(string fileName, int length)
        {
            var stopwatch = Stopwatch.StartNew();
            var parser = new HwTraceFileParser();
            var zipDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Huawei");
            var zipPath = Path.Combine(zipDir, fileName + ".gz");
            var stream = HwTraceFileParser.UnzipToMemoryStream(zipPath);
            stream.Position = 0L;
            parser.Parse(stream);
            Assert.AreEqual(parser.Lstmsg.Count, length);
            Console.WriteLine("avg {0}ms", stopwatch.ElapsedMilliseconds/1000f);
        }

    }
}
