using System;
using Abp.Domain.Entities;
using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Excel;

namespace Abp.EntityFramework.Entities
{
    [AutoMapFrom(typeof(VipDemandExcel))]
    public class VipDemand : Entity, IBeginDate, ITownId
    {
        public DateTime BeginDate { get; set; }

        public bool IsRecording { get; set; }

        public string PhoneNumber { get; set; }

        public string ContactPerson { get; set; }

        public string ReceiptNumber { get; set; }

        public string Department { get; set; }

        public string Area { get; set; }

        public string BelongedCity { get; set; }

        public string SerialNumber { get; set; }

        public string Phenomenon { get; set; }

        public string FinishResults { get; set; }

        public string SustainPerson { get; set; }

        public string StaffId { get; set; }

        [AutoMapPropertyResolve("ComplainCategoryDescription", typeof(VipDemandExcel), typeof(ComplainCategoryTransform))]
        public ComplainCategory ComplainCategory { get; set; }

        public string ProjectName { get; set; }

        public string ProjectContents { get; set; }
        
        public int TownId { get; set; }
    }
}