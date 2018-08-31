using Abp.Domain.Entities;
using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Common.Transform;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless.Complain;
using Lte.Domain.Excel;
using Lte.Domain.Regular.Attributes;

namespace Abp.EntityFramework.Entities.Maintainence
{
    [TypeDoc("巡检结果详细信息")]
    [AutoMapFrom(typeof(CheckingDetailsExcel))]
    public class CheckingDetails : Entity
    {
        [MemberDoc("巡检流水号")]
        public string CheckingFlowNumber { get; set; }

        [MemberDoc("巡检主题")]
        public string CheckingTheme { get; set; }

        [MemberDoc("检查结果")]
        public string Results { get; set; }

        [MemberDoc("是否需要整治")]
        [AutoMapPropertyResolve("NeedFixing", typeof(CheckingDetailsExcel), typeof(YesToBoolTransform))]
        public bool IsNeedFixing { get; set; }

        [MemberDoc("是否有图片")]
        [AutoMapPropertyResolve("ContainPicture", typeof(CheckingDetailsExcel), typeof(YesToBoolTransform))]
        public bool IsContainPicture { get; set; }

        [MemberDoc("图片存放目录")]
        public string Directory { get; set; }

        [MemberDoc("状态")]
        [AutoMapPropertyResolve("ComplainStateDescription", typeof(CheckingDetailsExcel), typeof(ComplainStateTransform))]
        public ComplainState ComplainState { get; set; }

    }
}
