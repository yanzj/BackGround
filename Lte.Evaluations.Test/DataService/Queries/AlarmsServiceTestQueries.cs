using Lte.Domain.Common;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;
using Lte.Parameters.Entities.Kpi;
using NUnit.Framework;

namespace Lte.Evaluations.DataService.Queries
{
    public static class AlarmsServiceTestQueries
    {
        public static void AssertBasicParameters(this AlarmView view, int eNodebId, string details)
        {
            Assert.AreEqual(view.ENodebId, eNodebId);
            Assert.AreEqual(view.Details, details);
        }

        public static void AssertPosition(this AlarmView view, string position, double duration)
        {
            Assert.AreEqual(view.Position, position);
            Assert.AreEqual(view.Duration, duration);
        }

        public static void AssertTypes(this AlarmView view, AlarmLevel level, AlarmCategory category, AlarmType type,
            string typeDescription)
        {
            Assert.AreEqual(view.AlarmTypeDescription, typeDescription);
            Assert.AreEqual(view.AlarmCategoryDescription, category.GetEnumDescription());
            Assert.AreEqual(view.AlarmLevelDescription, level.GetEnumDescription());
        }
    }
}
