using System;
using Lte.Domain.Common.Types;

namespace Lte.Domain.Common.Wireless.Complain
{
    [EnumTypeDescription(typeof(ComplainSource), Unknown)]
    public enum ComplainSource : byte
    {
        Number10000,
        Qq,
        Weixin,
        Voice,
        BranchService,
        Others,
        Wangting,
        Zhangting,
        Yingyeting,
        Unknown,
        Malfunction,
        Praise,
        Bill,
        InterComm,
        Appliance,
        Service,
        Network
    }

    public class ComplainSourceDescriptionTransform : DescriptionTransform<ComplainSource>
    {

    }

    public class ComplainSourceTransform : EnumTransform<ComplainSource>
    {
        public ComplainSourceTransform() : base(ComplainSource.Unknown)
        {
        }
    }

    internal static class ComplainSourceTuples
    {
        public static Tuple<object, string>[] GetTuples()
        {
            return new[]
            {
                new Tuple<object, string>(ComplainSource.Number10000, "10000号"),
                new Tuple<object, string>(ComplainSource.Qq, "10000号QQ客服"),
                new Tuple<object, string>(ComplainSource.Weixin, "10000号微信客服"),
                new Tuple<object, string>(ComplainSource.Voice, "手机移动语音"),
                new Tuple<object, string>(ComplainSource.Voice, "10000号语音呼入"),
                new Tuple<object, string>(ComplainSource.Voice, "/用户业务故障(全局)/个人客户投诉/移动语音(2016全新)/手机移动语音/"),
                new Tuple<object, string>(ComplainSource.Voice, "用户业务故障(全局)/个人客户投诉/移动语音(2016全新)/手机移动语音/"),
                new Tuple<object, string>(ComplainSource.Voice, "全业务投诉类别/网络质量/非漫游质量/掉话"),
                new Tuple<object, string>(ComplainSource.BranchService, "分公司客服中心"),
                new Tuple<object, string>(ComplainSource.Others, "其他"),
                new Tuple<object, string>(ComplainSource.Wangting, "网厅"),
                new Tuple<object, string>(ComplainSource.Zhangting, "掌厅"),
                new Tuple<object, string>(ComplainSource.Yingyeting, "营业厅"),
                new Tuple<object, string>(ComplainSource.Yingyeting, "渠道服务质量"),
                new Tuple<object, string>(ComplainSource.Unknown, "未知"),
                new Tuple<object, string>(ComplainSource.Malfunction, "网络侧故障(全局)"),
                new Tuple<object, string>(ComplainSource.Malfunction, "/网络侧故障(全局)/网络故障/集团NOC电子工单系统工单/移动交换专业故障/"),
                new Tuple<object, string>(ComplainSource.Praise, "表扬建议类"),
                new Tuple<object, string>(ComplainSource.Bill, "费用问题"),
                new Tuple<object, string>(ComplainSource.Bill, "充值缴费问题"),
                new Tuple<object, string>(ComplainSource.Bill, "营销及规则政策类"),
                new Tuple<object, string>(ComplainSource.InterComm, "互联互通"),
                new Tuple<object, string>(ComplainSource.Appliance, "局方设备"),
                new Tuple<object, string>(ComplainSource.Appliance, "无法正常使用（非终端问题）"),
                new Tuple<object, string>(ComplainSource.Service, "开通/变更/停用问题"),
                new Tuple<object, string>(ComplainSource.Service, "终端/UIM卡质量及售后"),
                new Tuple<object, string>(ComplainSource.Network, "移动网络质量"),
                new Tuple<object, string>(ComplainSource.Network, "/用户业务故障(全局)/个人客户投诉/移动/无线宽带(2016全新)/4G无线宽带/"),
                new Tuple<object, string>(ComplainSource.Network, "/用户业务故障(全局)/个人客户投诉/移动/无线宽带(2016全新)/（4G数据卡）无线宽带/"),
            };
        }
    }
}
