using Abp.EntityFramework.AutoMapper;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless.Complain;

namespace Abp.EntityFramework.Entities.Region
{
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