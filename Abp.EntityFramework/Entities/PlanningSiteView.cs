using System;
using Abp.EntityFramework.AutoMapper;

namespace Abp.EntityFramework.Entities
{
    [AutoMapFrom(typeof(PlanningSite))]
    public class PlanningSiteView
    {
        public int TownId { get; set; }

        public string District { get; set; }

        public string Town { get; set; }

        public string PlanNum { get; set; }

        public string PlanName { get; set; }

        public string FormalName { get; set; }

        public double Longtitute { get; set; }

        public double Lattitute { get; set; }

        public string TowerType { get; set; }

        public double? AntennaHeight { get; set; }

        public DateTime? CompleteDate { get; set; }

        public DateTime? YanshouDate { get; set; }

        public bool IsGotton { get; set; }

        public string SiteCategory { get; set; }

        public string SiteSource { get; set; }

        public string ShouzuShuoming { get; set; }

        public DateTime? GottenDate { get; set; }

        public string TowerContaction { get; set; }

        public DateTime? ContractDate { get; set; }

        public DateTime? FinishedDate { get; set; }

        public string TowerScheme { get; set; }

        public string TowerSiteName { get; set; }

        public string AntennaType { get; set; }
    }
}