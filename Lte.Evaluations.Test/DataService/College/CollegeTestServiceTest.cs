using Abp.EntityFramework.AutoMapper;
using Abp.Reflection;
using Lte.Evaluations.MockItems;
using Lte.Evaluations.Policy;
using Lte.MySqlFramework.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Linq;
using Abp.EntityFramework.Entities;

namespace Lte.Evaluations.DataService.College
{
    [TestFixture]
    public class College3GTestServiceTest
    {
        private AbpAutoMapperModule _module;
        private TypeFinder _typeFinder;
        private readonly Mock<ICollege3GTestRepository> _repository = new Mock<ICollege3GTestRepository>();
        private readonly Mock<ICollegeRepository> _collegeRepository = new Mock<ICollegeRepository>();
        private College3GTestService _service;

        [TestFixtureSetUp]
        public void TfSetup()
        {
            _typeFinder = new TypeFinder(new MyAssemblyFinder());
            _module = new AbpAutoMapperModule(_typeFinder);
            _module.PostInitialize();
            _service = new College3GTestService(_repository.Object, _collegeRepository.Object);
            _collegeRepository.MockThreeColleges();
            _collegeRepository.MockOpertions();
            _collegeRepository.MockGetId<ICollegeRepository, CollegeInfo>();
            _repository.MockOperations();
        }
        
        [Test]
        public void Test_MockValues()
        {
            Assert.IsNotNull(_collegeRepository.Object.GetAll().FirstOrDefault(x => x.Id == 1));
            Assert.IsNotNull(_collegeRepository.Object.Get(1));
            Assert.IsNotNull(_collegeRepository.Object.Get(2));
            Assert.IsNotNull(_collegeRepository.Object.Get(3));
            Assert.IsNotNull(_collegeRepository.Object.GetByName("college-1"));
            Assert.IsNotNull(_collegeRepository.Object.GetByName("college-2"));
            Assert.IsNotNull(_collegeRepository.Object.GetByName("college-3"));
        }
        
        [TestCase(1, new[] { 1, 2 }, new[] { "2015-10-10", "2015-4-7" }, 4, new[] { 15, 11.0 }, "2015-4-1", "2015-10-11", 2)]
        [TestCase(2, new[] { 2, 3 }, new[] { "2015-10-10", "2015-4-7" }, 4, new[] { 15.0, 11 }, "2015-5-2", "2015-10-12", 1)]
        [TestCase(3, new[] { 1, 2, 3 }, new[] { "2015-10-10", "2015-4-7", "2015-9-9" }, 4, new[] { 15, 11.0, 18 }, "2015-3-1", "2015-10-17", 3)]
        [TestCase(4, new[] { 2, 3, 1 }, new[] { "2015-10-10", "2015-4-7", "2015-7-8" }, 9, new[] { 14, 11.2, 88 }, "2015-7-3", "2015-9-8", 1)]
        [TestCase(5, new[] { 1, 2, 3, 2 }, new[] { "2015-10-10", "2015-4-7", "2015-9-9", "2015-4-17" }, 
            4, new[] { 15, 11.0, 18, 13 }, "2015-3-1", "2015-10-17", 3)]
        public void Test_GetAverageRates(int testNo,
            int[] collegeIds, string[] testDates, int hour, double[] rates,
            string begin, string end, int resultNum)
        {
            _repository.MockRateItems(collegeIds, testDates.Select(x => DateTime.Parse(x).AddHours(hour)).ToArray(), rates);

            var result = _service.GetAverageRates(DateTime.Parse(begin), DateTime.Parse(end));
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count, resultNum);
            for (var i = 0; i < result.Count; i++)
            {
                Console.WriteLine("Average Rate[{0}]: {1}", result.ElementAt(i).Key, result[result.ElementAt(i).Key]);
            }
        }
    }

    [TestFixture]
    public class College4GTestServiceTest
    {
        private AbpAutoMapperModule _module;
        private TypeFinder _typeFinder;
        private readonly Mock<ICollege4GTestRepository> _repository = new Mock<ICollege4GTestRepository>();
        private readonly Mock<ICollegeRepository> _collegeRepository = new Mock<ICollegeRepository>();
        private readonly Mock<IENodebRepository> _eNodebRepository = new Mock<IENodebRepository>();
        private readonly Mock<ICellRepository> _cellRepository = new Mock<ICellRepository>();
        private College4GTestService _service;

        [TestFixtureSetUp]
        public void TfSetup()
        {
            _typeFinder = new TypeFinder(new MyAssemblyFinder());
            _module = new AbpAutoMapperModule(_typeFinder);
            _module.PostInitialize();
            _service = new College4GTestService(_repository.Object, _collegeRepository.Object,
                _eNodebRepository.Object, _cellRepository.Object);
            _collegeRepository.MockThreeColleges();
            _collegeRepository.Setup(x => x.Get(It.IsAny<int>())).Returns<int>(
                id => new CollegeInfo
                {
                    Id = id,
                    Name = "college-1"
                });
        }

        [Test]
        public void Test_MockValues()
        {
            Assert.IsNotNull(_collegeRepository.Object.GetAll().FirstOrDefault(x => x.Id == 1));
            Assert.IsNotNull(_collegeRepository.Object.Get(1)); ;
        }
    }
}
