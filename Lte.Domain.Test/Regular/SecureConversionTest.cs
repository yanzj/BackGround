using System;
using Lte.Domain.Regular;
using Lte.Domain.Regular.Attributes;
using NUnit.Framework;

namespace Lte.Domain.Test.Regular
{
    [TestFixture]
    public class SecureConversionTest
    {
        [TestCase("1", 1)]
        [TestCase("123,456", 123456)]
        public void Test_ToInt(string str, int result)
        {
            var actual = str.Replace(",","").ConvertToInt(0);
            Assert.AreEqual(actual, result);
        }

        [TestCase("1.23", 1.23)]
        [TestCase("123,456.789", 123456.789)]
        public void Test_ToDouble(string str, double result)
        {
            var actual = str.Replace(",", "").ConvertToDouble(0);
            Assert.AreEqual(actual, result);
        }
    }

    [TestFixture]
    public class ArraySumTest
    {
        public class MyClass
        {
            public string A { get; set; }

            public DateTime Time { get; set; }

            [ArraySumProtection]
            public int C { get; set; }

            public double D { get; set; }

            public int E { get; set; }
        }

        [Test]
        public void Test_Sum()
        {
            var source = new MyClass[]
            {
                new MyClass
                {
                    A = "sa",
                    C = 11,
                    D = 22,
                    E = 22,
                    Time = new DateTime(2011, 1, 1)
                },
                new MyClass
                {
                    A = "sb",
                    C = 12,
                    D = 22,
                    E = 33,
                    Time = new DateTime(2012, 1, 2)
                }
            };
            var sum = source.ArraySum();
            Assert.AreEqual(sum.A,"sa");
            Assert.AreEqual(sum.C,11);
            Assert.AreEqual(sum.D,44);
            Assert.AreEqual(sum.E,55);
            Assert.AreEqual(sum.Time,new DateTime(2011,1,1));
            var average = source.Average();
            Assert.AreEqual(average.A, "sa");
            Assert.AreEqual(average.C, 11);
            Assert.AreEqual(average.D, 22);
            Assert.AreEqual(average.E, 27);
            Assert.AreEqual(average.Time, new DateTime(2011, 1, 1));
        }
    }
}
