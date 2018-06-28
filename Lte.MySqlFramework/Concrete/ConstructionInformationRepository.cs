using Abp.EntityFramework;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Excel;
using Lte.MySqlFramework.Abstract;

namespace Lte.MySqlFramework.Concrete
{
    public class ConstructionInformationRepository : EfRepositorySave<MySqlContext, ConstructionInformation>,
        IConstructionInformationRepository
    {
        public ConstructionInformationRepository(IDbContextProvider<MySqlContext> dbContextProvider)
            : base(dbContextProvider)
        {
        }

        public ConstructionInformation Match(ConstructionExcel stat)
        {
            return FirstOrDefault(x => x.ENodebId == stat.ENodebId && x.SectorId == stat.SectorId);
        }
    }
}