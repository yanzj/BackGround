﻿using System;
using Abp.Domain.Entities;
using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Common.Geo;
using Lte.Domain.Common.Transform;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Common.Wireless.Complain;
using Lte.Domain.Common.Wireless.Station;
using Lte.Domain.Excel;
using Lte.Domain.Regular.Attributes;

namespace Abp.EntityFramework.Entities.Complain
{
    [AutoMapFrom(typeof(ComplainDto), typeof(ComplainExcel), typeof(ComplainSupplyExcel))]
    public class ComplainItem : Entity, IBeginDate, ITownId, IGeoPoint<double>
    {
        public string SerialNumber { get; set; }

        [MemberDoc("客户电话")]
        public string SubscriberPhone { get; set; }
        
        public byte RepeatTimes { get; set; }

        [AutoMapPropertyResolve("ServiceType1", typeof(ComplainExcel), typeof(ComplainCategoryTransform))]
        [AutoMapPropertyResolve("ServiceCategoryDescription", typeof(ComplainDto), typeof(ComplainCategoryTransform))]
        public ComplainCategory ServiceCategory { get; set; }

        [AutoMapPropertyResolve("IsUrgentDescription", typeof(ComplainDto), typeof(YesToBoolTransform))]
        public bool IsUrgent { get; set; }

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

        public int TownId { get; set; }

        [AutoMapPropertyResolve("ComplainSourceDescription", typeof(ComplainDto), typeof(ComplainSourceTransform))]
        [AutoMapPropertyResolve("SourceDescription", typeof(ComplainExcel), typeof(ComplainSourceTransform))]
        [AutoMapPropertyResolve("SourceDescription", typeof(ComplainSupplyExcel), typeof(ComplainSourceTransform))]
        public ComplainSource ComplainSource { get; set; }

        public DateTime BeginTime { get; set; }

        public string City { get; set; }

        public string District { get; set; }

        public string RoadName { get; set; }
        
        public string BuildingName { get; set; }

        public string FinishContents { get; set; }

        public string CauseLocation { get; set; }

        public string PreProcessContents { get; set; }

        [AutoMapPropertyResolve("Subscriber4G", typeof(ComplainExcel), typeof(YesToBoolTransform))]
        [AutoMapPropertyResolve("IsSubscriber4GDescription", typeof(ComplainDto), typeof(YesToBoolTransform))]
        public bool IsSubscriber4G { get; set; }

        public double Longtitute { get; set; }
        
        public double Lattitute { get; set; }

        [AutoMapPropertyResolve("ComplainReasonDescription", typeof(ComplainDto), typeof(ComplainReasonTransform))]
        [AutoMapPropertyResolve("ReasonFirst", typeof(ComplainExcel), typeof(ComplainReasonTransform))]
        public ComplainReason ComplainReason { get; set; }

        [AutoMapPropertyResolve("ComplainSubReasonDescription", typeof(ComplainDto), typeof(ComplainSubReasonTransform))]
        [AutoMapPropertyResolve("ReasonSecond", typeof(ComplainExcel), typeof(ComplainSubReasonTransform))]
        public ComplainSubReason ComplainSubReason { get; set; }

        public string Grid { get; set; }
        
        [AutoMapPropertyResolve("NetworkTypeDescription", typeof(ComplainDto), typeof(NetworkTypeTransform))]
        [AutoMapPropertyResolve("NetworkDescription", typeof(ComplainExcel), typeof(NetworkTypeTransform))]
        [AutoMapPropertyResolve("NetworkDescription", typeof(ComplainSupplyExcel), typeof(NetworkTypeTransform))]
        public NetworkType NetworkType { get; set; }

        public string SitePosition { get; set; }

        [AutoMapPropertyResolve("IsIndoorDescription", typeof(ComplainDto), typeof(IndoorBoolTransform))]
        [AutoMapPropertyResolve("IndoorDescription", typeof(ComplainSupplyExcel), typeof(IndoorBoolTransform))]
        public bool IsIndoor { get; set; }

        [AutoMapPropertyResolve("ComplainSceneDescription", typeof(ComplainDto), typeof(ComplainSceneTransform))]
        [AutoMapPropertyResolve("Scene", typeof(ComplainExcel), typeof(ComplainSceneTransform))]
        public ComplainScene ComplainScene { get; set; }

        [AutoMapPropertyResolve("ComplainCategoryDescription", typeof(ComplainDto), typeof(ComplainCategoryTransform))]
        [AutoMapPropertyResolve("CategoryDescription", typeof(ComplainExcel), typeof(ComplainCategoryTransform))]
        [AutoMapPropertyResolve("CategoryDescription", typeof(ComplainSupplyExcel), typeof(ComplainCategoryTransform))]
        public ComplainCategory ComplainCategory { get; set; }

        [AutoMapPropertyResolve("CurrentStateDescription", typeof(ComplainDto), typeof(ComplainStateTransform))]
        [AutoMapPropertyResolve("CurrentStateDescription", typeof(ComplainSupplyExcel), typeof(ComplainStateTransform))]
        public ComplainState ComplainState { get; set; }

        [AutoMapPropertyResolve("BaiduOffsetDescription", typeof(ComplainSupplyExcel), typeof(YesToBoolTransform))]
        public bool IsBaiduOffset { get; set; }
    }
}
