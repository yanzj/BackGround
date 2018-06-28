using System.Collections.Generic;
using System.Threading.Tasks;
using Abp.Domain.Repositories;
using Abp.EntityFramework.Entities;
using Abp.EntityFramework.Repositories;
using Lte.Domain.Common.Wireless;

namespace Lte.MySqlFramework.Abstract
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