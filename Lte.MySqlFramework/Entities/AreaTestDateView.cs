using System;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;

namespace Lte.MySqlFramework.Entities
{
    [AutoMapFrom(typeof(AreaTestDate), typeof(Town))]
    public class AreaTestDateView
    {
        public string CityName { get; set; }

        public string DistrictName { get; set; }

        public string TownName { get; set; }

        public double Longtitute { get; set; }

        public double Lattitute { get; set; }

        [AutoMapPropertyResolve("AreaType", typeof(Town), typeof(ComplainSceneDescriptionTransform))]
        public string AreaTypeDescription { get; set; }

        public DateTime LatestDate2G { get; set; }

        public DateTime LatestDate3G { get; set; }

        public DateTime LatestDate4G { get; set; }

        public int TotalDays2G => (DateTime.Today - LatestDate2G).Days;

        public int TotalDays3G => (DateTime.Today - LatestDate3G).Days;

        public int TotalDays4G => (DateTime.Today - LatestDate4G).Days;
    }
}