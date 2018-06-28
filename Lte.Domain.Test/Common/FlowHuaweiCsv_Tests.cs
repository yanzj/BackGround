using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Common;
using Lte.Domain.Regular;
using NUnit.Framework;

namespace Lte.Domain.Test.Common
{
    [TestFixture]
    public class FlowHuaweiCsv_Tests
    {
        [Test]
        public void Test_ArrayAggregation_PreAction()
        {
            var list = new List<FlowHuaweiCsv>
            {
                new FlowHuaweiCsv {StatTime = new DateTime(2017, 1, 1), CellInfo = "123-456"},
                new FlowHuaweiCsv {StatTime = new DateTime(2017, 1, 3), CellInfo = "234-789"}
            };
            var sum = list.ArrayAggration(stat =>
            {
                stat.StatTime = new DateTime(2017, 2, 1);
                stat.CellInfo = "110-119";
            });
            Assert.AreEqual(sum.StatTime, new DateTime(2017, 2, 1));
            Assert.AreEqual(sum.CellInfo, "110-119");
        }

        [Test]
        public void Test_ArrayAggregation_SumProperties()
        {
            var list = new List<FlowHuaweiCsv>
            {
                new FlowHuaweiCsv
                {
                    StatTime = new DateTime(2017, 1, 1),
                    EmergencyRrcRequest = 10
                },
                new FlowHuaweiCsv
                {
                    CellInfo = "110-119",
                    EmergencyRrcRequest = 20
                }
            };
            var sum = list.ArrayAggration(stat =>
            {
                stat.StatTime = new DateTime(2017, 2, 1);
                stat.CellInfo = "eNodeB名称=容桂贝乐幼儿园, 本地小区标识=0, 小区名称=容桂贝乐幼儿园_0, eNodeB标识=500482, 小区双工模式=CELL_FDD, 小区标识=48";
            });
            Assert.AreEqual(sum.CellInfo, "eNodeB名称=容桂贝乐幼儿园, 本地小区标识=0, 小区名称=容桂贝乐幼儿园_0, eNodeB标识=500482, 小区双工模式=CELL_FDD, 小区标识=48");
            Assert.AreEqual(sum.StatTime, new DateTime(2017, 2, 1));
            Assert.AreEqual(sum.ENodebId, 500482);
            Assert.AreEqual(sum.LocalCellId, 0);
            Assert.AreEqual(sum.SectorId, 48);
            Assert.AreEqual(sum.EmergencyRrcRequest, 30);
        }

        [Test]
        public void Test_ArrayAggregation_AverageProperties()
        {
            var list = new List<FlowHuaweiCsv>
            {
                new FlowHuaweiCsv
                {
                    StatTime = new DateTime(2017, 1, 1),
                    EmergencyRrcRequest = 10,
                    AverageActiveUsers = 12
                },
                new FlowHuaweiCsv
                {
                    CellInfo = "110-119",
                    EmergencyRrcRequest = 20,
                    AverageActiveUsers = 14
                }
            };
            var sum = list.ArrayAggration(stat =>
            {
                stat.StatTime = new DateTime(2017, 2, 1);
                stat.CellInfo = "eNodeB名称=容桂贝乐幼儿园, 本地小区标识=0, 小区名称=容桂贝乐幼儿园_0, eNodeB标识=500482, 小区双工模式=CELL_FDD, 小区标识=48";
            });
            Assert.AreEqual(sum.ENodebId, 500482);
            Assert.AreEqual(sum.LocalCellId, 0);
            Assert.AreEqual(sum.SectorId, 48);
            Assert.AreEqual(sum.EmergencyRrcRequest, 30);
            Assert.AreEqual(sum.AverageActiveUsers, 13);
        }

        [Test]
        public void Test_ArrayAggregation_MaxProperties()
        {
            var list = new List<FlowHuaweiCsv>
            {
                new FlowHuaweiCsv
                {
                    StatTime = new DateTime(2017, 1, 1),
                    EmergencyRrcRequest = 10,
                    AverageActiveUsers = 12,
                    MaxActiveUsers = 3
                },
                new FlowHuaweiCsv
                {
                    CellInfo = "110-119",
                    EmergencyRrcRequest = 20,
                    AverageActiveUsers = 14,
                    MaxActiveUsers = 7
                }
            };
            var sum = list.ArrayAggration(stat =>
            {
                stat.StatTime = new DateTime(2017, 2, 1);
                stat.CellInfo = "eNodeB名称=容桂贝乐幼儿园, 本地小区标识=0, 小区名称=容桂贝乐幼儿园_0, eNodeB标识=500482, 小区双工模式=CELL_FDD, 小区标识=48";
            });
            Assert.AreEqual(sum.ENodebId, 500482);
            Assert.AreEqual(sum.LocalCellId, 0);
            Assert.AreEqual(sum.SectorId, 48);
            Assert.AreEqual(sum.EmergencyRrcRequest, 30);
            Assert.AreEqual(sum.AverageActiveUsers, 13);
            Assert.AreEqual(sum.MaxActiveUsers, 7);
        }

        [Test]
        public void Test_ArrayAggregation_CompoundProperties()
        {
            var list = new List<FlowHuaweiCsv>
            {
                new FlowHuaweiCsv
                {
                    StatTime = new DateTime(2017, 1, 1),
                    EmergencyRrcRequest = 10,
                    AverageActiveUsers = 12,
                    MaxActiveUsers = 3,
                    LastTtiDownlinkFlowInByteString = "324",
                    ButLastDownlinkDurationInMsString = "23"
                },
                new FlowHuaweiCsv
                {
                    CellInfo = "110-119",
                    EmergencyRrcRequest = 20,
                    AverageActiveUsers = 14,
                    MaxActiveUsers = 7,
                    LastTtiDownlinkFlowInByteString = "NIL",
                    ButLastDownlinkDurationInMsString = "31"
                }
            };
            var sum = list.ArrayAggration(stat =>
            {
                stat.StatTime = new DateTime(2017, 2, 1);
                stat.CellInfo = "eNodeB名称=容桂贝乐幼儿园, 本地小区标识=0, 小区名称=容桂贝乐幼儿园_0, eNodeB标识=500482, 小区双工模式=CELL_FDD, 小区标识=48";
            });
            Assert.AreEqual(sum.ENodebId, 500482);
            Assert.AreEqual(sum.LocalCellId, 0);
            Assert.AreEqual(sum.SectorId, 48);
            Assert.AreEqual(sum.EmergencyRrcRequest, 30);
            Assert.AreEqual(sum.AverageActiveUsers, 13);
            Assert.AreEqual(sum.MaxActiveUsers, 7);
            Assert.AreEqual(sum.LastTtiDownlinkFlowInByte, 324);
            Assert.AreEqual(sum.ButLastDownlinkDurationInMs, 54);
        }
    }
}
