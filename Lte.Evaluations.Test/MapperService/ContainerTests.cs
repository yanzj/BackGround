using Abp.EntityFramework.AutoMapper;
using Abp.Reflection;
using AutoMapper;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Regular;
using Lte.Evaluations.Policy;
using Lte.MySqlFramework.Entities;
using Lte.Parameters.Entities.Neighbor;
using Lte.Parameters.Entities.Switch;
using NUnit.Framework;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Cdma;
using Abp.EntityFramework.Entities.Complain;
using Abp.EntityFramework.Entities.Infrastructure;
using Abp.EntityFramework.Entities.Kpi;
using Abp.EntityFramework.Entities.Maintainence;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless.Complain;
using Lte.Domain.Common.Wireless.Kpi;
using Lte.Domain.Common.Wireless.Region;
using Lte.Domain.Common.Wireless.Station;
using Lte.Domain.Common.Wireless.Work;
using Lte.Domain.Excel;
using Lte.Evaluations.ViewModels.Precise;

namespace Lte.Evaluations.MapperService
{
    [TestFixture]
    public class MapFromENodebContainerServiceTest
    {
        private readonly ITypeFinder _typeFinder = new TypeFinder(new MyAssemblyFinder());

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            var module = new AbpAutoMapperModule(_typeFinder);
            module.PostInitialize();
        }

        [TestCase("abc", "ieowue", 1, 2, "10.17.165.0", "10.17.165.100")]
        [TestCase("arebc", "ieo--wue", 3, 4, "219.128.254.0", "219.128.254.41")]
        public void Test_OneItem(string name, string address, int townId, int eNodebId, string gatewayAddress,
            string ipAddress)
        {
            var container = new ENodebExcelWithTownIdContainer
            {
                ENodebExcel = new ENodebExcel
                {
                    Name = name,
                    Address = address,
                    ENodebId = eNodebId,
                    Ip = new IpAddress(ipAddress),
                    GatewayIp = new IpAddress(gatewayAddress)
                },
                TownId = townId
            };
            var item = Mapper.Map<ENodebExcelWithTownIdContainer, ENodebWithTownIdContainer>(container);

            item.ENodeb.ENodebId.ShouldBe(eNodebId);
            item.ENodeb.Name.ShouldBe(name);
            item.ENodeb.Address.ShouldBe(address);
            item.TownId.ShouldBe(townId);
            item.ENodeb.Ip.AddressString.ShouldBe(ipAddress);
            item.ENodeb.GatewayIp.AddressString.ShouldBe(gatewayAddress);
        }

        [TestCase(new [] { "abc"}, new [] { "ieowue"}, new [] { 1}, new [] { 2})]
        [TestCase(new[] { "abc", "ert" }, new[] { "ieowue", "oe90w" }, new[] { 1, 100 }, new[] { 2, 2077 })]
        [TestCase(new[] { "arebc"}, new[] { "ieo--wue"}, new[] { 3}, new[] { 4})]
        public void Test_MultiItems(string[] names, string[] addresses, int[] townIds, int[] eNodebIds)
        {
            var containers = names.Select((t, i) => new ENodebExcelWithTownIdContainer
            {
                ENodebExcel = new ENodebExcel
                {
                    Name = t,
                    Address = addresses[i],
                    ENodebId = eNodebIds[i]
                },
                TownId = townIds[i]
            });

            var items =
                Mapper.Map<IEnumerable<ENodebExcelWithTownIdContainer>, List<ENodebWithTownIdContainer>>(containers);
            items.ForEach(x=> { x.ENodeb.TownId = x.TownId; });
            var results = items.Select(x => x.ENodeb);
            results.Select(x => x.ENodebId).ToArray().ShouldBe(eNodebIds);
            results.Select(x => x.Name).ToArray().ShouldBe(names);
            results.Select(x => x.Address).ToArray().ShouldBe(addresses);
            results.Select(x => x.TownId).ToArray().ShouldBe(townIds);
        }
    }

    [TestFixture]
    public class CdmaRegionMapperTest
    {
        private AbpAutoMapperModule _module;
        private TypeFinder _typeFinder;

        [TestFixtureSetUp]
        public void Setup()
        {
            _typeFinder = new TypeFinder(new MyAssemblyFinder());
            _module = new AbpAutoMapperModule(_typeFinder);
            _module.PostInitialize();
        }

        [Test]
        public void Map_Null_Tests()
        {
            CdmaRegionStatExcel info = null;
            var stat = info.MapTo<CdmaRegionStat>();
            stat.ShouldBeNull();
        }

        [Test]
        public void Map_Region_Tests()
        {
            var info = new CdmaRegionStatExcel
            {
                Region = "foshan1",
                Drop2GNum = 22
            };
            var stat = info.MapTo<CdmaRegionStat>();
            stat.Region.ShouldBe("foshan1");
            stat.Drop2GNum.ShouldBe(22);
        }
    }

    [TestFixture]
    public class NeighborCellMongoTest
    {
        private AbpAutoMapperModule _module;
        private TypeFinder _typeFinder;

        [TestFixtureSetUp]
        public void TestFixtureSetup()
        {
            _typeFinder = new TypeFinder(new MyAssemblyFinder());
            _module = new AbpAutoMapperModule(_typeFinder);
            _module.PostInitialize();
        }

        [Test]
        public void Test()
        {
            var relation = new EUtranRelationZte
            {
                eNodeB_Id = 11111,
                isAnrCreated = 1,
                isHOAllowed = 1,
                isRemoveAllowed = 0,
                nCelPriority = 22
            };
            var item = relation.MapTo<NeighborCellMongo>();
            Assert.AreEqual(item.CellId, 11111);
            Assert.IsTrue(item.IsAnrCreated);
            item.HandoffAllowed.ShouldBeTrue();
            item.RemovedAllowed.ShouldBeFalse();
            item.CellPriority.ShouldBe(22);
        }

        [Test]
        public void Test_From_ExternalEUtranCellFDDZte()
        {
            var external = new ExternalEUtranCellFDDZte
            {
                eNodeB_Id = 12345,
                eNBId = 21,
                cellLocalId = 5,
                pci = 223,
                userLabel = "wer"
            };
            var item = external.MapTo<NeighborCellMongo>();
            Assert.AreEqual(item.CellId, 12345);
            item.NeighborCellId.ShouldBe(21);
            Assert.AreEqual(item.NeighborSectorId, 5);
            Assert.AreEqual(item.NeighborPci, 223);
            item.NeighborCellName.ShouldBe("wer");
        }

        [Test]
        public void Test_From_EutranIntraFreqNCell()
        {
            var cell = new EutranIntraFreqNCell
            {
                eNodeB_Id = 54321,
                eNodeBId = 22222,
                CellId = 4,
                NeighbourCellName = "ere",
                AnrFlag = 2,
                NoHoFlag = 1,
                NoRmvFlag = 0,
                CellMeasPriority = 4
            };
            var item = cell.MapTo<NeighborCellMongo>();
            Assert.AreEqual(item.CellId, 54321);
            Assert.AreEqual(item.NeighborCellId, 22222);
            Assert.AreEqual(item.NeighborSectorId, 4);
            Assert.AreEqual(item.NeighborCellName, "ere");
            Assert.IsTrue(item.IsAnrCreated);
            Assert.IsFalse(item.HandoffAllowed);
            Assert.IsTrue(item.RemovedAllowed);
            Assert.AreEqual(item.CellPriority, 4);
        }

        [Test]
        public void Test_ENodebIntraFreqHoView()
        {
            var info = new IntraRatHoComm
            {
                eNodeB_Id = 12,
                IntraFreqHoRprtInterval = 34,
                IntraRatHoRprtAmount = 56,
                IntraRatHoMaxRprtCell = 78,
                IntraFreqHoA3TrigQuan = 90,
                IntraFreqHoA3RprtQuan = 111
            };
            var item = info.MapTo<ENodebIntraFreqHoView>();
            item.ENodebId.ShouldBe(12);
            item.ReportInterval.ShouldBe(34);
            item.ReportAmount.ShouldBe(56);
            item.MaxReportCellNum.ShouldBe(78);
            item.TriggerQuantity.ShouldBe(90);
            item.ReportQuantity.ShouldBe(111);
        }

        [Test]
        public void Test_From_UeEUtranMeasurementZte()
        {
            var info = new UeEUtranMeasurementZte
            {
                eNodeB_Id = 12,
                reportInterval = 34,
                reportAmount = 56,
                maxReportCellNum = 78,
                triggerQuantity = 90,
                reportQuantity = 111
            };
            var item = info.MapTo<ENodebIntraFreqHoView>();
            item.ENodebId.ShouldBe(12);
            item.ReportInterval.ShouldBe(34);
            item.ReportAmount.ShouldBe(56);
            item.MaxReportCellNum.ShouldBe(78);
            item.TriggerQuantity.ShouldBe(90);
            item.ReportQuantity.ShouldBe(111);
        }

        [Test]
        public void Test_CellIntraFreqHoView()
        {
            var info = new IntraFreqHoGroup
            {
                eNodeB_Id = 12,
                IntraFreqHoA3Hyst = 34,
                IntraFreqHoA3TimeToTrig = 56,
                IntraFreqHoA3Offset = 78
            };
            var item = info.MapTo<CellIntraFreqHoView>();
            item.ENodebId.ShouldBe(12);
            item.Hysteresis.ShouldBe(34);
            item.TimeToTrigger.ShouldBe(56);
            item.A3Offset.ShouldBe(78);
        }

        [Test]
        public void Test_CellIntraFreqHoView_From_UeEUtranMeasurementZte()
        {
            var info = new UeEUtranMeasurementZte
            {
                eNodeB_Id = 12,
                hysteresis = 1.5,
                timeToTrigger = 34,
                a3Offset = 2.5
            };
            var item= info.MapTo<CellIntraFreqHoView>();
            item.ENodebId.ShouldBe(12);
            item.Hysteresis.ShouldBe(3);
            item.TimeToTrigger.ShouldBe(34);
            item.A3Offset.ShouldBe(5);
        }

        [Test]
        public void Test_ENodebInterFreqHoView()
        {
            var info = new IntraRatHoComm
            {
                eNodeB_Id = 12,
                InterFreqHoA4RprtQuan = 34,
                InterFreqHoA4TrigQuan = 5,
                InterFreqHoA1A2TrigQuan = 67,
                A3InterFreqHoA1A2TrigQuan = 8,
                InterFreqHoRprtInterval = 90
            };
            var item = info.MapTo<ENodebInterFreqHoView>();
            item.ENodebId.ShouldBe(12);
            item.InterFreqHoA4RprtQuan.ShouldBe(34);
            item.InterFreqHoA4TrigQuan.ShouldBe(5);
            item.InterFreqHoA1TrigQuan.ShouldBe(67);
            item.InterFreqHoA2TrigQuan.ShouldBe(67);
            item.A3InterFreqHoA1TrigQuan.ShouldBe(8);
            item.A3InterFreqHoA2TrigQuan.ShouldBe(8);
            item.InterFreqHoRprtInterval.ShouldBe(90);
        }

        [Test]
        public void Test_EmergencyCommunicationDto()
        {
            var info = new EmergencyCommunication
            {
                Description = "[abc]defg",
                DemandLevel = DemandLevel.LevelC,
                VehicleType = VehicleType.CdmaAl,
                TransmitFunction = "ghih",
                ContactPerson = "abc(123)",
                EmergencyState = EmergencyState.FiberFinish
            };
            var item = info.MapTo<EmergencyCommunicationDto>();
            Assert.AreEqual(item.DemandLevelDescription.GetEnumType<DemandLevel>(), DemandLevel.LevelC);
            Assert.AreEqual(item.VehicularTypeDescription.GetEnumType<VehicleType>(), VehicleType.CdmaAl);
            Assert.AreEqual(item.TransmitFunction, "ghih");
            Assert.AreEqual(item.Person, "abc");
            Assert.AreEqual(item.Phone, "123");
            Assert.AreEqual(item.VehicleLocation, "abc");
            Assert.AreEqual(item.OtherDescription, "defg");
            Assert.AreEqual(item.CurrentStateDescription.GetEnumType<EmergencyState>(), EmergencyState.FiberFinish);
        }
        
        [Test]
        public void Test_ENodebBtsIdPair()
        {
            var info = new CellExcel
            {
                ENodebId = 11223,
                ShareCdmaInfo = "5_2234_0"
            };
            var item = info.MapTo<ENodebBtsIdPair>();
            item.ENodebId.ShouldBe(11223);
            item.BtsId.ShouldBe(2234);
        }

        [Test]
        public void Test_ENodebBtsIdPair_2()
        {
            var info = new CellExcel
            {
                ENodebId = 11223,
                ShareCdmaInfo = null
            };
            var item = info.MapTo<ENodebBtsIdPair>();
            item.ENodebId.ShouldBe(11223);
            item.BtsId.ShouldBe(0);
        }

        [Test]
        public void Test_BtsWithTownIdContainer()
        {
            var excelContainer = new BtsExcelWithTownIdContainer
            {
                TownId = 33,
                BtsExcel = new BtsExcel
                {
                    Address = "abc",
                    Name = "def"
                }
            };
            var container = excelContainer.MapTo<BtsWithTownIdContainer>();
            container.TownId.ShouldBe(33);
            container.CdmaBts.Address.ShouldBe("abc");
            container.CdmaBts.Name.ShouldBe("def");
        }

        [Test]
        public void Test_ENodebWithTownIdContainer()
        {
            var excelContainer = new ENodebExcelWithTownIdContainer
            {
                TownId = 33,
                ENodebExcel = new ENodebExcel
                {
                    Address = "abc",
                    Name = "def",
                    Factory = "zte"
                }
            };
            var container = excelContainer.MapTo<ENodebWithTownIdContainer>();
            container.TownId.ShouldBe(33);
            container.ENodeb.Address.ShouldBe("abc");
            container.ENodeb.Name.ShouldBe("def");
            container.ENodeb.Factory.ShouldBe("zte");
        }

        [Test]
        public void Test_CellView()
        {
            var info = new Cell
            {
                ETilt = 0.2,
                MTilt = 0.3,
                IsOutdoor = false
            };
            var view = info.MapTo<CellView>();
            view.DownTilt.ShouldBe(0.5);
            view.Indoor.ShouldBe("室内");
        }

        [Test]
        public void Test_CdmaCellView()
        {
            var info = new CdmaCell
            {
                ETilt = 0.2,
                MTilt = 0.3,
                IsOutdoor = false
            };
            var view = info.MapTo<CdmaCellView>();
            view.DownTilt.ShouldBe(0.5);
            view.Indoor.ShouldBe("室内");
        }

        [Test]
        public void Test_CdmaCompoundCellView()
        {
            var info = new CdmaCell
            {
                ETilt = 0.2,
                MTilt = 0.3,
                IsOutdoor = false
            };
            var view = info.MapTo<CdmaCompoundCellView>();
            view.DownTilt.ShouldBe(0.5);
            view.Indoor.ShouldBe("室内");
        }

        [Test]
        public void Test_EmergencyCommunication()
        {
            var dto = new EmergencyCommunicationDto
            {
                DemandLevelDescription = "C级",
                VehicularTypeDescription = "L网华为",
                Person = "abc",
                Phone = "123",
                VehicleLocation = "def",
                OtherDescription = "ghi",
                CurrentStateDescription = "光纤起单"
            };
            var item = dto.MapTo<EmergencyCommunication>();
            item.DemandLevel.ShouldBe(DemandLevel.LevelC);
            item.VehicleType.ShouldBe(VehicleType.LteHuawei);
            item.ContactPerson.ShouldBe("abc(123)");
            item.Description.ShouldBe("[def]ghi");
            item.EmergencyState.ShouldBe(EmergencyState.FiberBegin);
        }
       
        [Test]
        public void Test_ComplainExcel()
        {
            var info = new ComplainExcel
            {
                SourceDescription = "分公司客服中心",
                FirstReason = "物业逼迁导致故障",
                SecondReason = "业务恢复但原因未知",
                NetworkDescription = "3G",
                IndoorDescription = "室内",
                Scene = "城中村",
                CategoryDescription = "4G-无信号或信号弱"
            };
            var item = info.MapTo<ComplainItem>();
            item.ComplainSource.ShouldBe(ComplainSource.BranchService);
            item.ComplainReason.ShouldBe(ComplainReason.BiqianMalfunction);
            item.NetworkType.ShouldBe(NetworkType.With3G);
            item.IsIndoor.ShouldBe(true);
            item.ComplainScene.ShouldBe(ComplainScene.VillageInCity);
            item.ComplainCategory.ShouldBe(ComplainCategory.WeakCoverage4G);
            item.District.ShouldBe(null);
        }

        [Test]
        public void Test_ComplainExcel_Grid()
        {
            var info = new ComplainExcel
            {
                Grid = "分公司客服中心"
            };
            var item = info.MapTo<ComplainItem>();
            item.District.ShouldBe(null);
        }

        [Test]
        public void Test_ComplainExcel_Grid_FS()
        {
            var info = new ComplainExcel
            {
                Grid = "FS分公司客服中心"
            };
            var item = info.MapTo<ComplainItem>();
            item.District.ShouldBe("分公司客服中心");
        }

        [Test]
        public void Test_ComplainExcel_CandidateDistrict()
        {
            var info = new ComplainExcel
            {
                CandidateDistrict = "分公司客服中心"
            };
            var item = info.MapTo<ComplainItem>();
            item.District.ShouldBe("分公司客服中心");
        }
        
        [Test]
        public void Test_TopDrop2GCellViewContainer()
        {
            var source = new TopCellContainer<TopDrop2GCell>
            {
                LteName = "lte-1",
                CdmaName = "cdma-2",
                TopCell = new TopDrop2GCell
                {
                    BtsId = 1,
                    CellId = 2,
                    CallAttempts = 3,
                    Drops = 4,
                    Frequency = 100,
                    MoAssignmentSuccess = 5,
                    MtAssignmentSuccess = 6,
                    SectorId = 7,
                    StatTime = new DateTime(2016, 5, 5)
                }
            };
            var dest = source.MapTo<TopDrop2GCellViewContainer>();
            dest.LteName.ShouldBe("lte-1");
            dest.CdmaName.ShouldBe("cdma-2");
            dest.TopDrop2GCellView.CellId.ShouldBe(2);
            dest.TopDrop2GCellView.CallAttempts.ShouldBe(3);
            dest.TopDrop2GCellView.Drops.ShouldBe(4);
            dest.TopDrop2GCellView.Frequency.ShouldBe((short)100);
            dest.TopDrop2GCellView.MoAssignmentSuccess.ShouldBe(5);
            dest.TopDrop2GCellView.MtAssignmentSuccess.ShouldBe(6);
            dest.TopDrop2GCellView.SectorId.ShouldBe((byte)7);
            dest.TopDrop2GCellView.StatTime.ShouldBe(new DateTime(2016, 5, 5));
        }

        [Test]
        public void Test_TopConnection3GCellViewContainer()
        {
            var source = new TopCellContainer<TopConnection3GCell>
            {
                LteName = "lte-1",
                CdmaName = "cdma-2",
                TopCell = new TopConnection3GCell
                {
                    BtsId = 1,
                    CellId = 2,
                    ConnectionAttempts = 3,
                    ConnectionFails = 4,
                    LinkBusyRate = 5,
                    SectorId = 7,
                    StatTime = new DateTime(2016, 5, 5)
                }
            };
            var dest = source.MapTo<TopConnection3GCellViewContainer>();
            dest.LteName.ShouldBe("lte-1");
            dest.CdmaName.ShouldBe("cdma-2");
            dest.TopConnection3GCellView.CellId.ShouldBe(2);
            dest.TopConnection3GCellView.ConnectionAttempts.ShouldBe(3);
            dest.TopConnection3GCellView.ConnectionFails.ShouldBe(4);
            dest.TopConnection3GCellView.LinkBusyRate.ShouldBe(5);
            dest.TopConnection3GCellView.SectorId.ShouldBe((byte)7);
            dest.TopConnection3GCellView.StatTime.ShouldBe(new DateTime(2016, 5, 5));
        }

        [Test]
        public void Test_TopDrop2GTrendViewContainer()
        {
            var source = new TopCellContainer<TopDrop2GTrend>
            {
                LteName = "lte-1",
                CdmaName = "cdma-2",
                TopCell = new TopDrop2GTrend
                {
                    BtsId = 1,
                    CellId = 2,
                    TotalCallAttempst = 3,
                    TopDates = 4,
                    TotalDrops = 5,
                    MoAssignmentSuccess = 6,
                    SectorId = 7,
                    MtAssignmentSuccess = 8
                }
            };
            var dest = source.MapTo<TopDrop2GTrendViewContainer>();
            dest.ENodebName.ShouldBe("lte-1");
            dest.CellName.ShouldBe("cdma-2-7");
            dest.TopDrop2GTrendView.CellId.ShouldBe(2);
            dest.TopDrop2GTrendView.TotalCallAttempst.ShouldBe(3);
            dest.TopDrop2GTrendView.TopDates.ShouldBe(4);
            dest.TopDrop2GTrendView.TotalDrops.ShouldBe(5);
            dest.TopDrop2GTrendView.MoAssignmentSuccess.ShouldBe(6);
            dest.TopDrop2GTrendView.MtAssignmentSuccess.ShouldBe(8);
        }

        [Test]
        public void Test_TopConnection3GTrendViewContainer()
        {
            var source = new TopCellContainer<TopConnection3GTrend>
            {
                LteName = "lte-1",
                CdmaName = "cdma-2",
                TopCell = new TopConnection3GTrend
                {
                    BtsId = 1,
                    CellId = 2,
                    ConnectionAttempts = 3,
                    TopDates = 4,
                    ConnectionFails = 5,
                    WirelessDrop = 6,
                    SectorId = 7,
                    LinkBusyRate = 8
                }
            };
            var dest = source.MapTo<TopConnection3GTrendViewContainer>();
            dest.ENodebName.ShouldBe("lte-1");
            dest.CellName.ShouldBe("cdma-2-7");
            dest.TopConnection3GTrendView.CellId.ShouldBe(2);
            dest.TopConnection3GTrendView.ConnectionAttempts.ShouldBe(3);
            dest.TopConnection3GTrendView.TopDates.ShouldBe(4);
            dest.TopConnection3GTrendView.ConnectionFails.ShouldBe(5);
            dest.TopConnection3GTrendView.WirelessDrop.ShouldBe(6);
            dest.TopConnection3GTrendView.LinkBusyRate.ShouldBe(8);
        }

        [Test]
        public void Test_TopConnection3GCell()
        {
            var info = new TopConnection3GCellExcel
            {
                BtsId = 112,
                CellName = "abc[12345]",
                ConnectionAttempts = 3,
                ConnectionFails = 4,
                Frequency = 5,
                LinkBusyRate = 6,
                SectorId = 7,
                WirelessDrop = 8,
                StatDate = new DateTime(2016, 4, 1),
                StatHour = 15,
                City = "Foshan"
            };
            var item = info.MapTo<TopConnection3GCell>();
            item.BtsId.ShouldBe(112);
            item.CellId.ShouldBe(12345);
            item.ConnectionAttempts.ShouldBe(3);
            item.ConnectionFails.ShouldBe(4);
            item.LinkBusyRate.ShouldBe(6);
            item.WirelessDrop.ShouldBe(8);
            item.StatTime.ShouldBe(new DateTime(2016, 4, 1, 15, 0, 0));
            item.City.ShouldBe("Foshan");
        }

        [Test]
        public void Test_TopCellContainer2()
        {
            var source = new TopCellContainer<TopDrop2GCell>
            {
                LteName = "aaa",
                CdmaName = "bbb",
                TopCell = new TopDrop2GCell
                {
                    CallAttempts = 1001,
                    Drops = 21
                }
            };
            var dest = Mapper.Map<TopCellContainer<TopDrop2GCell>, TopDrop2GCellViewContainer>(source);
            Assert.AreEqual(dest.LteName, "aaa");
            Assert.AreEqual(dest.CdmaName, "bbb");
            Assert.AreEqual(dest.TopDrop2GCellView.CallAttempts, 1001);
            Assert.AreEqual(dest.TopDrop2GCellView.Drops, 21);
        }

        [Test]
        public void Test_WorkItemView()
        {
            var info = new WorkItem
            {
                BeginTime = new DateTime(2016, 3, 4),
                Cause = WorkItemCause.Antenna,
                Comments = "abc",
                Deadline = new DateTime(2016, 4, 4),
                ENodebId = 12345,
                FeedbackTime = new DateTime(2016, 4, 4),
                FeedbackContents = "defg123",
                FinishTime = new DateTime(2016, 3, 6),
                RejectTimes = 34,
                SectorId = 3,
                SerialNumber = "123",
                StaffName = "ere",
                State = WorkItemState.Finished,
                Subtype = WorkItemSubtype.CallSetup,
                Type = WorkItemType.DailyTask,
                RepeatTimes = 19
            };
            var view = info.MapTo<WorkItemView>();
            view.BeginTime.ShouldBe(new DateTime(2016,3,4));
            view.WorkItemCause.ShouldBe(WorkItemCause.Antenna.GetEnumDescription());
            view.Comments.ShouldBe("abc");
            view.Deadline.ShouldBe(new DateTime(2016, 4, 4));
            view.ENodebId.ShouldBe(12345);
            view.FeedbackTime.ShouldBe(new DateTime(2016, 4, 4));
            view.FeedbackContents.ShouldBe("defg123");
            view.FinishTime.ShouldBe(new DateTime(2016, 3, 6));
            view.RejectTimes.ShouldBe((short)34);
            view.SectorId.ShouldBe((byte)3);
            view.SerialNumber.ShouldBe("123");
            view.StaffName.ShouldBe("ere");
            view.WorkItemState.ShouldBe(WorkItemState.Finished.GetEnumDescription());
            view.WorkItemSubType.ShouldBe(WorkItemSubtype.CallSetup.GetEnumDescription());
            view.WorkItemType.ShouldBe(WorkItemType.DailyTask.GetEnumDescription());
            view.RepeatTimes.ShouldBe((short)19);
        }
    }
}
