using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lte.Domain.Common.Types;

namespace Lte.Domain.Common.Wireless
{
    [EnumTypeDescription(typeof(ComplainReason), Unknown)]
    public enum ComplainReason : byte
    {
        OutOfBuisiness,
        SubscriberProblem,
        OtherMalfunction,
        NetworkMalfunction,
        NetworkOptimize,
        UnConfirmed,
        BiqianMalfunction,
        NeedNewSite,
        CustomerReservation,
        Unknown,
        NetworkQuality,
        Bill,
        ForeignRoam,
        CustomerSuggestion,
        ProvinceRoam,
        CityRoam,
        Service,
        ShortMessage,
    }

    public class ComplainReasonDescriptionTransform : DescriptionTransform<ComplainReason>
    {

    }

    public class ComplainReasonTransform : EnumTransform<ComplainReason>
    {
        public ComplainReasonTransform() : base(ComplainReason.Unknown)
        {
        }
    }

    internal static class ComplainReasonTuples
    {
        public static Tuple<object, string>[] GetTuples()
        {
            return new[]
            {
                new Tuple<object, string>(ComplainReason.OutOfBuisiness, "错派或非本专业"),
                new Tuple<object, string>(ComplainReason.SubscriberProblem, "客户侧问题"),
                new Tuple<object, string>(ComplainReason.SubscriberProblem, "取消问题"),
                new Tuple<object, string>(ComplainReason.SubscriberProblem, "变更问题"),
                new Tuple<object, string>(ComplainReason.SubscriberProblem, "终端质量/性能问题"),
                new Tuple<object, string>(ComplainReason.OtherMalfunction, "其他原因导致故障"),
                new Tuple<object, string>(ComplainReason.NetworkMalfunction, "网络设备故障"),
                new Tuple<object, string>(ComplainReason.NetworkMalfunction, "无线设备故障"),
                new Tuple<object, string>(ComplainReason.NetworkOptimize, "网络优化调整"),
                new Tuple<object, string>(ComplainReason.NetworkOptimize, "网络优化解决"),
                new Tuple<object, string>(ComplainReason.UnConfirmed, "未确认"),
                new Tuple<object, string>(ComplainReason.BiqianMalfunction, "物业逼迁导致故障"),
                new Tuple<object, string>(ComplainReason.BiqianMalfunction, "基站等设备要求移拆"),
                new Tuple<object, string>(ComplainReason.NeedNewSite, "需新增资源"),
                new Tuple<object, string>(ComplainReason.NeedNewSite, "新增资源"),
                new Tuple<object, string>(ComplainReason.NeedNewSite, "基站等设备要求安装"),
                new Tuple<object, string>(ComplainReason.CustomerReservation, "预约客户"),
                new Tuple<object, string>(ComplainReason.Unknown, "未知原因"),
                new Tuple<object, string>(ComplainReason.NetworkQuality, "上网质量"),
                new Tuple<object, string>(ComplainReason.NetworkQuality, "非漫游质量"),
                new Tuple<object, string>(ComplainReason.Bill, "费用类需求"),
                new Tuple<object, string>(ComplainReason.Bill, "计费规则争议"),
                new Tuple<object, string>(ComplainReason.Bill, "未按规则计费/怀疑计费错误"),
                new Tuple<object, string>(ComplainReason.Bill, "充值缴费后未及时复通"),
                new Tuple<object, string>(ComplainReason.Bill, "协议到期/业务变更费用争议"),
                new Tuple<object, string>(ComplainReason.ForeignRoam, "国际及港澳台漫游质量"),
                new Tuple<object, string>(ComplainReason.ProvinceRoam, "省际漫游质量"),
                new Tuple<object, string>(ComplainReason.CityRoam, "省内漫游质量"),
                new Tuple<object, string>(ComplainReason.CustomerSuggestion, "客户建议（无需回复客户）"),
                new Tuple<object, string>(ComplainReason.CustomerSuggestion, "服务态度差"),
                new Tuple<object, string>(ComplainReason.Service, "业务办理规则争议"),
                new Tuple<object, string>(ComplainReason.Service, "用户否认使用/订购/开通"),
                new Tuple<object, string>(ComplainReason.Service, "停复机问题"),
                new Tuple<object, string>(ComplainReason.Service, "信控及停复机规则争议"),
                new Tuple<object, string>(ComplainReason.Service, "无法正常办理业务"),
                new Tuple<object, string>(ComplainReason.ShortMessage, "短彩信类问题")
            };
        }
    }
}
