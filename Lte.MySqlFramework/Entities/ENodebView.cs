using System;
using System.Collections.Generic;
using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Infrastructure;
using Abp.EntityFramework.Entities.Region;
using Lte.Domain.Common.Geo;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Regular;

namespace Lte.MySqlFramework.Entities
{
    [AutoMapFrom(typeof(ENodeb), typeof(Town))]
    public class ENodebView : IGeoPointReadonly<double>, ITownId
    {
        public int ENodebId { get; set; }

        public int TownId { get; set; }

        public string Name { get; set; }

        public string Factory { get; set; }

        public IpAddress GatewayIp { get; set; }

        public IpAddress Ip { get; set; }

        public bool IsInUse { get; set; }

        public double Longtitute { get; set; }

        public double Lattitute { get; set; }

        public string Address { get; set; }

        public string PlanNum { get; set; }

        public DateTime OpenDate { get; set; }

        public string OpenDateString => OpenDate.ToShortDateString();

        public string CityName { get; set; }

        public string DistrictName { get; set; }

        public string TownName { get; set; }
    }

    public class ENodebCluster
    {
        public int LongtituteGrid { get; set; }

        public double Longtitute => (double) LongtituteGrid / 100000;

        public int LattituteGrid { get; set; }

        public double Lattitute => (double) LattituteGrid / 100000;

        public IEnumerable<ENodebView> ENodebViews { get; set; }
    }
}