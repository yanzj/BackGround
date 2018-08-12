using Abp.EntityFramework;
using Abp.EntityFramework.Entities.College;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Common.Types;
using Lte.Domain.Common.Wireless.Distribution;
using Lte.Domain.Excel;
using Lte.MySqlFramework.Abstract.College;

namespace Lte.MySqlFramework.Concrete.College
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