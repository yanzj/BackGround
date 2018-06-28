using Lte.Domain.Common;
using Lte.Domain.Common.Wireless;
using NUnit.Framework;
using System.Reflection;
using Lte.Domain.Common.Types;

namespace Lte.Domain.Test.Common
{
    [TestFixture]
    public class EnumTypeDescriptionAttributeTest
    {
        [Test]
        public void TestTypeName()
        {
            Assert.AreEqual(typeof (AlarmType).Name, "AlarmType");
        }
        
        [Test]
        public void TestEnumTypeDescriptionAttribute()
        {
            var attribute =
                typeof (AlarmType).GetCustomAttribute<EnumTypeDescriptionAttribute>();
            Assert.IsNotNull(attribute);
            var list = attribute.TupleList;
            Assert.AreEqual(list.Length, 73);
            var defaultValue = attribute.DefaultValue;
            Assert.AreEqual(defaultValue, AlarmType.Others);
        }

        [Test]
        public void TestGetEnumType()
        {
            var alarmType = "天馈驻波比异常(198098465)".GetEnumType<AlarmType>();
            Assert.AreEqual(alarmType, AlarmType.VswrLte);
            var alarmLevel = "主要".GetEnumType<AlarmLevel>();
            Assert.AreEqual(alarmLevel, AlarmLevel.Primary);
            alarmLevel = "警告".GetEnumType<AlarmLevel>();
            Assert.AreEqual(alarmLevel, AlarmLevel.Warning);
            var alarmCategory = "处理错误告警".GetEnumType<AlarmCategory>();
            Assert.AreEqual(alarmCategory, AlarmCategory.ProcessError);
            var antennaPort = "2T2R".GetEnumType<AntennaPortsConfigure>();
            Assert.AreEqual(antennaPort, AntennaPortsConfigure.Antenna2T2R);
        }

        [Test]
        public void TestGetEnumTypeAlternative()
        {
            var alarmType = "天馈驻波比异常(198098465)".GetEnumType(WirelessPublic.AlarmTypeHuaweiList);
            Assert.AreEqual(alarmType, AlarmType.Others);
            alarmType = "BBU CPRI光模块/电接口不在位告警".GetEnumType(WirelessPublic.AlarmTypeHuaweiList);
            Assert.AreEqual(alarmType, AlarmType.BbuCpriLost);
        }

        [Test]
        public void TestGetEnumDescription()
        {
            var alarmDescription = AlarmType.VswrLte.GetEnumDescription();
            Assert.AreEqual(alarmDescription, "天馈驻波比异常(198098465)");
            alarmDescription = AlarmType.BbuCpriLost.GetEnumDescription();
            Assert.AreEqual(alarmDescription, "其他");
            var levelDescription = AlarmLevel.Serious.GetEnumDescription();
            Assert.AreEqual(levelDescription, "严重");
            var categoryDescription = AlarmCategory.ProcessError.GetEnumDescription();
            Assert.AreEqual(categoryDescription, "处理错误告警");
            var portDescription = AntennaPortsConfigure.Antenna4T4R.GetEnumDescription();
            Assert.AreEqual(portDescription, "4T4R");
        }

        [Test]
        public void TestGetEnumDescriptionAlternative()
        {
            var alarmDecription = AlarmType.VswrLte.GetEnumDescription(WirelessPublic.AlarmTypeHuaweiList);
            Assert.AreEqual(alarmDecription, "天馈驻波比异常(198098465)");
            alarmDecription = AlarmType.BbuCpriLost.GetEnumDescription(WirelessPublic.AlarmTypeHuaweiList);
            Assert.AreEqual(alarmDecription, "BBU CPRI光模块/电接口不在位告警");
        }

        [Test]
        public void TestGetNextStateDescription()
        {
            var currentState = "通信车申请";
            var nextState = currentState.GetNextStateDescription(EmergencyState.Finish);
            Assert.AreEqual(nextState==null?null:((EmergencyState)nextState).GetEnumDescription(), "光纤起单");
            Assert.AreEqual((EmergencyState)2,EmergencyState.FiberBegin);
        }
    }
}
