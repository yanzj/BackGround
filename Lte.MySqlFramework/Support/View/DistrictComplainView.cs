using Lte.Domain.Regular.Attributes;

namespace Lte.MySqlFramework.Support.View
{
    [TypeDoc("区域抱怨工单统计")]
    public class DistrictComplainView
    {
        [MemberDoc("区域")]
        public string District { get; set; }

        [MemberDoc("2G投诉单数量")]
        public int Complain2G { get; set; }
        
        [MemberDoc("3G投诉单数量")]
        public int Complain3G { get; set; }
        
        [MemberDoc("4G投诉单数量")]
        public int Complain4G { get; set; }

        public int ComplainAll => Complain2G + Complain3G + Complain4G;
        
        [MemberDoc("2G需求单数量")]
        public int Demand2G { get; set; }
        
        [MemberDoc("3G需求单数量")]
        public int Demand3G { get; set; }
        
        [MemberDoc("4G需求单数量")]
        public int Demand4G { get; set; }

        public int DemandAll => Demand2G + Demand3G + Demand4G;

        public int Total => ComplainAll + DemandAll;
    }
}