using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.EntityFramework.Entities.Infrastructure;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Common.Wireless;
using Lte.Domain.Common.Wireless.Distribution;

namespace Lte.MySqlFramework.Abstract.Infrastructure
{
    public interface IInfrastructureRepository : IRepository<InfrastructureInfo>, ISaveChanges
    {
        IEnumerable<int> GetCollegeInfrastructureIds(string collegeName, InfrastructureType type);

        IEnumerable<int> GetHotSpotInfrastructureIds(string name, InfrastructureType type, HotspotType hotspotType);
        
        Task InsertHotSpotCell(string hotSpotName, HotspotType hotspotType, int id);

        Task InsertCollegeENodeb(string collegeName, int id);

        Task InsertCollegeBts(string collegeName, int id);
    }
}