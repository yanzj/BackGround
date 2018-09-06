using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Common.Geo;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Common.Wireless.Region;

namespace Abp.EntityFramework.Entities.College
{
    [AutoMapFrom(typeof(CollegeInfo))]
    public class CollegeView: ICityDistrictTown, IGeoPointReadonly<double>
    {
        public int Id { get;set; }

        public virtual DateTime? LastModificationTime { get; set; }

        public virtual long? LastModifierUserId { get; set; }

        public string Name { get; set; }

        public int TownId { get; set; }

        public string District { get; set; }

        public string Town { get; set; }

        public string City { get; set; }

        public RectangleRange RectangleRange { get; set; }

        public double Longtitute => RectangleRange == null ? 0 : (RectangleRange.West + RectangleRange.East) / 2;

        public double Lattitute => RectangleRange == null ? 0 : (RectangleRange.South + RectangleRange.North) / 2;
    }
}
