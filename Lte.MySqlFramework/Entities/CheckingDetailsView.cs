using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Maintainence;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Common.Wireless.Complain;
using Lte.Domain.Regular.Attributes;

namespace Lte.MySqlFramework.Entities
{
    [AutoMapFrom(typeof(CheckingDetails))]
    [TypeDoc("巡检结果详细信息")]
    public class CheckingDetailsView
    {
        [MemberDoc("巡检流水号")]
        public string CheckingFlowNumber { get; set; }

        [MemberDoc("巡检主题")]
        public string CheckingTheme { get; set; }

        [MemberDoc("检查结果")]
        public string Results { get; set; }

        [MemberDoc("是否需要整治")]
        [AutoMapPropertyResolve("IsNeedFixing", typeof(CheckingDetails), typeof(YesNoTransform))]
        public string NeedFixing { get; set; }

        [MemberDoc("是否有图片")]
        [AutoMapPropertyResolve("IsContainPicture", typeof(CheckingDetails), typeof(YesNoTransform))]
        public string ContainPicture { get; set; }

        [MemberDoc("图片存放目录")]
        public string Directory { get; set; }

        [MemberDoc("状态")]
        [AutoMapPropertyResolve("ComplainState", typeof(CheckingDetails), typeof(ComplainStateDescriptionTransform))]
        public string ComplainStateDescription { get; set; }
    }
}
