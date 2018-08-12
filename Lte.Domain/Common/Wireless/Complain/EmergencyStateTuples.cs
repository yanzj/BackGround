using System;
using Lte.Domain.Common.Types;

namespace Lte.Domain.Common.Wireless.Complain
{
    [EnumTypeDescription(typeof(EmergencyState), Begin)]
    public enum EmergencyState : byte
    {
        Begin,
        Register,
        FiberBegin,
        ElectricPrepare,
        FiberFinish,
        VehicleInPlace,
        VehicleInService,
        Test,
        Finish
    }

    public class EmergencyStateDescriptionTransform : DescriptionTransform<EmergencyState>
    {

    }

    public class EmergencyStateTransform : EnumTransform<EmergencyState>
    {
        public EmergencyStateTransform() : base(EmergencyState.Begin)
        {
        }
    }

    internal static class EmergencyStateTuples
    {
        public static Tuple<object, string>[] GetTuples()
        {
            return new[]
            {
                new Tuple<object, string>(EmergencyState.Begin, "生成工单"),
                new Tuple<object, string>(EmergencyState.Register, "通信车申请"),
                new Tuple<object, string>(EmergencyState.FiberBegin, "光纤起单"),
                new Tuple<object, string>(EmergencyState.ElectricPrepare, "电源准备"),
                new Tuple<object, string>(EmergencyState.FiberFinish, "光纤调通"),
                new Tuple<object, string>(EmergencyState.VehicleInPlace, "通信车就位"),
                new Tuple<object, string>(EmergencyState.VehicleInService, "通信车开通"),
                new Tuple<object, string>(EmergencyState.Test, "优化测试"),
                new Tuple<object, string>(EmergencyState.Finish, "完成")
            };
        }
    }
}
