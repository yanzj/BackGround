using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless.Work;
using Lte.Domain.Excel;
using Lte.Domain.Regular.Attributes;

namespace Abp.EntityFramework.Entities.Maintainence
{
    [TypeDoc("省公司巡检计划")]
    [AutoMapFrom(typeof(CheckingProjectProvince))]
    public class CheckingProjectProvinceView
    {
        [MemberDoc("任务编号")]
        public string WorkItemNumber { get; set; }

        [MemberDoc("地市")]
        public string City { get; set; }

        [MemberDoc("任务名称")]
        public string WorkItemName { get; set; }

        [MemberDoc("任务状态")]
        [AutoMapPropertyResolve("WorkItemState", typeof(CheckingProjectProvince), typeof(WorkItemStateDescriptionTransform))]
        public string WorkItemStateDescription { get; set; }

        [MemberDoc("计划名称")]
        public string ProjectName { get; set; }

        [MemberDoc("当前处理人员")]
        public string Processor { get; set; }

        [MemberDoc("接收班组")]
        public string Receiver { get; set; }

        [MemberDoc("任务开始时间")]
        public DateTime BeginDate { get; set; }

        [MemberDoc("任务完成时限")]
        public DateTime FinishDate { get; set; }
    }
}
