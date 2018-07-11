using System;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Common.Geo;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Regular.Attributes;

namespace Abp.EntityFramework.Entities
{
    [AutoMapFrom(typeof(ComplainItem))]
    [TypeDoc("后端投诉工单")]
    public class ComplainDto : IStateChange, IBeginDate, ICityDistrictTown, IGeoPoint<double>
    {
        [MemberDoc("工单编号")]
        public string SerialNumber { get; set; }

        [MemberDoc("客户电话")]
        public string SubscriberPhone { get; set; }

        [MemberDoc("重复次数")]
        public byte RepeatTimes { get; set; }

        [AutoMapPropertyResolve("ServiceCategory", typeof(ComplainItem), typeof(ComplainCategoryDescriptionTransform))]
        public string ServiceCategoryDescription { get; set; }

        [AutoMapPropertyResolve("IsUrgent", typeof(ComplainItem), typeof(YesNoTransform))]
        public string IsUrgentDescription { get; set; }

        [MemberDoc("客户名称")]
        public string SubscriberInfo { get; set; }

        [MemberDoc("联系电话1")]
        public string ContactPhone { get; set; }

        [MemberDoc("联系人")]
        public string ContactPerson { get; set; }

        [MemberDoc("联系地址")]
        public string ContactAddress { get; set; }

        [MemberDoc("受理人班组")]
        public string ManagerInfo { get; set; }

        [MemberDoc("受理内容")]
        public string ComplainContents { get; set; }

        [MemberDoc("受理时间")]
        public DateTime BeginDate { get; set; }

        [MemberDoc("全程超时时间")]
        public DateTime Deadline { get; set; }

        [MemberDoc("当前处理班组")]
        public string CurrentProcessor { get; set; }

        [MemberDoc("当前班组接单时间")]
        public DateTime ProcessTime { get; set; }

        [MemberDoc("电子运维流水号")]
        public string OssSerialNumber { get; set; }

        [AutoMapPropertyResolve("ComplainSource", typeof(ComplainItem), typeof(ComplainSourceDescriptionTransform))]
        [MemberDoc("工单来源")]
        public string ComplainSourceDescription { get; set; }

        public DateTime BeginTime { get; set; }

        public string City { get; set; }

        public string District { get; set; }

        public string Town { get; set; }

        [MemberDoc("路名")]
        public string RoadName { get; set; }

        [MemberDoc("楼宇名称")]
        public string BuildingName { get; set; }
        
        [MemberDoc("原因定性")]
        public string CauseLocation { get; set; }

        [MemberDoc("预处理内容")]
        public string PreProcessContents { get; set; }

        [ExcelColumn("回单内容")]
        public string FinishContents { get; set; }

        [AutoMapPropertyResolve("IsSubscriber4G", typeof(ComplainItem), typeof(YesNoTransform))]
        [MemberDoc("是否为4G用户")]
        public string IsSubscriber4GDescription { get; set; }

        [MemberDoc("经度")]
        public double Longtitute { get; set; }

        [MemberDoc("纬度")]
        public double Lattitute { get; set; }

        [AutoMapPropertyResolve("ComplainReason", typeof(ComplainItem), typeof(ComplainReasonDescriptionTransform))]
        [MemberDoc("原因定性一级")]
        public string ComplainReasonDescription { get; set; }

        [AutoMapPropertyResolve("ComplainSubReason", typeof(ComplainItem), typeof(ComplainSubReasonDescriptionTransform))]
        [MemberDoc("原因定性二级")]
        public string ComplainSubReasonDescription { get; set; }

        [MemberDoc("归属网格")]
        public string Grid { get; set; }

        [AutoMapPropertyResolve("NetworkType", typeof(ComplainItem), typeof(NetworkTypeDescriptionTransform))]
        [MemberDoc("业务类型")]
        public string NetworkTypeDescription { get; set; }

        [MemberDoc("相关站点名称")]
        public string SitePosition { get; set; }

        [AutoMapPropertyResolve("IsIndoor", typeof(ComplainItem), typeof(IndoorDescriptionTransform))]
        [MemberDoc("室内室外")]
        public string IsIndoorDescription { get; set; }

        [AutoMapPropertyResolve("ComplainScene", typeof(ComplainItem), typeof(ComplainSceneDescriptionTransform))]
        [MemberDoc("使用场合")]
        public string ComplainSceneDescription { get; set; }

        [AutoMapPropertyResolve("ComplainCategory", typeof(ComplainItem), typeof(ComplainCategoryDescriptionTransform))]
        [MemberDoc("表象大类")]
        public string ComplainCategoryDescription { get; set; }

        [AutoMapPropertyResolve("ComplainState", typeof(ComplainItem), typeof(ComplainStateDescriptionTransform))]
        [MemberDoc("工单状态")]
        public string CurrentStateDescription { get; set; }

        public string NextStateDescription
        {
            get
            {
                var nextState = CurrentStateDescription.GetNextStateDescription(ComplainState.Archive);
                return nextState == null ? null : ((ComplainState)nextState).GetEnumDescription();
            }
        }

        [MemberDoc("是否为百度经纬度")]
        public bool IsBaiduOffset { get; set; }
    }
}