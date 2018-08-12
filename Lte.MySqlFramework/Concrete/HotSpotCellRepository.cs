using Abp.EntityFramework;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.College;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Common.Wireless.Distribution;
using Lte.Domain.Excel;
using Lte.MySqlFramework.Abstract;

namespace Lte.MySqlFramework.Concrete
{
    public class HotSpotCellRepository : EfRepositorySave<MySqlContext, HotSpotCellId>, IHotSpotCellRepository
    {
        public HotSpotCellRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }
        
        public HotSpotCellId Match(HotSpotCellExcel stat)
        {
            var type = stat.HotSpotTypeDescription.GetEnumType<HotspotType>();
            return FirstOrDefault(x => x.HotspotType == type && x.HotspotName == stat.HotspotName);
        }
    }
}