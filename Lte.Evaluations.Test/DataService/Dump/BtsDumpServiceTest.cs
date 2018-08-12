using Abp.EntityFramework.AutoMapper;
using Abp.Reflection;
using Lte.Evaluations.DataService.Queries;
using Lte.Evaluations.MockItems;
using Lte.Evaluations.Policy;
using Lte.Parameters.MockOperations;
using Moq;
using NUnit.Framework;
using Shouldly;
using System.Collections.Generic;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Cdma;
using Abp.EntityFramework.Entities.Region;
using Lte.Domain.Excel;
using Lte.MySqlFramework.Abstract;
using Lte.MySqlFramework.Entities;

namespace Lte.Evaluations.DataService.Dump
{
    [TestFixture]
    public class BtsDumpServiceTest
    {
        private readonly Mock<IBtsRepository> _btsRepository = new Mock<IBtsRepository>();
        private readonly Mock<ITownRepository> _townRepository = new Mock<ITownRepository>();
        private BtsDumpService _service;
        private readonly ITypeFinder _typeFinder = new TypeFinder(new MyAssemblyFinder());


        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            var module = new AbpAutoMapperModule(_typeFinder);
            module.PostInitialize();
            _service = new BtsDumpService(_btsRepository.Object, _townRepository.Object);
            _btsRepository.MockOperation();
            _btsRepository.MockGetId<IBtsRepository, CdmaBts>();
            _btsRepository.MockRepositorySaveItems<CdmaBts, IBtsRepository>();
            _townRepository.MockGetId<ITownRepository, Town>();
            _townRepository.MockSixTowns();
        }

        [SetUp]
        public void Setup()
        {
            _btsRepository.MockThreeBtss();
        }

        [TestCase("abc", "ieowue", 1, 2, 112.998939,22.34284)]
        [TestCase("arebc", "ieo--wue", 3, 4, 113.98748, 23.5786)]
        public void Test_SingleItem(string name, string address, int townId, int btsId, double longtitute, double lattitute)
        {
            var infos = new List<BtsExcel>
            {
                new BtsExcel
                {
                    Name = name,
                    Address = address,
                    BtsId = btsId,
                    Longtitute = longtitute,
                    Lattitute = lattitute,
                    DistrictName = "district-" + townId,
                    TownName = "town-" + townId
                }
            };
            _service.DumpBtsExcels(infos);
            _btsRepository.Object.Count().ShouldBe(4);
            _btsRepository.Object.GetAllList()[3].ShouldBe(name, address, townId, btsId, longtitute, lattitute);
        }
    }
    
}
