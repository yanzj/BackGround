using Abp.EntityFramework.AutoMapper;
using Abp.Reflection;
using Lte.Domain.Regular;
using Lte.Evaluations.DataService.Queries;
using Lte.Evaluations.MockItems;
using Lte.Evaluations.Policy;
using Lte.Parameters.MockOperations;
using Moq;
using NUnit.Framework;
using Shouldly;
using System.Collections.Generic;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Infrastructure;
using Abp.EntityFramework.Entities.Region;
using Lte.Domain.Common;
using Lte.Domain.Excel;
using Lte.MySqlFramework.Abstract;
using Lte.MySqlFramework.Entities;

namespace Lte.Evaluations.DataService.Dump
{
    [TestFixture]
    public class ENodebDumpServiceTest
    {
        private readonly Mock<IENodebRepository> _eNodebRepository = new Mock<IENodebRepository>();
        private readonly Mock<ITownRepository> _townRepository = new Mock<ITownRepository>();
        private ENodebDumpService _service;
        private readonly ITypeFinder _typeFinder = new TypeFinder(new MyAssemblyFinder());


        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            var module = new AbpAutoMapperModule(_typeFinder);
            module.PostInitialize();
            _service = new ENodebDumpService(_eNodebRepository.Object, _townRepository.Object);
            _eNodebRepository.MockOperations();
            _eNodebRepository.MockGetId<IENodebRepository, ENodeb>();
            _eNodebRepository.MockRepositorySaveItems<ENodeb, IENodebRepository>();
            _townRepository.MockGetId<ITownRepository, Town>();
            _townRepository.MockSixTowns();
        }

        [SetUp]
        public void Setup()
        {
            _eNodebRepository.MockThreeENodebs();
        }

        [TestCase("abc", "ieowue", 1, 2, "10.17.165.0", "10.17.165.100", true)]
        [TestCase("arebc", "ieo--wue", 3, 4, "219.128.254.0", "219.128.254.41", false)]
        public void Test_SingleItem(string name, string address, int townId, int eNodebId, string gatewayAddress,
            string ipAddress, bool existed)
        {
            var infos = new List<ENodebExcel>
            {
                new ENodebExcel
                {
                    Name = name,
                    Address = address,
                    ENodebId = eNodebId,
                    Ip = new IpAddress(ipAddress),
                    GatewayIp = new IpAddress(gatewayAddress),
                    CityName = "city-" + townId,
                    DistrictName = "district-" + townId,
                    TownName = "town-" + townId
                }
            };
            _service.DumpNewEnodebExcels(infos);
            if (existed)
            {
                _eNodebRepository.Object.Count().ShouldBe(3);
            }
            else
            {
                _eNodebRepository.Object.Count().ShouldBe(4);
                _eNodebRepository.Object.GetAllList()[3].ShouldBe(name,address,townId,eNodebId,gatewayAddress,ipAddress);
            }
            
        }
    }
}
