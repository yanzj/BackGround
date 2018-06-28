using Lte.MySqlFramework.Entities;
using NUnit.Framework;
using Shouldly;

namespace Lte.Evaluations.DataService.Kpi
{
    public static class TopDrop2GTestQueries
    {
        public static void AssertEqual(this TopDrop2GCellView view, byte sectorId, int drops, int assignmentSuccess,
            string lteName, string cdmaName)
        {
            Assert.AreEqual(view.SectorId, sectorId);
            Assert.AreEqual(view.Drops, drops);
            Assert.AreEqual(view.TrafficAssignmentSuccess, assignmentSuccess);
            Assert.AreEqual(view.LteName, lteName);
            Assert.AreEqual(view.CdmaName, cdmaName);
        }

        public static void AssertEqual(this TopDrop2GTrendView view, int drops, int assignmentSuccess, string eNodebName,
            string cellName, int topDates)
        {
            view.TotalDrops.ShouldBe(drops);
            view.TotalCallAttempst.ShouldBe(assignmentSuccess);
            view.TopDates.ShouldBe(topDates);
            view.ENodebName.ShouldBe(eNodebName);
            view.CellName.ShouldBe(cellName);
        }
    }
}
