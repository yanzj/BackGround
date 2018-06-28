using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Lte.Domain.Common.Types;

namespace Lte.Domain.Common.Wireless
{
    [EnumTypeDescription(typeof(AlarmType), Others)]
    public enum AlarmType : short
    {
        CeNotEnough,//0
        StarUnlocked,
        TrunkProblem,
        RssiProblem,
        CellDown,//4
        VswrProblem,
        VswrLte,
        Unimportant,
        LinkBroken,
        X2Broken,
        X2UserPlane,//10
        S1Broken,
        S1UserPlane,
        EthernetBroken,
        LteCellDown,
        LteCellError,//15
        SuperCellDown,
        ENodebDown,
        GnssStar,
        GnssFeed,
        PaDeactivate,//20
        RruBroken,
        RxChannel,
        SntpFail,
        VersionError,
        InitializationError,//25
        BoardInexist,
        BoardInitialize,
        BoardPowerDown,
        BoardCommunication,
        BoardSoftId,//30
        FiberReceiver,
        FiberModule,
        BbuInitialize,
        Temperature,
        FanTemperature,//35
        NoClock,
        InnerError,
        SoftwareAbnormal,
        ApparatusPowerDown,
        InputVolte,//40
        OuterApparatus,
        ParametersConfiguation,
        BadPerformance,//43
        Others,
        DatabaseDelay,
        PciCrack,//46
        RruRtwp,
        BbuCpriInterface,
        BbuCpriLost,
        EletricAntenna,
        RfAld,
        RruCpriInterface,
        RruInterfacePerformance,
        RruPowerDown,
        RruRtwpUnbalance,
        RruClock,
        RruOmcLink,
        ClockReference,
        Database,
        AntennaLink,
        UserPlane,
        RemoteOmc,
        LoginError,
        AnalogLoad,
        NbIotOut,
        LicenseExpired,
        CpuOverload,
        CpuOverloadSerious,
        PaDisconnect,
        PbBroken,
        S1AllBroken,
        ParameterError,
        OmcLinkError,
        BoardNoConfig,
        FanError,
        LicenseConfig,
        PatchError,
        ClockSourceAbnormal,
        SynchronizationLost,
        EthernetBandwidth,
        HardwareConfig,
        UserLocked,
        PasswordError,
        MainSourceInput,
        BbuCpriBad,
        BbuCpriLine,
        BbuBoard,
        BbuFan,
        BbuDcOutput,
        OsmuOss,
        RruCascade,
        VcsService,
        SecurePassword,
        CanbusCommunication,
        VersionBack,
        DiskUsage,
        BoardIncreament,
        BoardClock,
        BoardTemperature,
        BoardHardware,
        EletricAntennaMoter,
        ElectricAntennaData,
        AlarmReportJam,
        RackConfig,
        ConfigFile,
        TaskFail,
        RfTransmit,
        RfMode,
        RfInputSource,
        RruRuntime,
        RruDc,
        AntennaHardware,
        NeVersion,
        SinrUnbalance,
        ReconfigurationFailure,
        GeneralENodebDown,
        GeneralCellDown
    }

    public class AlarmTypeDescriptionTransform : ValueResolver<AlarmType, string>
    {
        protected override string ResolveCore(AlarmType source)
        {
            return source.GetEnumDescription(WirelessPublic.AlarmTypeHuaweiList);
        }
    }

    public class AlarmTypeTransform : ValueResolver<string, AlarmType>
    {
        protected override AlarmType ResolveCore(string source)
        {
            return source.GetEnumType(WirelessPublic.AlarmTypeHuaweiList);
        }
    }

    public class AlarmZteTypeTransform : EnumTransform<AlarmType>
    {
        public AlarmZteTypeTransform() : base(AlarmType.Others)
        {
        }
    }

    internal static class AlarmTypeTuples
    {
        public static Tuple<object, string>[] GetTuples()
        {
            return new[]
            {
                new Tuple<object, string>(AlarmType.CeNotEnough, "CE不足"),
                new Tuple<object, string>(AlarmType.CpuOverload, "CPU过载告警(198092391)"),
                new Tuple<object, string>(AlarmType.CpuOverloadSerious, "CPU过载严重告警(198092390)"),
                new Tuple<object, string>(AlarmType.GnssStar, "GNSS接收机搜星故障(198096837)"),
                new Tuple<object, string>(AlarmType.GnssFeed, "GNSS天馈链路故障(198096836)"),
                new Tuple<object, string>(AlarmType.LicenseExpired, "License文件即将到期(198097673)"),
                new Tuple<object, string>(AlarmType.LteCellDown, "LTE小区退出服务(198094419)"),
                new Tuple<object, string>(AlarmType.NbIotOut, "NB-IoT小区退出服务(200200019)"),
                new Tuple<object, string>(AlarmType.PaDisconnect, "PA关断(1118)"),
                new Tuple<object, string>(AlarmType.PaDeactivate, "PA去使能(198098440)"),
                new Tuple<object, string>(AlarmType.PbBroken, "PB链路断(198097606)"),
                new Tuple<object, string>(AlarmType.RruBroken, "RRU链路断(198097605)"),
                new Tuple<object, string>(AlarmType.RssiProblem, "RSSI问题"),
                new Tuple<object, string>(AlarmType.RxChannel, "RX通道异常(198098469)"),
                new Tuple<object, string>(AlarmType.S1Broken, "S1断链告警(198094420)"),
                new Tuple<object, string>(AlarmType.S1Broken, "S1断链告警(200200020)"),
                new Tuple<object, string>(AlarmType.S1AllBroken, "S1口链路全断(1360)"),
                new Tuple<object, string>(AlarmType.S1UserPlane, "S1用户面路径不可用(198094466)"),
                new Tuple<object, string>(AlarmType.SntpFail, "SNTP对时失败(198092014)"),
                new Tuple<object, string>(AlarmType.X2Broken, "X2断链告警(198094421)"),
                new Tuple<object, string>(AlarmType.X2UserPlane, "X2用户面路径不可用(198094467)"),
                new Tuple<object, string>(AlarmType.VersionError, "版本包故障(198097567)"),
                new Tuple<object, string>(AlarmType.Unimportant, "不影响业务问题"),
                new Tuple<object, string>(AlarmType.ParameterError, "参数配置错误(198097511)"),
                new Tuple<object, string>(AlarmType.ParameterError, "参数配置错误(198098464)"),
                new Tuple<object, string>(AlarmType.OmcLinkError, "操作维护通道配置错误(198097172)"),
                new Tuple<object, string>(AlarmType.SuperCellDown, "超级小区CP退出服务(198094440)"),
                new Tuple<object, string>(AlarmType.InitializationError, "初始化失败(198092070)"),
                new Tuple<object, string>(AlarmType.TrunkProblem, "传输问题"),
                new Tuple<object, string>(AlarmType.BoardInexist, "单板不在位(198092072)"),
                new Tuple<object, string>(AlarmType.BoardInitialize, "单板处于初始化状态(198092348)"),
                new Tuple<object, string>(AlarmType.BoardPowerDown, "单板电源关断(198092057)"),
                new Tuple<object, string>(AlarmType.BoardCommunication, "单板通讯链路断(198097060)"),
                new Tuple<object, string>(AlarmType.BoardNoConfig, "单板未配置(198092203)"),
                new Tuple<object, string>(AlarmType.FanError, "风扇故障(198098111)"),
                new Tuple<object, string>(AlarmType.FiberReceiver, "光口接收链路故障(198098319)"),
                new Tuple<object, string>(AlarmType.FiberReceiver, "光口接收链路恶化(198098320)"),
                new Tuple<object, string>(AlarmType.FiberModule, "光模块不可用(198098318)"),
                new Tuple<object, string>(AlarmType.BbuInitialize, "基带单元处于初始化状态(198097050)"),
                new Tuple<object, string>(AlarmType.ENodebDown, "基站退出服务(198094422)"),
                new Tuple<object, string>(AlarmType.ENodebDown, "基站退出服务(200200022)"),
                new Tuple<object, string>(AlarmType.FanTemperature, "进风口温度异常(198092042)"),
                new Tuple<object, string>(AlarmType.NoClock, "没有可用的空口时钟源(198092217)"),
                new Tuple<object, string>(AlarmType.InnerError, "内部故障(198098467)"),
                new Tuple<object, string>(AlarmType.LicenseConfig, "配置数据和License授权不匹配(198097651)"),
                new Tuple<object, string>(AlarmType.PatchError, "热补丁故障(198092396)"),
                new Tuple<object, string>(AlarmType.Others, "其他告警"),
                new Tuple<object, string>(AlarmType.SoftwareAbnormal, "软件运行异常(198097604)"),
                new Tuple<object, string>(AlarmType.ApparatusPowerDown, "设备掉电(198092295)"),
                new Tuple<object, string>(AlarmType.ClockSourceAbnormal, "时钟参考源异常(1101)"),
                new Tuple<object, string>(AlarmType.InputVolte, "输入电压异常(198092053)"),
                new Tuple<object, string>(AlarmType.StarUnlocked, "锁星问题"),
                new Tuple<object, string>(AlarmType.VswrLte, "天馈驻波比异常(198098465)"),
                new Tuple<object, string>(AlarmType.SynchronizationLost, "同步丢失(198092215)"),
                new Tuple<object, string>(AlarmType.OuterApparatus, "外部扩展设备故障(198098468)"),
                new Tuple<object, string>(AlarmType.ParametersConfiguation, "网元不支持配置的参数(198097510)"),
                new Tuple<object, string>(AlarmType.LinkBroken, "网元断链告警(198099803)"),
                new Tuple<object, string>(AlarmType.Temperature, "温度异常(198097061)"),
                new Tuple<object, string>(AlarmType.LicenseConfig, "系统无可用License文件(198097670)"),
                new Tuple<object, string>(AlarmType.LteCellError, "小区关断告警(198094461)"),
                new Tuple<object, string>(AlarmType.CellDown, "小区退服"),
                new Tuple<object, string>(AlarmType.BadPerformance, "性能门限越界(1513)"),
                new Tuple<object, string>(AlarmType.DatabaseDelay, "性能数据入库延迟(15010001)"),
                new Tuple<object, string>(AlarmType.EthernetBandwidth, "以太网带宽不足(198098253)"),
                new Tuple<object, string>(AlarmType.EthernetBroken, "以太网物理连接断(198098252)"),
                new Tuple<object, string>(AlarmType.HardwareConfig, "硬件类型和配置不一致(198092029)"),
                new Tuple<object, string>(AlarmType.UserLocked, "用户被锁定(1000)"),
                new Tuple<object, string>(AlarmType.PasswordError, "用户登录密码输入错误(1050)"),
                new Tuple<object, string>(AlarmType.BoardSoftId, "找不到单板软件标识(198092397)"),
                new Tuple<object, string>(AlarmType.VswrProblem, "驻波比问题"),
                new Tuple<object, string>(AlarmType.MainSourceInput, "主用电源输入断电告警(198092265)"),
                new Tuple<object, string>(AlarmType.GeneralCellDown, "小区退服"),
                new Tuple<object, string>(AlarmType.GeneralENodebDown, "基站退服")
            };
        }
    }
}
