using Abp.EntityFramework.AutoMapper;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Station;
using Lte.Domain.Common.Geo;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;

namespace Lte.MySqlFramework.Entities
{
    [AutoMapFrom(typeof(ConstructionInformation), typeof(ENodebBase))]
    public class StationCellView : IGeoPoint<double?>, IDistrictTown
    {
        public string CellSerialNum { get; set; }
        
        public string ENodebName { get; set; }
        
        public double? Longtitute { get; set; }
        
        public double? Lattitute { get; set; }

        [AutoMapPropertyResolve("StationDistrict", typeof(ENodebBase))]
        public string District { get; set; }

        [AutoMapPropertyResolve("StationTown", typeof(ENodebBase))]
        public string Town { get; set; }
    }
}