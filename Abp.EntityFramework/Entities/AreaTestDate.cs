using System;
using Abp.Domain.Entities;
using Lte.Domain.Common.Wireless;

namespace Abp.EntityFramework.Entities
{
    public class AreaTestDate : Entity, IArea
    {
        public string Area { get; set; }

        public DateTime LatestDate2G { get; set; }

        public DateTime LatestDate3G { get; set; }

        public DateTime LatestDate4G { get; set; }
    }
}