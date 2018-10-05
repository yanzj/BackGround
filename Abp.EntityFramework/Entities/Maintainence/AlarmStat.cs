using System;
using Abp.Domain.Entities;
using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Common.Transform;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless.Alarm;
using Lte.Domain.Common.Wireless.Work;
using Lte.Domain.Regular.Attributes;

namespace Abp.EntityFramework.Entities.Maintainence
{
    [TypeDoc("告警统计实体（在Sqlserver数据库中）")]
    [AutoMapFrom(typeof(AlarmStatCsv), typeof(AlarmStatHuawei))]
    public class AlarmStat : Entity
    {
        [MemberDoc("基站编号")]
        [AutoMapPropertyResolve("ENodebIdString", typeof(AlarmStatHuawei), typeof(StringToIntTransform))]
        public int ENodebId { get; set; }

        [MemberDoc("扇区编号")]
        [AutoMapPropertyResolve("ObjectId", typeof(AlarmStatCsv), typeof(SectorIdTransform))]
        public byte SectorId { get; set; }

        [MemberDoc("告警编号")]
        [AutoMapPropertyResolve("AlarmId", typeof(AlarmStatCsv), typeof(StringToIntTransform))]
        public int AlarmId { get; set; }

        [MemberDoc("发生时间")]
        public DateTime HappenTime { get; set; }

        [MemberDoc("恢复时间")]
        [AutoMapPropertyResolve("RecoverTime", typeof(AlarmStatCsv), typeof(DateTimeTransform))]
        [AutoMapPropertyResolve("RecoverTime", typeof(AlarmStatHuawei), typeof(StringToDateTimeTransform))]
        public DateTime RecoverTime { get; set; }

        [MemberDoc("告警等级")]
        [AutoMapPropertyResolve("AlarmLevelDescription", typeof(AlarmStatCsv), typeof(AlarmLevelTransform))]
        [AutoMapPropertyResolve("AlarmLevelDescription", typeof(AlarmStatHuawei), typeof(AlarmLevelTransform))]
        public AlarmLevel AlarmLevel { get; set; }

        [MemberDoc("告警分类")]
        [AutoMapPropertyResolve("AlarmCategoryDescription", typeof(AlarmStatCsv), typeof(AlarmCategoryTransform))]
        public AlarmCategory AlarmCategory { get; set; }

        [MemberDoc("告警类型")]
        [AutoMapPropertyResolve("AlarmCodeDescription", typeof(AlarmStatCsv), typeof(AlarmZteTypeTransform))]
        [AutoMapPropertyResolve("AlarmCodeDescription", typeof(AlarmStatHuawei), typeof(AlarmTypeTransform))]
        public AlarmType AlarmType { get; set; }

        [MemberDoc("详细信息")]
        public string Details { get; set; }
        
    }
}
