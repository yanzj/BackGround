using System;
using System.Collections.Generic;
using System.Linq;
using Abp.EntityFramework.Dependency;
using Abp.EntityFramework.Entities.Complain;
using Lte.Domain.Common.Wireless.Complain;
using Lte.Domain.Common.Wireless.Station;
using Lte.Domain.Regular.Attributes;

namespace Lte.MySqlFramework.Support.View
{
    [TypeDoc("分区域单天抱怨量工单统计")]
    public class DistrictComplainDateView : IStatDate
    {
        [MemberDoc("统计日期")]
        public DateTime StatDate { get; set; }

        [MemberDoc("分区域抱怨量工单列表")]
        public IEnumerable<DistrictComplainView> DistrictComplainViews { get; set; }

        public static DistrictComplainDateView GenerateDistrictComplainDateView(List<ComplainItem> stats,
            IEnumerable<string> districts)
        {
            var results = GenerateDistrictComplainList(stats, districts);
            results.Add(new DistrictComplainView
            {
                District = "佛山",
                Complain2G =
                    stats.Count(
                        s =>
                            s.NetworkType == NetworkType.With2G &&
                            s.ServiceCategory == ComplainCategory.NetworkQuality),
                Complain3G =
                    stats.Count(
                        s =>
                            (s.NetworkType == NetworkType.With3G || s.NetworkType == NetworkType.With2G3G) &&
                            s.ServiceCategory == ComplainCategory.NetworkQuality),
                Complain4G =
                    stats.Count(
                        s =>
                            (s.NetworkType == NetworkType.With4G || s.NetworkType == NetworkType.With2G3G4G ||
                             s.NetworkType == NetworkType.With2G3G4G4GPlus) &&
                            s.ServiceCategory == ComplainCategory.NetworkQuality),
                Demand2G =
                    stats.Count(
                        s =>
                            s.NetworkType == NetworkType.With2G &&
                            s.ServiceCategory == ComplainCategory.Appliance),
                Demand3G =
                    stats.Count(
                        s =>
                            (s.NetworkType == NetworkType.With3G || s.NetworkType == NetworkType.With2G3G) &&
                            s.ServiceCategory == ComplainCategory.Appliance),
                Demand4G =
                    stats.Count(
                        s =>
                            (s.NetworkType == NetworkType.With4G || s.NetworkType == NetworkType.With2G3G4G ||
                             s.NetworkType == NetworkType.With2G3G4G4GPlus) &&
                            s.ServiceCategory == ComplainCategory.Appliance)
            });
            return new DistrictComplainDateView
            {
                StatDate = stats.First().BeginDate.Date,
                DistrictComplainViews = results
            };
        }

        public static List<DistrictComplainView> GenerateDistrictComplainList(List<ComplainItem> stats, 
            IEnumerable<string> districts)
        {
            var results = districts.Select(x => new DistrictComplainView
            {
                District = x,
                Complain2G =
                    stats.Count(
                        s =>
                            s.District == x && s.NetworkType == NetworkType.With2G &&
                            s.ServiceCategory == ComplainCategory.NetworkQuality),
                Complain3G =
                    stats.Count(
                        s =>
                            s.District == x &&
                            (s.NetworkType == NetworkType.With3G || s.NetworkType == NetworkType.With2G3G) &&
                            s.ServiceCategory == ComplainCategory.NetworkQuality),
                Complain4G =
                    stats.Count(
                        s =>
                            s.District == x &&
                            (s.NetworkType == NetworkType.With4G || s.NetworkType == NetworkType.With2G3G4G ||
                             s.NetworkType == NetworkType.With2G3G4G4GPlus) &&
                            s.ServiceCategory == ComplainCategory.NetworkQuality),
                Demand2G =
                    stats.Count(
                        s =>
                            s.District == x && s.NetworkType == NetworkType.With2G &&
                            s.ServiceCategory == ComplainCategory.Appliance),
                Demand3G =
                    stats.Count(
                        s =>
                            s.District == x &&
                            (s.NetworkType == NetworkType.With3G || s.NetworkType == NetworkType.With2G3G) &&
                            s.ServiceCategory == ComplainCategory.Appliance),
                Demand4G =
                    stats.Count(
                        s =>
                            s.District == x &&
                            (s.NetworkType == NetworkType.With4G || s.NetworkType == NetworkType.With2G3G4G ||
                             s.NetworkType == NetworkType.With2G3G4G4GPlus) &&
                            s.ServiceCategory == ComplainCategory.Appliance)
            }).ToList();
            return results;
        }
    }
}