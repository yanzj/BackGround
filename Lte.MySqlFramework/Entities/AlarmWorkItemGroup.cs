using Lte.Domain.Common.Geo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Infrastructure;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Regular.Attributes;

namespace Lte.MySqlFramework.Entities
{
    public abstract class AlarmWorkItemGroup : IGeoPoint<double>
    {
        [MemberDoc("热点经度")]
        public double Longtitute { get; set; }

        [MemberDoc("热点纬度")]
        public double Lattitute { get; set; }

        [MemberDoc("告警个数")]
        public int AlarmCounts { get; set; }

        [MemberDoc("告警列表")]
        public List<AlarmWorkItemView> ItemList { get; set; }
    }

    [AutoMapFrom(typeof(ENodeb))]
    [TypeDoc("故障热点小区")]
    public class ENodebAlarmWorkItemGroup : AlarmWorkItemGroup, IENodebId, IENodebName
    {
        [MemberDoc("基站编号")]
        public int ENodebId { get; set; }

        [MemberDoc("基站名称")]
        [AutoMapPropertyResolve("Name", typeof(ENodeb))]
        public string ENodebName { get; set; }
    }

    [AutoMapFrom(typeof(Cell))]
    public class CellAlarmWorkItemGroup : AlarmWorkItemGroup, ILteCellQuery, IENodebName
    {
        [MemberDoc("基站编号")]
        public int ENodebId { get; set; }

        [MemberDoc("扇区编号")]
        public byte SectorId { get; set; }

        [MemberDoc("基站名称")]
        public string ENodebName { get; set; }
    }
}
