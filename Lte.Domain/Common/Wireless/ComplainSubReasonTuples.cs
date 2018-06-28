using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Common.Types;

namespace Lte.Domain.Common.Wireless
{
    [EnumTypeDescription(typeof(ComplainSubReason), Others)]
    public enum ComplainSubReason : byte
    {
        Biqian,
        ParameterAdjust,
        OutOfBuisiness,
        NothingWithNetwork,
        BaseStationMalfunction,
        WrongDestination,
        OutInterference,
        ProjectNotBegin,
        UnableToConfirmCustomer,
        WuyeProblem,
        WrongReasonJustified,
        RecoverButUnknownReason,
        EmergencyOptimization,
        SubscriberFeeling,
        SubscriberTerminal,
        ReservationTest,
        Others,
        CallDrop,
        UnderConstruction,
        BillProblem,
        NoCoverage,
        PoorCoverage,
        Normal,
        AntennaAdjust,
        BscMalfunction,
        MicroMalfunction
    }

    public class ComplainSubReasonDescriptionTransform : DescriptionTransform<ComplainSubReason>
    {

    }

    public class ComplainSubReasonTransform : EnumTransform<ComplainSubReason>
    {
        public ComplainSubReasonTransform() : base(ComplainSubReason.Others)
        {
        }
    }

    internal static class ComplainSubReasonTuples
    {
        public static Tuple<object, string>[] GetTuples()
        {
            return new[]
            {
                new Tuple<object, string>(ComplainSubReason.Biqian, "逼迁关停"),
                new Tuple<object, string>(ComplainSubReason.AntennaAdjust, "天线调整"),
                new Tuple<object, string>(ComplainSubReason.ParameterAdjust, "参数调整"),
                new Tuple<object, string>(ComplainSubReason.OutOfBuisiness, "非本专业原因"),
                new Tuple<object, string>(ComplainSubReason.OutOfBuisiness, "非本专业"),
                new Tuple<object, string>(ComplainSubReason.OutOfBuisiness, "延迟收发"),
                new Tuple<object, string>(ComplainSubReason.OutOfBuisiness, "异常停开机"),
                new Tuple<object, string>(ComplainSubReason.NothingWithNetwork, "查中自复"),
                new Tuple<object, string>(ComplainSubReason.NothingWithNetwork, "非网络质量投诉"),
                new Tuple<object, string>(ComplainSubReason.NothingWithNetwork, "客户要求取消"),
                new Tuple<object, string>(ComplainSubReason.NothingWithNetwork, "业务恢复但原因未知"),
                new Tuple<object, string>(ComplainSubReason.BaseStationMalfunction, "基站设备故障"),
                new Tuple<object, string>(ComplainSubReason.BaseStationMalfunction, "故障"),
                new Tuple<object, string>(ComplainSubReason.BscMalfunction, "系统故障"),
                new Tuple<object, string>(ComplainSubReason.BscMalfunction, "BSC设备故障"),
                new Tuple<object, string>(ComplainSubReason.MicroMalfunction, "微型直放站故障"),
                new Tuple<object, string>(ComplainSubReason.WrongDestination, "申告地错派"),
                new Tuple<object, string>(ComplainSubReason.OutInterference, "外部干扰"),
                new Tuple<object, string>(ComplainSubReason.OutInterference, "网优干扰"),
                new Tuple<object, string>(ComplainSubReason.ProjectNotBegin, "未立项"),
                new Tuple<object, string>(ComplainSubReason.ProjectNotBegin, "无建设计划"),
                new Tuple<object, string>(ComplainSubReason.UnableToConfirmCustomer, "无法联系用户"),
                new Tuple<object, string>(ComplainSubReason.UnableToConfirmCustomer, "无法联系"),
                new Tuple<object, string>(ComplainSubReason.UnableToConfirmCustomer, "无法与用户确认"),
                new Tuple<object, string>(ComplainSubReason.WuyeProblem, "物业阻挠"),
                new Tuple<object, string>(ComplainSubReason.WuyeProblem, "物业原因无替换（非局方原因）"),
                new Tuple<object, string>(ComplainSubReason.WrongReasonJustified, "业务表象错派"),
                new Tuple<object, string>(ComplainSubReason.RecoverButUnknownReason, "业务恢复但原因未知"),
                new Tuple<object, string>(ComplainSubReason.EmergencyOptimization, "应急优化解决"),
                new Tuple<object, string>(ComplainSubReason.SubscriberFeeling, "用户感知问题"),
                new Tuple<object, string>(ComplainSubReason.SubscriberFeeling, "回音/杂音/断续"),
                new Tuple<object, string>(ComplainSubReason.SubscriberFeeling, "网速慢"),
                new Tuple<object, string>(ComplainSubReason.SubscriberFeeling, "网络难以连接"),
                new Tuple<object, string>(ComplainSubReason.SubscriberTerminal, "用户终端问题"),
                new Tuple<object, string>(ComplainSubReason.SubscriberTerminal, "用户终端"),
                new Tuple<object, string>(ComplainSubReason.SubscriberTerminal, "无法使用数据业务"),
                new Tuple<object, string>(ComplainSubReason.SubscriberTerminal, "有信号无法登陆"),
                new Tuple<object, string>(ComplainSubReason.SubscriberTerminal, "有信号无法呼入、呼出"),
                new Tuple<object, string>(ComplainSubReason.ReservationTest, "预约客户测试"),
                new Tuple<object, string>(ComplainSubReason.Others, "其他原因"),
                new Tuple<object, string>(ComplainSubReason.CallDrop, "掉话"),
                new Tuple<object, string>(ComplainSubReason.CallDrop, "频繁掉线"),
                new Tuple<object, string>(ComplainSubReason.UnderConstruction, "新增资源建设中"),
                new Tuple<object, string>(ComplainSubReason.UnderConstruction, "新增资源"),
                new Tuple<object, string>(ComplainSubReason.UnderConstruction, "替换站建设中"),
                new Tuple<object, string>(ComplainSubReason.UnderConstruction, "建设中已超预计时间"),
                new Tuple<object, string>(ComplainSubReason.UnderConstruction, "建设中未超预计时间"),
                new Tuple<object, string>(ComplainSubReason.UnderConstruction, "已立项待建设"),
                new Tuple<object, string>(ComplainSubReason.BillProblem, "计费规则争议"),
                new Tuple<object, string>(ComplainSubReason.BillProblem, "套餐外超量使用计费规则争议"),
                new Tuple<object, string>(ComplainSubReason.BillProblem, "客户对上网清单不认可"),
                new Tuple<object, string>(ComplainSubReason.BillProblem, "小额快速退费"),
                new Tuple<object, string>(ComplainSubReason.BillProblem, "用户否认使用/多用"),
                new Tuple<object, string>(ComplainSubReason.BillProblem, "余额争议"),
                new Tuple<object, string>(ComplainSubReason.BillProblem, "门户网站及客户端充值"),
                new Tuple<object, string>(ComplainSubReason.BillProblem, "无故被变更业务"),
                new Tuple<object, string>(ComplainSubReason.BillProblem, "要求减免费用"),
                new Tuple<object, string>(ComplainSubReason.BillProblem, "业务变更后无法享受原优惠"),
                new Tuple<object, string>(ComplainSubReason.NoCoverage, "覆盖问题"),
                new Tuple<object, string>(ComplainSubReason.NoCoverage, "无信号"),
                new Tuple<object, string>(ComplainSubReason.NoCoverage, "无网无信号"),
                new Tuple<object, string>(ComplainSubReason.NoCoverage, "无信号或信号差 "),
                new Tuple<object, string>(ComplainSubReason.NoCoverage, "无信号或信号差"),
                new Tuple<object, string>(ComplainSubReason.NoCoverage, "无信号或信号差（来访）"),
                new Tuple<object, string>(ComplainSubReason.NoCoverage, "无信号或信号差（出访）"),
                new Tuple<object, string>(ComplainSubReason.NoCoverage, "无信号或信号弱"),
                new Tuple<object, string>(ComplainSubReason.NoCoverage, "信号弱/不稳定"),
                new Tuple<object, string>(ComplainSubReason.PoorCoverage, "弱覆盖"),
                new Tuple<object, string>(ComplainSubReason.Normal, "测试正常"),
            };
        }
    }
}
