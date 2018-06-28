using System;
using Abp.Domain.Entities;
using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Excel;

namespace Abp.EntityFramework.Entities
{
    [AutoMapFrom(typeof(PlanningSiteExcel))]
    public class PlanningSite : Entity, ITownId
    {
        public int TownId { get; set; }

        public string PlanNum { get; set; }
        
        public string PlanName { get; set; }

        public string TowerNum { get; set; }
        
        public string TowerName { get; set; }

        public string FormalName { get; set; }

        public string SiteCategory { get; set; }

        public string SiteSource { get; set; }

        public string ShouzuShuoming { get; set; }

        public double Longtitute { get; set; }

        public double Lattitute { get; set; }

        public string TowerType { get; set; }
        
        public double? AntennaHeight { get; set; }

        public DateTime? CompleteDate { get; set; }
        
        public DateTime? YanshouDate { get; set; }

        public bool IsGotton { get; set; }

        public DateTime? GottenDate { get; set; }

        public string TowerContaction { get; set; }

        public DateTime? ContractDate { get; set; }
        
        public DateTime? FinishedDate { get; set; }

        public string TowerScheme { get; set; }

        public string TowerSiteName { get; set; }

        public string AntennaType { get; set; }
    }
}
