using Abp.EntityFramework.AutoMapper;
using Abp.Reflection;
using Lte.Evaluations.Policy;
using NUnit.Framework;
using Shouldly;
using System.Collections.Generic;
using System.Linq;
using Lte.MySqlFramework.Entities;

namespace Lte.Evaluations.MapperService
{
    [TestFixture]
    public class PciCellPairListTest
    {
        private readonly ITypeFinder _typeFinder = new TypeFinder(new MyAssemblyFinder());

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            var module = new AbpAutoMapperModule(_typeFinder);
            module.PostInitialize();
        }

        [Test]
        public void Test_MapDistinct()
        {
            var originalList = new List<PciCell>
            {
                new PciCell {ENodebId = 1, SectorId = 2, Pci = 3},
                new PciCell {ENodebId = 1, SectorId = 2, Pci = 4},
                new PciCell {ENodebId = 2, SectorId = 2, Pci = 1},
                new PciCell {ENodebId = 3, SectorId = 2, Pci = 3},
                new PciCell {ENodebId = 1, SectorId = 2, Pci = 4},
                new PciCell {ENodebId = 3, SectorId = 2, Pci = 3},
                new PciCell {ENodebId = 1, SectorId = 4, Pci = 3},
                new PciCell {ENodebId = 2, SectorId = 4, Pci = 1},
            };
            var destList=originalList.MapTo<IEnumerable<PciCellPair>>().Distinct(new PciCellPairComparer()).ToList();
            destList.Count.ShouldBe(4);
            destList[0].ENodebId.ShouldBe(1);
            destList[0].Pci.ShouldBe((short)3);
            destList[1].Pci.ShouldBe((short)4);
            destList[2].ENodebId.ShouldBe(2);
            destList[3].ENodebId.ShouldBe(3);
        }
    }
}
