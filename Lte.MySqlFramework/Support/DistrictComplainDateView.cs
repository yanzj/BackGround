using System;
using System.Collections.Generic;
using System.Linq;
using Abp.EntityFramework.Dependency;
using Abp.EntityFramework.Entities;
using Lte.Domain.Common.Wireless;

namespace Lte.MySqlFramework.Support
{
    public class DistrictComplainDateView : IStatDate
    {
        public DateTime StatDate { get; set; }

        public IEnumerable<DistrictComplainView> DistrictComplainViews { get; set; }

        public static DistrictComplainDateView GenerateDistrictComplainDateView(List<ComplainItem> stats,
            IEnumerable<string> districts)
        {
            var results = GenerateDistrictComplainList(stats, districts);
            results.Add(new DistrictComplainView
            {
                District = "·ðÉ½",
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