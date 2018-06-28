using Lte.Parameters.MockOperations;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Abp.EntityFramework.Entities;
using Lte.Domain.Common.Wireless;
using Lte.MySqlFramework.Abstract;
using Lte.MySqlFramework.Entities;

namespace Lte.Evaluations.MockItems
{
    public static class MockInstanceService
    {
        public static void MockThreeBtss(this Mock<IBtsRepository> repository)
        {
            repository.MockQueryItems(new List<CdmaBts>
            {
                new CdmaBts {Id = 1, BtsId = 1, Name = "Bts-1", Address = "Address-1", TownId = 1 },
                new CdmaBts {Id = 2, BtsId = 2, Name = "Bts-2", Address = "Address-2", TownId = 2 },
                new CdmaBts {Id = 3, BtsId = 3, Name = "Bts-3", Address = "Address-3", TownId = 3 }
            }.AsQueryable());
        }

        public static void MockThreeBtss(this Mock<IBtsRepository> repository, int[] townIds)
        {
            repository.MockQueryItems(new List<CdmaBts>
            {
                new CdmaBts {Id = 1, BtsId = 1, Name = "Bts-1", TownId = townIds[0] },
                new CdmaBts {Id = 2, BtsId = 2, Name = "Bts-2", TownId = townIds[1] },
                new CdmaBts {Id = 3, BtsId = 3, Name = "Bts-3", TownId = townIds[2] }
            }.AsQueryable());
        }

        public static void MockSixBtssWithENodebId(this Mock<IBtsRepository> repository)
        {
            repository.MockQueryItems(new List<CdmaBts>
            {
                new CdmaBts {Id = 1, BtsId = 1, Name = "Bts-1", ENodebId = 1},
                new CdmaBts {Id = 2, BtsId = 2, Name = "Bts-2", ENodebId = 2},
                new CdmaBts {Id = 3, BtsId = 3, Name = "Bts-3", ENodebId = 3},
                new CdmaBts {Id = 4, BtsId = 4, Name = "Bts-4", ENodebId = 4},
                new CdmaBts {Id = 5, BtsId = 5, Name = "Bts-5", ENodebId = 5},
                new CdmaBts {Id = 6, BtsId = 6, Name = "Bts-6", ENodebId = 6}
            }.AsQueryable());
        }

        public static void MockSixCells(this Mock<ICdmaCellRepository> repository, double lon = 113.01, double lat = 23.01)
        {
            repository.MockQueryItems(new List<CdmaCell>
            {
                new CdmaCell
                {
                    Id = 1,
                    BtsId = 1,
                    SectorId = 1,
                    RsPower = 1.1,
                    Height = 20,
                    IsOutdoor = true,
                    MTilt = 1.1,
                    ETilt = 2.2,
                    Azimuth = 30,
                    Longtitute = lon,
                    Lattitute = lat
                },
                new CdmaCell
                {
                    Id = 2,
                    BtsId = 2,
                    SectorId = 1,
                    RsPower = 1.1,
                    Height = 20,
                    IsOutdoor = true,
                    MTilt = 1.1,
                    ETilt = 2.2,
                    Azimuth = 60,
                    Longtitute = lon,
                    Lattitute = lat
                },
                new CdmaCell
                {
                    Id = 3,
                    BtsId = 2,
                    SectorId = 2,
                    RsPower = 1.1,
                    Height = 20,
                    IsOutdoor = true,
                    MTilt = 1.1,
                    ETilt = 2.2,
                    Azimuth = 90,
                    Longtitute = lon,
                    Lattitute = lat
                },
                new CdmaCell
                {
                    Id = 4,
                    BtsId = 3,
                    SectorId = 1,
                    RsPower = 1.1,
                    Height = 20,
                    IsOutdoor = true,
                    MTilt = 1.1,
                    ETilt = 2.2,
                    Azimuth = 150,
                    Longtitute = lon,
                    Lattitute = lat
                },
                new CdmaCell
                {
                    Id = 5,
                    BtsId = 3,
                    SectorId = 2,
                    RsPower = 1.1,
                    Height = 20,
                    IsOutdoor = true,
                    MTilt = 1.1,
                    ETilt = 2.2,
                    Azimuth = 210,
                    Longtitute = lon,
                    Lattitute = lat
                },
                new CdmaCell
                {
                    Id = 6,
                    BtsId = 3,
                    SectorId = 3,
                    RsPower = 1.1,
                    Height = 20,
                    IsOutdoor = true,
                    MTilt = 1.1,
                    ETilt = 2.2,
                    Azimuth = 270,
                    Longtitute = lon,
                    Lattitute = lat
                }
            }.AsQueryable());
        }

        public static void MockSixCells(this Mock<ICellRepository> repository, double lon = 113.01, double lat = 23.01,
            short pci = 111, bool isInUse = true)
        {
            repository.MockQueryItems(GenerateCells(lon, lat, pci, isInUse).AsQueryable());
        }

        public static void MockRangeCells(this Mock<ICellRepository> repository)
        {
            repository.Setup(
                x => x.GetAllList(It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>(), It.IsAny<double>())).Returns(GenerateCells(113, 23, 111));
        }

        private static List<Cell> GenerateCells(double lon, double lat, short pci, bool isInUse = true)
        {
            return new List<Cell>
            {
                new Cell
                {
                    Id = 1,
                    ENodebId = 1,
                    SectorId = 1,
                    RsPower = 1.1,
                    Height = 20,
                    IsOutdoor = true,
                    MTilt = 1.1,
                    ETilt = 2.2,
                    Azimuth = 30,
                    Longtitute = lon,
                    Lattitute = lat,
                    Pci = pci,
                    IsInUse = isInUse
                },
                new Cell
                {
                    Id = 2,
                    ENodebId = 2,
                    SectorId = 1,
                    RsPower = 1.1,
                    Height = 20,
                    IsOutdoor = true,
                    MTilt = 1.1,
                    ETilt = 2.2,
                    Azimuth = 60,
                    Longtitute = lon,
                    Lattitute = lat,
                    Pci = pci,
                    IsInUse = isInUse
                },
                new Cell
                {
                    Id = 3,
                    ENodebId = 2,
                    SectorId = 2,
                    RsPower = 1.1,
                    Height = 20,
                    IsOutdoor = true,
                    MTilt = 1.1,
                    ETilt = 2.2,
                    Azimuth = 90,
                    Longtitute = lon,
                    Lattitute = lat,
                    Pci = pci,
                    IsInUse = isInUse
                },
                new Cell
                {
                    Id = 4,
                    ENodebId = 3,
                    SectorId = 1,
                    RsPower = 1.1,
                    Height = 20,
                    IsOutdoor = true,
                    MTilt = 1.1,
                    ETilt = 2.2,
                    Azimuth = 150,
                    Longtitute = lon,
                    Lattitute = lat,
                    Pci = pci,
                    IsInUse = isInUse
                },
                new Cell
                {
                    Id = 5,
                    ENodebId = 3,
                    SectorId = 2,
                    RsPower = 1.1,
                    Height = 20,
                    IsOutdoor = true,
                    MTilt = 1.1,
                    ETilt = 2.2,
                    Azimuth = 210,
                    Longtitute = lon,
                    Lattitute = lat,
                    Pci = pci,
                    IsInUse = isInUse
                },
                new Cell
                {
                    Id = 6,
                    ENodebId = 3,
                    SectorId = 3,
                    RsPower = 1.1,
                    Height = 20,
                    IsOutdoor = true,
                    MTilt = 1.1,
                    ETilt = 2.2,
                    Azimuth = 270,
                    Longtitute = lon,
                    Lattitute = lat,
                    Pci = pci,
                    IsInUse = isInUse
                }
            };
        }

        public static void MockThreeColleges(this Mock<ICollegeRepository> repository)
        {
            repository.MockAuditedItems(new List<CollegeInfo>
            {
                new CollegeInfo
                {
                    Id = 1,
                    Name = "college-1"
                },
                new CollegeInfo
                {
                    Id = 2,
                    Name = "college-2"
                },
                new CollegeInfo
                {
                    Id = 3,
                    Name = "college-3"
                }
            }.AsQueryable());
        }

        public static void MockThreeENodebs(this Mock<IENodebRepository> repository)
        {
            repository.MockQueryItems(new List<ENodeb>
            {
                new ENodeb {Id = 1, ENodebId = 1, Name = "ENodeb-1", TownId = 1, Address = "Address-1", PlanNum = "FSL-1"},
                new ENodeb {Id = 2, ENodebId = 2, Name = "ENodeb-2", TownId = 2, Address = "Address-2", PlanNum = "FSL-2"},
                new ENodeb {Id = 3, ENodebId = 3, Name = "ENodeb-3", TownId = 3, Address = "Address-3", PlanNum = "FSL-3"}
            }.AsQueryable());
        }
        
        public static void MockThreeCollegeENodebs(this Mock<IInfrastructureRepository> repository)
        {
            repository.MockQueryItems(new List<InfrastructureInfo>
            {
                new InfrastructureInfo
                {
                    HotspotName = "College-1",
                    HotspotType = HotspotType.College,
                    Id = 1,
                    InfrastructureType = InfrastructureType.ENodeb,
                    InfrastructureId = 1
                },
                new InfrastructureInfo
                {
                    HotspotName = "College-2",
                    HotspotType = HotspotType.College,
                    Id = 2,
                    InfrastructureType = InfrastructureType.ENodeb,
                    InfrastructureId = 2
                },
                new InfrastructureInfo
                {
                    HotspotName = "College-3",
                    HotspotType = HotspotType.College,
                    Id = 3,
                    InfrastructureType = InfrastructureType.ENodeb,
                    InfrastructureId = 3
                }
            }.AsQueryable());
        }

        public static void MockSixCollegeCells(this Mock<IInfrastructureRepository> repository)
        {
            repository.MockQueryItems(new List<InfrastructureInfo>
            {
                new InfrastructureInfo
                {
                    HotspotName = "College-1",
                    HotspotType = HotspotType.College,
                    Id = 1,
                    InfrastructureType = InfrastructureType.Cell,
                    InfrastructureId = 1
                },
                new InfrastructureInfo
                {
                    HotspotName = "College-2",
                    HotspotType = HotspotType.College,
                    Id = 2,
                    InfrastructureType = InfrastructureType.Cell,
                    InfrastructureId = 2
                },
                new InfrastructureInfo
                {
                    HotspotName = "College-3",
                    HotspotType = HotspotType.College,
                    Id = 3,
                    InfrastructureType = InfrastructureType.Cell,
                    InfrastructureId = 3
                },
                new InfrastructureInfo
                {
                    HotspotName = "College-4",
                    HotspotType = HotspotType.College,
                    Id = 4,
                    InfrastructureType = InfrastructureType.Cell,
                    InfrastructureId = 4
                },
                new InfrastructureInfo
                {
                    HotspotName = "College-5",
                    HotspotType = HotspotType.College,
                    Id = 5,
                    InfrastructureType = InfrastructureType.Cell,
                    InfrastructureId = 5
                },
                new InfrastructureInfo
                {
                    HotspotName = "College-6",
                    HotspotType = HotspotType.College,
                    Id = 6,
                    InfrastructureType = InfrastructureType.Cell,
                    InfrastructureId = 6
                }
            }.AsQueryable());
        }

        public static void MockSixCollegeCdmaCells(this Mock<IInfrastructureRepository> repository)
        {
            repository.MockQueryItems(new List<InfrastructureInfo>
            {
                new InfrastructureInfo
                {
                    HotspotName = "College-1",
                    HotspotType = HotspotType.College,
                    Id = 1,
                    InfrastructureType = InfrastructureType.CdmaCell,
                    InfrastructureId = 1
                },
                new InfrastructureInfo
                {
                    HotspotName = "College-2",
                    HotspotType = HotspotType.College,
                    Id = 2,
                    InfrastructureType = InfrastructureType.CdmaCell,
                    InfrastructureId = 2
                },
                new InfrastructureInfo
                {
                    HotspotName = "College-3",
                    HotspotType = HotspotType.College,
                    Id = 3,
                    InfrastructureType = InfrastructureType.CdmaCell,
                    InfrastructureId = 3
                },
                new InfrastructureInfo
                {
                    HotspotName = "College-4",
                    HotspotType = HotspotType.College,
                    Id = 4,
                    InfrastructureType = InfrastructureType.CdmaCell,
                    InfrastructureId = 4
                },
                new InfrastructureInfo
                {
                    HotspotName = "College-5",
                    HotspotType = HotspotType.College,
                    Id = 5,
                    InfrastructureType = InfrastructureType.CdmaCell,
                    InfrastructureId = 5
                },
                new InfrastructureInfo
                {
                    HotspotName = "College-6",
                    HotspotType = HotspotType.College,
                    Id = 6,
                    InfrastructureType = InfrastructureType.CdmaCell,
                    InfrastructureId = 6
                }
            }.AsQueryable());
        }


        public static void MockSixTowns(this Mock<ITownRepository> repository)
        {
            var ids = new[] { 1, 2, 3, 4, 5, 6 };
            repository.MockQueryItems(ids.Select(x => new Town
            {
                Id = x,
                CityName = "city-" + x,
                DistrictName = "district-" + x,
                TownName = "town-" + x
            }).AsQueryable());
        }
    }
}
