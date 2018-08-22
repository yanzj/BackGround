using System;
using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Common.Wireless.Complain;
using Lte.Domain.Regular.Attributes;

namespace Abp.EntityFramework.Entities.Complain
{
    [AutoMapFrom(typeof(VipDemand))]
    [TypeDoc("VIP需求信息数据单元")]
    public class VipDemandDto : IDistrictTown, ITownId, IBeginDate
    {
        public string SerialNumber { get; set; }
        
        public int TownId { get; set; }

        public string District { get; set; }

        public string Town { get; set; }

        public DateTime BeginDate { get; set; }

        [AutoMapPropertyResolve("IsRecording", typeof(VipDemand), typeof(YesNoTransform))]
        public string Recording { get; set; }

        public string PhoneNumber { get; set; }

        public string ContactPerson { get; set; }

        public string ReceiptNumber { get; set; }

        public string Department { get; set; }

        public string Area { get; set; }

        public string BelongedCity { get; set; }
        
        public string Phenomenon { get; set; }

        public string FinishResults { get; set; }

        public string SustainPerson { get; set; }

        public string StaffId { get; set; }

        [AutoMapPropertyResolve("ComplainCategory", typeof(VipDemand), typeof(ComplainCategoryDescriptionTransform))]
        public string ComplainCategoryDescription { get; set; }

        public string ProjectName { get; set; }

        public string ProjectContents { get; set; }
    }
}