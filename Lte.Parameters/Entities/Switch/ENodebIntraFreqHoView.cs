using System;
using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Common.Types;
using Lte.Domain.Regular.Attributes;

namespace Lte.Parameters.Entities.Switch
{
    [AutoMapFrom(typeof(IntraRatHoComm), typeof(UeEUtranMeasurementZte))]
    [TypeDoc("基站级同频切换参数列表")]
    public class ENodebIntraFreqHoView
    {
        [AutoMapPropertyResolve("eNodeB_Id", typeof(IntraRatHoComm))]
        [AutoMapPropertyResolve("eNodeB_Id", typeof(UeEUtranMeasurementZte))]
        [MemberDoc("基站编号")]
        public int ENodebId { get; set; }

        [MemberDoc("更新日期")]
        public DateTime? UpdateDate { get; set; }

        [AutoMapPropertyResolve("IntraFreqHoRprtInterval", typeof(IntraRatHoComm))]
        [AutoMapPropertyResolve("reportInterval", typeof(UeEUtranMeasurementZte))]
        [MemberDoc("切换事件触发后周期上报测量报告间隔")]
        public int ReportInterval { get; set; }

        [AutoMapPropertyResolve("IntraRatHoRprtAmount", typeof(IntraRatHoComm))]
        [AutoMapPropertyResolve("reportAmount", typeof(UeEUtranMeasurementZte))]
        [MemberDoc("切换事件触发后周期上报测量报告次数")]
        public int ReportAmount { get; set; }

        [AutoMapPropertyResolve("IntraRatHoMaxRprtCell", typeof(IntraRatHoComm))]
        [AutoMapPropertyResolve("maxReportCellNum", typeof(UeEUtranMeasurementZte))]
        [MemberDoc("最大上报小区个数")]
        public int MaxReportCellNum { get; set; }

        [AutoMapPropertyResolve("IntraFreqHoA3TrigQuan", typeof(IntraRatHoComm))]
        [AutoMapPropertyResolve("triggerQuantity", typeof(UeEUtranMeasurementZte))]
        [MemberDoc("测量事件触发类型")]
        public int TriggerQuantity { get; set; }

        [AutoMapPropertyResolve("IntraFreqHoA3RprtQuan", typeof(IntraRatHoComm))]
        [AutoMapPropertyResolve("reportQuantity", typeof(UeEUtranMeasurementZte))]
        [MemberDoc("测量事件触发后上报类型")]
        public int ReportQuantity { get; set; }

        [AutoMapPropertyResolve("measCfgIdx", typeof(UeEUtranMeasurementZte))]
        [MemberDoc("中兴测量配置号")]
        public int ConfigIndex { get; set; }
    }
}