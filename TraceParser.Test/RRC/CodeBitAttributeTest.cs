using Lte.Domain.Common;
using Lte.Domain.Common.Types;
using Moq;
using NUnit.Framework;
using TraceParser.Common;

namespace TraceParser.Test.RRC
{
    public class Foo
    {
        [CodeBit(Position = 2, BitToBeRead = 55)]
        public string Field1;

        [CodeBit(Position = 5, BitToBeRead = 67)]
        public string Field2;

        [CodeBit(Position = 7, BitToBeRead = 33)]
        public string Field3;
    }

    public class Bar
    {
        [CodeBit(Position = 2, BitToBeRead = 55)]
        public string Field1 { get; set; }

        [CodeBit(Position = 5, BitToBeRead = 67)]
        public string Field2 { get; set; }

        [CodeBit(Position = 7, BitToBeRead = 33)]
        public string Field3 { get; set; }
    }

    [TestFixture]
    public class CodeBitAttributeTest
    {
        private readonly Mock<IBitArrayReader> _reader = new Mock<IBitArrayReader>();

        [OneTimeSetUp]
        public void TestFixtureSetup()
        {
            _reader.Setup(x => x.ReadBitString(It.IsAny<int>())).Returns<int>(position => "code" + position.ToString());
        }

        [Test]
        public void Test_Field()
        {
            var foo = new Foo();
            foo.ReadCodeBits(_reader.Object, 2);
            Assert.AreEqual(foo.Field1, "code55");
            foo.ReadCodeBits(_reader.Object, 5);
            Assert.AreEqual(foo.Field2, "code67");
            foo.ReadCodeBits(_reader.Object, 7);
            Assert.AreEqual(foo.Field3, "code33");
        }

        [Test]
        public void Test_Property()
        {
            var bar = new Bar();
            bar.ReadCodeBits(_reader.Object, 2);
            Assert.AreEqual(bar.Field1, "code55");
            bar.ReadCodeBits(_reader.Object, 5);
            Assert.AreEqual(bar.Field2, "code67");
            bar.ReadCodeBits(_reader.Object, 7);
            Assert.AreEqual(bar.Field3, "code33");
        }
    }
}
