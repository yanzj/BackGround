using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Cdma;
using Abp.EntityFramework.Entities.Infrastructure;
using Abp.Reflection;
using Lte.Evaluations.MockItems;
using Lte.Evaluations.Policy;
using Lte.MySqlFramework.Abstract;
using Lte.MySqlFramework.Entities;
using Lte.Parameters.MockOperations;
using Moq;
using NUnit.Framework;

namespace Lte.Evaluations.DataService.Dump
{
    public abstract class CellDumpServiceTestBase
    {
        protected readonly Mock<IBtsRepository> BtsRepository = new Mock<IBtsRepository>();
        protected readonly Mock<ICellRepository> CellRepository = new Mock<ICellRepository>();
        protected CellDumpService Service;
        private readonly ITypeFinder _typeFinder = new TypeFinder(new MyAssemblyFinder());

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            Service = new CellDumpService(BtsRepository.Object, CellRepository.Object, null);
            BtsRepository.MockOperation();
            BtsRepository.MockGetId<IBtsRepository, CdmaBts>();
            BtsRepository.MockThreeBtss();
            CellRepository.MockRepositorySaveItems<Cell, ICellRepository>();
            var module = new AbpAutoMapperModule(_typeFinder);
            module.PostInitialize();
        }

    }
}
