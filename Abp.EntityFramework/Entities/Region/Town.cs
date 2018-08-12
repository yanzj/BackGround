using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;
using Abp.EntityFramework.AutoMapper;
using AutoMapper;
using Lte.Domain.Common.Geo;
using Lte.Domain.Common.Types;
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

    [AutoMapFrom(typeof(Town))]
    public class TownView
    {
        public string CityName { get; set; }

        public string DistrictName { get; set; }

        public string TownName { get; set; }

        [AutoMapPropertyResolve("AreaType", typeof(Town), typeof(ComplainSceneDescriptionTransform))]
        public string AreaTypeDescription { get; set; }

    }
}