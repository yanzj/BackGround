using Abp.EntityFramework.AutoMapper;
using Abp.Reflection;
using Lte.Evaluations.MockItems;
using Lte.Evaluations.Policy;
using Moq;
using NUnit.Framework;
using System.Linq;
using Abp.EntityFramework.Entities.College;
using Lte.MySqlFramework.Abstract.College;
using Lte.MySqlFramework.Abstract.Infrastructure;
using Lte.MySqlFramework.Abstract.Test;

namespace Lte.Evaluations.DataService.College
{
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
