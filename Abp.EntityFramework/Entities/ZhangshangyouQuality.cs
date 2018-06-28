using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Common;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Regular.Attributes;

namespace Abp.EntityFramework.Entities
{
    [TypeDoc("掌上优测试详单")]
    [AutoMapFrom(typeof(ZhangshangyouQualityCsv))]
    public class ZhangshangyouQuality : Entity, IStatTime
    {
        [MemberDoc("任务编号")]
        public string SerialNumber { get; set; }

        [MemberDoc("时间")]
        public DateTime StatTime { get; set; }

        [MemberDoc("回传网络")]
        [AutoMapPropertyResolve("BackhaulNetworkDescription", typeof(ZhangshangyouQualityCsv), typeof(NetworkTypeTransform))]
        public NetworkType BackhaulNetwork { get; set; }
    }
}
