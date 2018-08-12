using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Regular.Attributes;

namespace Abp.EntityFramework.Entities.Complain
{
    [AutoMapFrom(typeof(ComplainItem))]
    [TypeDoc("抱怨信息（后端投诉工单）与经纬度校正相关信息视图")]
    public class ComplainPositionDto
    {
        [MemberDoc("投诉单编号")]
        public string SerialNumber { get; set; }

        [MemberDoc("城市")]
        public string City { get; set; }

        [MemberDoc("区域")]
        public string District { get; set; }
        
        [MemberDoc("镇区")]
        public string Town { get; set; }

        [MemberDoc("道路名称，作为匹配地理位置的第一重要信息")]
        public string RoadName { get; set; }

        [MemberDoc("楼宇名称，作为匹配地理位置的第二重要信息")]
        public string BuildingName { get; set; }

        [MemberDoc("经度，需要匹配的经度，通过百度地图API获取")]
        public double Longtitute { get; set; }

        [MemberDoc("纬度，需要匹配的经度，通过百度地图API获取")]
        public double Lattitute { get; set; }

        [MemberDoc("站点位置，作为匹配地理位置的第三重要信息")]
        public string SitePosition { get; set; }

        [MemberDoc("投诉内容，作为匹配地理位置的次重要信息")]
        public string ComplainContents { get; set; }

        [MemberDoc("联系地址，作为匹配地理位置的次要信息，因为用户开户地址通常与投诉地址不是同一地址")]
        public string ContactAddress { get; set; }
    }
}