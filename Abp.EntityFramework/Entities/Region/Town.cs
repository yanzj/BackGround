using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;
using AutoMapper;
using Lte.Domain.Common.Geo;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Common.Wireless.Complain;

namespace Abp.EntityFramework.Entities.Region
{
    public class Town : Entity, ITown, IGeoPoint<double>
    {
        [MaxLength(20)]
        public string CityName { get; set; }

        [MaxLength(20)]
        public string DistrictName { get; set; }

        [MaxLength(20)]
        public string TownName { get; set; }

        [IgnoreMap]
        public double Longtitute { get; set; }

        [IgnoreMap]
        public double Lattitute { get; set; }

        public ComplainScene AreaType { get; set; }
    }
}