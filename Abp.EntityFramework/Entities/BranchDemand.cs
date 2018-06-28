using System;
using Abp.Domain.Entities;
using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;

namespace Abp.EntityFramework.Entities
{

    [AutoMapFrom(typeof(BranchDemandExcel), typeof(BranchDemandDto))]
    public class BranchDemand : Entity, ITownId
    {
        public DateTime BeginDate { get; set; }

        public string SerialNumber { get; set; }

        public int TownId { get; set; }

        public string ComplainContents { get; set; }

        [AutoMapPropertyResolve("FirstContents", typeof(BranchDemandExcel), typeof(DateTimeNowLabelTransform))]
        public string ProcessContents { get; set; }

        [AutoMapPropertyResolve("SolveFunctionDescription", typeof(BranchDemandExcel), typeof(SolveFunctionTransform))]
        [AutoMapPropertyResolve("SolveFunctionDescription", typeof(BranchDemandDto), typeof(SolveFunctionTransform))]
        public SolveFunction SolveFunction { get; set; }

        [AutoMapPropertyResolve("IsSolvedDescription", typeof(BranchDemandExcel), typeof(YesToBoolTransform))]
        [AutoMapPropertyResolve("IsSolvedDescription", typeof(BranchDemandDto), typeof(YesToBoolTransform))]
        public bool IsSolved { get; set; }

        public DateTime? EndDate { get; set; }

        public double Lontitute { get; set; }

        public double Lattitute { get; set; }

        public string SubscriberInfo { get; set; }

        public string ManagerInfo { get; set; }
    }
}
