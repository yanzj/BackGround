using Abp.EntityFramework.AutoMapper;
using Abp.Reflection;
using Lte.Evaluations.DataService.Basic;
using Lte.Evaluations.MockItems;
using Lte.Evaluations.Policy;
using Lte.Evaluations.ViewModels.Precise;
using Lte.MySqlFramework.Abstract;
using Lte.MySqlFramework.Entities;
using Lte.Parameters.Entities.Basic;
using Lte.Parameters.MockOperations;
using Moq;
using NUnit.Framework;
using Shouldly;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.EntityFramework.Entities;

namespace Lte.Evaluations.DataService.Mr
{
    [TestFixture]
    public class PreciseWorkItemTest
    {
        private readonly Mock<IPreciseWorkItemCellRepository> _repository = new Mock<IPreciseWorkItemCellRepository>();
        private readonly Mock<ICellPowerService> _powerRepository = new Mock<ICellPowerService>();
        private readonly Mock<ICellRepository> _cellRepository = new Mock<ICellRepository>();

        private readonly ITypeFinder _typeFinder = new TypeFinder(new MyAssemblyFinder());

        private PreciseWorkItemService _serivice;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _repository.MockOperation();
            _repository.MockRepositorySaveItems<PreciseWorkItemCell, IPreciseWorkItemCellRepository>();
            _repository.MockRepositoryUpdateWorkItemCell<PreciseWorkItemCell, IPreciseWorkItemCellRepository>();
            _repository.MockQueryItems(new List<PreciseWorkItemCell>
            {
                new PreciseWorkItemCell
                {
                    ENodebId = 1,
                    SectorId = 2,
                    WorkItemNumber = "007"
                }
            }.AsQueryable());
            _powerRepository.Setup(x => x.Query(It.IsAny<int>(), It.IsAny<byte>())).Returns<int, byte>(null);
            _powerRepository.Setup(x => x.Query(1, 2)).Returns(new CellPower
            {
                ENodebId = 1,
                SectorId = 2,
                RsPower = 15.2
            });
            _powerRepository.Setup(x => x.Query(1, 2)).Returns(new CellPower
            {
                ENodebId = 3,
                SectorId = 4,
                RsPower = 16.2
            });
            _cellRepository.MockOperations();
            _cellRepository.MockQueryItems(new List<Cell>
            {
                new Cell
                {
                    ENodebId = 1,
                    SectorId = 2,
                    MTilt = 0.1,
                    ETilt = 3
                },
                new Cell
                {
                    ENodebId = 3,
                    SectorId = 4,
                    MTilt = 5.1,
                    ETilt = 2
                }
            }.AsQueryable());

            _serivice = new PreciseWorkItemService(_repository.Object, _powerRepository.Object, _cellRepository.Object);

            var module = new AbpAutoMapperModule(_typeFinder);
            module.PostInitialize();

        }

        [Test]
        public void Test_GetBySectorId()
        {
            var cell = _cellRepository.Object.GetBySectorId(1, 2);
            cell.ShouldNotBeNull();
            cell = _cellRepository.Object.GetBySectorId(3, 4);
            cell.ShouldNotBeNull();
        }

        [Test]
        public void Test_OriginLength()
        {
            _repository.Object.Get("007", 1, 2).ShouldNotBeNull();
            _repository.Object.Get("007", 2, 2).ShouldBeNull();
        }

        [Test]
        public void Test_InsertNewItem()
        {
            _repository.Object.Insert(new PreciseWorkItemCell
            {
                ENodebId = 3,
                SectorId = 4,
                WorkItemNumber = "007",
                Mod3Share = 2.4
            });
            var item = _repository.Object.Get("007", 3, 4);
            item.ShouldNotBeNull();
            item.ENodebId.ShouldBe(3);
            item.SectorId.ShouldBe((byte)4);
            item.Mod3Share.ShouldBe(2.4);
        }

        [Test]
        public void Test_InsertNewItemAsync()
        {
            _repository.Object.InsertAsync(new PreciseWorkItemCell
            {
                ENodebId = 3,
                SectorId = 4,
                WorkItemNumber = "007",
                Mod3Share = 2.4
            });
            var item = _repository.Object.Get("007", 3, 4);
            item.ShouldNotBeNull();
            item.ENodebId.ShouldBe(3);
            item.SectorId.ShouldBe((byte) 4);
            item.Mod3Share.ShouldBe(2.4);
        }

        [Test]
        public void Test_UpdateExistedItem()
        {
            _repository.Object.Update(new PreciseWorkItemCell
            {
                ENodebId = 1,
                SectorId = 2,
                WorkItemNumber = "007",
                Mod3Share = 2.7
            });
            var item = _repository.Object.Get("007", 1, 2);
            item.ShouldNotBeNull();
            item.ENodebId.ShouldBe(1);
            item.SectorId.ShouldBe((byte) 2);
            item.Mod3Share.ShouldBe(2.7);
        }

        [Test]
        public void Test_UpdateExistedItemAsync()
        {
            _repository.Object.UpdateAsync(new PreciseWorkItemCell
            {
                ENodebId = 1,
                SectorId = 2,
                WorkItemNumber = "007",
                Mod3Share = 2.7
            });
            var item = _repository.Object.Get("007", 1, 2);
            item.ShouldNotBeNull();
            item.ENodebId.ShouldBe(1);
            item.SectorId.ShouldBe((byte) 2);
            item.Mod3Share.ShouldBe(2.7);
        }

        [Test]
        public void Test_Update_PreciseInterferenceNeighbor()
        {
            _serivice.Update(new PreciseInterferenceNeighborsContainer
            {
                WorkItemNumber = "007",
                Items = new List<PreciseInterferenceNeighborDto>
                {
                    new PreciseInterferenceNeighborDto
                    {
                        ENodebId = 1,
                        SectorId = 2,
                        Db10Share = 1.1,
                        Db6Share = 1.2,
                        Mod3Share = 2.1,
                        Mod6Share = 2.2
                    }
                }
            });
            var item = _serivice.Query("007", 1, 2);
            item.ShouldNotBeNull();
        }

        [Test]
        public async Task Test_Update_PreciseInterferenceNeighborAsync()
        {
            await _serivice.UpdateAsync(new PreciseInterferenceNeighborsContainer
            {
                WorkItemNumber = "007",
                Items = new List<PreciseInterferenceNeighborDto>
                {
                    new PreciseInterferenceNeighborDto
                    {
                        ENodebId = 1,
                        SectorId = 2,
                        Db10Share = 1.1,
                        Db6Share = 1.2,
                        Mod3Share = 2.1,
                        Mod6Share = 2.2
                    }
                }
            });
            var item = _serivice.Query("007", 1, 2);
            item.ShouldNotBeNull();
            item.Db10Share.ShouldBe(1.1);
            item.Db6Share.ShouldBe(1.2);
            item.Mod3Share.ShouldBe(2.1);
            item.Mod6Share.ShouldBe(2.2);
        }

        [Test]
        public void Test_Insert_PreciseInterferenceNeighbor_Prepare()
        {
            var container = new PreciseInterferenceNeighborsContainer
            {
                WorkItemNumber = "007",
                Items = new List<PreciseInterferenceNeighborDto>
                {
                    new PreciseInterferenceNeighborDto
                    {
                        ENodebId = 3,
                        SectorId = 4,
                        Db10Share = 3.1,
                        Db6Share = 3.2,
                        Mod3Share = 4.1,
                        Mod6Share = 4.2
                    }
                }
            };
            var neighbor = container.Items[0];
            var item = _repository.Object.Get(container.WorkItemNumber, neighbor.ENodebId, neighbor.SectorId);
            item.ShouldBeNull();
            item = neighbor.MapTo<PreciseWorkItemCell>();
            item.ShouldNotBeNull();
            item.ENodebId.ShouldBe(3);
            item.SectorId.ShouldBe((byte) 4);
            item.Db10Share.ShouldBe(3.1);
            item.Db6Share.ShouldBe(3.2);
            item.Mod3Share.ShouldBe(4.1);
            item.Mod6Share.ShouldBe(4.2);
            item.WorkItemNumber = container.WorkItemNumber;
            _repository.Object.Insert(item);
            var items = _repository.Object.GetAll();
            items.Count().ShouldBe(2);
            items.ElementAt(1).ENodebId.ShouldBe(3);
            items.ElementAt(1).SectorId.ShouldBe((byte) 4);
            items.ElementAt(1).WorkItemNumber.ShouldBe("007");
            item = _repository.Object.Get("007", 3, 4);
            item.ShouldNotBeNull();
            item.ENodebId.ShouldBe(3);
            item.SectorId.ShouldBe((byte)4);
            item.Db10Share.ShouldBe(3.1);
            item.Db6Share.ShouldBe(3.2);
            item.Mod3Share.ShouldBe(4.1);
            item.Mod6Share.ShouldBe(4.2);
        }

        [Test]
        public void Test_Insert_PreciseInterferenceNeighbor()
        {
            var container = new PreciseInterferenceNeighborsContainer
            {
                WorkItemNumber = "007",
                Items = new List<PreciseInterferenceNeighborDto>
                {
                    new PreciseInterferenceNeighborDto
                    {
                        ENodebId = 3,
                        SectorId = 4,
                        Db10Share = 3.1,
                        Db6Share = 3.2,
                        Mod3Share = 4.1,
                        Mod6Share = 4.2
                    }
                }
            };
            _serivice.Update(container);
            var item = _serivice.Query("007", 3, 4);
            item.ShouldNotBeNull();
            item.ENodebId.ShouldBe(3);
            item.SectorId.ShouldBe((byte)4);
            item.Db10Share.ShouldBe(3.1);
            item.Db6Share.ShouldBe(3.2);
            item.Mod3Share.ShouldBe(4.1);
            item.Mod6Share.ShouldBe(4.2);
            item.OriginalDownTilt.ShouldBe(7.1);
        }

        [Test]
        public async Task Test_Insert_PreciseInterferenceNeighbor_Async()
        {
            var container = new PreciseInterferenceNeighborsContainer
            {
                WorkItemNumber = "007",
                Items = new List<PreciseInterferenceNeighborDto>
                {
                    new PreciseInterferenceNeighborDto
                    {
                        ENodebId = 3,
                        SectorId = 4,
                        Db10Share = 3.1,
                        Db6Share = 3.2,
                        Mod3Share = 4.1,
                        Mod6Share = 4.2
                    }
                }
            };
            await _serivice.UpdateAsync(container);
            var item = _serivice.Query("007", 3, 4);
            item.ShouldNotBeNull();
            item.ENodebId.ShouldBe(3);
            item.SectorId.ShouldBe((byte)4);
            item.Db10Share.ShouldBe(3.1);
            item.Db6Share.ShouldBe(3.2);
            item.Mod3Share.ShouldBe(4.1);
            item.Mod6Share.ShouldBe(4.2);
            item.OriginalDownTilt.ShouldBe(7.1);
        }

        [Test]
        public void Test_Insert_PreciseInterFerenceVictim()
        {
            var container = new PreciseInterferenceVictimsContainer
            {
                WorkItemNumber = "007",
                Items = new List<PreciseInterferenceVictimDto>
                {
                    new PreciseInterferenceVictimDto
                    {
                        ENodebId = 3,
                        SectorId = 4,
                        BackwardDb10Share = 3.3,
                        BackwardDb6Share = 3.4,
                        BackwardMod3Share = 4.3,
                        BackwardMod6Share =4.4
                    }
                }
            };
            _serivice.Update(container);
            var item = _serivice.Query("007", 3, 4);
            item.ShouldNotBeNull();
        }

        [Test]
        public async Task Test_Insert_PreciseInterFerenceVictim_Async()
        {
            var container = new PreciseInterferenceVictimsContainer
            {
                WorkItemNumber = "007",
                Items = new List<PreciseInterferenceVictimDto>
                {
                    new PreciseInterferenceVictimDto
                    {
                        ENodebId = 3,
                        SectorId = 4,
                        BackwardDb10Share = 3.3,
                        BackwardDb6Share = 3.4,
                        BackwardMod3Share = 4.3,
                        BackwardMod6Share =4.4
                    }
                }
            };
            await _serivice.UpdateAsync(container);
            var item = _serivice.Query("007", 3, 4);
            item.ShouldNotBeNull();
        }
    }
}
