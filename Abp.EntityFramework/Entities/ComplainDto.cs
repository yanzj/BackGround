using System;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;

namespace Abp.EntityFramework.Entities
{
    [AutoMapFrom(typeof(ComplainItem))]
    public class ComplainDto : IStateChange, IBeginDate, ICityDistrictTown
    {
        public string SerialNumber { get; set; }

        public string SubscriberPhone { get; set; }

        public byte RepeatTimes { get; set; }

        [AutoMapPropertyResolve("ServiceCategory", typeof(ComplainItem), typeof(ComplainCategoryDescriptionTransform))]
        public string ServiceCategoryDescription { get; set; }

        [AutoMapPropertyResolve("IsUrgent", typeof(ComplainItem), typeof(YesNoTransform))]
        public string IsUrgentDescription { get; set; }

        public string SubscriberInfo { get; set; }

        public string ContactPhone { get; set; }

        public string ContactPerson { get; set; }

        public string ContactAddress { get; set; }

        public string ManagerInfo { get; set; }

        public string ComplainContents { get; set; }

        public DateTime BeginDate { get; set; }

        public DateTime Deadline { get; set; }

        public string CurrentProcessor { get; set; }

        public DateTime ProcessTime { get; set; }

        public string OssSerialNumber { get; set; }

        [AutoMapPropertyResolve("ComplainSource", typeof(ComplainItem), typeof(ComplainSourceDescriptionTransform))]
        public string ComplainSourceDescription { get; set; }

        public DateTime BeginTime { get; set; }

        public string City { get; set; }

        public string District { get; set; }

        public string Town { get; set; }

        public string RoadName { get; set; }

        public string BuildingName { get; set; }

        public string CauseLocation { get; set; }

        public string PreProcessContents { get; set; }
        
        [AutoMapPropertyResolve("IsSubscriber4G", typeof(ComplainItem), typeof(YesNoTransform))]
        public string IsSubscriber4GDescription { get; set; }

        public double Longtitute { get; set; }

        public double Lattitute { get; set; }

        [AutoMapPropertyResolve("ComplainReason", typeof(ComplainItem), typeof(ComplainReasonDescriptionTransform))]
        public string ComplainReasonDescription { get; set; }

        [AutoMapPropertyResolve("ComplainSubReason", typeof(ComplainItem), typeof(ComplainSubReasonDescriptionTransform))]
        public string ComplainSubReasonDescription { get; set; }

        public string Grid { get; set; }

        [AutoMapPropertyResolve("NetworkType", typeof(ComplainItem), typeof(NetworkTypeDescriptionTransform))]
        public string NetworkTypeDescription { get; set; }

        public string SitePosition { get; set; }

        [AutoMapPropertyResolve("IsIndoor", typeof(ComplainItem), typeof(IndoorDescriptionTransform))]
        public string IsIndoorDescription { get; set; }

        [AutoMapPropertyResolve("ComplainScene", typeof(ComplainItem), typeof(ComplainSceneDescriptionTransform))]
        public string ComplainSceneDescription { get; set; }

        [AutoMapPropertyResolve("ComplainCategory", typeof(ComplainItem), typeof(ComplainCategoryDescriptionTransform))]
        public string ComplainCategoryDescription { get; set; }

        [AutoMapPropertyResolve("ComplainState", typeof(ComplainItem), typeof(ComplainStateDescriptionTransform))]
        public string CurrentStateDescription { get; set; }

        public string NextStateDescription
        {
            get
            {
                var nextState = CurrentStateDescription.GetNextStateDescription(ComplainState.Archive);
                return nextState == null ? null : ((ComplainState)nextState).GetEnumDescription();
            }
        }
    }
}