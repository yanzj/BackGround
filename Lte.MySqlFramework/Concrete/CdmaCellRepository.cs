using System.Collections.Generic;
using System.Linq;
using Abp.EntityFramework;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Entities.Cdma;
using Abp.EntityFramework.Repositories;
using Lte.MySqlFramework.Abstract;

namespace Lte.MySqlFramework.Concrete
{
    public class CdmaCellRepository : EfRepositorySave<MySqlContext, CdmaCell>, ICdmaCellRepository
    {
        public List<CdmaCell> GetAllList(int btsId)
        {
            return GetAll().Where(x => x.BtsId == btsId).ToList();
        }

        public List<CdmaCell> GetAllInUseList()
        {
            return GetAll().Where(x => x.IsInUse).ToList();
        }
        
        public CdmaCell GetBySectorId(int btsId, byte sectorId)
        {
            return FirstOrDefault(x => x.BtsId == btsId && x.SectorId == sectorId);
        }

        public CdmaCell GetBySectorIdAndCellType(int btsId, byte sectorId, string cellType)
        {
            return FirstOrDefault(x => x.BtsId == btsId && x.SectorId == sectorId && x.CellType == cellType);
        }

        public CdmaCellRepository(IDbContextProvider<MySqlContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}